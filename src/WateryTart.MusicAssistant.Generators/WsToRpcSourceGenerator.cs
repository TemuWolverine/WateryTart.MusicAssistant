using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WateryTart.MusicAssistant.Generators;

[Generator]
public class WsToRpcSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Find method declarations that have attributes (we'll filter for [ToRpc] at semantic level)
        var methods = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (node, _) => node is MethodDeclarationSyntax mds && mds.AttributeLists.Count > 0 && mds.ParameterList.Parameters.Count > 0,
            transform: (ctx, _) => (MethodDeclarationSyntax)ctx.Node)
            .Where(m => m != null)
            .Collect();

        var compilationAndMethods = context.CompilationProvider.Combine(methods);

        context.RegisterSourceOutput(compilationAndMethods, (spc, source) =>
        {
            var (compilation, methodDecls) = source;
            var toRpcAttrFqn = "WateryTart.MusicAssistant.Generators.Attributes.ToRpcAttribute";

            // Group generated methods by target rpc class so we can emit 'using' and namespace only once.
            var methodsByClass = new Dictionary<string, List<string>>(StringComparer.Ordinal);

            foreach (var methodDecl in methodDecls)
            {
                var model = compilation.GetSemanticModel(methodDecl.SyntaxTree);
                var methodSymbol = model.GetDeclaredSymbol(methodDecl) as IMethodSymbol;
                if (methodSymbol == null)
                    continue;

                // Only process methods explicitly marked with [ToRpc]
                if (!methodSymbol.GetAttributes().Any(ad => ad.AttributeClass?.ToDisplayString() == toRpcAttrFqn))
                {
                    continue;
                }

                // must be an extension method (first parameter has 'this') and receiver is MusicAssistantClientWs
                if (methodSymbol.Parameters.Length == 0)
                {
                    continue;
                }

                var firstParam = methodSymbol.Parameters[0];

                // Check both semantic and syntax forms of the 'this' modifier.
                bool hasThisSyntax = methodDecl.ParameterList.Parameters.Count > 0 &&
                                     methodDecl.ParameterList.Parameters[0].Modifiers.Any(m => m.IsKind(SyntaxKind.ThisKeyword));

                if (!methodSymbol.IsExtensionMethod && !hasThisSyntax)
                {
                    continue;
                }

                var receiverType = firstParam.Type.ToDisplayString(); // e.g. "MusicAssistantClientWs" or fully qualified
                if (!receiverType.EndsWith("MusicAssistantClientWs"))
                {
                    continue;
                }

                // find an invocation of SendAsync<TResponse>(c, messageExpr) inside the method
                var invocations = methodDecl.DescendantNodes().OfType<InvocationExpressionSyntax>();
                InvocationExpressionSyntax sendAsyncInvocation = null;
                TypeSyntax sendAsyncTypeArgSyntax = null;
                ExpressionSyntax messageExpr = null;

                foreach (var inv in invocations)
                {
                    // get the invoked name (handle IdentifierName, GenericName, MemberAccess)
                    string invokedName = null;
                    var expr = inv.Expression;

                    if (expr is IdentifierNameSyntax idn)
                    {
                        invokedName = idn.Identifier.Text;
                    }
                    else if (expr is GenericNameSyntax gns)
                    {
                        invokedName = gns.Identifier.Text;
                        if (gns.TypeArgumentList.Arguments.Count == 1)
                            sendAsyncTypeArgSyntax = gns.TypeArgumentList.Arguments[0];
                    }
                    else if (expr is MemberAccessExpressionSyntax maes)
                    {
                        if (maes.Name is GenericNameSyntax gns2)
                        {
                            invokedName = gns2.Identifier.Text;
                            if (gns2.TypeArgumentList.Arguments.Count == 1)
                                sendAsyncTypeArgSyntax = gns2.TypeArgumentList.Arguments[0];
                        }
                        else
                        {
                            invokedName = maes.Name.Identifier.Text;
                        }
                    }

                    if (invokedName != "SendAsync")
                        continue;

                    if (sendAsyncTypeArgSyntax == null && expr is GenericNameSyntax fallbackGns && fallbackGns.TypeArgumentList.Arguments.Count == 1)
                        sendAsyncTypeArgSyntax = fallbackGns.TypeArgumentList.Arguments[0];

                    // check arguments: first should be 'c' or name of first param, second is message
                    var args = inv.ArgumentList.Arguments;
                    if (args.Count >= 2)
                    {
                        var firstArg = args[0].Expression.ToString();
                        if (firstArg == methodSymbol.Parameters[0].Name || firstArg == "c")
                        {
                            messageExpr = args[1].Expression;
                            sendAsyncInvocation = inv;
                            break;
                        }
                    }
                }

                if (sendAsyncInvocation == null)
                {
                    continue; // nothing to rewrite
                }

                // determine response type symbol TResponse
                ITypeSymbol responseTypeSymbol = null;
                try
                {
                    if (sendAsyncTypeArgSyntax != null)
                    {
                        var tSym = model.GetTypeInfo(sendAsyncTypeArgSyntax).Type;
                        responseTypeSymbol = tSym;
                    }
                }
                catch
                {
                }

                // Determine inner type to use for RPC Send:
                ITypeSymbol innerTypeSymbol = null;
                if (responseTypeSymbol != null)
                {
                    var baseType = responseTypeSymbol;
                    while (baseType != null)
                    {
                        if (baseType is INamedTypeSymbol nts && nts.IsGenericType && nts.Name == "ResponseBase")
                        {
                            innerTypeSymbol = nts.TypeArguments.FirstOrDefault();
                            break;
                        }
                        baseType = baseType.BaseType;
                    }
                }

                // Build target rpc class name
                var className = methodSymbol.ContainingType.Name; // e.g. MusicAssistantClientWsExtensions
                var rpcClassName = className.Replace("WsExtensions", "RpcExtensions");
                if (rpcClassName == className)
                    rpcClassName = "MusicAssistantClientRpcExtensions";

                // Build the single method text (signature + body), we will group by rpcClassName
                // compose parameters string but replace first parameter type to MusicAssistantClientRpc
                var paramListText = new StringBuilder();
                paramListText.Append("(");
                for (int i = 0; i < methodDecl.ParameterList.Parameters.Count; i++)
                {
                    var p = methodDecl.ParameterList.Parameters[i];
                    var pText = p.ToFullString();
                    if (i == 0)
                    {
                        var nameOnly = p.Identifier.Text;
                        var hasThis = p.Modifiers.Any(m => m.IsKind(SyntaxKind.ThisKeyword));
                        var prefix = hasThis ? "this " : "";
                        pText = prefix + "MusicAssistantClientRpc " + nameOnly;
                    }
                    if (i > 0) paramListText.Append(", ");
                    paramListText.Append(pText.Trim());
                }
                paramListText.Append(")");

                // return type conversion
                var originalReturn = methodDecl.ReturnType.ToFullString().Trim(); // e.g. Task<TempResponse>
                string generatedReturn;
                if (innerTypeSymbol != null)
                {
                    var innerName = innerTypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                    generatedReturn = $"System.Threading.Tasks.Task<{innerName}?>";
                }
                else
                {
                    generatedReturn = originalReturn;
                }

                // rewrite body by replacing the SendAsync invocation with a Send invocation
                var originalMethodText = methodDecl.Body?.ToFullString() ?? methodDecl.ExpressionBody?.ToFullString() ?? methodDecl.ToFullString();
                var messageExprText = messageExpr?.ToFullString() ?? "null";
                string replacementInvoke;
                if (innerTypeSymbol != null)
                {
                    replacementInvoke = $"{methodSymbol.Parameters[0].Name}.Send<{innerTypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}>({messageExprText})";
                }
                else
                {
                    var respDisplay = responseTypeSymbol?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "object";
                    replacementInvoke = $"{methodSymbol.Parameters[0].Name}.Send<{respDisplay}>({messageExprText})";
                }

                var rewrittenBody = originalMethodText.Replace(sendAsyncInvocation.ToFullString(), replacementInvoke);

                // ensure async keyword present
                var modifiers = methodDecl.Modifiers.ToFullString().Trim();
                if (!modifiers.Contains("async"))
                    modifiers = modifiers + (modifiers.Length > 0 ? " async" : "async");

                var methodName = methodDecl.Identifier.Text;
                var typeParams = methodDecl.TypeParameterList?.ToFullString() ?? "";

                var firstBrace = rewrittenBody.IndexOf('{');
                var lastBrace = rewrittenBody.LastIndexOf('}');
                string newBodyText;
                if (firstBrace >= 0 && lastBrace > firstBrace)
                    newBodyText = rewrittenBody.Substring(firstBrace + 1, lastBrace - firstBrace - 1);
                else
                    newBodyText = rewrittenBody;

                // build single method block
                var methodSb = new StringBuilder();

                // Detect if the generated return is nullable and wrap method with #nullable directives if so.
                bool isReturnNullable = generatedReturn.Contains("?");

                if (isReturnNullable)
                    methodSb.AppendLine("#nullable enable");

                methodSb.AppendLine($"        {modifiers} {generatedReturn} {methodName}{typeParams}{paramListText}");
                methodSb.AppendLine("        {");
                foreach (var line in newBodyText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    methodSb.AppendLine("            " + line.TrimEnd());
                }
                methodSb.AppendLine("        }");

                if (isReturnNullable)
                    methodSb.AppendLine("#nullable disable");

                var methodBlock = methodSb.ToString();

                // store method text under rpcClassName
                if (!methodsByClass.TryGetValue(rpcClassName, out var list))
                {
                    list = new List<string>();
                    methodsByClass[rpcClassName] = list;
                }
                list.Add(methodBlock);
            }

            // emit single generated file containing usings, namespace and grouped classes
            if (methodsByClass.Count > 0)
            {
                var outSb = new StringBuilder();
                outSb.AppendLine("// <auto-generated/>");
                outSb.AppendLine("using System.Threading.Tasks;");
                outSb.AppendLine("using WateryTart.MusicAssistant.Generators.Attributes;");
                outSb.AppendLine("using WateryTart.MusicAssistant.Messages;");
                outSb.AppendLine("using WateryTart.MusicAssistant.Models;");
                outSb.AppendLine("using WateryTart.MusicAssistant.Models.Enums;");
                outSb.AppendLine("using WateryTart.MusicAssistant.Responses;");
                outSb.AppendLine();
                outSb.AppendLine("namespace WateryTart.MusicAssistant.RpcExtensions");
                outSb.AppendLine("{");

                foreach (var kv in methodsByClass)
                {
                    var rpcClassName = kv.Key;
                    outSb.AppendLine($"    public static partial class {rpcClassName}");
                    outSb.AppendLine("    {");
                    foreach (var methodText in kv.Value)
                    {
                        outSb.Append(methodText);
                        outSb.AppendLine();
                    }
                    outSb.AppendLine("    }");
                    outSb.AppendLine();
                }

                outSb.AppendLine("}");

                spc.AddSource("WsToRpc_generated.g.cs", SourceText.From(outSb.ToString(), Encoding.UTF8));
            }
        });
    }
}
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace WateryTart.MusicAssistant.Generators;

internal static class SymbolExtensions
{
    public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceSymbol @namespace)
    {
        foreach (var member in @namespace.GetMembers())
        {
            if (member is INamedTypeSymbol type)
                yield return type;
            else if (member is INamespaceSymbol nestedNs)
                foreach (var nestedType in nestedNs.GetAllTypes())
                    yield return nestedType;
        }
    }
}
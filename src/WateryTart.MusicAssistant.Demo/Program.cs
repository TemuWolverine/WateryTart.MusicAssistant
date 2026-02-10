using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.RpcExtensions;
using WateryTart.MusicAssistant.WsExtensions;

string username = "";
string password = "";

var client = new MusicAssistantClient("");
AuthUser result = await client.WithRpc().GetAuthToken(username, password);
LoginResults result2 = await client.WithWs().GetAuthToken(username, password);
client.SetToken(result2.Credentials.Token);
await client.WithWs().Connect();

var rpcMe = await client.WithRpc().GetAuthMe();
var wsMe = await client.WithWs().GetAuthMe();


Console.WriteLine("hi");
Console.ReadKey();
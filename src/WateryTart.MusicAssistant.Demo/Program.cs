using System.Runtime;
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.RPCExtensions;
using WateryTart.MusicAssistant.WebSocketExtensions;

string username = "";
string password = "";

var _rpcClient = new RpcClient("http://10.0.1.20:8095/api");
var credentials = await _rpcClient.LoginAsync(username, password);

_rpcClient.SetAuthToken(credentials.Token);
var players = await _rpcClient.PlayersAllAsync();


WsClient _wsClient = new WsClient();
LoginResults credentials2 = await _wsClient.Login(username, password, "10.0.1.20:8095");
var connected = await _wsClient.Connect(credentials2.Credentials);

var players2 = await _wsClient.PlayersAllAsync();

Console.WriteLine("hi");
Console.ReadKey();
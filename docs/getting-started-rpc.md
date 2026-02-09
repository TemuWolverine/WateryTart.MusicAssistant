```csharp
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.RPCExtensions;

RpcClient _rpcClient = new RpcClient("http://musicassistantserver:8095/api");
IMusicAssistantCredentials credentials = await _rpcClient.LoginAsync("username", "password");
_rpcClient.SetAuthToken(credentials.Token);
```

```csharp
var playersResponse = await _rpcClient.PlayersAllAsync();
foreach (var y in playersResponse)
{
	//Do something with the players 
}
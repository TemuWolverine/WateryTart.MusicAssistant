# WateryTart.MusicAssistant
> Disclaimer: This is an unofficial project and is not affiliated with, endorsed by, or associated with the Music Assistant project.

A .NET8/10 client library for accesseing Music Assistant's API [![build](https://github.com/TemuWolverine/WateryTart.MusicAssistant/actions/workflows/main.yml/badge.svg)](https://github.com/TemuWolverine/WateryTart.MusicAssistant/actions/workflows/main.yml)

This library does not contain a Sendspin client for playback, for that I recommend [Sendspin.SDK](https://github.com/chrisuthe/windowsSpin/wiki).

## Getting Started
### Install
Clone or install from nuget  
```dotnet add package WateryTart.MusicAssistant```

### Prereqs
* .NET 8.0/10.0 SDK
* [Music Assistant](https://www.music-assistant.io/) already installed.

If you need to check the Music Assistant API docs, they can be found at   
```
http://<Your Install>/api-docs/
```

### Overview
Music Assistant features *two* API's, a websocket and "RPC-like" interface. 
The ***recommended*** (by Music Assistant) API is the websocket API which (in their words) 
> The WebSocket API provides real-time bidirectional communication and automatic event notifications. Perfect for applications that need live updates.

While the RPC API 
> The HTTP API provides a simple RPC-like interface for executing commands. This allows you to call the same commands available via WebSocket over a simple HTTP POST endpoint. Perfect for one-off commands without needing real-time updates. 



### Usage: Connecting/Logging In
Calling signatures stay the same in most cases, with only the result varying.
Websocket responses from MusicAssistant are wrapped in a response/result with the matching message ID etc.

#### Websocket
```csharp
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.WebSocketExtensions;

WsClient _wsClient = new WsClient();
LoginResults credentials = await _wsClient.Login("username", "password^^", "musicassistantserver:8095");
bool connected = await _wsClient.Connect(credentials.Credentials);
 ```

Notice the url does not include **http** or **api**. just IP/HOSTNAME:PORT. ws:// and /ws will be added automatically

#### RPC
```csharp
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.RPCExtensions;

RpcClient _rpcClient = new RpcClient("http://musicassistantserver:8095/api");
IMusicAssistantCredentials credentials = await _rpcClient.LoginAsync("username", "password");
_rpcClient.SetAuthToken(credentials.Token);
```


### Usage: Data calls
 As mentioned above, the websocket responses from Music Assistant are wrapped, and thats reflected in the client library

```json
//a websocket response
{
  "message_id" : "1234", /*same ID as automatically fed in to match response*/
  "partial": false,
  "result" : { /*the actual object*/  }
}

//rpc response
{
	// the actual object
}
```

Lets get the `Players` info.

#### Websocket
```csharp
var playersResponse = await _wsClient.PlayersAllAsync();
foreach (var y in playersResponse.Result)
{
	//Do something with the players 
}
 ```

#### Rpc
 ```csharp
var playersResponse = await _rpcClient.PlayersAllAsync();
foreach (var y in playersResponse)
{
	//Do something with the players 
}
 ```

### Usage: Events
Only the websocket client has events as its pushed from the server to the client, and its presented as an `IObservable<BaseEventResponse>`

```csharp
_wsClient.Events
	.ObserveOn(RxApp.MainThreadScheduler)
	.SelectMany(e => Observable.FromAsync(() => OnEvents(e)))
	.Subscribe());



public async Task OnEvents(BaseEventResponse e)
{
    switch (e.EventName)
    {
        case EventType.MediaItemPlayed:
        case EventType.QueueTimeUpdated:
        case EventType.PlayerAdded:
        case EventType.PlayerUpdated:
        case EventType.PlayerRemoved:
        case EventType.QueueUpdated:
        default:
            break;
    }
}

```
See the Media Assistant API docs for the full list of events.

# WateryTart.MusicAssistant
> Disclaimer: This is an unofficial project and is not affiliated with, endorsed by, or associated with the Music Assistant project.
> It is really just a proof of concept that has gotten out of hand - I'm very much a hobbyist coder, so I won't be surprised if I haven't somehow invented the definitive example of 'Worst Practices'

A .NET10 client library for accesseing Music Assistant's API

## Getting Started
### Install
Clone or install from nuget  
```dotnet add package WateryTart.MusicAssistant```

### Prereqs
* .NET 10.0 SDK
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
#### Websocket
```csharp
WsClient _wsClient = new WsClient();
var connected = await _wsClient.Connect(_settings.Credentials);
 ```

#### RPC
```csharp
RpcClient _rpcClient = new RpcClient("http://musicassistantserver:8095");
var credentials = await _rpcClient.LoginAsync("username", "password");
_rpcClient.SetAuthToken(credentials.Token);
```


### Usage: Data calls
Calling signatures stay the same in most cases, with only the result varying.
Websocket responses from MusicAssistant are wrapped 

```json
{
  "message_id" : "1234", /*same ID as automatically fed in to match response*/
  "partial": false,
  "result" : { /*the actual object*/  }
}
```
Due to the nature of a RPC web request, this wrapper isn't really needed as message_id's don't need to be tracked to match a response to a request.

In this case, lets get the `Players` info
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

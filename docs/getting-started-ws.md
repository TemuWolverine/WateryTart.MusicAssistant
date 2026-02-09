```csharp
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.WebSocketExtensions;

WsClient _wsClient = new WsClient();
LoginResults credentials = await _wsClient.Login("username", "password^^", "musicassistantserver:8095");
bool connected = await _wsClient.Connect(credentials.Credentials);
 ```


Whe websocket responses from Music Assistant are wrapped, and thats reflected in the client library

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
Events
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
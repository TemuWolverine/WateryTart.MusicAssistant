# WateryTart.MusicAssistant
> Disclaimer: This is an unofficial project and is not affiliated with, endorsed by, or associated with the Music Assistant project.

A .NET8/10 client library for accesseing Music Assistant's API  

[![build](https://github.com/TemuWolverine/WateryTart.MusicAssistant/actions/workflows/main.yml/badge.svg)](https://github.com/TemuWolverine/WateryTart.MusicAssistant/actions/workflows/main.yml)

This library does not contain a Sendspin client for playback, for that I recommend [Sendspin.SDK](https://github.com/chrisuthe/windowsSpin/wiki).

## v2.0.0 Breaking Changes
Reworked the entire API to use a fluent-ish interface, brought the RPC API coverage up to the same as the WS (still not 100%).
Method calls are now consistently named across both interfaces.

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

### Initialisation
```csharp
using WateryTart.MusicAssistant;
using WateryTart.MusicAssistant.RpcExtensions;
using WateryTart.MusicAssistant.WsExtensions;

var client = new MusicAssistantClient("10.0.1.20:8095");
```

The url can also be set with `client.SetUrl("10.0.1.20:8095");`

Calls to each API are structured the same
`client.WithRpc()` or `client.WithWs()` followed by the method you want to call.
ie, `client.WithRpc().GetAuthMe();` or `client.WithWs().GetAuthMe()`.

Note, because the two APIs from Music Assistant return different results - the web socket results are in a wrapper - each WateryTart.Music Assistant call will return different objects

### Get Auth

```csharp
AuthUser result = await client.WithRpc().GetAuthToken(username, password);
or
LoginResults result2 = await client.WithWs().GetAuthToken(username, password);
```

Then set the token (or skip straight to the token if you have one saved)
```
client.SetToken(result2.Credentials.Token);
```

### Making a call
Once you've set a token, you can make calls to either API. API calls are all async and return a `Task<T>` where T is the expected result. 

All calls are added via extensions methods, so adding further API coverage will require changes to the core client. 

```
//TODO ADD MORE DOCUMENTATION
```


### Events
Only the websocket client has events as its pushed from the server to the client, and its presented as an `IObservable<BaseEventResponse>`

```csharp
_wsClient.WithWs().Events
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

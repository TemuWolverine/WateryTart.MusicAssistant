using WateryTart.MusicAssistant.Events;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;

namespace WateryTart.MusicAssistant;

public interface IWsClient
{
    Task<LoginResults> Login(string username, string password, string baseurl);

    Task<bool> Connect(IMusicAssistantCredentials credentials);

    void Send<T>(MessageBase message, Action<string> ResponseHandler, bool ignoreConnection = false);
    Task DisconnectAsync();

    bool IsConnected { get; }

    IObservable<BaseEventResponse> Events { get; }
}
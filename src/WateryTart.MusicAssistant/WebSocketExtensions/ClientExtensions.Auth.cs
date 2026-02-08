using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions;

public static partial class WebsocketClientExtensions
{
    public static void GetAuthToken(this IWsClient c, string username, string password, Action<AuthResponse> responseHandler)
    {
        var m = new Message(Commands.AuthLogin)
        {
            args = new Dictionary<string, object>()
                {
                    { "username", username },
                    { "password", password }
                }
        };

        c.Send<AuthResponse>(m, Deserialise<AuthResponse>(responseHandler), true);
    }
}
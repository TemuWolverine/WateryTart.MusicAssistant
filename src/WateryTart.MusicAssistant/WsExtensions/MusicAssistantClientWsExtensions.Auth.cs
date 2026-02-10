using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{

    public static async Task<UserResponse> GetAuthMe(this MusicAssistantClientWs c)
    {
        return await SendAsync<UserResponse>(c, ClientHelpers.JustCommand(Commands.AuthMe));
    }
    public static void GetAuthToken(this MusicAssistantClientWs c, string username, string password, Action<AuthResponse> responseHandler)
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


    /// <summary>
    /// Create a new long-lived access token for current user or another user (admin only). Long-lived tokens are intended for external integrations and API access. They expire after 10 years and do NOT auto-renew on use. Short-lived tokens (for regular user sessions) are only created during login and auto-renew on each use (sliding 30-day expiration window).
    /// <param name="name">The name/description for the token (e.g., "Home Assistant", "Mobile App").</param>
    /// <param name="password">Optional user ID to create token for (admin only).</param>
    /// </summary>
    public static async Task<StringResponse> GetAuthTokenCreate(this MusicAssistantClientWs c, string name, string userid = "")
    {
        var m = new Message(Commands.AuthTokenCreate)
        {
            args = new Dictionary<string, object>()
            {
                { "name", name },
            }
        };

        if (!string.IsNullOrEmpty(userid))
            m.args.Add("userid", userid);

        return await SendAsync<StringResponse>(c, m);
    }

    /// <summary>
    /// Get current user's auth tokens or another user's tokens (admin only).
    /// </summary>
    public static async Task<AuthTokenResponse> GetAuthTokens(this MusicAssistantClientWs c)
    {
        return await SendAsync<AuthTokenResponse>(c, ClientHelpers.JustCommand(Commands.AuthTokens));
    }
}
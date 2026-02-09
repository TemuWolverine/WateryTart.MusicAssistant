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

    public static async Task<UserResponse> GetMeAsync(this IWsClient c)
    {
        return await SendAsync<UserResponse>(c, JustCommand(Commands.AuthMe));
    }

    public static async Task<ProviderResponse> GetAuthProvidersAsync(this IWsClient c)
    {
        return await SendAsync<ProviderResponse>(c, JustCommand(Commands.AuthProviders));
    }
    /// <summary>
    /// Create a new long-lived access token for current user or another user (admin only). Long-lived tokens are intended for external integrations and API access. They expire after 10 years and do NOT auto-renew on use. Short-lived tokens (for regular user sessions) are only created during login and auto-renew on each use (sliding 30-day expiration window).
    /// <param name="name">The name/description for the token (e.g., "Home Assistant", "Mobile App").</param>
    /// <param name="password">Optional user ID to create token for (admin only).</param>
    /// </summary>
    public static async Task<StringResponse> AuthTokenCreateAsync(this IWsClient c, string name, string userid="")
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
    /* Not functional?
    public static async Task<ProviderResponse> AuthTokenRevokeAsync(this IWsClient c, string token)
    {
        return await SendAsync<ProviderResponse>(c, JustCommand(Commands.AuthProviders));
    }
    */

    /// <summary>
    /// Get current user's auth tokens or another user's tokens (admin only).
    /// </summary>
    public static async Task<AuthTokenResponse> GetAuthTokens(this IWsClient c)
    {
        return await SendAsync<AuthTokenResponse>(c, JustCommand(Commands.AuthTokens));
    }

    /*
    Unimplemented
    auth/tokens
    auth/user
    auth/user/create
    auth/user/delete
    auth/user/disable
    auth/user/enable
    auth/user/providers
    auth/user/unlike_provider
    auth/user/update
    auth/users
    */
}
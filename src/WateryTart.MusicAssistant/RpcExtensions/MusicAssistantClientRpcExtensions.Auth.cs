using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    public static async Task<AuthUser?> GetAuthToken(this MusicAssistantClientRpc c, string username, string password)
    {
        var m = new Message(Commands.AuthLogin)
        {
            args = new Dictionary<string, object>()
            {
                { "username", username },
                { "password", password },
                { "provider_id", "builtin" }
            }
        };

        return await c.Send<AuthUser>(m);
    }

    public static async Task<User?> GetAuthMe(this MusicAssistantClientRpc c)
    {
        return await c.Send<User>(ClientHelpers.JustCommand(Commands.AuthMe));
    }

    /// <summary>
    /// Create a new long-lived access token for current user or another user (admin only). Long-lived tokens are intended for external integrations and API access. They expire after 10 years and do NOT auto-renew on use. Short-lived tokens (for regular user sessions) are only created during login and auto-renew on each use (sliding 30-day expiration window).
    /// <param name="name">The name/description for the token (e.g., "Home Assistant", "Mobile App").</param>
    /// <param name="password">Optional user ID to create token for (admin only).</param>
    /// </summary>
    public static async Task<string?> GetAuthTokenCreate(this MusicAssistantClientRpc c, string name, string userid = "")
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

        return await c.Send<string?>(m);
    }

    /// <summary>
    /// Get current user's auth tokens or another user's tokens (admin only).
    /// </summary>
    public static async Task<List<AuthToken>?> GetAuthTokens(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<AuthToken>?>(ClientHelpers.JustCommand(Commands.AuthTokens));
    }
}
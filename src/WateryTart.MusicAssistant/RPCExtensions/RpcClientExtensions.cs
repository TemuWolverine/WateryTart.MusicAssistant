using System.Threading.Tasks;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.RPCExtensions;

/// <summary>
/// Provides extension methods for the <see cref="RpcClient"/> class to interact with the Music Assistant API.
/// Includes authentication, player control, queue management, and search operations.
/// </summary>
public static partial class RpcClientExtensions
{

    /// <summary>
    /// Authenticates a user with the given username and password, returning an authentication token and user details.
    /// </summary>
    /// <param name="username">The username for authentication.</param>
    /// <param name="password">The password for authentication.</param>
    /// <returns>An <see cref="AuthUser"/> object containing authentication details, or null if authentication fails.</returns>
    public static async Task<AuthUser?> GetAuthToken(this RpcClient c, string username, string password)
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

    /// <summary>
    /// Retrieves a list of all available players from the Music Assistant server.
    /// </summary>
    /// <returns>A list of <see cref="Player"/> objects, or null if the request fails.</returns>
    public static async Task<List<Player>?> PlayersAllAsync(this RpcClient c)
    {
        return await c.Send<List<Player>>(WebSocketExtensions.WebsocketClientExtensions.JustCommand(Commands.PlayersAll));
    }

    /// <summary>
    /// Skips to the next item in the queue for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerNextAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerNext, playerId, "player_id"));
    }

    /// <summary>
    /// Starts playback for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPlayAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPlay, playerId, "player_id"));
    }

    /// <summary>
    /// Toggles play/pause for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPlayPauseAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPlayPause, playerId, "player_id"));
    }

    /// <summary>
    /// Skips to the previous item in the queue for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPreviousAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }

    /// <summary>
    /// Retrieves all player queues from the Music Assistant server.
    /// </summary>
    /// <returns>A list of <see cref="PlayerQueue"/> objects, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerQueuesAllAsync(this RpcClient c)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustCommand(Commands.PlayerQueuesAll));
    }

    /// <summary>
    /// Gets the active queue for a specific player.
    /// </summary>
    /// <param name="id">The ID of the player.</param>
    /// <returns>The <see cref="PlayerQueue"/> for the player, or null if not found.</returns>
    public static async Task<PlayerQueue?> PlayerActiveQueueAsync(this RpcClient c, string id)
    {
        return await c.Send<PlayerQueue>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerActiveQueue, id, "player_id"));
    }

    /// <summary>
    /// Gets all items in a specific queue.
    /// </summary>
    /// <param name="id">The ID of the queue.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> items, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerQueueItemsAsync(this RpcClient c, string id)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerQueueItems, id, "queue_id"));
    }

    /// <summary>
    /// Increases the group volume for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeUpAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
    }

    /// <summary>
    /// Decreases the group volume for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeDownAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
    }

    /// <summary>
    /// Plays a media item in the specified queue with the given play mode and radio mode option.
    /// </summary>
    /// <param name="queueId">The ID of the queue.</param>
    /// <param name="t">The media item to play.</param>
    /// <param name="mode">The play mode (e.g., play, replace, add).</param>
    /// <param name="radiomode">Whether to enable radio mode.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayAsync(this RpcClient c, string queueId, MediaItemBase t, PlayMode mode, bool radiomode)
    {
        var modestr = mode switch
        {
            PlayMode.Play => "play",
            PlayMode.Replace => "replace",
            PlayMode.Next => "next",
            PlayMode.ReplaceNext => "replace_next",
            PlayMode.Add => "add",
            _ => "unknown"
        };

        var mediaArray = new string?[] { t.Uri };

        var m = new Message(Commands.PlayerQueuePlayMedia)
        {
            args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "media", mediaArray },
                    { "option", modestr }
                }
        };

        if (radiomode)
            m.args.Add("radio_mode", true);

        return await c.Send<List<PlayerQueue>>(m);
    }


    /// <summary>
    /// Searches for media items matching the query, with optional result limit and library-only filter.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of results to return (optional).</param>
    /// <param name="library_only">Whether to restrict the search to the library only (default: true).</param>
    /// <returns>A <see cref="Search"/> result object, or null if the request fails.</returns>
    public static async Task<Search?> SearchAsync(this RpcClient c, string query, int? limit = null, bool library_only = true)
    {
        var args = new Dictionary<string, object>()
            {
                { "search_query", query },
                { "library_only", library_only }
            };

        if (limit != null)
            args.Add("limit", limit);

        var m = new Message(Commands.MusicSearch)
        {
            args = args
        };

        var y = await c.Send<Search>(m);
        return y;
    }

}

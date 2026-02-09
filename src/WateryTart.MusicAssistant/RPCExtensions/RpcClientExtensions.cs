using System.Threading.Tasks;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.RPCExtensions;

public static partial class RpcClientExtensions
{

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

    /* Player related commands */
    public static async Task<List<Player>?> PlayersAllAsync(this RpcClient c)
    {
        return await c.Send<List<Player>>(WebSocketExtensions.WebsocketClientExtensions.JustCommand(Commands.PlayersAll));
    }

    public static async Task<List<PlayerQueue>?> PlayerNextAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerNext, playerId, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerPlayAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPlay, playerId, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerPlayPauseAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPlayPause, playerId, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerPreviousAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerQueuesAllAsync(this RpcClient c)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustCommand(Commands.PlayerQueuesAll));
    }

    public static async Task<PlayerQueue?> PlayerActiveQueueAsync(this RpcClient c, string id)
    {
        return await c.Send<PlayerQueue>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerActiveQueue, id, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerQueueItemsAsync(this RpcClient c, string id)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerQueueItems, id, "queue_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeUpAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
    }

    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeDownAsync(this RpcClient c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(WebSocketExtensions.WebsocketClientExtensions.JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
    }

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



    /*Search*/
    public static async Task<Search?> SearchAsync(this RpcClient c, string query, int? limit = null, bool library_only = true)
    {
        var args = new Dictionary<string, object>()
            {
                { "search_query", query },
                { "library_only", library_only }
            };

        if (limit != null)
            args.Add("limit", limit);

        var m = new Message(Commands.Search)
        {
            args = args
        };

        var y = await c.Send<Search>(m);
        return y;
    }

}

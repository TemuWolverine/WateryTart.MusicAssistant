using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions;

public static partial class WebsocketClientExtensions
{
    public static async Task<PlayersQueuesResponse> PlayAsync(this IWsClient c, string queueId, MediaItemBase t, PlayMode mode, bool radiomode)
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

        return await SendAsync<PlayersQueuesResponse>(c, m);
    }


    public static async Task<PlayersQueuesResponse> PlayerNextAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerNext, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPlayAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPlay, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPlayPauseAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPlayPause, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPreviousAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }

    public static async Task<PlayerResponse> PlayersAllAsync(this IWsClient c)
    {
        return await SendAsync<PlayerResponse>(c, JustCommand(Commands.PlayersAll));
    }

    public static async Task<PlayersQueuesResponse> PlayerQueuesAllAsync(this IWsClient c)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustCommand(Commands.PlayerQueuesAll));
    }

    public static async Task<PlayerQueueResponse> PlayerActiveQueueAsync(this IWsClient c, string id)
    {
        return await SendAsync<PlayerQueueResponse>(c, JustId(Commands.PlayerActiveQueue, id, "player_id"));
    }

    public static async Task<PlayerQueueItemsResponse> PlayerQueueItemsAsync(this IWsClient c, string id)
    {
        return await SendAsync<PlayerQueueItemsResponse>(c, JustId(Commands.PlayerQueueItems, id, "queue_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerGroupVolumeUpAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerGroupVolumeDownAsync(this IWsClient c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
    }
}
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    /*MusicAssistantClientWs*/

    public static async Task<PlayerQueueResponse> GetPlayerActiveQueueAsync(this MusicAssistantClientWs c, string id)
    {
        return await SendAsync<PlayerQueueResponse>(c, ClientHelpers.JustId(Commands.PlayerActiveQueue, id, "player_id"));
    }

    /// <summary>
    /// Gets all items in a specific queue.
    /// </summary>
    /// <param name="id">The ID of the queue.</param>
    public static async Task<PlayerQueueItemsResponse> GetPlayerQueueItemsAsync(this MusicAssistantClientWs c, string id)
    {
        return await SendAsync<PlayerQueueItemsResponse>(c, ClientHelpers.JustId(Commands.PlayerQueueItems, id, "queue_id"));
    }

    public static async Task<PlayersQueuesResponse> GetPlayerQueuesAllAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustCommand(Commands.PlayerQueuesAll));
    }


    /// <summary>
    /// Retrieves a list of all available players from the Music Assistant server.
    /// </summary>
    /// <returns>A list of <see cref="Player"/> objects, or null if the request fails.</returns>
    public static async Task<PlayerResponse> GetPlayersAllAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<PlayerResponse>(c, ClientHelpers.JustCommand(Commands.PlayersAll));
    }

    public static async Task<PlayersQueuesResponse> PlayAsync(this MusicAssistantClientWs c, string queueId, MediaItemBase t, PlayMode mode, bool radiomode)
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

    public static async Task<PlayersQueuesResponse> PlayerGroupVolumeDownAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerGroupVolumeUpAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerNextAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerNext, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPlayAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerPlay, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPlayPauseAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerPlayPause, playerId, "player_id"));
    }

    public static async Task<PlayersQueuesResponse> PlayerPreviousAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }
}
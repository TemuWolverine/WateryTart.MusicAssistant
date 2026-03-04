using WateryTart.MusicAssistant.Generators.Attributes;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
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
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "media", mediaArray },
                    { "option", modestr }
                }
        };

        if (radiomode)
            m.Args.Add("radio_mode", true);

        return await SendAsync<PlayersQueuesResponse>(c, m);
    }

    public static async Task<PlayersQueuesResponse> SetPlayerGroupVolumeAsync(this MusicAssistantClientWs c, string playerId, int volume)
    {
        var m = new Message(Commands.PlayerGroupVolume)
        {
            Args = new Dictionary<string, object>()
                {
                    { "player_id", playerId },
                    { "volume_level", volume },
                }
        };

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

    [ToRpc]
    public static async Task<PlayersQueuesResponse> PlayerPreviousAsync(this MusicAssistantClientWs c, string playerId)
    {
        return await SendAsync<PlayersQueuesResponse>(c, ClientHelpers.JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }

    public static async Task<TempResponse> PlayerSeekAsync(this MusicAssistantClientWs c, string queueId, int position)
    {
        var m = new Message(Commands.PlayerQueuesSeek)
        {
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "position", position },
                }
        };

        return await SendAsync<TempResponse>(c, m);
    }

    [ToRpc]
    public static async Task<TempResponse> SetPlayerQueueRepeatAsync(this MusicAssistantClientWs c, string queueId, RepeatMode mode)
    {
        var m = new Message(Commands.PlayerQueuesRepeat)
        {
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "repeat_mode", mode },
                }
        };

        return await SendAsync<TempResponse>(c, m);
    }


    [ToRpc]
    public static async Task<TempResponse> SetPlayerQueueShuffleAsync(this MusicAssistantClientWs c, string queueId, bool shuffle_enable)
    {
        var m = new Message(Commands.PlayerQueuesShuffle)
        {
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "shuffle_enabled", shuffle_enable },
                }
        };

        return await SendAsync<TempResponse>(c, m);
    }

    [ToRpc]
    public static async Task<TempResponse> SetPlayerQueueDontStopTheMusicAsync(this MusicAssistantClientWs c, string queueId, bool dont_stop_the_music_enabled)
    {
        var m = new Message(Commands.PlayerQueuesDontStopTheMusic)
        {
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "dont_stop_the_music_enabled", dont_stop_the_music_enabled },
                }
        };

        return await SendAsync<TempResponse>(c, m);
    }

    [ToRpc]
    public static async Task<TempResponse> ClearPlayerQueueAsync(this MusicAssistantClientWs c, string queueId, bool skip_stop = true)
    {
        var m = new Message(Commands.PlayerQueuesClear)
        {
            Args = new Dictionary<string, object>()
                {
                    { "queue_id", queueId },
                    { "skip_stop", skip_stop },
                }
        };

        return await SendAsync<TempResponse>(c, m);
    }
}
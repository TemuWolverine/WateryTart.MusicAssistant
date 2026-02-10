using System.Collections.Generic;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    /// <summary>
    /// Gets the active queue for a specific player.
    /// </summary>
    /// <param name="id">The ID of the player.</param>
    /// <returns>The <see cref="PlayerQueue"/> for the player, or null if not found.</returns>
    public static async Task<PlayerQueue?> GetPlayerActiveQueueAsync(this MusicAssistantClientRpc c, string id)
    {
        return await c.Send<PlayerQueue>(ClientHelpers.JustId(Commands.PlayerActiveQueue, id, "player_id"));
    }

    /// <summary>
    /// Gets all items in a specific queue.
    /// </summary>
    /// <param name="id">The ID of the queue.</param>
    public static async Task<List<QueuedItem>?> GetPlayerQueueItemsAsync(this MusicAssistantClientRpc c, string id)
    {
        return await c.Send< List<QueuedItem>> (ClientHelpers.JustId(Commands.PlayerQueueItems, id, "queue_id"));
    }

    /// <summary>
    /// Retrieves all player queues from the Music Assistant server.
    /// </summary>
    /// <returns>A list of <see cref="PlayerQueue"/> objects, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> GetPlayerQueuesAllAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustCommand(Commands.PlayerQueuesAll));
    }

    /// <summary>
    /// Retrieves a list of all available players from the Music Assistant server.
    /// </summary>
    /// <returns>A list of <see cref="Player"/> objects, or null if the request fails.</returns>
    public static async Task<List<Player>?> GetPlayersAllAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<Player>>(ClientHelpers.JustCommand(Commands.PlayersAll));
    }

    /// <summary>
    /// Plays a media item in the specified queue with the given play mode and radio mode option.
    /// </summary>
    /// <param name="queueId">The ID of the queue.</param>
    /// <param name="t">The media item to play.</param>
    /// <param name="mode">The play mode (e.g., play, replace, add).</param>
    /// <param name="radiomode">Whether to enable radio mode.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayAsync(this MusicAssistantClientRpc c, string queueId, MediaItemBase t, PlayMode mode, bool radiomode)
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
    /// Decreases the group volume for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeDownAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
    }

    /// <summary>
    /// Increases the group volume for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerGroupVolumeUpAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
    }

    /// <summary>
    /// Skips to the next item in the queue for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerNextAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerNext, playerId, "player_id"));
    }

    /// <summary>
    /// Starts playback for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPlayAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerPlay, playerId, "player_id"));
    }

    /// <summary>
    /// Toggles play/pause for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPlayPauseAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerPlayPause, playerId, "player_id"));
    }

    /// <summary>
    /// Skips to the previous item in the queue for the specified player.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <returns>A list of <see cref="PlayerQueue"/> objects representing the updated queue, or null if the request fails.</returns>
    public static async Task<List<PlayerQueue>?> PlayerPreviousAsync(this MusicAssistantClientRpc c, string playerId)
    {
        return await c.Send<List<PlayerQueue>>(ClientHelpers.JustId(Commands.PlayerPrevious, playerId, "player_id"));
    }
}
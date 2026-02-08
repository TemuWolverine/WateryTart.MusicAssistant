using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions;

public static partial class WebsocketClientExtensions
{
    extension(IWsClient c)
    {
        public async Task<PlayersQueuesResponse> PlayAsync(string queueId, MediaItemBase t, PlayMode mode, bool radiomode)
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


        public async Task<PlayersQueuesResponse> PlayerNextAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerNext, playerId, "player_id"));
        }

        public async Task<PlayersQueuesResponse> PlayerPlayAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPlay, playerId, "player_id"));
        }

        public async Task<PlayersQueuesResponse> PlayerPlayPauseAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPlayPause, playerId, "player_id"));
        }

        public async Task<PlayersQueuesResponse> PlayerPreviousAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerPrevious, playerId, "player_id"));
        }

        public async Task<PlayerResponse> PlayersAllAsync()
        {
            return await SendAsync<PlayerResponse>(c, JustCommand(Commands.PlayersAll));
        }

        public async Task<PlayersQueuesResponse> PlayerQueuesAllAsync()
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustCommand(Commands.PlayerQueuesAll));
        }

        public async Task<PlayerQueueResponse> PlayerActiveQueueAsync(string id)
        {
            return await SendAsync<PlayerQueueResponse>(c, JustId(Commands.PlayerActiveQueue, id, "player_id"));
        }

        public async Task<PlayerQueueItemsResponse> PlayerQueueItemsAsync(string id)
        {
            return await SendAsync<PlayerQueueItemsResponse>(c, JustId(Commands.PlayerQueueItems, id, "queue_id"));
        }

        public async Task<PlayersQueuesResponse> PlayerGroupVolumeUpAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerGroupVolumeUp, playerId, "player_id"));
        }

        public async Task<PlayersQueuesResponse> PlayerGroupVolumeDownAsync(string playerId)
        {
            return await SendAsync<PlayersQueuesResponse>(c, JustId(Commands.PlayerGroupVolumeDown, playerId, "player_id"));
        }
    }
}
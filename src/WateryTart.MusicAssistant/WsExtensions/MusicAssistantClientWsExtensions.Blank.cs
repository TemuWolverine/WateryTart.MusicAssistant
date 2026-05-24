using WateryTart.MusicAssistant.Generators.Attributes;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    [ToRpc]
    public static async Task<CountResponse> GetTrackLyrics(this MusicAssistantClientWs c, MediaItem track)
    {
        var m = new Message(Commands.MetadataGetTrackLyrics)
        {
            Args = new Dictionary<string, object>()
                {
                    { "track", track }
                }
        };
        return await SendAsync<CountResponse>(c, m);
    }
}
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    /*MusicAssistantClientWs*/
    public static async Task<CountResponse> GetTrackLyrics(this MusicAssistantClientWs c, MediaItem track)
    {
        var m = new Message(Commands.MetadataGetTrackLyrics)
        {
            args = new Dictionary<string, object>()
                {
                    { "track", track }
                }
        };
        return await SendAsync<CountResponse>(c, m);
    }
}
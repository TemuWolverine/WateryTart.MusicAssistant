using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    /*MusicAssistantClientWs*/
    public static async Task<StringArrayResponse> GetLyricsAsync(this MusicAssistantClientWs c, Item track)
    {
        var m = new Message(Commands.MetadataGetTrackLyrics)
        {
            args = new Dictionary<string, object>()
                {
                    { "track", track }
                }
        };
        return await SendAsync<StringArrayResponse>(c, m);
    }
}
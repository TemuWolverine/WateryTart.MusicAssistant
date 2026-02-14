using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    /*MusicAssistantClientRpc*/

    public static async Task<string[]?> GetLyricsAsync(this MusicAssistantClientRpc c, Item track)
    {
        var m = new Message(Commands.MetadataGetTrackLyrics)
        {
            args = new Dictionary<string, object>()
                {
                    { "track", track }
                }
        };
        return await c.Send<string[]?>(m);
    }
}
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    public static async Task<Artist?> GetArtistAsync(this MusicAssistantClientRpc c, string artistId, string providerInstanceIdOrDomain)
    {
        return await c.Send<Artist?>(ClientHelpers.IdAndProvider(Commands.MusicArtistGet, artistId, providerInstanceIdOrDomain));
    }


}
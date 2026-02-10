using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    public static async Task<Artist?> GetArtistAsync(this MusicAssistantClientRpc c, string artistId, string providerInstanceIdOrDomain)
    {
        return await c.Send<Artist?>(ClientHelpers.IdAndProvider(Commands.MusicArtistGet, artistId, providerInstanceIdOrDomain));
    }

    public static async Task<List<Artist>?> GetArtistsAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<Artist>?>(ClientHelpers.JustCommand(Commands.MusicArtistsGet));
    }

    public static async Task<List<Album>?> GetArtistAlbumsAsync(this MusicAssistantClientRpc c, string artistId, string providerInstanceIdOrDomain)
    {
        return await c.Send<List<Album>?>(ClientHelpers.IdAndProvider(Commands.MusicArtistAlbums, artistId, providerInstanceIdOrDomain));
    }

    public static async Task<int?> GetArtistCountAsync(this MusicAssistantClientRpc c)
    {
        var m = new Message(Commands.MusicArtistsCount)
        {
            args = new Dictionary<string, object>()
            {
                { "favorite_only", "false" },
                { "album_artists_only", "true" }
            }
        };
        return await c.Send<int?>(m);
    }
}
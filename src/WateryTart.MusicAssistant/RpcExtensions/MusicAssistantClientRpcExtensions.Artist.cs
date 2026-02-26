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

    public static async Task<List<Artist>?> GetArtistsAsync(this MusicAssistantClientRpc c, bool favourite = false, string? search = null, int? limit = null, int? offset = null, string? order_by = null, bool album_artists_only = false)
    {
        var args = new Dictionary<string, object>()
        {
            { "favorite", favourite },
            { "album_artists_only", album_artists_only }
        };

        if (!string.IsNullOrEmpty(search))
            args.Add("search", search);

        if (!string.IsNullOrEmpty(order_by))
            args.Add("order_by", order_by);

        if (limit != null)
            args.Add("limit", limit);

        if (offset != null)
            args.Add("offset", offset);

        var m = new Message(Commands.MusicArtistsGet)
        {
            args = args
        };

        return await c.Send<List<Artist>?>(m);
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
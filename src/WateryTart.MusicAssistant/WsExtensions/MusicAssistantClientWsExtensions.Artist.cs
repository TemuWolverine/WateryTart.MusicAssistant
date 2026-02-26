using System.Reflection.Metadata;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    public static async Task<ArtistResponse> GetArtistAsync(this MusicAssistantClientWs c, string artistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<ArtistResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicArtistGet, artistId, providerInstanceIdOrDomain));
    }

    public static async Task<ArtistsResponse> GetArtistsAsync(this MusicAssistantClientWs c, bool favourite = false, string? search = null, int? limit = null, int? offset = null, string? order_by = null, bool album_artists_only = false)
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

        return await SendAsync<ArtistsResponse>(c, m);
    }

    public static async Task<AlbumsResponse> GetArtistAlbumsAsync(this MusicAssistantClientWs c, string artistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<AlbumsResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicArtistAlbums, artistId, providerInstanceIdOrDomain));
    }

    public static async Task<CountResponse> GetArtistCountAsync(this MusicAssistantClientWs c)
    {
        var m = new Message(Commands.MusicArtistsCount)
        {
            args = new Dictionary<string, object>()
            {
                { "favorite_only", "false" },
                { "album_artists_only", "true" }
            }
        };
        return await SendAsync<CountResponse>(c, m);
    }
}
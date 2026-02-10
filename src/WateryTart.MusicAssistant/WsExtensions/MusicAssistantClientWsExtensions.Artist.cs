using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    public static async Task<ArtistResponse> GetArtistAsync(this MusicAssistantClientWs c, string artistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<ArtistResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicArtistGet, artistId, providerInstanceIdOrDomain));
    }

    public static async Task<ArtistsResponse> GetArtistsAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<ArtistsResponse>(c, ClientHelpers.JustCommand(Commands.MusicArtistsGet));
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
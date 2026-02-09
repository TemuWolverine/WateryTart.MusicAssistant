using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions;
public static partial class WebsocketClientExtensions
{
    public static async Task<ArtistResponse> ArtistGetAsync(this IWsClient c, string artistid, string provider_instance_id_or_domain)
    {
        return await SendAsync<ArtistResponse>(c, IdAndProvider(Commands.MusicArtistGet, artistid, provider_instance_id_or_domain));
    }

    public static async Task<ArtistsResponse> ArtistsGetAsync(this IWsClient c)
    {
        return await SendAsync<ArtistsResponse>(c, JustCommand(Commands.MusicArtistsGet));
    }

    public static async Task<AlbumsResponse> ArtistAlbumsAsync(this IWsClient c, string artistid, string provider_instance_id_or_domain)
    {
        return await SendAsync<AlbumsResponse>(c, IdAndProvider(Commands.MusicArtistAlbums, artistid, provider_instance_id_or_domain));
    }

    public static async Task<CountResponse> ArtistCountAsync(this IWsClient c)
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
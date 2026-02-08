using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions;

public static partial class WebsocketClientExtensions
{
    public static async Task<RecommendationResponse> MusicRecommendationsAsync(this IWsClient c)
    {
        return await SendAsync<RecommendationResponse>(c, JustCommand(Commands.MusicRecommendations));
    }

    public static async Task<CountResponse> AlbumsCountAsync(this IWsClient c)
    {
        var m = new Message("music/albums/count")
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", "false" },
                    { "album_types", "[\"album\", \"single\", \"live\", \"soundtrack\", \"compilation\", \"ep\", \"unknown\"]" }
                }
        };

        return await SendAsync<CountResponse>(c, m);
    }

    public static async Task<CountResponse> TrackCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/tracks/count", "false", "favourite_only"));
    }

    public static async Task<TracksResponse> TracksGetAsync(this IWsClient c, int? limit = null, int? offset = null)
    {
        var m = new Message("music/tracks/library_items")
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", "false" },
                }
        };
        if (limit.HasValue)
        {
            m.args["limit"] = limit.Value.ToString();
        }
        if (offset.HasValue)
        {
            m.args["offset"] = offset.Value.ToString();
        }
        return await SendAsync<TracksResponse>(c, m);
    }

    public static async Task<CountResponse> PlaylistsCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/playlists/count", "false", "favorite_only"));
    }

    public static async Task<CountResponse> AudiobookCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/audiobooks/count", "false", "favorite_only"));
    }

    public static async Task<CountResponse> PodcastCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/podcasts/count", "false", "favorite_only"));
    }

    public static async Task<CountResponse> GenreCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/genre/count", "false", "favorite_only"));
    }
    public static async Task<CountResponse> RadiosCountAsync(this IWsClient c)
    {
        return await SendAsync<CountResponse>(c, JustId("music/radios/count", "false", "favorite_only"));
    }

    public static async Task<PlaylistResponse> PlaylistGetAsync(this IWsClient c, string playlistId, string provider_instance_id_or_domain)
    {
        return await SendAsync<PlaylistResponse>(c, IdAndProvider(Commands.PlaylistGet, playlistId, provider_instance_id_or_domain));
    }

    public static async Task<PlaylistsResponse> PlaylistsGetAsync(this IWsClient c, int? limit = null, int? offset = null)
    {
        var m = new Message("music/playlists/library_items")
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", "false" },

                }
        };

        if (limit.HasValue)
        {
            m.args["limit"] = limit.Value.ToString();
        }
        if (offset.HasValue)
        {
            m.args["offset"] = offset.Value.ToString();
        }

        return await SendAsync<PlaylistsResponse>(c, m);
    }

    public static async Task<TracksResponse> PlaylistTracksGetAsync(this IWsClient c, string playlistId, string provider_instance_id_or_domain)
    {
        return await SendAsync<TracksResponse>(c, IdAndProvider(Commands.PlaylistTracksGet, playlistId, provider_instance_id_or_domain));
    }

    public static async Task<ArtistsResponse> ArtistsGetAsync(this IWsClient c, int? limit = null, int? offset = null)
    {
        var m = new Message("music/artists/library_items")
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", "false" },
                }
        };

        if (limit.HasValue)
        {
            m.args["limit"] = limit.Value.ToString();
        }
        if (offset.HasValue)
        {
            m.args["offset"] = offset.Value.ToString();
        }

        return await SendAsync<ArtistsResponse>(c, m);
    }

    public static async Task<AlbumsResponse> MusicAlbumsLibraryItemsAsync(this IWsClient c, int? limit = null, int? offset = null)
    {
        var m = new Message(Commands.MusicAlbumLibraryItems)
        {
            args = new Dictionary<string, object>()
        };

        if (limit.HasValue)
        {
            m.args["limit"] = limit.Value.ToString();
        }
        if (offset.HasValue)
        {
            m.args["offset"] = offset.Value.ToString();
        }

        return await SendAsync<AlbumsResponse>(c, m);
    }

    public static async Task<AlbumResponse> MusicAlbumGetAsync(this IWsClient c, string id, string provider_instance_id_or_domain)
    {
        return await SendAsync<AlbumResponse>(c, IdAndProvider(Commands.MusicAlbumGet, id, provider_instance_id_or_domain));
    }

    public static async Task<TracksResponse> MusicAlbumTracksAsync(this IWsClient c, string id, string provider_instance_id_or_domain)
    {
        return await SendAsync<TracksResponse>(c, IdAndProvider(Commands.MusicAlbumTracks, id, provider_instance_id_or_domain));
    }

    public static async Task<TracksResponse> MusicSimilarTracksAsync(this IWsClient c, string id, string provider_instance_id_or_domain)
    {
        return await SendAsync<TracksResponse>(c, IdAndProvider(Commands.MusicSimilarTracks, id, provider_instance_id_or_domain));
    }
}
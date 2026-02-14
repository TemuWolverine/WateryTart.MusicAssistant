using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    /// <summary>
    /// Retrieves the count of albums, optionally filtered by favorite status
    /// </summary>
    /// <param name="favouriteOnly">Whether to restrict results favourites only.</param>
    /// <returns>A <see cref="CountResponse"/> containing the album count.</returns>
    public static async Task<CountResponse> GetAlbumsCountAsync(this MusicAssistantClientWs c, bool favouriteOnly = false)
    {
        var m = new Message(Commands.MusicAlbumsCount)
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", favouriteOnly },
                    { "album_types", "[\"album\", \"single\", \"live\", \"soundtrack\", \"compilation\", \"ep\", \"unknown\"]" }
                }
        };

        return await SendAsync<CountResponse>(c, m);
    }

    /// <summary>
    /// Retrieves a list of artist library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>An <see cref="ArtistsResponse"/> containing the artists.</returns>
    public static async Task<ArtistsResponse> GetArtistsAsync(this MusicAssistantClientWs c, int? limit = null, int? offset = null)
    {
        var m = new Message(Commands.MusicArtistsGet)
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

    /// <summary>
    /// Retrieves the count of audiobooks, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the audiobook count.</returns>
    public static async Task<CountResponse> GetAudiobookCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId(Commands.MusicAudiobooksCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the count of genres, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the genre count.</returns>
    public static async Task<CountResponse> GetGenreCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId("music/genre/count", "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves album details for a specific album and provider.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>An <see cref="AlbumResponse"/> with album details.</returns>
    public static async Task<AlbumResponse> GetMusicAlbumAsync(this MusicAssistantClientWs c, string id, string providerInstanceIdOrDomain)
    {
        return await SendAsync<AlbumResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicAlbumGet, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a list of album library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>An <see cref="AlbumsResponse"/> containing the albums.</returns>
    public static async Task<AlbumsResponse> GetMusicAlbumsLibraryItemsAsync(this MusicAssistantClientWs c, int? limit = null, int? offset = null)
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

    /// <summary>
    /// Retrieves the tracks for a specific album and provider.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="TracksResponse"/> containing the album tracks.</returns>
    public static async Task<TracksResponse> GetMusicAlbumTracksAsync(this MusicAssistantClientWs c, string id, string providerInstanceIdOrDomain)
    {
        return await SendAsync<TracksResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicAlbumTracks, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves music recommendations.
    /// </summary>
    /// <returns>A <see cref="RecommendationResponse"/> containing recommended items.</returns>
    public static async Task<RecommendationResponse> GetMusicRecommendationsAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<RecommendationResponse>(c, ClientHelpers.JustCommand(Commands.MusicRecommendations));
    }

    /// <summary>
    /// Retrieves tracks similar to a specific track and provider.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="TracksResponse"/> containing similar tracks.</returns>
    public static async Task<TracksResponse> GetMusicSimilarTracksAsync(this MusicAssistantClientWs c, string id, string providerInstanceIdOrDomain)
    {
        return await SendAsync<TracksResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicSimilarTracks, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a playlist by ID and provider.
    /// </summary>
    /// <param name="playlistId">The playlist ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="PlaylistResponse"/> containing playlist details.</returns>
    public static async Task<PlaylistResponse> GetPlaylistAsync(this MusicAssistantClientWs c, string playlistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<PlaylistResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicPlaylistsGet, playlistId, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a list of playlist library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>A <see cref="PlaylistsResponse"/> containing the playlists.</returns>
    public static async Task<PlaylistsResponse> GetPlaylistsAsync(this MusicAssistantClientWs c, int? limit = null, int? offset = null)
    {
        var m = new Message(Commands.MusicPlaylistsLibraryItems)
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

    /// <summary>
    /// Retrieves the count of playlists, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the playlist count.</returns>
    public static async Task<CountResponse> GetPlaylistsCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId(Commands.MusicPlaylistsCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the tracks for a specific playlist and provider.
    /// </summary>
    /// <param name="playlistId">The playlist ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="TracksResponse"/> containing the playlist tracks.</returns>
    public static async Task<TracksResponse> GetPlaylistTracksAsync(this MusicAssistantClientWs c, string playlistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<TracksResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicPlaylistsPlaylistTracks, playlistId, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves the count of podcasts, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the podcast count.</returns>
    public static async Task<CountResponse> GetPodcastCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId(Commands.MusicPodcastsCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the count of radios, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the radio count.</returns>
    public static async Task<CountResponse> GetRadiosCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId(Commands.MusicRadiosCount, "false", "favorite_only"));
    }

    public static async Task<TracksResponse> GetRecentlyAddedTracksAsync(this MusicAssistantClientWs c, int limit = 0)
    {
        return await SendAsync<TracksResponse>(c, ClientHelpers.JustId(Commands.MusicRecentlyAddedTracks, "limit", limit.ToString()));
    }

    public static async Task<TracksResponse> GetRecentlyPlayedItemsAsync(this MusicAssistantClientWs c, int limit = 0, string userid = "",
            string queueid = "", bool fullyPlayedOnly = false, bool userInitiatedOnly = false)
    {
        var m = new Message(Commands.MusicRecentlyPlayedTracks)
        {
            args = new Dictionary<string, object>()
            {
                { "limit", limit},
                {"fully_played_only", fullyPlayedOnly},
                {"user_initiated_only", userInitiatedOnly}
            }
        };

        if (!string.IsNullOrEmpty(userid))
            m.args["user_id"] = userid;
        if (!string.IsNullOrEmpty(queueid))
            m.args["queue_id"] = queueid;

        return await SendAsync<TracksResponse>(c, m); ;
    }

    /// <summary>
    /// Retrieves the albums for a specific track and provider, with an option to restrict to library items.
    /// </summary>
    /// <param name="itemid">The track ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <param name="inLibraryOnly">Whether to restrict results to library items only.</param>
    /// <returns>An <see cref="AlbumsResponse"/> containing the albums.</returns>
    public static async Task<AlbumsResponse> GetTrackAlbumsAsync(this MusicAssistantClientWs c, string itemid, string providerInstanceIdOrDomain, bool inLibraryOnly = false)
    {
        var m = new Message(Commands.MusicTracksTrackAlbum)
        {
            args = new Dictionary<string, object>()
            {
                { "item_id", itemid },
                {"provider_instance_id_or_domain", providerInstanceIdOrDomain},
                {"in_library_only", inLibraryOnly}
            }
        };

        return await SendAsync<AlbumsResponse>(c, m);
    }

    /// <summary>
    /// Retrieves the count of tracks, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="CountResponse"/> containing the track count.</returns>
    public static async Task<CountResponse> GetTrackCountAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<CountResponse>(c, ClientHelpers.JustId(Commands.MusicTracksCount, "false", "favourite_only"));
    }

    /// <summary>
    /// Retrieves a list of track library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>A <see cref="TracksResponse"/> containing the tracks.</returns>
    public static async Task<TracksResponse> GetTracksAsync(this MusicAssistantClientWs c, int? limit = null, int? offset = null)
    {
        var m = new Message(Commands.MusicTracksLibraryItems)
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

    public static async Task<TempResponse> AddFavoriteItemAsync(this MusicAssistantClientWs c, MediaItemBase t)
    {
        var m = new Message(Commands.MusicFavouritesAddItem)
        {
            args = new Dictionary<string, object>()
                {
                    { "item", t },
                }
        };
        return await SendAsync<TempResponse>(c, m);
    }

    public static async Task<TempResponse> RemoveFavoriteItemAsync(this MusicAssistantClientWs c, MediaItemBase t)
    {
        var m = new Message(Commands.MusicFavouritesRemoveItem)
        {
            args = new Dictionary<string, object>()
                {
                    { "media_type", t.MediaType },
                    { "library_item_id", t.ItemId}
                }
        };
        return await SendAsync<TempResponse>(c, m);
    }

    public static async Task<ItemResponse> GetLibraryItemAsync(this MusicAssistantClientWs c, MediaType type, string itemId, string providerInstanceIdOrDomain)
    {
        var m = new Message(Commands.MusicGetLibraryItem)
        {
            args = new Dictionary<string, object>()
                {
                    { "media_type", type },
                    { "item_id", itemId},
                    { "provider_instance_id_or_domain", providerInstanceIdOrDomain}
                }
        };
        return await SendAsync<ItemResponse>(c, m);
    }
    
}
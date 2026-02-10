using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    /// <summary>
    /// Retrieves the count of albums, optionally filtered by favorite status
    /// </summary>
    /// <param name="favouriteOnly">Whether to restrict results favourites only.</param>
    /// <returns>A <see cref="int"/> containing the album count.</returns>
    public static async Task<int?> GetAlbumsCountAsync(this MusicAssistantClientRpc c, bool favouriteOnly = false)
    {
        var m = new Message(Commands.MusicAlbumsCount)
        {
            args = new Dictionary<string, object>()
                {
                    { "favorite_only", favouriteOnly },
                    { "album_types", "[\"album\", \"single\", \"live\", \"soundtrack\", \"compilation\", \"ep\", \"unknown\"]" }
                }
        };

        return await c.Send<int?>(m);
    }

    /// <summary>
    /// Retrieves a list of artist library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>An <see cref="List<Artist>"/> containing the artists.</returns>
    public static async Task<List<Artist>?> GetArtistsAsync(this MusicAssistantClientRpc c, int? limit = null, int? offset = null)
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

        return await c.Send<List<Artist>?>(m);
    }

    /// <summary>
    /// Retrieves the count of audiobooks, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the audiobook count.</returns>
    public static async Task<int?> GetAudiobookCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int?>(ClientHelpers.JustId(Commands.MusicAudiobooksCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the count of genres, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the genre count.</returns>
    public static async Task<int?> GetGenreCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int?>(ClientHelpers.JustId("music/genre/count", "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves album details for a specific album and provider.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>An <see cref="Album"/> with album details.</returns>
    public static async Task<Album?> GetMusicAlbumAsync(this MusicAssistantClientRpc c, string id, string providerInstanceIdOrDomain)
    {
        return await c.Send<Album?>(ClientHelpers.IdAndProvider(Commands.MusicAlbumGet, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a list of album library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>An <see cref="ListAlbum"/> containing the albums.</returns>
    public static async Task<List<Album>?> GetMusicAlbumsLibraryItemsAsync(this MusicAssistantClientRpc c, int? limit = null, int? offset = null)
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

        return await c.Send<List<Album>?>(m);
    }

    /// <summary>
    /// Retrieves the tracks for a specific album and provider.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="List<Item>"/> containing the album tracks.</returns>
    public static async Task<List<Item>?> GetMusicAlbumTracksAsync(this MusicAssistantClientRpc c, string id, string providerInstanceIdOrDomain)
    {
        return await c.Send<List<Item>?>(ClientHelpers.IdAndProvider(Commands.MusicAlbumTracks, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves music recommendations.
    /// </summary>
    /// <returns>A <see cref="List<Recommendation>"/> containing recommended items.</returns>
    public static async Task<List<Recommendation>?> GetMusicRecommendationsAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<Recommendation>?>(ClientHelpers.JustCommand(Commands.MusicRecommendations));
    }

    /// <summary>
    /// Retrieves tracks similar to a specific track and provider.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="List<Item>"/> containing similar tracks.</returns>
    public static async Task<List<Item>?> GetMusicSimilarTracksAsync(this MusicAssistantClientRpc c, string id, string providerInstanceIdOrDomain)
    {
        return await c.Send<List<Item>?>(ClientHelpers.IdAndProvider(Commands.MusicSimilarTracks, id, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a playlist by ID and provider.
    /// </summary>
    /// <param name="playlistId">The playlist ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="Playlist"/> containing playlist details.</returns>
    public static async Task<Playlist?> GetPlaylistAsync(this MusicAssistantClientRpc c, string playlistId, string providerInstanceIdOrDomain)
    {
        return await c.Send<Playlist?>(ClientHelpers.IdAndProvider(Commands.MusicPlaylistsGet, playlistId, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves a list of playlist library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>A <see cref="List<Playlist>"/> containing the playlists.</returns>
    public static async Task<List<Playlist>?> GetPlaylistsAsync(this MusicAssistantClientRpc c, int? limit = null, int? offset = null)
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

        return await c.Send<List<Playlist>?>(m);
    }

    /// <summary>
    /// Retrieves the count of playlists, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the playlist count.</returns>
    public static async Task<int> GetPlaylistsCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int>(ClientHelpers.JustId(Commands.MusicPlaylistsCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the tracks for a specific playlist and provider.
    /// </summary>
    /// <param name="playlistId">The playlist ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <returns>A <see cref="List<Item>"/> containing the playlist tracks.</returns>
    public static async Task<List<Item>?> GetPlaylistTracksAsync(this MusicAssistantClientRpc c, string playlistId, string providerInstanceIdOrDomain)
    {
        return await c.Send<List<Item>?>(ClientHelpers.IdAndProvider(Commands.MusicPlaylistsPlaylistTracks, playlistId, providerInstanceIdOrDomain));
    }

    /// <summary>
    /// Retrieves the count of podcasts, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the podcast count.</returns>
    public static async Task<int> GetPodcastCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int>(ClientHelpers.JustId(Commands.MusicPodcastsCount, "false", "favorite_only"));
    }

    /// <summary>
    /// Retrieves the count of radios, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the radio count.</returns>
    public static async Task<int> GetRadiosCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int>(ClientHelpers.JustId(Commands.MusicRadiosCount, "false", "favorite_only"));
    }

    public static async Task<List<Item>?> GetRecentlyAddedTracksAsync(this MusicAssistantClientRpc c, int limit = 0)
    {
        return await c.Send<List<Item>?>(ClientHelpers.JustId(Commands.MusicRecentlyAddedTracks, "limit", limit.ToString()));
    }

    public static async Task<List<Item>?> GetRecentlyPlayedItemsAsync(this MusicAssistantClientRpc c, int limit = 0, string userid = "",
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

        return await c.Send<List<Item>?>(m); ;
    }

    /// <summary>
    /// Retrieves the albums for a specific track and provider, with an option to restrict to library items.
    /// </summary>
    /// <param name="itemid">The track ID.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain.</param>
    /// <param name="inLibraryOnly">Whether to restrict results to library items only.</param>
    /// <returns>An <see cref="List<Album>"/> containing the albums.</returns>
    public static async Task<List<Album>?> GetTrackAlbumsAsync(this MusicAssistantClientRpc c, string itemid, string providerInstanceIdOrDomain, bool inLibraryOnly = false)
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

        return await c.Send<List<Album>?>(m);
    }

    /// <summary>
    /// Retrieves the count of tracks, optionally filtered by favorite status.
    /// </summary>
    /// <returns>A <see cref="int"/> containing the track count.</returns>
    public static async Task<int> GetTrackCountAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<int>(ClientHelpers.JustId(Commands.MusicTracksCount, "false", "favourite_only"));
    }

    /// <summary>
    /// Retrieves a list of track library items, with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="offset">Number of items to skip.</param>
    /// <returns>A <see cref="List<Item>"/> containing the tracks.</returns>
    public static async Task<List<Item>?> GetTracksAsync(this MusicAssistantClientRpc c, int? limit = null, int? offset = null)
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
        return await c.Send<List<Item>?>(m);
    }
}
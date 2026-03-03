using WateryTart.MusicAssistant.Generators.Attributes;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    /// <summary>
    /// Retrieves detailed information for a specific artist.
    /// </summary>
    /// <param name="artistId">The unique identifier of the artist.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain hosting the artist.</param>
    /// <returns>A task that represents the asynchronous operation, containing the artist details.</returns>
    public static async Task<ArtistResponse> GetArtistAsync(this MusicAssistantClientWs c, string artistId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<ArtistResponse>(c, ClientHelpers.IdAndProvider(Commands.MusicArtistGet, artistId, providerInstanceIdOrDomain));
    }

    [ToRpc]
    /// <summary>
    /// Retrieves a filtered and sorted list of artists from the Music Assistant library.
    /// </summary>
    /// <param name="favourite">If <c>true</c>, returns only favorite artists. Default is <c>false</c>.</param>
    /// <param name="search">Optional search query to filter artists by name.</param>
    /// <param name="limit">Maximum number of artists to return.</param>
    /// <param name="offset">Number of artists to skip (for pagination).</param>
    /// <param name="order_by">Optional custom ordering field.</param>
    /// <param name="album_artists_only">If <c>true</c>, returns only album artists (excludes track-only artists). Default is <c>false</c>.</param>
    /// <param name="order">Predefined sort order. Ignored if <paramref name="order_by"/> is specified.</param>
    /// <returns>A task that represents the asynchronous operation, containing the list of artists.</returns>

    public static async Task<ArtistsResponse> GetArtistsAsync(this MusicAssistantClientWs c, bool favourite = false, string? search = null, int? limit = null, int? offset = null, string? order_by = null, bool album_artists_only = false, OrderBy order = OrderBy.Unknown)
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

        if (string.IsNullOrEmpty(order_by) && order != OrderBy.Unknown)
            args.Add("order_by", order);

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

    [ToRpc]
    /// <summary>
    /// Retrieves all albums associated with a specific artist.
    /// </summary>
    /// <param name="artistId">The unique identifier of the artist.</param>
    /// <param name="providerInstanceIdOrDomain">The provider instance ID or domain hosting the artist.</param>
    /// <param name="in_library_only">If <c>true</c>, returns only albums that are in the user's library. Default is <c>false</c>.</param>
    /// <returns>A task that represents the asynchronous operation, containing the list of albums by the artist.</returns>
    public static async Task<AlbumsResponse> GetArtistAlbumsAsync(this MusicAssistantClientWs c, string artistId, string providerInstanceIdOrDomain, bool in_library_only = false)
    {
        var m = new Message(Commands.MusicArtistAlbums)
        {
            args = new Dictionary<string, object>
            {
                { "item_id", artistId },
                { "provider_instance_id_or_domain", providerInstanceIdOrDomain },
                {  "in_library_only", in_library_only   }
            }
        };

        return await SendAsync<AlbumsResponse>(c, m);
    }

    [ToRpc]
    /// <summary>
    /// Retrieves the total count of artists in the Music Assistant library.
    /// </summary>
    /// <param name="favourite_only">If <c>true</c>, counts only favorite artists. Default is <c>false</c>.</param>
    /// <param name="album_artists_only">If <c>true</c>, counts only album artists (excludes track-only artists). Default is <c>false</c>.</param>
    /// <returns>A task that represents the asynchronous operation, containing the count of artists.</returns>
    public static async Task<CountResponse> GetArtistCountAsync(this MusicAssistantClientWs c, bool favourite_only = false, bool album_artists_only = false)
    {
        var m = new Message(Commands.MusicArtistsCount)
        {
            args = new Dictionary<string, object>()
            {
                { "favorite_only", favourite_only },
                { "album_artists_only", album_artists_only }
            }
        };
        return await SendAsync<CountResponse>(c, m);
    }
}
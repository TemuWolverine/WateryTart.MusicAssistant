using WateryTart.MusicAssistant.Generators.Attributes;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    [ToRpc]
    /// <summary>
    /// Searches for media items matching the query, with optional result limit and library-only filter.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of results to return (optional).</param>
    /// <param name="library_only">Whether to restrict the search to the library only (default: true).</param>
    /// <returns>A <see cref="Search"/> result object, or null if the request fails.</returns>
    public static async Task<SearchResponse> SearchAsync(this MusicAssistantClientWs c, string query, int? limit = null, bool library_only = false)
    {
        var args = new Dictionary<string, object>()
        {
            { "search_query", query },
            { "library_only", library_only }
        };

        if (limit != null)
            args.Add("limit", limit);

        var m = new Message(Commands.MusicSearch)
        {
            args = args
        };

        return await SendAsync<SearchResponse>(c, m);
    }
}
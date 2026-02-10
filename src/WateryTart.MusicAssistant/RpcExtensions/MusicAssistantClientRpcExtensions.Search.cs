using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    /// <summary>
    /// Searches for media items matching the query, with optional result limit and library-only filter.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of results to return (optional).</param>
    /// <param name="library_only">Whether to restrict the search to the library only (default: true).</param>
    /// <returns>A <see cref="Search"/> result object, or null if the request fails.</returns>
    public static async Task<Search?> SearchAsync(this MusicAssistantClientRpc c, string query, int? limit = null, bool library_only = true)
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

        var y = await c.Send<Search>(m);
        return y;
    }
}
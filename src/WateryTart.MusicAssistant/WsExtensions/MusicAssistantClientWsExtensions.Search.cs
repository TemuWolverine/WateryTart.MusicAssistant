using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
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
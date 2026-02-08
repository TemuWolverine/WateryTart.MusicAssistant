using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WebSocketExtensions
{
    public static partial class WebsocketClientExtensions
    {
        public static async Task<SearchResponse> SearchAsync(this IWsClient c, string query, int? limit = null, bool library_only = true)
        {
            var args = new Dictionary<string, object>()
            {
                { "search_query", query },
                { "library_only", library_only }
            };

            if (limit != null)
                args.Add("limit", limit);

            var m = new Message(Commands.Search)
            {
                args = args
            };
            
            return await SendAsync<SearchResponse>(c, m);
        }
    }
}

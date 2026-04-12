using WateryTart.MusicAssistant.Generators.Attributes;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    [ToRpc]
    public static async Task<GenresResponse> GetGenresLibraryItemsAsync(this MusicAssistantClientWs c, int? limit = null, int? offset = null, string? search = null, OrderBy orderby = OrderBy.Unknown, bool favourite = false)
    {
        var m = new Message(Commands.GenresLibraryItems)
        {
            Args = new Dictionary<string, object>()
                {
                    { "favorite_only", favourite },
                }
        };

        if (limit.HasValue)
        {
            m.Args["limit"] = limit.Value.ToString();
        }
        if (offset.HasValue)
        {
            m.Args["offset"] = offset.Value.ToString();
        }
        if (!string.IsNullOrEmpty(search))
        {
            m.Args["search"] = search;
        }
        if (orderby != OrderBy.Unknown)
        {
            m.Args["order_by"] = orderby;
        }
        return await SendAsync<GenresResponse>(c, m);
    }

    public static async Task<GenreResponse> GetGenreAsync(this MusicAssistantClientWs c, string genreId, string providerInstanceIdOrDomain)
    {
        return await SendAsync<GenreResponse>(c, ClientHelpers.IdAndProvider(Commands.GenresGet, genreId, providerInstanceIdOrDomain));
    }
}
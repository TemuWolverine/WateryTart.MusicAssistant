using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;
using WateryTart.MusicAssistant.Responses;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{
    public static async Task<ProviderManifestResponse> GetProvidersManifestsAsync(this MusicAssistantClientWs c)
    {
        return await SendAsync<ProviderManifestResponse>(c, ClientHelpers.JustCommand(Commands.ProvidersManifests));
    }
}
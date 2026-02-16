using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.RpcExtensions;

public static partial class MusicAssistantClientRpcExtensions
{
    public static async Task<List<ProviderManifest>?> GetProvidersManifestsAsync(this MusicAssistantClientRpc c)
    {
        return await c.Send<List<ProviderManifest>?>(ClientHelpers.JustCommand(Commands.ProvidersManifests));
    }

}
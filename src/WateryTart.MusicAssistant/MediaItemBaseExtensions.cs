namespace WateryTart.MusicAssistant.Models;

public static partial class MediaItemBaseExtensions
{
    public static string? GetProviderInstance(this MediaItemBase i)
    {
        string? provider = string.Empty;
        if (!string.IsNullOrEmpty(i.Provider))
            provider = i.Provider;
        else if (i.ProviderMappings != null)
            provider = i.ProviderMappings.FirstOrDefault()?.ProviderInstance;

        return provider;
    }

}

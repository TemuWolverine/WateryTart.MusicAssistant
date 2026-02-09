using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class Provider
{
    [JsonPropertyName("provider_id")]
    public string ProviderId { get; set; }

    [JsonPropertyName("provider_type")]
    public string ProviderType { get; set; }

    [JsonPropertyName("requires_redirect")]
    public bool RequiresRedirect { get; set; }
}
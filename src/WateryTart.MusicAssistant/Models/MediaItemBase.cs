using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Models;

[ObservableObject]
public abstract partial class MediaItemBase
{
    [JsonPropertyName("item_id")]
    public string? ItemId { get; set; }

    public string? Provider { get; set; }
    [ObservableProperty] 
    public partial string Name { get; set; }
    [JsonPropertyName("version")] public string? Version { get; set; }

    [JsonPropertyName("sort_name")]
    public string? SortName { get; set; }

    public string? Uri { get; set; }

    [JsonPropertyName("external_ids")]
    public List<List<string>>? ExternalIds { get; set; }

    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; set; }

    [JsonPropertyName("translation_key")]
    public object? TranslationKey { get; set; }

    [JsonPropertyName("media_type")]
    public MediaType MediaType { get; set; }

    [JsonPropertyName("provider_mappings")]
    public List<ProviderMapping>? ProviderMappings { get; set; }
    [JsonPropertyName("metadata")] 
    public Metadata? Metadata { get; set; }
    [JsonPropertyName("favorite")]
    public bool Favorite { get; set; }
    [JsonPropertyName("year")] public int? Year { get; set; }
    [JsonPropertyName("image")] public Image? Image { get; set; }
}
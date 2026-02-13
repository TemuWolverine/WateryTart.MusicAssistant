using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Models;


public abstract class MediaItemBase : INotifyPropertyChanged
{
    private bool favorite;

    [JsonPropertyName("item_id")]
    public string? ItemId { get; set; }

    public string? Provider { get; set; }
    public string? Name { get; set; }
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
    [JsonPropertyName("metadata")] public Metadata? Metadata { get; set; }
    [JsonPropertyName("favorite")]
    public bool Favorite
    {
        get => favorite; 
        set
        {
            favorite = value; 
            NotifyPropertyChanged();
        }
    }
    [JsonPropertyName("year")] public int? Year { get; set; }
    [JsonPropertyName("image")] public Image? Image { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
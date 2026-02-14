using WateryTart.MusicAssistant.Generators.Attributes;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

[NotifyPropertyChanged]
public partial class QueuedItem

{
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    [JsonPropertyName("image")]
    public Image? Image { get; set; }

    [NotifyingProperty]
    [JsonPropertyName("media_item")]
    public partial MediaItem? MediaItem { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("queue_id")]
    public string? QueueId { get; set; }

    [JsonPropertyName("queue_item_id")]
    public string? QueueItemId { get; set; }

    [NotifyingProperty]
    [JsonPropertyName("sort_index")]
    public partial int SortIndex { get; set; }

    [JsonPropertyName("StreamDetails")]
    public Streamdetails? StreamDetails { get; set; }
}
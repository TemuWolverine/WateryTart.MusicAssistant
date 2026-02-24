using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Generators.Attributes;

namespace WateryTart.MusicAssistant.Models;

[NotifyPropertyChanged]
public partial class PlayerQueue
{

    [JsonPropertyName("queue_id")]
    public string? QueueId { get; set; }
    
    [JsonPropertyName("active")]
    public bool Active { get; set; }
    
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    
    [JsonPropertyName("available")]
    [NotifyingProperty]
    public partial bool Available { get; set; }
    
    [JsonPropertyName("items")]
    public Int64 Items { get; set; }
    
    [JsonPropertyName("shuffle_enabled")]
    [NotifyingProperty]
    public partial bool ShuffleEnabled { get; set; }
    
    [JsonPropertyName("repeat_mode")]
    [NotifyingProperty]
    public partial string? RepeatMode { get; set; }
    
    [JsonPropertyName("dont_stop_the_music_enabled")]
    [NotifyingProperty]
    public partial bool DontStopTheMusicEnabled { get; set; }

    [JsonPropertyName("current_index")]
    [NotifyingProperty]
    public partial Int64? CurrentIndex { get; set; }
    
    [JsonPropertyName("index_in_buffer")]
    public Int64? IndexInBuffer { get; set; }
    
    [JsonPropertyName("elapsed_time")]
    public double? ElapsedTime { get; set; }
    
    [JsonPropertyName("elapsed_time_last_updated")]
    public double? ElapsedTimeLastUpdated { get; set; }

    [JsonPropertyName("state")]
    [NotifyingProperty]
    public partial string? State { get; set; }

    [JsonPropertyName("current_item")]
    [NotifyingProperty]
    public partial QueuedItem? CurrentItem { get; set; }
}
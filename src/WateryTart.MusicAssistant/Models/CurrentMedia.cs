using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Models;

[ObservableObject]
public partial class CurrentMedia
{
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }
    
    [JsonPropertyName("media_type")]
    public MediaType MediaType { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("artist")]
    public string? Artist { get; set; }
    
    [JsonPropertyName("album")]
    public string? Album { get; set; }
    
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
    
    [JsonPropertyName("duration")]
    public double? Duration { get; set; }
    
    [JsonPropertyName("queue_id")]
    public string? QueueId { get; set; }
    
    [JsonPropertyName("queue_item_id")]
    public string? QueueItemId { get; set; }

    [JsonPropertyName("elapsed_time")]
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Progress))]
    public partial double? ElapsedTime { get; set; }

    [JsonPropertyName("progress")]
    public double Progress
    {
        get
        {
            if (Duration == null || Duration == 0 || ElapsedTime == null)
                return 0;
            
            return ((double)ElapsedTime / (double)Duration) * 100;
        }
    }

    [JsonPropertyName("elapsed_time_last_updated")]
    public double? elapsed_time_last_updated { get; set; }
}
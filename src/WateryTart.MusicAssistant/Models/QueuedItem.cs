using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class QueuedItem : INotifyPropertyChanged
{
    [JsonPropertyName("queue_id")]
    public string? QueueId { get; set; }
    
    [JsonPropertyName("queue_item_id")]
    public string? QueueItemId { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }
    
    
    private int sortIndex;
    [JsonPropertyName("sort_index")]
    public int SortIndex
    {
        get => sortIndex;
        set
        {
            if (sortIndex != value)
            {
                sortIndex = value;
                OnPropertyChanged(nameof(SortIndex));
            }
        }
    }


    
    private MediaItem? mediaItem;
    [JsonPropertyName("media_item")]
    public MediaItem? MediaItem
    {
        get => mediaItem;
        set
        {
            if (mediaItem != value)
            {
                mediaItem = value;
                OnPropertyChanged(nameof(MediaItem));
            }
        }
    }
    [JsonPropertyName("image")]
    public Image? Image { get; set; }

    [JsonPropertyName("StreamDetails")]
    public Streamdetails? StreamDetails { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}




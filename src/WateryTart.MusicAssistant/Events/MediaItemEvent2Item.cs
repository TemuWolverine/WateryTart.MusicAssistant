using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Events;

public class MediaItemEvent2Item : MediaItemEventItem
{

    [JsonPropertyName("artists")] 
    public new List<string>? Artists { get; set; }

    [JsonPropertyName("album")] public new string? Album { get; set; }
}
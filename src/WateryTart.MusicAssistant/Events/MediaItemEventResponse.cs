using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.Events;

public class MediaItemEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new MediaItemEventItem? data { get; set; }
}

public class MediaItemEvent2Response : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new MediaItemEvent2Item? data { get; set; }
}


public class MediaItemEvent2Item : MediaItemEventItem
{

    [JsonPropertyName("artists")] 
    public new List<string>? Artists { get; set; }

    [JsonPropertyName("album")] public new string? Album { get; set; }
}
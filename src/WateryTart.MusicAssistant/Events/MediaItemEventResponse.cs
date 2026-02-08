using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Events;

public class MediaItemEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new MediaItemEventItem? data { get; set; }
}
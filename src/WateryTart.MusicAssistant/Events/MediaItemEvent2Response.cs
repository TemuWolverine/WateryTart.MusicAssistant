using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Events;

public class MediaItemEvent2Response : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new MediaItemEvent2Item? data { get; set; }
}

using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Events;

public class PlayerQueueTimeUpdatedEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new int data { get; set; }
}
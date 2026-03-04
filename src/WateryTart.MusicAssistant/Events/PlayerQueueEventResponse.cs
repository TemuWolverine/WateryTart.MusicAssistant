using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.Events;

public class PlayerQueueEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new PlayerQueue? Data { get; set; }
}
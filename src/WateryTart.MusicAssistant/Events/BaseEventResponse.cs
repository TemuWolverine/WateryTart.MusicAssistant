using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Events;

public class BaseEventResponse
{
    [JsonPropertyName("event")] public EventType EventName { get; set; }
    
    [JsonPropertyName("object_id")] public string? ObjectId { get; set; }
    
    [JsonPropertyName("data")] public object? Data { get; set; }
}
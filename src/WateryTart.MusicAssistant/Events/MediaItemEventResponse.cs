using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.Events;

public class MediaItemEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new MediaItemEventItem? data { get; set; }
}
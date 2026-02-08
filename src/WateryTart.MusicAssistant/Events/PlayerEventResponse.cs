using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.Events;

public class PlayerEventResponse : BaseEventResponse
{
    [JsonPropertyName("data")]
    public new Player? data { get; set; }
}
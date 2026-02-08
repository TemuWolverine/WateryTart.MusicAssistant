using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class Artist : MediaItemBase
{
    [JsonPropertyName("available")]
    public bool Available { get; set; }
}

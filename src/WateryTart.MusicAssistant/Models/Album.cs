using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Models;

public class Album : MediaItemBase
{
    [JsonPropertyName("artists")]  public List<Artist>? Artists { get; set; }
    [JsonPropertyName("album_type")] public AlbumType AlbumType { get; set; }
}

[JsonConverter(typeof(FallbackEnumConverter<AlbumType>))]
public enum AlbumType
{
    Unknown,
    Album,
    Single,
    Live,
    Soundtrack,
    Compilation,
    Ep,
}
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<RepeatMode>))]
public enum RepeatMode
{
    Unknown,
    Off,
    One,
    All, 
}


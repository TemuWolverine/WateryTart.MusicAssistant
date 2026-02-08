using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<PlayMode>))]
public enum PlayMode
{
    Unknown,
    Play,
    Replace,
    Next,
    ReplaceNext,
    Add
}
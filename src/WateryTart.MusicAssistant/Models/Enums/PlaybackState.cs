using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<PlaybackState>))]
public enum PlaybackState
{
    Unknown,
    Idle,
    Playing,
    Paused,
    Stopped
}
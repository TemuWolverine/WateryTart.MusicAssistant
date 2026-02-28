using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<OrderBy>))]
public enum OrderBy
{
    Unknown,
    name,
    name_desc,
    sort_name,
    sort_name_desc,
    timestamp_added,
    timestamp_added_desc,
    last_played,
    last_played_desc,
    play_count,
    play_count_desc,
    duration,
    duration_desc
}
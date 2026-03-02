using System.ComponentModel;
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<OrderBy>))]
public enum OrderBy
{
    Unknown,
    [Description("Alphabetical (A-Z)")]
    name,

    [Description("Alphabetical (Z-A)")]
    name_desc,

    [Description("Sort Name (A-Z)")]
    sort_name,

    [Description("Sort Name (Z-A)")]
    sort_name_desc,

    [Description("Timestamp Added")]
    timestamp_added,
    [Description("Timestamp Added (Desc)")]
    timestamp_added_desc,

    [Description("Last Played")]
    last_played,
    [Description("Last Played (Desc)")]
    last_played_desc,

    [Description("Play Count")]
    play_count,

    [Description("Play Count (Desc)")]
    play_count_desc,

    [Description("Duration")]
    duration,

    [Description("Duration (Desc)")]
    duration_desc
}
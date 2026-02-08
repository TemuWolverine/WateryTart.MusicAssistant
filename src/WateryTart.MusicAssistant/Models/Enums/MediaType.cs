using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<MediaType>))]
public enum MediaType
{
    Unknown,
    Artist,
    Album,
    Track,
    Genre,
    Playlist,
    Radio,
    Podcast,
    PodcastEpisode,
    Audiobook,
    Folder
}
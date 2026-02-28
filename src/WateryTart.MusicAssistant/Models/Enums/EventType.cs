using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Converters;

namespace WateryTart.MusicAssistant.Models.Enums;

[JsonConverter(typeof(FallbackEnumConverter<EventType>))]
public enum EventType
{
    Unknown,
    PlayerAdded,
    PlayerUpdated,
    PlayerRemoved,
    PlayerConfigUpdated,
    QueueAdded,
    QueueUpdated,
    QueueItemsUpdated,
    QueueTimeUpdated,
    MediaItemAdded,
    MediaItemUpdated,
    MediaItemDeleted,
    MediaItemPlayed,
    ProvidersUpdated,
    SyncTasksUpdated,
    ApplicationShutdown
}
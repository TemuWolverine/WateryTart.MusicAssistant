using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Responses;

public abstract class ResponseBase<T>
{
    [JsonPropertyName("message_id")]
    public string? MessageId { get; set; }

    [JsonPropertyName("partial")]
    public bool Partial { get; set; }

    [JsonPropertyName("result")]
    public T? Result { get; set; }
}
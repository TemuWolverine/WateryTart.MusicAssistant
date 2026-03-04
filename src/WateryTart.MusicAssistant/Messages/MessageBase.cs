using System.Text.Json;
using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Messages;

public abstract class MessageBase(string command)
{
    [JsonPropertyName("args")] public Dictionary<string, object>? Args { get; set; }
    [JsonPropertyName("message_id")] public string MessageId { get; set; } = Guid.NewGuid().ToString();
    [JsonPropertyName("command")] public string Command { get; set; } = command;

    public string ToJson()
    {
        return this switch
        {
            Message msg => JsonSerializer.Serialize(msg, MusicAssistantJsonContext.Default.Message),
            Auth auth => JsonSerializer.Serialize(auth, MusicAssistantJsonContext.Default.Auth),
            _ => JsonSerializer.Serialize(this, MusicAssistantJsonContext.Default.GetTypeInfo(this.GetType())!)
        };
    }
}

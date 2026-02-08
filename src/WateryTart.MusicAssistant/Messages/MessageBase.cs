using System.Text.Json;

namespace WateryTart.MusicAssistant.Messages;

public abstract class MessageBase(string command)
{
    public Dictionary<string, object>? args { get; set; }
    public string message_id { get; set; } = Guid.NewGuid().ToString();
    public string command { get; set; } = command;

    public string ToJson()
    {
        return this switch
        {
            Message msg => JsonSerializer.Serialize(msg, MediaAssistantJsonContext.Default.Message),
            Auth auth => JsonSerializer.Serialize(auth, MediaAssistantJsonContext.Default.Auth),
            _ => JsonSerializer.Serialize(this, MediaAssistantJsonContext.Default.GetTypeInfo(this.GetType())!)
        };
    }
}

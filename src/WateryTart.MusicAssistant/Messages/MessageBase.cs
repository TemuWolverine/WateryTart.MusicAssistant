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
            Message msg => JsonSerializer.Serialize(msg, Service.MassClient.MassClientJsonContext.Default.Message),
            Auth auth => JsonSerializer.Serialize(auth, Service.MassClient.MassClientJsonContext.Default.Auth),
            _ => JsonSerializer.Serialize(this, Service.MassClient.MassClientJsonContext.Default.GetTypeInfo(this.GetType())!)
        };
    }
}

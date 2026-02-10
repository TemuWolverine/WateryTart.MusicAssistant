using WateryTart.MusicAssistant.Messages;

namespace WateryTart.MusicAssistant;

public static class ClientHelpers
{
    internal static MessageBase JustCommand(string command)
    {
        return new Message(command);
    }

    internal static MessageBase JustId(string command, string id, string idLabel = "item_id")
    {
        var m = new Message(command)
        {
            args = new Dictionary<string, object>
            {
                { idLabel, id },
            }
        };

        return m;
    }

    internal static MessageBase IdAndProvider(string command, string id, string provider)
    {
        var m = new Message(command)
        {
            args = new Dictionary<string, object>
            {
                { "item_id", id },
                { "provider_instance_id_or_domain", provider }
            }
        };

        return m;
    }
}
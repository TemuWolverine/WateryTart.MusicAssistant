using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using WateryTart.MusicAssistant.Messages;

namespace WateryTart.MusicAssistant.WebSocketExtensions;

public static partial class WebsocketClientExtensions
{
    internal static Task<T> SendAsync<T>(IWsClient client, MessageBase message)
    {
        var tcs = new TaskCompletionSource<T>();
        try
        {
            client.Send<T>(message, (response) =>
            {
                try
                {
                    var typeInfo = MediaAssistantJsonContext.Default.GetTypeInfo(typeof(T)) as JsonTypeInfo<T>;
                    if (typeInfo == null) 
                        return;

                    var result = JsonSerializer.Deserialize(response, typeInfo);
                    if (result != null)
                        tcs.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                    tcs.TrySetException(ex);
                }
            });
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }
        return tcs.Task;
    }

    public static MessageBase JustCommand(string command)
    {
        return new Message(command);
    }

    public static MessageBase JustId(string command, string id, string idLabel = "item_id")
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

    public static MessageBase IdAndProvider(string command, string id, string provider)
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

    private static Action<string> Deserialise<T>(Action<T> responseHandler)
    {
        Action<string> d = (r) =>
        {
            var y = JsonSerializer.Deserialize<T>(r, WsClient.SerializerOptions);
            if (y != null)
                responseHandler(y);
        };

        return d;
    }
}
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using WateryTart.MusicAssistant.Messages;

namespace WateryTart.MusicAssistant.WsExtensions;

public static partial class MusicAssistantClientWsExtensions
{

    private static Action<string> Deserialise<T>(Action<T> responseHandler)
    {
        Action<string> d = (r) =>
        {
            var y = JsonSerializer.Deserialize<T>(r, MusicAssistantClient.SerializerOptions);
            if (y != null)
                responseHandler(y);
        };

        return d;
    }
    internal static Task<T> SendAsync<T>(MusicAssistantClientWs client, MessageBase message)
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
}
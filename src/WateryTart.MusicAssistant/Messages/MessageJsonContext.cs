using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Messages;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Message))]
[JsonSerializable(typeof(Auth))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
internal partial class MessageJsonContext : JsonSerializerContext
{
}
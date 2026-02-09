using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models.Auth;

public class AuthToken
{
    [JsonPropertyName("token_id")]
    public string TokenId { get; set; }

    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("token_hash")]
    public string TokenHash { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("last_used_at")]
    public DateTime? LastUsedAt { get; set; }

    [JsonPropertyName("is_long_lived")]
    public bool IsLongLived { get; set; }
}
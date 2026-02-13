using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models.Auth
{
    public class User
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("avatar_url")]
        public object AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("preferences")]
        public UserPreferences? Preferences { get; set; }

        [JsonPropertyName("provider_filter")]
        public List<object> ProviderFilter { get; set; } = new List<object>();

        [JsonPropertyName("player_filter")]
        public List<object> PlayerFilter { get; set; } = new List<object>();
    }



}
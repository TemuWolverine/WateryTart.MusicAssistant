using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models.Auth
{
    public class User
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("avatar_url")]
        public object AvatarUrl { get; set; }

        [JsonPropertyName("preferences")]
        public UserPreferences Preferences { get; set; }

        [JsonPropertyName("provider_filter")]
        public List<object> ProviderFilter { get; set; }

        [JsonPropertyName("player_filter")]
        public List<object> PlayerFilter { get; set; }
    }



}
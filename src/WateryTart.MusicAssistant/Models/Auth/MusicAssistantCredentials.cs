using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models.Auth;

public class MusicAssistantCredentials : IMusicAssistantCredentials
{
    [JsonPropertyName("Token")] 
    public string? Token { get; set; }

    [JsonPropertyName("BaseUrl")] 
    public string? BaseUrl { get; set; }
    
    [JsonPropertyName("Username")] 
    public string? Username { get; set; } = string.Empty;
}
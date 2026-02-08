namespace WateryTart.MusicAssistant.Models.Auth;

public interface IMusicAssistantCredentials
{
    public string? Token { get; set; }
    public string? BaseUrl { get; set; }
    public string? Username { get; set; }
}
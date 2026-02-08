namespace WateryTart.MusicAssistant.Models.Auth;

public class LoginResults
{
    public MusicAssistantCredentials? Credentials { get; set; }
    public bool Success { get; set; }
    public string Error { get; set; } = string.Empty;
}
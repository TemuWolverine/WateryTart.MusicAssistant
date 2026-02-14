using System.Text.Json;
using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant;

public class MusicAssistantClient
{
    /// <summary>
    /// Shared JSON serializer options for all MassClient operations. AOT-compatible with snake_case naming.
    /// </summary>
    internal static readonly JsonSerializerOptions SerializerOptions = new()
    {
        TypeInfoResolver = MusicAssistantJsonContext.Default,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    internal string? Baseurl;
    private MusicAssistantClientRpc? _rpcConnection;
    private string? _token;
    private MusicAssistantClientWs? _wsConnection;

    public MusicAssistantClient()
    {

    }
    public MusicAssistantClient(string baseurl)
    {
        Baseurl = GetJustHost(baseurl);
    }

    public string? GetToken() => _token;

    public void SetToken(string token)
    {
        _token = token;

        //Only set the token on the connections if they already exist
        _rpcConnection?.SetToken(token);
        _wsConnection?.SetToken(token);
    }

    private string GetJustHost(string urlOrHost)
    {
        if (Uri.TryCreate(urlOrHost, UriKind.Absolute, out var uri))
        {
            // If the port is not specified, use the default port for the scheme
            int port = uri.IsDefaultPort ? (uri.Scheme == Uri.UriSchemeHttps ? 443 : 80) : uri.Port;
            return $"{uri.Host}:{port}";
        }
        return urlOrHost;
    }
    public void SetBaseUrl(string baseurl)
    {
        Baseurl = GetJustHost(baseurl);
        _rpcConnection?.SetUrl(GetRpcUrl());
        _wsConnection?.SetUrl(GetWebSocketUrl());
    }

    public MusicAssistantClientRpc WithRpc()
    {
        return _rpcConnection ??= new MusicAssistantClientRpc(this, GetRpcUrl());
    }

    public MusicAssistantClientWs WithWs()
    {
        return _wsConnection ??= new MusicAssistantClientWs(this, GetWebSocketUrl());
    }

    internal string GetRpcUrl()
    {
        // WebSocket is on the same port as HTTP, just different protocol
        return $"http://{Baseurl}/api";
    }

    internal string GetWebSocketUrl()
    {
        // WebSocket is on the same port as HTTP, just different protocol
        return $"ws://{Baseurl}/ws";
    }
}
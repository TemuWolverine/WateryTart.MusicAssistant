using System.Text.Json;
using WateryTart.MusicAssistant.Messages;

namespace WateryTart.MusicAssistant;

public class MusicAssistantClientRpc
{
    internal readonly MusicAssistantClient parent;
    private string _baseurl;
    private HttpClient client;
    private string? _token;
    internal void SetToken(string token)
    {
        _token = token;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
    }

    public MusicAssistantClientRpc(MusicAssistantClient parentClient, string baseurl)
    {
        parent = parentClient;
        _baseurl = baseurl;
        client = new HttpClient();
    }

    internal void SetUrl(string url)
    {
        _baseurl = url;
    }

    /// <summary>
    /// Sends an RPC message to the Music Assistant API and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The expected response type.</typeparam>
    /// <param name="message">The message to send.</param>
    /// <returns>The deserialized response of type <typeparamref name="T"/>, or null if deserialization fails.</returns>
    public async Task<T?> Send<T>(MessageBase message)
    {
        //Build request
        var request = new HttpRequestMessage(HttpMethod.Post, _baseurl);
        request.Content = new StringContent(message.ToJson());

        //Send request
        var response = await client.SendAsync(request);

        //Convert response to T 
        var responseBody = await response.Content.ReadAsStringAsync();
        T responseProper = JsonSerializer.Deserialize<T>(responseBody);

        return responseProper;
    }

    public async Task Send(MessageBase message)
    {
        //Build request
        var request = new HttpRequestMessage(HttpMethod.Post, _baseurl);
        request.Content = new StringContent(message.ToJson());

        //Send request
        var response = await client.SendAsync(request);

        //Convert response to T 
        var responseBody = await response.Content.ReadAsStringAsync();
    }
}
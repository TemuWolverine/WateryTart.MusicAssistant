using System.Net.Http.Headers;
using System.Text.Json;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;

namespace WateryTart.MusicAssistant
{
    public class RpcClient
    {
        private readonly IMusicAssistantCredentials _credentials;
        private readonly string _baseUrl;
        private HttpClient client;
        public RpcClient(IMusicAssistantCredentials credentials, string baseUrl = "http://10.0.1.20:8095/api")
        {
            _credentials = credentials;
            _baseUrl = baseUrl;
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentials.Token);


           // Send<List<Player>>(new Message(Commands.PlayersAll));
        }

        public async Task<T?> Send<T>(MessageBase message)
        {
            //Build request
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl);
            request.Content = new StringContent(message.ToJson());

            //Send request
            var response = await client.SendAsync(request);

            //Convert response to T 
            var responseBody = await response.Content.ReadAsStringAsync();
            T responseProper = JsonSerializer.Deserialize<T>(responseBody);

            return responseProper;
        }
    }
}

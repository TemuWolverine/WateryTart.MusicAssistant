using System.Buffers.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;

namespace WateryTart.MusicAssistant
{
    public class RpcClient
    {
        private readonly string _baseUrl;
        private HttpClient client;
        public RpcClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            client = new HttpClient();
            
        }

        public async Task<IMusicAssistantCredentials?> LoginAsync(string username, string password)
        {
            
            return new MusicAssistantCredentials()
            {
                BaseUrl = _baseUrl,
                Token = "1234"
            };
        }

        public void SetAuthToken(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

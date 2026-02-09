using System.Buffers.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.RPCExtensions;

namespace WateryTart.MusicAssistant
{
    /// <summary>
    /// Provides methods for communicating with the Music Assistant RPC API.
    /// Handles authentication, sending messages, and managing authorization tokens.
    /// </summary>
    public class RpcClient
    {
        private readonly string _baseUrl;
        private HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcClient"/> class with the specified base URL.
        /// </summary>
        /// <param name="baseUrl">The base URL of the Music Assistant API.</param>
        public RpcClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            client = new HttpClient();
        }

        /// <summary>
        /// Authenticates the user with the provided username and password.
        /// </summary>
        /// <param name="username">The username for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <returns>
        /// An <see cref="IMusicAssistantCredentials"/> instance containing the authentication token and user information,
        /// or null if authentication fails.
        /// </returns>
        public async Task<IMusicAssistantCredentials?> LoginAsync(string username, string password)
        {
            var result = await RpcClientExtensions.GetAuthToken(this, username, password);

            return new MusicAssistantCredentials
            {
                Username = result.User.Username,
                Token = result.AccessToken,
                BaseUrl = _baseUrl
            };
        }

        /// <summary>
        /// Sets the authorization token for subsequent requests.
        /// </summary>
        /// <param name="token">The bearer token to use for authorization.</param>
        public void SetAuthToken(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

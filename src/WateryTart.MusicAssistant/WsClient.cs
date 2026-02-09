using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using WateryTart.MusicAssistant.Events;
using WateryTart.MusicAssistant.Messages;
using WateryTart.MusicAssistant.Models.Auth;
using WateryTart.MusicAssistant.Models.Enums;
using WateryTart.MusicAssistant.Responses;
using WateryTart.MusicAssistant.WebSocketExtensions;
using Websocket.Client;

namespace WateryTart.MusicAssistant
{
    /// <summary>
    /// Provides a WebSocket client for communicating with the Music Assistant server.
    /// Handles authentication, message routing, event streaming, and connection management.
    /// </summary>
    public class WsClient : IWsClient
    {
        internal WebsocketClient? _client;
        /// <summary>
        /// Maps message IDs to response handlers for routing incoming messages.
        /// </summary>
        internal ConcurrentDictionary<string, Action<string>> _routing = new();
        private IMusicAssistantCredentials? creds;

        private CancellationTokenSource _connectionCts = new CancellationTokenSource();
        /// <summary>
        /// Subject for publishing event responses to observers.
        /// </summary>
        private readonly Subject<BaseEventResponse?> subject = new Subject<BaseEventResponse?>();
        private IDisposable? _reconnectionSubscription;
        private IDisposable? _messageSubscription;

        // Track if we're already attempting to connect
        private Task _currentConnectTask = Task.CompletedTask;
        private readonly object _connectLock = new object();

        private bool _isAuthenticated = false;
        private readonly Queue<string> _pendingMessages = new();
        private readonly object _authLock = new object();

        private readonly ILogger logger;
        /// <summary>
        /// Shared JSON serializer options for all MassClient operations. AOT-compatible with snake_case naming.
        /// </summary>
        internal static readonly JsonSerializerOptions SerializerOptions = new()
        {
            TypeInfoResolver = MediaAssistantJsonContext.Default,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="WsClient"/> class and configures logging.
        /// </summary>
        public WsClient()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = factory.CreateLogger("MassWsClient");
        }

        /// <summary>
        /// Authenticates with the Music Assistant server using the provided credentials and base URL.
        /// </summary>
        /// <param name="username">The username for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <param name="baseurl">The base URL of the Music Assistant server.</param>
        /// <returns>A <see cref="LoginResults"/> object indicating success or failure and containing credentials if successful.</returns>
        public async Task<LoginResults> Login(string username, string password, string baseurl)
        {
            MusicAssistantCredentials mc = new MusicAssistantCredentials();

            var factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options = { KeepAliveInterval = TimeSpan.FromSeconds(1) }
            });

            // Music Assistant uses port 8095 for HTTP and 8097 for WebSocket API
            var wsUrl = GetWebSocketUrl(baseurl);

            var tcs = new TaskCompletionSource<LoginResults>();
            using (_client = new WebsocketClient(new Uri(wsUrl), factory))
            {
                logger.LogInformation("Connecting to WebSocket: {WsUrl}", wsUrl);
                _client.MessageReceived.Subscribe(OnNext);
                await _client.Start();
                logger.LogInformation("WebSocket connected, sending auth request");

                this.GetAuthToken(username, password, (response) =>
                {
                    logger.LogInformation("Auth response received: success={Success}", response?.Result?.Success);
                    if (response?.Result == null)
                    {
                        tcs.TrySetResult(new LoginResults { Success = false, Error = "No response from server" });
                        return;
                    }
                    if (!response.Result.Success)
                    {
                        var r = new LoginResults
                        {
                            Success = false,
                            Error = response.Result.Error ?? "Authentication failed"

                        };
                        tcs.TrySetResult(r);
                        return;
                    }
                    var success = new LoginResults
                    {
                        Credentials = new MusicAssistantCredentials
                        {
                            Token = response.Result.AccessToken,
                            BaseUrl = baseurl
                        },
                        Success = true
                    };
                    tcs.TrySetResult(success);
                });

                // Add timeout to prevent hanging forever
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return new LoginResults { Success = false, Error = "Connection timed out" };
                }

                return await tcs.Task;
            }
        }

        /// <summary>
        /// Connects to the Music Assistant WebSocket server using the provided credentials.
        /// Handles reconnection and authentication.
        /// </summary>
        /// <param name="credentials">The credentials to use for authentication.</param>
        /// <returns>True if connection and authentication succeed; otherwise, false.</returns>
        public async Task<bool> Connect(IMusicAssistantCredentials credentials)
        {
            var x = new MediaAssistantJsonContext();
            logger.LogInformation("WS Connecting");

            lock (_connectLock)
            {
                if (!_currentConnectTask.IsCompleted)
                {
                    return false;
                }
            }

            creds = credentials;
            _isAuthenticated = false;

            _reconnectionSubscription?.Dispose();
            _messageSubscription?.Dispose();
            _client?.Dispose();
            _connectionCts = new CancellationTokenSource();

            var factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options = { KeepAliveInterval = TimeSpan.FromSeconds(1) }
            });

            if (credentials.BaseUrl == null)
            {
                logger.LogError("No credential base url found");
                return false;
            }

            var wsUrl = GetWebSocketUrl(credentials.BaseUrl);
            _client = new WebsocketClient(new Uri(wsUrl), factory);

            _reconnectionSubscription = _client.ReconnectionHappened.Subscribe(info =>
            {
                if (!_connectionCts.Token.IsCancellationRequested)
                {
                    SendLogin(credentials);
                }
            });

            _messageSubscription = _client.MessageReceived.Subscribe(OnNext);

            await _client.Start();
            SendLogin(credentials);

            var authTimeout = Task.Delay(TimeSpan.FromSeconds(10), _connectionCts.Token);
            var authCompleted = WaitForAuthenticationAsync();

            var completedTask = await Task.WhenAny(authCompleted, authTimeout);

            if (completedTask == authTimeout)
            {
                logger.LogWarning("Authentication timeout");
                return false;
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(Timeout.Infinite, _connectionCts.Token);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("Background connection task cancelled");
                }
            });

            return _isAuthenticated;
        }

        /// <summary>
        /// Converts a base URL (e.g., "192.168.1.63:8095") to the WebSocket URL.
        /// Music Assistant WebSocket is on the same port as HTTP, just different protocol.
        /// </summary>
        /// <param name="baseUrl">The base URL of the Music Assistant server.</param>
        /// <returns>The WebSocket URL as a string.</returns>
        private static string GetWebSocketUrl(string baseUrl)
        {
            // WebSocket is on the same port as HTTP, just different protocol
            return $"ws://{baseUrl}/ws";
        }

        /// <summary>
        /// Sends an authentication message to the server using the provided credentials.
        /// </summary>
        /// <param name="credentials">The credentials containing the authentication token.</param>
        private void SendLogin(IMusicAssistantCredentials credentials)
        {
            logger.LogInformation("Sending authentication...");
            var argsx = new Dictionary<string, object>() { { "token", credentials.Token } };
            var auth = new Auth()
            {
                message_id = "auth-" + Guid.NewGuid(),
                args = argsx
            };

            _routing.TryAdd(auth.message_id, (response) =>
            {
                logger.LogInformation("Auth response: {Response}", response);

                if (!response.Contains("error"))
                {
                    lock (_authLock)
                    {
                        _isAuthenticated = true;

                        // Send all pending messages
                        while (_pendingMessages.Count > 0)
                        {
                            var pending = _pendingMessages.Dequeue();
                            _client?.Send(pending);
                        }
                    }
                }
            });

            // Use JsonSerializer.Serialize with JsonTypeInfo to avoid RequiresUnreferencedCode warning
            var json = JsonSerializer.Serialize(auth, MediaAssistantJsonContext.Default.Auth);
            logger.LogInformation("Sending auth: {Json}", json);
            _client?.Send(json);
        }

        /// <summary>
        /// Sends a message to the server and registers a response handler for the message.
        /// Optionally ignores connection state.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="message">The message to send.</param>
        /// <param name="responseHandler">The handler to invoke when a response is received.</param>
        /// <param name="ignoreConnection">If true, sends the message regardless of connection state.</param>
        public void Send<T>(MessageBase message, Action<string> responseHandler, bool ignoreConnection = false)
        {

            var json = message.ToJson();
            _routing.TryAdd(message.message_id, responseHandler);  // Changed from Add

            if (!ignoreConnection && (_client == null || !_client.IsRunning))
            {
                lock (_connectLock)
                {
                    if (_currentConnectTask.IsCompleted)
                    {
                        _currentConnectTask = ConnectSafely();
                    }
                }
            }

            _client?.Send(json);
        }

        private async Task ConnectSafely()
        {
            logger.LogDebug("WS Connecting");
            try
            {
                if (creds != null) 
                    await Connect(creds);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Connect error");
            }
        }

        /// <summary>
        /// Indicates whether the client is currently connected to the server.
        /// </summary>
        public bool IsConnected => (_client != null && _client.IsRunning);

        /// <summary>
        /// Handles incoming WebSocket messages, routes responses, and publishes events.
        /// </summary>
        /// <param name="response">The received WebSocket response message.</param>
        private void OnNext(ResponseMessage response)
        {
            if (string.IsNullOrEmpty(response.Text))
                return;

            if (response.Text.Contains("\"server_id\"") && !response.Text.Contains("\"message_id\""))
            {
                return;
            }

            // Use JsonSerializer.Deserialize with JsonTypeInfo to avoid RequiresUnreferencedCode warning
            TempResponse? y = JsonSerializer.Deserialize(response.Text, MediaAssistantJsonContext.Default.TempResponse);

            // Use TryRemove instead of ContainsKey + indexer
            if (y?.message_id != null && _routing.TryRemove(y.message_id, out var handler))
            {
                handler?.Invoke(response.Text);
                return;
            }

            try
            {
                var e = JsonSerializer.Deserialize<BaseEventResponse>(response.Text, SerializerOptions);
                if (e == null)
                    return;

                switch (e.EventName)
                {
                    case EventType.MediaItemPlayed:
                        subject.OnNext(JsonSerializer.Deserialize(response.Text, MediaAssistantJsonContext.Default.MediaItemEventResponse));
                        break;
                    case EventType.PlayerAdded:
                    case EventType.PlayerUpdated:
                    case EventType.PlayerRemoved:
                    case EventType.PlayerConfigUpdated:
                        try
                        {
                            var x = JsonSerializer.Deserialize(response.Text, MediaAssistantJsonContext.Default.PlayerEventResponse);
                            subject.OnNext(x);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Error in player config update");
                        }

                        break;

                    case EventType.QueueAdded:
                    case EventType.QueueUpdated:
                        subject.OnNext(JsonSerializer.Deserialize(response.Text, MediaAssistantJsonContext.Default.PlayerQueueEventResponse));
                        break;
                    case EventType.QueueItemsUpdated:
                        break;
                    case EventType.QueueTimeUpdated:
                        subject.OnNext(JsonSerializer.Deserialize(response.Text, MediaAssistantJsonContext.Default.PlayerQueueTimeUpdatedEventResponse));
                        break;

                    default:
                        subject.OnNext(e);
                        break;
                }
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "JSON Deserialization Error");
                logger.LogDebug("Path: {Path}", ex.Path);
            }
        }

        /// <summary>
        /// Gets an observable stream of event responses from the server.
        /// </summary>
        public IObservable<BaseEventResponse> Events => subject;

        /// <summary>
        /// Disconnects from the server and disposes of all resources.
        /// </summary>
        public async Task DisconnectAsync()
        {
            try
            {
                // Cancel the connection immediately
                _connectionCts?.Cancel();
            }
            catch { }

            try
            {
                if (_client != null)
                {
                    // Disable reconnection first
                    _client.IsReconnectionEnabled = false;

                    if (_client.IsRunning)
                    {
                        try
                        {
                            await _client.Stop(WebSocketCloseStatus.NormalClosure, "Shutdown");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Error calling Stop");
                        }

                        // Force abort if still running
                        if (_client.IsRunning)
                        {
                            logger.LogDebug("WebSocket still running, attempting abort...");
                            _client.NativeClient?.Abort();
                        }

                        await Task.Delay(500);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error stopping WebSocket");
            }

            try
            {
                _reconnectionSubscription?.Dispose();
                _messageSubscription?.Dispose();
                _client?.Dispose();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error disposing WebSocket");
            }

            try
            {
                _connectionCts?.Dispose();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error disposing CTS");
            }

            try
            {
                subject?.Dispose();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error disposing subject");
            }
        }

        /// <summary>
        /// Waits asynchronously until authentication is complete or the connection is cancelled.
        /// </summary>
        private async Task WaitForAuthenticationAsync()
        {
            while (!_isAuthenticated && !_connectionCts.Token.IsCancellationRequested)
            {
                await Task.Delay(100);
            }
        }
    }
}
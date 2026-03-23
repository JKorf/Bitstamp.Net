using System.Collections.Concurrent;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc />
    public class BitstampUserClientProvider : IBitstampUserClientProvider
    {
        private ConcurrentDictionary<string, IBitstampRestClient> _restClients = new ConcurrentDictionary<string, IBitstampRestClient>();
        private ConcurrentDictionary<string, IBitstampSocketClient> _socketClients = new ConcurrentDictionary<string, IBitstampSocketClient>();

        private readonly IOptions<BitstampRestOptions> _restOptions;
        private readonly IOptions<BitstampSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => BitstampExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public BitstampUserClientProvider(Action<BitstampOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<BitstampRestOptions> restOptions,
            IOptions<BitstampSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.Timeout = restOptions.Value.RequestTimeout;
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, BitstampCredentials credentials, BitstampEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IBitstampRestClient GetRestClient(string userIdentifier, BitstampCredentials? credentials = null, BitstampEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IBitstampSocketClient GetSocketClient(string userIdentifier, BitstampCredentials? credentials = null, BitstampEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IBitstampRestClient CreateRestClient(string userIdentifier, BitstampCredentials? credentials, BitstampEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new BitstampRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IBitstampSocketClient CreateSocketClient(string userIdentifier, BitstampCredentials? credentials, BitstampEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new BitstampSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<BitstampRestOptions> SetRestEnvironment(BitstampEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new BitstampRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<BitstampSocketOptions> SetSocketEnvironment(BitstampEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new BitstampSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}

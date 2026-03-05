using Bitstamp.Net.Clients.ExchangeApi;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Options;
using Bitstamp.Net.Objects.Sockets;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc cref="IBitstampSocketClient" />
    public class BitstampSocketClient : BaseSocketClient, IBitstampSocketClient
    {
        private readonly IBitstampRestClient _restClient;
        private readonly BitstampSocketKeyGenerator _keyGenerator;

        /// <inheritdoc />
        public IBitstampSocketClientExchangeApi ExchangeApi { get; }

        #region ctor

        /// <summary>
        /// Create a new instance of the BitstampSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitstampSocketClient(Action<BitstampSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of the BitstampSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public BitstampSocketClient(IOptions<BitstampSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Bitstamp")
        {
            Initialize(options.Value);

            _restClient = new BitstampRestClient(null, loggerFactory, Options.Create(ApplyOptionsDelegate<BitstampRestOptions>(o =>
            {
                o.Environment = options.Value.Environment;
                o.ApiCredentials = options.Value.ApiCredentials;
            })));
            _keyGenerator = new BitstampSocketKeyGenerator(_restClient);

            ExchangeApi = AddApiClient(new BitstampSocketClientExchangeApi(_logger, options.Value, _keyGenerator));
        }
        #endregion

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            _restClient.SetApiCredentials(credentials);
            ExchangeApi.SetApiCredentials(credentials);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitstampSocketOptions> optionsDelegate)
        {
            BitstampSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            ExchangeApi.SetOptions(options);
        }

    }
}

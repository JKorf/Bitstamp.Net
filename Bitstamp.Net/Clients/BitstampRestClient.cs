using Bitstamp.Net.Clients.ExchangeApi;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc cref="IBitstampRestClient" />
    public class BitstampRestClient : BaseRestClient<BitstampEnvironment, BitstampCredentials>, IBitstampRestClient
    {
        /// <inheritdoc />
        public IBitstampRestClientExchangeApi ExchangeApi { get; }

        #region ctor
        /// <summary>
        /// Create a new instance of the BitstampRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitstampRestClient(Action<BitstampRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the BitstampRestClient
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BitstampRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BitstampRestOptions> options)
            : base(loggerFactory, "Bitstamp")
        {
            Initialize(options.Value);

            ExchangeApi = AddApiClient(new BitstampRestClientExchangeApi(_logger, httpClient, options.Value));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitstampRestOptions> optionsDelegate)
        {
            BitstampRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
        #endregion
    }
}

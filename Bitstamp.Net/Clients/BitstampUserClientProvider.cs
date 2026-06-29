using System.Collections.Concurrent;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc />
    public class BitstampUserClientProvider : UserClientProvider<
        IBitstampRestClient,
        IBitstampSocketClient,
        BitstampRestOptions,
        BitstampSocketOptions,
        BitstampCredentials,
        BitstampEnvironment
        >, IBitstampUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => BitstampExchange.ExchangeName;

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
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IBitstampRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<BitstampRestOptions> options)
            => new BitstampRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IBitstampSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<BitstampSocketOptions> options)
            => new BitstampSocketClient(options, loggerFactory);
    }
}

using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Exchange API
    /// </summary>
    public interface IBitstampRestClientExchangeApi : IRestApiClient<BitstampCredentials>
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBitstampRestClientExchangeApiAccount"/>
        IBitstampRestClientExchangeApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitstampRestClientExchangeApiExchangeData"/>
        IBitstampRestClientExchangeApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBitstampRestClientExchangeApiTrading"/>
        IBitstampRestClientExchangeApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        IBitstampRestClientExchangeApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IBitstampRestClientExchangeSharedApi SharedApi { get; }
    }
}

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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitstampRestClientExchangeApiShared SharedClient { get; }
    }
}

using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitstamp API. 
    /// </summary>
    public interface IBitstampRestClient : IRestClient<BitstampCredentials>
    {
        /// <summary>
        /// Exchange API endpoints
        /// </summary>
        /// <see cref="IBitstampRestClientExchangeApi"/>
        IBitstampRestClientExchangeApi ExchangeApi { get; }
    }
}
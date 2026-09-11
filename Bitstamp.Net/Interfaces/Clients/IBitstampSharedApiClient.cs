using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Bitstamp
    /// </summary>
    public interface IBitstampSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// REST shared API implementations
        /// </summary>
        IBitstampRestClientExchangeSharedApi Rest { get; }

        /// <summary>
        /// WebSocket shared API implementations
        /// </summary>
        IBitstampSocketClientExchangeSharedApi Socket { get; }
    }
}

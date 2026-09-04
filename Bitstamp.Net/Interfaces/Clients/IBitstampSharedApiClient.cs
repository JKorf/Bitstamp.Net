using Bitstamp.Net.Interfaces.Clients.ExchangeApi;

namespace Bitstamp.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Bitstamp
    /// </summary>
    public interface IBitstampSharedApiClient
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

using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitstamp websocket API
    /// </summary>
    public interface IBitstampSocketClient : ISocketClient<BitstampCredentials>
    {
        /// <summary>
        /// API exchange streams
        /// </summary>
        /// <see cref="IBitstampSocketClientExchangeApi"/>
        IBitstampSocketClientExchangeApi ExchangeApi { get; }
    }
}
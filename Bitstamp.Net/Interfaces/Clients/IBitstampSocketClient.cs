using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitstamp websocket API
    /// </summary>
    public interface IBitstampSocketClient : ISocketClient
    {
        /// <summary>
        /// API exchange streams
        /// </summary>
        /// <see cref="IBitstampSocketClientExchangeApi"/>
        IBitstampSocketClientExchangeApi ExchangeApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
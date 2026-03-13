using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Websocket subscriptions for the Bitstamp API 
    /// </summary>
    public interface IBitstampSocketClientExchangeApi : ISocketApiClient<BitstampCredentials>
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitstampSocketClientExchangeApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to live trade updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampTradeUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book diff updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFullOrderBookUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to funding rate updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitstampFundingRateUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to orderbook snapshot updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user order updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user trade updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampUserTradeUpdate>> handler, CancellationToken ct = default);
    }
}

using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Authentication;
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
        /// [V1] Get the shared socket subscription client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        IBitstampSocketClientExchangeApiShared SharedClient { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IBitstampSocketClientExchangeSharedApi SharedApi { get; }

        /// <summary>
        /// Subscribe to live trade updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampTradeUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book diff updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFullOrderBookUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to funding rate updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitstampFundingRateUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to orderbook snapshot updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user order updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderUpdate>> handler, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user trade updates
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="handler">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampUserTradeUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to prevented self-trades on the private-live_trades channel. These events are not executions.
        /// <see href="https://www.bitstamp.net/websocket/v2/#:~:text=Private%20Live%20Trades"/>
        /// </summary>
        /// <param name="symbol">Symbol, for example <c>BTC/USD</c>.</param>
        /// <param name="handler">Handler for the two order ids and prevented quantity.</param>
        /// <param name="ct">Cancellation token for establishing the subscription.</param>
        /// <returns>The subscription and its connection status.</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToSelfTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampSelfTradeUpdate>> handler, CancellationToken ct = default);
    }
}

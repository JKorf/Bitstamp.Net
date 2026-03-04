using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    public interface IBitstampSocketClientExchangeApi : ISocketApiClient
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitstampSocketClientExchangeApiShared SharedClient { get; }

        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampTradeUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// <see href="https://www.bitstamp.net/websocket/v2/"/>
        /// </summary>
        Task<CallResult<UpdateSubscription>> SubscribeToFullOrderBookUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitstampFundingRateUpdate>> handler, CancellationToken ct = default);
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default);
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<BitstampSocketData<BitstampOrderUpdate>>> handler, CancellationToken ct = default);
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampUserTradeUpdate>> handler, CancellationToken ct = default);
    }
}

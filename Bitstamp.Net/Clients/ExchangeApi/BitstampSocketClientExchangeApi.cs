using Bitstamp.Net.Clients.MessageHandlers;
using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using Bitstamp.Net.Objects.Options;
using Bitstamp.Net.Objects.Sockets;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    /// <inheritdoc cref="IBitstampSocketClientExchangeApi" />
    internal partial class BitstampSocketClientExchangeApi : SocketApiClient<BitstampEnvironment, BitstampAuthenticationProvider, BitstampCredentials>, IBitstampSocketClientExchangeApi
    {
        #region fields
        private readonly BitstampSocketKeyGenerator _keyGenerator;
        /// <inheritdoc />
        public new BitstampSocketOptions ClientOptions => (BitstampSocketOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => BitstampErrors.RestErrorMapping;

        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of BitstampSocketClient with default options
        /// </summary>
        internal BitstampSocketClientExchangeApi(ILogger logger, BitstampSocketOptions options, BitstampSocketKeyGenerator keyGenerator)
            : base(logger, options.Environment.SocketBaseAddress, options, options.ApiOptions)
        {
            AddSystemSubscription(new BitstampReconnectSubsciption(logger));
            _keyGenerator = keyGenerator;

            RegisterPeriodicQuery(
               "ping",
               options.ApiOptions.PingInterval,
               x => new BitstampQuery<BitstampSubscriptionData>(Enums.SocketEventType.Heartbeat, null, null),
                (connection, result) =>
               {
                   if (result.Error?.ErrorType == ErrorType.Timeout)
                   {
                       // Ping timeout, reconnect
                       _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                       _ = connection.TriggerReconnectAsync();
                   }
               });
        }
        #endregion

        #region Methods
        private BitstampSubscription<T> BuildSubscription<T>(string channel, string symbol, Action<DateTime, string?, BitstampSocketData<T>> handler, BitstampSocketAuthToken? authToken = null)
            => new BitstampSubscription<T>(_logger, channel, symbol, handler, authToken);

        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitstampExchange._serializerContext));

        public IBitstampSocketClientExchangeApiShared SharedClient => this;

        protected override async Task<CallResult> RevitalizeRequestAsync(Subscription subscription)
        {
            if (subscription is not BitstampSubscription authSubscription)
                return new CallResult(null);


            var newToken = await _keyGenerator.GenerateWebsocketKeyAsync().ConfigureAwait(false);
            if (!newToken.Success)
                return newToken.AsDataless();

            authSubscription.AuthToken = newToken.Data;
            return new CallResult(null);
        }

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
            => Task.FromResult<Query?>(null);

        protected override BitstampAuthenticationProvider CreateAuthenticationProvider(BitstampCredentials credentials)
            => new BitstampAuthenticationProvider(credentials);

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
            => new BitstampSocketMessageHandler();

        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BitstampExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
        #endregion

        #region Subscriptions
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampTradeUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampTradeUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<BitstampTradeUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });
            var subscription = BuildSubscription("live_trades", BitstampExchange.SymbolToPathParameter(symbol), internalHandler);
            return SubscribeAsync(subscription, ct);
        }

        public Task<CallResult<UpdateSubscription>> SubscribeToFullOrderBookUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<BitstampOrderBookUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });
            var subscription = BuildSubscription("diff_order_book", BitstampExchange.SymbolToPathParameter(symbol), internalHandler);
            return SubscribeAsync(subscription, ct);
        }

        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderBookUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<BitstampOrderBookUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Snapshot)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });
            var subscription = BuildSubscription("order_book", BitstampExchange.SymbolToPathParameter(symbol), internalHandler);
            return SubscribeAsync(subscription, ct);
        }

        public Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitstampFundingRateUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampFundingRateUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<BitstampFundingRateUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Snapshot)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });
            var subscription = BuildSubscription("funding_rate", BitstampExchange.SymbolToPathParameter(symbol), internalHandler);
            return SubscribeAsync(subscription, ct);
        }


        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<BitstampOrderUpdate>> handler, CancellationToken ct = default)
        {
            var authToken = await _keyGenerator.GenerateWebsocketKeyAsync().ConfigureAwait(false);
            if (!authToken.Success)
                return new CallResult<UpdateSubscription>(authToken.Error!);

            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampOrderUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);
                data.Data.OrderEvent = GetOrderEvent(data.Event);

                handler(
                    new DataEvent<BitstampOrderUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });

            var subscription = BuildSubscription("private-my_orders", BitstampExchange.SymbolToPathParameter(symbol), internalHandler, authToken.Data);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        private OrderEvent GetOrderEvent(SocketEventType @event)
            => @event switch
            {
                SocketEventType.OrderCreated => OrderEvent.OrderCreated,
                SocketEventType.OrderChanged => OrderEvent.OrderChanged,
                SocketEventType.OrderDeleted => OrderEvent.OrderDeleted,
                _ => OrderEvent.OrderChanged
            };

        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<BitstampUserTradeUpdate>> handler, CancellationToken ct = default)
        {
            var authToken = await _keyGenerator.GenerateWebsocketKeyAsync().ConfigureAwait(false);
            if (!authToken.Success)
                return new CallResult<UpdateSubscription>(authToken.Error!);

            var internalHandler = new Action<DateTime, string?, BitstampSocketData<BitstampUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                var timestamp = data.Data!.Timestamp;
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<BitstampUserTradeUpdate>(BitstampExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel!)
                    );
            });

            var subscription = BuildSubscription("private-my_trades", BitstampExchange.SymbolToPathParameter(symbol), internalHandler, authToken.Data);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }
        #endregion
    }
}

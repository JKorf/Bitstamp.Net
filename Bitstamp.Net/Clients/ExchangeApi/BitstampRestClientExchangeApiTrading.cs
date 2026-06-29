using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class BitstampRestClientExchangeApiTrading : IBitstampRestClientExchangeApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitstampRestClientExchangeApi _baseClient;

        internal BitstampRestClientExchangeApiTrading(BitstampRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Limit Order

        /// <inheritdoc />
        public Task<HttpResult<BitstampOrder>> PlaceLimitOrderAsync(
            string symbol,
            OrderSide side,
            decimal price,
            OrderType? orderType = null,
            decimal? quantity = null,
            decimal? limitPrice = null,
            bool? dailyOrder = null,
            bool? iocOrder = null,
            bool? fokOrder = null,
            bool? mocOrder = null,
            bool? gtdOrder = null,
            bool? reduceOnly = null,
            decimal? leverage = null,
            decimal? stopPrice = null,
            TriggerType? triggerType = null,
            decimal? activationPrice = null,
            decimal? trailingDelta = null,
            MarginMode? marginMode = null,
            DateTime? expireTime = null,
            string? clientOrderId = null, 
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings)
            {
                { "price", price },
            };
#warning test amount/price decimal or string
            parameters.Add("subtype", orderType);
            parameters.Add("amount", quantity);
            parameters.Add("limit_price", limitPrice);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("daily_order", dailyOrder);
            parameters.Add("ioc_order", iocOrder);
            parameters.Add("fok_order", fokOrder);
            parameters.Add("moc_order", mocOrder);
            parameters.Add("gtd_order", gtdOrder);
            parameters.Add("expire_time", expireTime);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.Add("stop_price", stopPrice);
            parameters.Add("activation_price", activationPrice);
            parameters.Add("trailing_delta", trailingDelta);
            parameters.Add("trigger", triggerType);
            parameters.Add("trigger", reduceOnly);

            var uri = side == OrderSide.Buy ? $"/api/v2/buy/{BitstampExchange.SymbolToPathParameter(symbol)}/" : $"/api/v2/sell/{BitstampExchange.SymbolToPathParameter(symbol)}/";
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, uri, BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Place Market Order

        /// <inheritdoc />
        public Task<HttpResult<BitstampOrder>> PlaceMarketOrderAsync(
            string symbol,
            OrderSide side,
            OrderType? orderType = null,
            decimal? quantity = null,
            bool? reduceOnly = null,
            decimal? leverage = null,
            decimal? stopPrice = null,
            TriggerType? triggerType = null,
            decimal? activationPrice = null,
            decimal? trailingDelta = null,
            MarginMode? marginMode = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("subtype", orderType);
            parameters.Add("amount", quantity);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.Add("stop_price", stopPrice);
            parameters.Add("activation_price", activationPrice);
            parameters.Add("trailing_delta", trailingDelta);
            parameters.Add("trigger", triggerType);
            parameters.Add("trigger", reduceOnly);

            var uri = side == OrderSide.Buy ? $"/api/v2/buy/market/{BitstampExchange.SymbolToPathParameter(symbol)}/" : $"/api/v2/sell/market/{BitstampExchange.SymbolToPathParameter(symbol)}/";
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, uri, BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<BitstampOrderEvent[]>> GetOrderHistoryAsync(OrderSource orderSource, string symbol, string? fromEventId = null, string? toEventId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("order_source", orderSource);
            parameters.Add("market", symbol);
            parameters.Add("since_id", fromEventId);
            parameters.Add("until_id", toEventId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/account_order_data/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampOrderEvent[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public Task<HttpResult<BitstampCancelOrderResponse>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/cancel_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelOrderResponse>(request, parameters, ct);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<HttpResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/cancel_all_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelAllOrderResponse>(request, null, ct);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<HttpResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/cancel_all_order/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelAllOrderResponse>(request, null, ct);
        }

        #endregion

        #region Replace Order

        /// <inheritdoc />
        public Task<HttpResult<BitstampReplaceResponse>> ReplaceOrderAsync(
            decimal price,
            decimal quantity,
            long? id = null,
            string? clientOrderId = null,
            string? newClientOrderId = null, 
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            parameters.Add("orig_client_order_id", clientOrderId);
            parameters.Add("client_order_id", newClientOrderId);
            parameters.Add("amount", quantity);
            parameters.Add("price", price);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/replace_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampReplaceResponse>(request, parameters, ct);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public Task<HttpResult<BitstampOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, bool? includeTrades = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("omit_transactions", includeTrades);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/order_status/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<HttpResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/open_orders/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOpenOrder[]>(request, null, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<HttpResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/open_orders/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOpenOrder[]>(request, null, ct);
        }

        #endregion

        #region Get Derivatives User Trades

        /// <inheritdoc />
        public Task<HttpResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(
            long? orderId = null,
            long? afterId = null,
            SortOrder? sort = null,
            DateTime? startTime = null,
            DateTime? endTime = null, 
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            parameters.Add("sort", sort);
            parameters.Add("order_id", orderId);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("after_id", afterId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/trade_history/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Derivatives User Trades

        /// <inheritdoc />
        public Task<HttpResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(
            string symbol,
            long? orderId = null,
            long? afterId = null,
            SortOrder? sort = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            parameters.Add("sort", sort);
            parameters.Add("order_id", orderId);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("after_id", afterId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v2/trade_history/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPosition[]>> GetOpenPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/open_positions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPosition[]>> GetOpenPositionsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v2/open_positions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Status

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPosition>> GetPositionStatusAsync(string positionId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v2/position_status/{positionId}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string? sinceId = null, SortOrder? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("since_id", sinceId);
            parameters.Add("sort", sort);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/position_history/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string symbol, string? sinceId = null, SortOrder? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("since_id", sinceId);
            parameters.Add("sort", sort);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v2/position_history/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Positions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampClosePositionsResponse>> ClosePositionsAsync(string? symbol = null, MarginMode? marginMode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("order_type", OrderType.Market);
            parameters.Add("market", symbol == null ? null : BitstampExchange.SymbolToPathParameter(symbol));
            parameters.Add("margin_mode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/close_positions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampClosePositionsResponse>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<HttpResult<BitstampPosition>> ClosePositionAsync(string positionId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("position_id", positionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/close_position/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Settlement Transactions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("since_id", sinceId);
            parameters.Add("sort", sort);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/position_settlement_transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampSettleTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Settlement Transactions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string symbol, string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("since_id", sinceId);
            parameters.Add("sort", sort);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v2/position_settlement_transactions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampSettleTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Update Position Collateral

        /// <inheritdoc />
        public async Task<HttpResult> UpdatePositionCollateralAsync(string positionId, decimal newCollateralQuantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("position_id", positionId);
            parameters.Add("new_account", newCollateralQuantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/adjust_position_collateral/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


    }
}

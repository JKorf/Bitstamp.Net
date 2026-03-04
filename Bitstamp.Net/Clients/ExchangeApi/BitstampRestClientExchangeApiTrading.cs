using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
        public Task<WebCallResult<BitstampOrder>> PlaceLimitOrderAsync(
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
            var parameters = new ParameterCollection
            {
                { "price", price },
            };
            parameters.AddOptionalEnum("subtype", orderType);
            parameters.AddOptional("amount", quantity);
            parameters.AddOptional("limit_price", limitPrice);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalBoolString("daily_order", dailyOrder);
            parameters.AddOptionalBoolString("ioc_order", iocOrder);
            parameters.AddOptionalBoolString("fok_order", fokOrder);
            parameters.AddOptionalBoolString("moc_order", mocOrder);
            parameters.AddOptionalBoolString("gtd_order", gtdOrder);
            parameters.AddOptionalMilliseconds("expire_time", expireTime);
            parameters.AddOptionalEnum("margin_mode", marginMode);
            parameters.AddOptionalString("leverage", leverage);
            parameters.AddOptionalString("stop_price", stopPrice);
            parameters.AddOptionalString("activation_price", activationPrice);
            parameters.AddOptionalString("trailing_delta", trailingDelta);
            parameters.AddOptionalEnum("trigger", triggerType);
            parameters.AddOptionalBoolString("trigger", reduceOnly);

            var uri = side == OrderSide.Buy ? $"/api/v2/buy/{BitstampExchange.SymbolToPathParameter(symbol)}/" : $"/api/v2/sell/{BitstampExchange.SymbolToPathParameter(symbol)}/";
            var request = _definitions.GetOrCreate(HttpMethod.Post, uri, BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Place Market Order

        /// <inheritdoc />
        public Task<WebCallResult<BitstampOrder>> PlaceMarketOrderAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("subtype", orderType);
            parameters.AddOptional("amount", quantity);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalEnum("margin_mode", marginMode);
            parameters.AddOptionalString("leverage", leverage);
            parameters.AddOptionalString("stop_price", stopPrice);
            parameters.AddOptionalString("activation_price", activationPrice);
            parameters.AddOptionalString("trailing_delta", trailingDelta);
            parameters.AddOptionalEnum("trigger", triggerType);
            parameters.AddOptionalBoolString("trigger", reduceOnly);

            var uri = side == OrderSide.Buy ? $"/api/v2/buy/market/{BitstampExchange.SymbolToPathParameter(symbol)}/" : $"/api/v2/sell/market/{BitstampExchange.SymbolToPathParameter(symbol)}/";
            var request = _definitions.GetOrCreate(HttpMethod.Post, uri, BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampOrderEvent[]>> GetOrderHistoryAsync(OrderSource orderSource, string symbol, string? fromEventId = null, string? toEventId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("order_source", orderSource);
            parameters.Add("market", symbol);
            parameters.AddOptional("since_id", fromEventId);
            parameters.AddOptional("until_id", toEventId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/account_order_data/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampOrderEvent[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public Task<WebCallResult<BitstampCancelOrderResponse>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("id", orderId);
            parameters.AddOptional("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/cancel_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelOrderResponse>(request, parameters, ct);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/cancel_all_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelAllOrderResponse>(request, null, ct);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/cancel_all_order/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampCancelAllOrderResponse>(request, null, ct);
        }

        #endregion

        #region Replace Order

        /// <inheritdoc />
        public Task<WebCallResult<BitstampReplaceResponse>> ReplaceOrderAsync(
            decimal price,
            decimal quantity,
            long? id = null,
            string? clientOrderId = null,
            string? newClientOrderId = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("id", id);
            parameters.AddOptional("orig_client_order_id", clientOrderId);
            parameters.AddOptional("client_order_id", newClientOrderId);
            parameters.Add("amount", quantity);
            parameters.Add("price", price);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/replace_order/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampReplaceResponse>(request, parameters, ct);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public Task<WebCallResult<BitstampOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, bool? includeTrades = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("id", orderId);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalBoolString("omit_transactions", includeTrades);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/order_status/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/open_orders/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOpenOrder[]>(request, null, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/open_orders/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampOpenOrder[]>(request, null, ct);
        }

        #endregion

        #region Get Derivatives User Trades

        /// <inheritdoc />
        public Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(int? limit = null, SortOrder? sort = null, long? orderId = null, DateTime? sinceDate = null, DateTime? untilDate = null, long? afterId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("sort", sort);
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptionalMilliseconds("since_timestamp", sinceDate);
            parameters.AddOptionalMilliseconds("until_timestamp", untilDate);
            parameters.AddOptional("after_id", afterId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/trade_history/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Derivatives User Trades

        /// <inheritdoc />
        public Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(string symbol, int? limit = null, SortOrder? sort = null, long? orderId = null, DateTime? sinceDate = null, DateTime? untilDate = null, long? afterId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("sort", sort);
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptionalMilliseconds("since_timestamp", sinceDate);
            parameters.AddOptionalMilliseconds("until_timestamp", untilDate);
            parameters.AddOptional("after_id", afterId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/trade_history/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/open_positions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/open_positions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Status

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPosition>> GetPositionStatusAsync(string positionId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/position_status/{positionId}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string? sinceId = null, string? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("since_id", sinceId);
            parameters.AddOptional("sort", sort);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/position_history/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string symbol, string? sinceId = null, string? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("since_id", sinceId);
            parameters.AddOptional("sort", sort);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/position_history/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPositionHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampClosePositionsResponse>> ClosePositionsAsync(string? symbol = null, MarginMode? marginMode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("order_type", OrderType.Market);
            parameters.AddOptional("market", symbol == null ? null : BitstampExchange.SymbolToPathParameter(symbol));
            parameters.AddOptionalEnum("margin_mode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/close_positions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampClosePositionsResponse>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampPosition>> ClosePositionAsync(string positionId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("position_id", positionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/close_position/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampPosition>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Settlement Transactions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("since_id", sinceId);
            parameters.AddOptional("sort", sort);
            parameters.AddOptionalMillisecondsString("since_timestamp", startTime);
            parameters.AddOptionalMillisecondsString("until_timestamp", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/position_settlement_transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampSettleTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Settlement Transactions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string symbol, string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("since_id", sinceId);
            parameters.AddOptional("sort", sort);
            parameters.AddOptionalMillisecondsString("since_timestamp", startTime);
            parameters.AddOptionalMillisecondsString("until_timestamp", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/position_settlement_transactions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampSettleTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Update Position Collateral

        /// <inheritdoc />
        public async Task<WebCallResult> UpdatePositionCollateralAsync(string positionId, decimal newCollateralQuantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("position_id", positionId);
            parameters.Add("new_account", newCollateralQuantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/adjust_position_collateral/", BitstampExchange.RateLimiter.Rest, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


    }
}

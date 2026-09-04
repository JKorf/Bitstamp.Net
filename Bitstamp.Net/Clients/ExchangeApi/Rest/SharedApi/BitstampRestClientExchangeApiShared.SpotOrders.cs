using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampRestClientExchangeSharedApi
    {
        #region Place Spot Order

        public PlaceSpotOrderOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        async Task<ICallResult<SharedId>> IPlaceSpotOrder.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
            => await PlaceSpotOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (request.OrderType == SharedOrderType.Market)
            {
                var result = await _api.Trading.PlaceMarketOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                    quantity: request.Quantity?.QuantityInBaseAsset,
                    clientOrderId: request.ClientOrderId,
                    ct: ct
                    ).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            }
            else
            {
                var result = await _api.Trading.PlaceLimitOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                    price: request.Price ?? 0,
                    quantity: request.Quantity?.QuantityInBaseAsset,
                    clientOrderId: request.ClientOrderId,
                    iocOrder: request.TimeInForce == SharedTimeInForce.ImmediateOrCancel,
                    fokOrder: request.TimeInForce == SharedTimeInForce.FillOrKill,
                    mocOrder: request.OrderType == SharedOrderType.LimitMaker,
                    ct: ct
                    ).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            }
        }

        #endregion

        #region Get Spot Order

        public GetSpotOrderOptions GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        async Task<ICallResult<SharedSpotOrder>> IGetSpotOrder.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var orders = await _api.Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedSpotOrder>(orders);

            return HttpResult.Ok(orders, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, orders.Data.Symbol),
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                ParseOrderType(orders.Data.OrderType),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(orders.Data.Status),
                orders.Data.CreateTime)
            {
                ClientOrderId = orders.Data.ClientOrderId,
                OrderPrice = orders.Data.Price,
                AveragePrice = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.BaseQuantity * x.Price) / orders.Data.Trades.Sum(x => x.BaseQuantity) : null,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.Fee) : 0,
#pragma warning restore CS0618 // Type or member is obsolete
                OrderQuantity = new SharedOrderQuantity(orders.Data.Quantity != null ? orders.Data.Quantity : (orders.Data.Trades?.Sum(x => x.BaseQuantity) + orders.Data.QuantityRemaining) ?? 0, null, null),
                QuantityFilled = new SharedOrderQuantity(orders.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, orders.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
            });
        }

        #endregion

        #region Get Open Spot Orders

        public GetOpenSpotOrdersOptions GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        async Task<ICallResult<SharedSpotOrder[]>> IGetOpenSpotOrders.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenSpotOrdersAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedSpotOrder[]>> GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            string? symbol = request.Symbol?.GetSymbol(FormatSymbol);

            HttpResult<BitstampOpenOrder[]> orders;
            if (symbol != null)            
                orders = await _api.Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);            
            else            
                orders = await _api.Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);            

            if (!orders.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(orders);
                        
            return HttpResult.Ok(orders, orders.Data.Where(x => !x.Symbol.EndsWith("-perp", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => 
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.Id.ToString(),
                        SharedOrderType.Limit,
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        SharedOrderStatus.Open,
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.OriginalQuantity, null, null),
                        QuantityFilled = new SharedOrderQuantity(x.Quantity, null, null)
                    }).ToArray());
        }

        #endregion

        #region Get Closed Spot Orders

        public GetSpotClosedOrdersOptions GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, true, true, false, 500);
        async Task<ICallResult<SharedSpotOrder[]>> IGetClosedSpotOrders.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedSpotOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedSpotOrder[]>> GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            // Determine page token
            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 500;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Trading.GetOrderHistoryAsync(
                OrderSource.Orderbook,
                request.Symbol!.GetSymbol(FormatSymbol),
                fromEventId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toEventId: direction == DataDirection.Descending ? pageParams.FromId : null,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

            var closeData = result.Data.Where(x => x.Event == OrderEvent.OrderDeleted);
            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => direction == DataDirection.Ascending 
                    ? Pagination.NextPageFromId(result.Data.First().EventId)
                    : Pagination.NextPageFromId(result.Data.Last().EventId),
                    result.Data.Length,
                    result.Data.Select(x => x.Data.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(closeData, x => x.Data.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedSpotOrder(
                        request.Symbol,
                        symbol,
                        x.Data.Id.ToString(),
                        ParseOrderType(x.Data.OrderType),
                        x.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Data),
                        x.Data.Timestamp)
                    {
                        ClientOrderId = x.Data.ClientOrderId,
                        OrderPrice = x.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Data.OrderQuantity, null, null),
                        QuantityFilled = new SharedOrderQuantity(x.Data.QuantityFilled, null, null)
                    })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(BitstampOrderEventData data)
        {
            if (data.OrderQuantity != data.QuantityFilled)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Filled;
        }

        #region Get Spot Order Trades

        public GetSpotOrderTradesOptions GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        async Task<ICallResult<SharedUserTrade[]>> IGetSpotOrderTrades.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetSpotOrderTradesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedUserTrade[]>> GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await _api.Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Trades?.Select(x => new SharedUserTrade(
                request.Symbol,
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                x.TradeId.ToString(),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantities.TryGetValue(request.Symbol!.BaseAsset, out var quantity) ? quantity: 0),
                x.Price,
                x.Timestamp)
            {
                ClientOrderId = orders.Data.ClientOrderId,
                Fee = x.Fee,
            })?.ToArray() ?? []);
        }

        #endregion

        #region Get Spot User Trade History

        Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetSpotUserTradeHistoryAsync(request, nextPageToken, ct);
        GetSpotUserTradeHistoryOptions ISpotOrderRestClient.GetSpotUserTradesOptions => GetSpotUserTradeHistoryOptions;

        public GetSpotUserTradeHistoryOptions GetSpotUserTradeHistoryOptions { get; } = new GetSpotUserTradeHistoryOptions(_exchangeName, true, true, true, 1000);
        async Task<ICallResult<SharedUserTrade[]>> IGetSpotUserTradeHistory.GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetSpotUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedUserTrade[]>> GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetSpotUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetUserTransactionsAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                sort: direction == DataDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var trades = result.Data.Where(x => x.Type == TransactionType.MarketTrade);
            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.OrderId!.ToString()!,
                        x.Id.ToString(),
                        GetSide(request.Symbol, x),
                        GetSide(request.Symbol, x) == SharedOrderSide.Buy ? new SharedOrderQuantity(x.ReceivedQuantity) : new SharedOrderQuantity(Math.Abs(x.SentQuantity)),
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee
                    })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedOrderSide GetSide(SharedSymbol symbol, BitstampUserTransaction trade)
        {
            if (string.Equals(trade.SentAsset, symbol.BaseAsset, StringComparison.InvariantCultureIgnoreCase))
                return SharedOrderSide.Sell;

            return SharedOrderSide.Buy;
        }

        #region Cancel Spot Order

        public CancelSpotOrderOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        async Task<ICallResult<SharedId>> ICancelSpotOrder.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.Id.ToString()));
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Open || status == OrderStatus.CancelPending) return SharedOrderStatus.Open;
            if (status == OrderStatus.Expired || status == OrderStatus.Canceled) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Finished) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;

            return SharedOrderType.Limit;
        }

        #region Get Spot Order By Client Order Id

        public GetSpotOrderByClientOrderIdOptions GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<ICallResult<SharedSpotOrder>> IGetSpotOrderByClientOrderId.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.OrderType),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                AveragePrice = order.Data.Trades?.Length > 0 ? order.Data.Trades.Sum(x => x.BaseQuantity * x.Price) / order.Data.Trades.Sum(x => x.BaseQuantity) : null,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.Trades?.Length > 0 ? order.Data.Trades.Sum(x => x.Fee) : 0,
#pragma warning restore CS0618 // Type or member is obsolete
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity != null ? order.Data.Quantity : (order.Data.Trades?.Sum(x => x.BaseQuantity) + order.Data.QuantityRemaining) ?? 0, null, null),
                QuantityFilled = new SharedOrderQuantity(order.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, order.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
            });
        }

        #endregion

        #region Cancel Spot Order By Client Order Id

        public CancelSpotOrderByClientOrderIdOptions CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<ICallResult<SharedId>> ICancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.Id.ToString() ?? request.OrderId));
        }

        #endregion
    }
}

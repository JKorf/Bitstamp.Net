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
        #region Futures Order client

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false);

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (request.OrderType == SharedOrderType.Market)
            {
                var result = await _api.Trading.PlaceMarketOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                    quantity: request.Quantity?.QuantityInBaseAsset,
                    clientOrderId: request.ClientOrderId,
                    reduceOnly: request.ReduceOnly,
                    leverage: request.Leverage,
                    marginMode: request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
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
                    reduceOnly: request.ReduceOnly,
                    leverage: request.Leverage,
                    marginMode: request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                    ct: ct
                    ).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            }
        }

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var orders = await _api.Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder>(orders);

            return HttpResult.Ok(orders, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, orders.Data.Symbol),
                    orders.Data.Symbol,
                    orders.Data.Id.ToString(),
                    ParseOrderType(orders.Data.OrderType),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(orders.Data.Status),
                    orders.Data.CreateTime)
                {
                    ClientOrderId = orders.Data.ClientOrderId,
                    Leverage = orders.Data.Leverage,
                    ReduceOnly = orders.Data.ReduceOnly,
                    OrderPrice = orders.Data.Price,
                    AveragePrice = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.BaseQuantity * x.Price) / orders.Data.Trades.Sum(x => x.BaseQuantity) : null,
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.Fee) : 0,
#pragma warning restore CS0618 // Type or member is obsolete
                OrderQuantity = new SharedOrderQuantity(orders.Data.Quantity != null ? orders.Data.Quantity : (orders.Data.Trades?.Sum(x => x.BaseQuantity) + orders.Data.QuantityRemaining) ?? 0, null, null),
                    QuantityFilled = new SharedOrderQuantity(orders.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, orders.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
                });
        }

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            string? symbol = request.Symbol?.GetSymbol(FormatSymbol);

            HttpResult<BitstampOpenOrder[]> orders;
            if (symbol != null)
                orders = await _api.Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            else
                orders = await _api.Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);

            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Where(x => x.Symbol.EndsWith("-perp", StringComparison.InvariantCultureIgnoreCase)).Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
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

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, true, true, false, 500);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

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
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

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
                    .Select(x => new SharedFuturesOrder(
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

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
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
                new SharedOrderQuantity(x.Quantities.TryGetValue(request.Symbol!.BaseAsset, out var quantity) ? quantity : 0),
                x.Price,
                x.Timestamp)
            {
                ClientOrderId = orders.Data.ClientOrderId,
                Fee = x.Fee,
            })?.ToArray() ?? []);
        }

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, nextPageToken, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Trading.GetDerivativesUserTradesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                sort: SortOrder.Descending,
                limit: pageParams.Limit,
                afterId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromId(result.Data.Min(x => x.TradeId)),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.OrderId!.ToString()!,
                        x.TradeId.ToString(),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(x.Quantity),
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset
                    })
                    .ToArray(), nextPageRequest);
        }

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.Id.ToString()));
        }

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            HttpResult<BitstampPosition[]> result;
            if (request.Symbol != null)
                result = await _api.Trading.GetOpenPositionsAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            else
                result = await _api.Trading.GetOpenPositionsAsync(ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        new SharedOrderQuantity(Math.Abs(x.Quantity)),
                        default)
            {
                UnrealizedPnl = x.UnrealizedPnl,
                LiquidationPrice = x.EstimatedLiquidationPrice,
                Leverage = x.Leverage,
                AverageOpenPrice = x.EntryPrice,
                PositionMode = SharedPositionMode.OneWay,
                PositionSide = x.Quantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short
            }).ToArray());
        }

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.PositionSide), typeof(SharedPositionSide), "The position side to close", SharedPositionSide.Long),
                new ParameterDescription(nameof(ClosePositionRequest.Quantity), typeof(decimal), "Quantity of the position is required", 0.1m)
            }
        };
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.Trading.ClosePositionsAsync(
                symbol,
                request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            var id = result.Data.Closed.FirstOrDefault()?.Id ?? result.Data.Failed.FirstOrDefault()?.Id;
            return HttpResult.Ok(result, new SharedId(id?.ToString() ?? string.Empty));
        }
        #endregion
    }
}

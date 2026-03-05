using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampRestClientExchangeApi : IBitstampRestClientExchangeApiShared
    {
        private const string _topicSpotId = "BitstampSpot";
        private const string _topicFuturesId = "BitstampFutures";

        public string Exchange => BitstampExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot, TradingMode.PerpetualLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(false, true, true, 1000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.SixHours,
            SharedKlineInterval.TwelveHours,
            SharedKlineInterval.OneDay)
        {
        };

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            var direction = request.Direction ?? DataDirection.Descending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime ?? DateTime.UtcNow,
                limit: limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime).AddSeconds(-(int)(interval))),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                   Exchange,
                   TradingMode.Spot,
                   ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                   .Select(x =>
                       new SharedKline(
                           request.Symbol,
                           symbol,
                           x.OpenTime,
                           x.ClosePrice,
                           x.HighPrice,
                           x.LowPrice,
                           x.OpenPrice,
                           x.Volume))
                   .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client

        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult(
                Exchange, 
                TradingMode.Spot,
                result.Data.Where(x => x.MarketType == MarketType.Spot).Select(s => 
                    new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, true)
                    {
                        MinTradeQuantity = s.MinimumOrderQuantity,
                        MinNotionalValue = s.MinimumOrderValue,
                        MaxTradeQuantity = s.MaximumOrderQuantity,
                        PriceDecimals = s.QuoteDecimals,
                        QuantityDecimals = s.BaseDecimals
                    }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, response.Data);
            return response;
        }
        async Task<ExchangeResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicSpotId, baseAsset));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicSpotId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, symbol));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, symbolName));
        }
        #endregion

        #region Ticker client

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot, 
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, result.Data.Symbol),
                    symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    result.Data.Volume,
                    result.Data.PercentageChange24Hrs)
            {
                QuoteVolume = result.Data.Vwap * result.Data.Volume
            });
        }

        GetTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetAllTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedSpotTicker[]>(
                Exchange,
                TradingMode.Spot,
                result.Data.Where(x => x.MarketType == MarketType.Spot).Select(x => 
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                    x.Symbol!,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    x.Volume,
                    x.PercentageChange24Hrs)
            {
                QuoteVolume = x.Vwap * x.Volume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetOrderBookAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedBookTicker(
                request.Symbol,
                symbol,
                resultTicker.Data.Asks[0].Price,
                resultTicker.Data.Asks[0].Quantity,
                resultTicker.Data.Bids[0].Price,
                resultTicker.Data.Bids[0].Quantity));
        }

        #endregion

        #region Recent Trades

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradesAsync(
                symbol,
                Period.Hour,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol!.TradingMode, result.Data.Take(request.Limit ?? 1000).Select(x =>
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(AccountTypeFilter.Spot, AccountTypeFilter.Margin);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Spot || request.AccountType == null)
            {
                var result = await Account.GetAccountBalancesAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)).ToArray());
            }
            else
            {
                var result = await Account.GetMarginInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                var resultList = new List<SharedBalance>();
                foreach (var item in result.Data.Assets)
                    resultList.Add(new SharedBalance(item.Asset, item.Available, item.TotalQuantity));                

                return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, resultList.ToArray());
            }
        }

        #endregion

        #region Spot Order client

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (request.OrderType == SharedOrderType.Market)
            {
                var result = await Trading.PlaceMarketOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                    quantity: request.Quantity?.QuantityInBaseAsset,
                    clientOrderId: request.ClientOrderId,
                    ct: ct
                    ).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
            }
            else
            {
                var result = await Trading.PlaceLimitOrderAsync(
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

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return orders.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, orders.Data.Symbol),
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
                Fee = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.Fee) : 0,
                OrderQuantity = new SharedOrderQuantity(orders.Data.Quantity != null ? orders.Data.Quantity : (orders.Data.Trades?.Sum(x => x.BaseQuantity) + orders.Data.QuantityRemaining) ?? 0, null, null),
                QuantityFilled = new SharedOrderQuantity(orders.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, orders.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            string? symbol = request.Symbol?.GetSymbol(FormatSymbol);

            WebCallResult<BitstampOpenOrder[]> orders;
            if (symbol != null)            
                orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);            
            else            
                orders = await Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);            

            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);
                        
            return orders.AsExchangeResult<SharedSpotOrder[]>(
                Exchange, 
                TradingMode.Spot,
                orders.Data.Where(x => !x.Symbol.EndsWith("-perp", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => 
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
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

        GetClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetClosedOrdersOptions(true, true, false, 500);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            // Determine page token
            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 500;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetOrderHistoryAsync(
                OrderSource.Orderbook,
                request.Symbol!.GetSymbol(FormatSymbol),
                fromEventId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toEventId: direction == DataDirection.Descending ? pageParams.FromId : null,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

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

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(closeData, x => x.Data.Timestamp, request.StartTime, request.EndTime, direction)
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

        private SharedOrderStatus ParseOrderStatus(BitstampOrderEventData data)
        {
            if (data.OrderQuantity != data.QuantityFilled)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Filled;
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode, orders.Data.Trades?.Select(x => new SharedUserTrade(
                request.Symbol,
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                x.TradeId.ToString(),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantities.TryGetValue(request.Symbol.BaseAsset, out var quantity) ? quantity: 0,
                x.Price,
                x.Timestamp)
            {
                ClientOrderId = orders.Data.ClientOrderId,
                Fee = x.Fee,
            })?.ToArray() ?? []);
        }

        GetUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetUserTradesOptions(true, true, true, 1000);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetUserTransactionsAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                sort: direction == DataDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            var trades = result.Data.Where(x => x.Type == TransactionType.MarketTrade);
            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                        x.Symbol,
                        x.OrderId!.ToString()!,
                        x.Id.ToString(),
                        GetSide(request.Symbol, x),
                        GetSide(request.Symbol, x) == SharedOrderSide.Buy ? x.ReceivedQuantity : Math.Abs(x.SentQuantity),
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee
                    })
                    .ToArray(), nextPageRequest);
        }

        private SharedOrderSide GetSide(SharedSymbol symbol, BitstampUserTransaction trade)
        {
            if (string.Equals(trade.SentAsset, symbol.BaseAsset, StringComparison.InvariantCultureIgnoreCase))
                return SharedOrderSide.Sell;

            return SharedOrderSide.Buy;
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(order.Data.Id.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Open || status == OrderStatus.CancelPending) return SharedOrderStatus.Open;
            if (status == OrderStatus.Expired || status == OrderStatus.Canceled) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;

            return SharedOrderType.Limit;
        }


        #endregion

        #region Spot Client Id Order Client

        EndpointOptions<GetOrderRequest> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
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
                Fee = order.Data.Trades?.Length > 0 ? order.Data.Trades.Sum(x => x.Fee) : 0,
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity != null ? order.Data.Quantity : (order.Data.Trades?.Sum(x => x.BaseQuantity) + order.Data.QuantityRemaining) ?? 0, null, null),
                QuantityFilled = new SharedOrderQuantity(order.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, order.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
            });
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(order.Data.Id.ToString() ?? request.OrderId));
        }
        #endregion

        #region Asset client

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var asset = assets.Data.SingleOrDefault(x => string.Equals(x.Asset, request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return new ExchangeWebResult<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(asset.Asset)
            {
                FullName = asset.Name,
                Networks = asset.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    DepositEnabled = x.DepositStatus == EnabledStatus.Enabled,
                    WithdrawEnabled = x.WithdrawalStatus == EnabledStatus.Enabled,
                    MinWithdrawQuantity = x.WithdrawalMinQuantity
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(false);
        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, assets.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name,
                Networks = x.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    DepositEnabled = x.DepositStatus == EnabledStatus.Enabled,
                    WithdrawEnabled = x.WithdrawalStatus == EnabledStatus.Enabled,
                    MinWithdrawQuantity = x.WithdrawalMinQuantity
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client
        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, request.Network!, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, TradingMode.Spot, new[] { new SharedDepositAddress(request.Asset, depositAddresses.Data.Address)
            {
                Network = request.Network,
                TagOrMemo = depositAddresses.Data.DestinationTag?.ToString() ?? depositAddresses.Data.MemoId
            }
            });
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(false, true, false, 1000);
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetDepositsAsync(
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Finalized,
                            x.Timestamp,
                            x.Status == DepositStatus.Finalized ? SharedTransferStatus.Completed
                            : x.Status == DepositStatus.InProgress || x.Status == DepositStatus.Pending ? SharedTransferStatus.InProgress
                            : SharedTransferStatus.Failed)
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionId
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 1000, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }
        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(false, true, false, 1000);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetWithdrawalsAsync(
                request.Asset,
                limit: limit,
                offset: pageParams.Offset ?? 0,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Finished, x.Timestamp)
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionId
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawCryptoAsync(
                request.Asset,
                quantity: request.Quantity,
                address: request.Address,
                network: request.Network,
                memoId: request.AddressTag,
                destinationTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.WithdrawalId.ToString()));
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetFeesAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(result.Data.Fees.MakerFee, result.Data.Fees.TakerFee));
        }
        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(false, true, true, 1000, false);

        async Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFundingRate[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFundingRate[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp).AddHours(-1)),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            // Return
            return result.AsExchangeResult(
                Exchange,
                request.Symbol.TradingMode,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingRate(x.FundingRate, x.Timestamp))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult<SharedFuturesSymbol[]>(
                Exchange,
                TradingMode.PerpetualLinear, 
                result.Data.Where(x => x.MarketType == MarketType.Perpetual).Select(s =>
                    new SharedFuturesSymbol(
                        TradingMode.PerpetualLinear, 
                        s.BaseAsset,
                        s.QuoteAsset,
                        s.Name,
                        true)
                    {
                        MinTradeQuantity = s.MinimumOrderQuantity,
                        MinNotionalValue = s.MinimumOrderValue,
                        MaxTradeQuantity = s.MaximumOrderQuantity,
                        PriceDecimals = s.QuoteDecimals,
                        QuantityDecimals = s.BaseDecimals,
                        ContractSize = s.ContractSize
                    }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, response.Data);
            return response;
        }
        async Task<ExchangeResult<SharedSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicFuturesId, baseAsset));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Futures symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicFuturesId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, symbol));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, symbolName));
        }
        #endregion

        #region Ticker client

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            return result.AsExchangeResult(
                Exchange,
                TradingMode.PerpetualLinear,
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, result.Data.Symbol),
                    symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    result.Data.Volume,
                    result.Data.PercentageChange24Hrs)
                {
                    MarkPrice = result.Data.MarkPrice,
                    IndexPrice = result.Data.IndexPrice                    
                });
        }

        GetTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, TradingMode.PerpetualLinear, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetAllTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedFuturesTicker[]>(
                Exchange,
                TradingMode.PerpetualLinear,
                result.Data.Where(x => x.MarketType == MarketType.Perpetual).Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                    x.Symbol!,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    x.Volume,
                    x.PercentageChange24Hrs)
                {
                    MarkPrice = x.MarkPrice,
                    IndexPrice = x.IndexPrice
                }).ToArray());
        }

        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.GetLeverageSettingsAsync(
                marginMode: request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross: MarginMode.Isolated,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            if (!result.Data.Any())
                return result.AsExchangeError<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found")));

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedLeverage(result.Data.First().LeverageCurrent)
            {
                Side = request.PositionSide
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions()
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.SetLeverageAsync(
                marginMode: request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross: MarginMode.Isolated,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                (int)request.Leverage,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedLeverage(result.Data.LeverageCurrent));
        }
        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOpenInterest(result.Data.OpenInterest ?? 0));
        }

        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(false, true, false, 1000)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetPositionHistoryRequest.Symbol), typeof(SharedSymbol), "The symbol to get position history for", "ETH-USDT")
            }
        };
        async Task<ExchangeWebResult<SharedPositionHistory[]>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionHistory[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetPositionHistoryAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                sinceId: pageParams.FromId,
                sort: SortOrder.Descending,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionHistory[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromId(result.Data.OrderBy(x => x.CloseTime).First().Id),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                            x.Symbol,
                            x.QuantityDelta >= 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                            x.EntryPrice,
                            x.ExitPrice,
                            x.QuantityDelta,
                            x.RealizedPnl,
                            x.CloseTime)
                        {
                            Leverage = x.Leverage,
                            PositionId = x.Id
                        })
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Futures Order client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(false);

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        string IFuturesOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedTradingModes,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderTypes,
                ((IFuturesOrderRestClient)this).FuturesSupportedTimeInForce,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (request.OrderType == SharedOrderType.Market)
            {
                var result = await Trading.PlaceMarketOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                    quantity: request.Quantity?.QuantityInBaseAsset,
                    clientOrderId: request.ClientOrderId,
                    reduceOnly: request.ReduceOnly,
                    leverage: request.Leverage,
                    marginMode: request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                    ct: ct
                    ).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
            }
            else
            {
                var result = await Trading.PlaceLimitOrderAsync(
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

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return orders.AsExchangeResult(
                Exchange,
                TradingMode.PerpetualLinear, 
                new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, orders.Data.Symbol),
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
                    Fee = orders.Data.Trades?.Length > 0 ? orders.Data.Trades.Sum(x => x.Fee) : 0,
                    OrderQuantity = new SharedOrderQuantity(orders.Data.Quantity != null ? orders.Data.Quantity : (orders.Data.Trades?.Sum(x => x.BaseQuantity) + orders.Data.QuantityRemaining) ?? 0, null, null),
                    QuantityFilled = new SharedOrderQuantity(orders.Data.Trades?.Sum(x => x.BaseQuantity) ?? 0, orders.Data.Trades?.Sum(x => x.QuoteQuantity) ?? 0, null)
                });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            string? symbol = request.Symbol?.GetSymbol(FormatSymbol);

            WebCallResult<BitstampOpenOrder[]> orders;
            if (symbol != null)
                orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            else
                orders = await Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);

            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedFuturesOrder[]>(
                Exchange, 
                TradingMode.PerpetualLinear, 
                orders.Data.Where(x => x.Symbol.EndsWith("-perp", StringComparison.InvariantCultureIgnoreCase)).Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
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

        GetClosedOrdersOptions IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new GetClosedOrdersOptions(true, true, false, 500);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 500;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetOrderHistoryAsync(
                OrderSource.Orderbook,
                request.Symbol!.GetSymbol(FormatSymbol),
                fromEventId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toEventId: direction == DataDirection.Descending ? pageParams.FromId : null,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

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

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.PerpetualLinear,
                    ExchangeHelpers.ApplyFilter(closeData, x => x.Data.Timestamp, request.StartTime, request.EndTime, direction)
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

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetOrderAsync(orderId, includeTrades: true, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode, orders.Data.Trades?.Select(x => new SharedUserTrade(
                request.Symbol,
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                x.TradeId.ToString(),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantities.TryGetValue(request.Symbol.BaseAsset, out var quantity) ? quantity : 0,
                x.Price,
                x.Timestamp)
            {
                ClientOrderId = orders.Data.ClientOrderId,
                Fee = x.Fee,
            })?.ToArray() ?? []);
        }

        GetUserTradesOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new GetUserTradesOptions(false, true, true, 1000);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetDerivativesUserTradesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                sort: SortOrder.Descending,
                limit: pageParams.Limit,
                afterId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromId(result.Data.Min(x => x.TradeId)),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.PerpetualLinear,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                        x.Symbol,
                        x.OrderId!.ToString()!,
                        x.TradeId.ToString(),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        x.Quantity,
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset
                    })
                    .ToArray(), nextPageRequest);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(order.Data.Id.ToString()));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPosition[]>(Exchange, validationError);

            WebCallResult<BitstampPosition[]> result;
            if (request.Symbol != null)
                result = await Trading.GetOpenPositionsAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            else
                result = await Trading.GetOpenPositionsAsync(ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

            return result.AsExchangeResult(
                Exchange, 
                TradingMode.PerpetualLinear, 
                result.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                        x.Symbol,
                        Math.Abs(x.Quantity),
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

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.PositionSide), typeof(SharedPositionSide), "The position side to close", SharedPositionSide.Long),
                new ParameterDescription(nameof(ClosePositionRequest.Quantity), typeof(decimal), "Quantity of the position is required", 0.1m)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await Trading.ClosePositionsAsync(
                symbol,
                request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            var id = result.Data.Closed.FirstOrDefault()?.Id ?? result.Data.Failed.FirstOrDefault()?.Id;
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(id?.ToString() ?? string.Empty));
        }
        #endregion
    }
}

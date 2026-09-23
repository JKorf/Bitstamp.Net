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
        #region Get Ticker

        async Task<IExchangeCallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            if (request.Symbol!.TradingMode == TradingMode.Spot)
            {
                var result = await GetSpotTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
            else
            {
                var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
        }

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions => GetTickerOptions;
        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);

        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, result.Data.Symbol),
                    symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    new SharedOrderQuantity(result.Data.Volume, result.Data.Vwap * result.Data.Volume),
                    result.Data.PercentageChange24Hrs)
            {
            });
        }

        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(result);

            return HttpResult.Ok(result, new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, result.Data.Symbol),
                    symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    new SharedOrderQuantity(result.Data.Volume, result.Data.Vwap * result.Data.Volume),
                    result.Data.PercentageChange24Hrs)
                {
                    MarkPrice = result.Data.MarkPrice,
                    IndexPrice = result.Data.IndexPrice
                });
        }

        #endregion

        #region Get All Tickers

        async Task<IExchangeCallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetAllTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            IEnumerable<BitstampTicker> data = result.Data;
            if (request.TradingMode == TradingMode.Spot)
                data = data.Where(x => x.MarketType == MarketType.Spot);
            else if (request.TradingMode.HasValue)
                data = data.Where(x => x.MarketType == MarketType.Perpetual);

            return HttpResult.Ok<SharedTicker[]>(result, data.Select(x =>
            {
                if (x.MarketType == MarketType.Spot)
                {
                    return new SharedSpotTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol!,
                        x.LastPrice,
                        x.HighPrice,
                        x.LowPrice,
                        new SharedOrderQuantity(x.Volume, x.Vwap * x.Volume),
                        x.PercentageChange24Hrs);
                }

                return (SharedTicker)new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol!,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume, x.Vwap * x.Volume),
                    x.PercentageChange24Hrs)
                {
                    MarkPrice = x.MarkPrice,
                    IndexPrice = x.IndexPrice
                };
            }).ToArray());
        }

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(request, ct);
        GetAllTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllTickersOptions;

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName);

        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetAllTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Where(x => x.MarketType == MarketType.Spot).Select(x =>
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol!,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume, x.Vwap * x.Volume),
                    x.PercentageChange24Hrs)
            {
            }).ToArray());
        }

        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetAllTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Where(x => x.MarketType == MarketType.Perpetual).Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol!,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume, x.Vwap * x.Volume),
                    x.PercentageChange24Hrs)
                {
                    MarkPrice = x.MarkPrice,
                    IndexPrice = x.IndexPrice
                }).ToArray());
        }

        #endregion

    }
}

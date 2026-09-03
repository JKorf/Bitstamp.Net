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
        #region Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
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

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;


        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
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

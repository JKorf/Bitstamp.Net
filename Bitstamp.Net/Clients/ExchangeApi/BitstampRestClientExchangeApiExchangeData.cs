using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class BitstampRestClientExchangeApiExchangeData : IBitstampRestClientExchangeApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitstampRestClientExchangeApi _baseClient;

        internal BitstampRestClientExchangeApiExchangeData(BitstampRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Symbols

        /// <inheritdoc />
        public Task<WebCallResult<BitstampSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/markets/", BitstampExchange.RateLimiter.Rest);
            return _baseClient.SendAsync<BitstampSymbol[]>(request, null, ct);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public Task<WebCallResult<BitstampAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/currencies/", BitstampExchange.RateLimiter.Rest);
            return _baseClient.SendAsync<BitstampAsset[]>(request, null, ct);
        }

        #endregion

        #region Get All Tickers

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTicker[]>> GetAllTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/ticker/", BitstampExchange.RateLimiter.Rest);
            return _baseClient.SendAsync<BitstampTicker[]>(request, null, ct);
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/ticker/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest);
            return _baseClient.SendAsync<BitstampTicker>(request, null, ct);
        }

        #endregion

        #region Get Hour Ticker

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTicker>> GetHourTickerAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/ticker_hour/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest);
            return _baseClient.SendAsync<BitstampTicker>(request, null, ct);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampKline[]>> GetKlinesAsync(
            string symbol,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool? excludeCurrentCandle = null,
            CancellationToken ct = default)
        {
            if (limit <= 0 || limit > 1000)
                limit = 1000;

            var parameters = new ParameterCollection();
            parameters.AddEnum("step", interval);
            parameters.Add("limit", limit ?? 1000);
            parameters.AddOptionalSeconds("start", startTime);
            parameters.AddOptionalSeconds("end", endTime);
            parameters.AddOptionalBoolString("exclude_current_candle", excludeCurrentCandle);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/ohlc/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, false);
            var result = await _baseClient.SendAsync<BitstampKlinesResult>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Data?.KLines ?? []);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public Task<WebCallResult<BitstampOrderBookUpdate>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "group", 1 }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/order_book/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitstampOrderBookUpdate>(request, parameters, ct);
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTrade[]>> GetTradesAsync(string symbol, Period? period = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("time", period);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/transactions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitstampTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Eur Usd Conversion Rate

        /// <inheritdoc />
        public Task<WebCallResult<BitstampConversionRate>> GetEurUsdConversionRateAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/eur_usd/", BitstampExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitstampConversionRate>(request, null, ct);
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public Task<WebCallResult<BitstampFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/funding_rate/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitstampFundingRate>(request, null, ct);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampFundingRateHistory[]>> GetFundingRateHistoryAsync(
            string symbol,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("since_timestamp", startTime);
            parameters.AddOptionalSeconds("until_timestamp", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/funding_rate_history/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, false);
            var result = await _baseClient.SendAsync<BitstampFundingRateHistoryWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitstampFundingRateHistory[]>(result.Data?.History);
        }

        #endregion

        #region Get Margin Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampMarginTiers[]>> GetMarginTiersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/margin_tiers/", BitstampExchange.RateLimiter.Rest, 1, false);
            var result = await _baseClient.SendAsync<BitstampMarginTiers[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Collateral Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampCollateralAsset[]>> GetCollateralAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/collateral_currencies/", BitstampExchange.RateLimiter.Rest, 1, false, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCollateralAsset[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}

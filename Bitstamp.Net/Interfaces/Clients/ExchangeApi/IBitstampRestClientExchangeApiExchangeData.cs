using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitstampRestClientExchangeApiExchangeData
    {
        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.Bitstamp.com/api/v2/common/http/time" /><br />
        /// Endpoint:<br />
        /// GET /v2/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        //Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetTradingPairs"/>
        /// </summary>
        Task<WebCallResult<BitstampSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetCurrencies"/>
        /// </summary>
        Task<WebCallResult<BitstampAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetCurrencyPairTickers"/>
        /// </summary>
        Task<WebCallResult<BitstampTicker[]>> GetAllTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetMarketTicker"/>
        /// </summary>
        Task<WebCallResult<BitstampTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WebCallResult<BitstampTicker>> GetHourTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetOHLCData"/>
        /// </summary>
        Task<WebCallResult<BitstampKline[]>> GetKlinesAsync(
            string symbol,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool? excludeCurrentCandle = null,
            CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Order-book/operation/GetOrderBook"/>
        /// </summary>
        Task<WebCallResult<BitstampOrderBookUpdate>> GetOrderBookAsync(string symbol, CancellationToken ct = default);

        Task<WebCallResult<BitstampTrade[]>> GetTradesAsync(string symbol, Period? period = null, CancellationToken ct = default);

        Task<WebCallResult<BitstampConversionRate>> GetEurUsdConversionRateAsync(CancellationToken ct = default);

        Task<WebCallResult<BitstampFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);
        Task<WebCallResult<BitstampFundingRateHistory[]>> GetFundingRateHistoryAsync(
            string symbol,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get margin tiers
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetMarginTiers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampMarginTiers[]>> GetMarginTiersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get collateral assets
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetCollateralCurrencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCollateralAsset[]>> GetCollateralAssetsAsync(CancellationToken ct = default);

    }
}

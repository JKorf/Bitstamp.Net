using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitstampRestClientExchangeApiExchangeData
    {
        /// <summary>
        /// Get trading symbols supported
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetMarkets" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/markets/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get assets supported
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetCurrencies" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/currencies/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get 24h ticker data for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetCurrencyPairTickers" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/ticker/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTicker[]>> GetAllTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get 24h ticker data
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetMarketTicker" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/ticker/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get ticker data for the last hour
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetMarketTicker" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/ticker_hour/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTicker>> GetHourTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Tickers/operation/GetMarketTicker" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/ohlc/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="excludeCurrentCandle">Whether to exclude the current in-progress candle</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampKline[]>> GetKlinesAsync(
            string symbol,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool? excludeCurrentCandle = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get full order book snapshot
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Order-book" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/order_book/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOrderBookUpdate>> GetOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Transactions-public" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/transactions/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="period">The period to get trades for</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTrade[]>> GetTradesAsync(string symbol, Period? period = null, CancellationToken ct = default);

        /// <summary>
        /// Get Euro / USD conversion rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetEURUSDConversionRate" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/eur_usd/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampConversionRate>> GetEurUsdConversionRateAsync(CancellationToken ct = default);

        /// <summary>
        /// Get current funding rate info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetFundingRate" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/funding_rate/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Market-info/operation/GetFundingRateHistory" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/funding_rate_history/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampFundingRateHistory[]>> GetFundingRateHistoryAsync(
            string symbol,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get margin tiers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetMarginTiers" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/margin_tiers/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampMarginTiers[]>> GetMarginTiersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get collateral assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetCollateralCurrencies" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/collateral_currencies/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCollateralAsset[]>> GetCollateralAssetsAsync(CancellationToken ct = default);

    }
}

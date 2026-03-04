using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitstampRestClientExchangeApiTrading
    {

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenLimitSellOrder"/> 
        /// </summary>
        Task<WebCallResult<BitstampOrder>> PlaceLimitOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenLimitSellOrder"/> 
        /// </summary>
        Task<WebCallResult<BitstampOrder>> PlaceMarketOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Orders/operation/CancelOrdersForMarket"/> 
        /// </summary>
        Task<WebCallResult<BitstampCancelOrderResponse>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://www.bitstamp.net/api/#tag/Orders/operation/AccountOrderData" /></para>
        /// </summary>
        /// <param name="orderSource">Order source</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="fromEventId">Filter by from event id</param>
        /// <param name="toEventId">Filter by to event id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOrderEvent[]>> GetOrderHistoryAsync(OrderSource orderSource, string symbol, string? fromEventId = null, string? toEventId = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(CancellationToken ct = default);

        Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Orders/operation/CancelOrdersForMarket"/> 
        /// </summary>
        Task<WebCallResult<BitstampOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, bool? includeTrades = null, CancellationToken ct = default);
        Task<WebCallResult<BitstampReplaceResponse>> ReplaceOrderAsync(
            decimal price,
            decimal quantity,
            long? id = null,
            string? clientOrderId = null,
            string? newClientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Orders/operation/GetAllOpenOrders"/> 
        /// </summary>
        Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(CancellationToken ct = default);

        Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// <see href="https://www.bitstamp.net/api/#tag/Orders/operation/GetTradeHistory"/>
        /// </summary>
        /// <param name="limit">(Optional) Limit result to that many records (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting order: asc - ascending; desc - descending (default: desc).</param>
        /// <param name="orderId">Order ID.</param>
        /// <param name="sinceDate">(Optional) Show only trades from unix timestamp.</param>
        /// <param name="untilDate">Show only trades to unix timestamp.</param>
        /// <param name="afterId">(Optional) Show only records from specified id. If after_id parameter is used, limit parameter is set to 1000.</param>
        Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(int? limit = null, SortOrder? sort = null, long? orderId = null, DateTime? sinceDate = null, DateTime? untilDate = null, long? afterId = null, CancellationToken ct = default);

        /// <summary>
        /// <see href="https://www.bitstamp.net/api/#tag/Orders/operation/GetTradeHistory"/>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="limit">(Optional) Limit result to that many records (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting order: asc - ascending; desc - descending (default: desc).</param>
        /// <param name="orderId">Order ID.</param>
        /// <param name="sinceDate">(Optional) Show only trades from unix timestamp.</param>
        /// <param name="untilDate">Show only trades to unix timestamp.</param>
        /// <param name="afterId">(Optional) Show only records from specified id. If after_id parameter is used, limit parameter is set to 1000.</param>
        Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(string symbol, int? limit = null, SortOrder? sort = null, long? orderId = null, DateTime? sinceDate = null, DateTime? untilDate = null, long? afterId = null, CancellationToken ct = default);


        /// <summary>
        /// Get open positions
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetOpenPositionList" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open positions for a symbol
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetOpenPositionMarketList" /></para>
        /// </summary>
        /// <param name="symbol">Symbol filter</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get position status
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionStatus" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition>> GetPositionStatusAsync(string positionId, CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionHistoryList" /></para>
        /// </summary>
        /// <param name="sinceId">Since id</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string? sinceId = null, string? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get position history for a symbol
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionHistoryList" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="sinceId">Since id</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(string symbol, string? sinceId = null, string? sort = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Close positions
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/ClosePositions" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ethusd-perp`</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampClosePositionsResponse>> ClosePositionsAsync(string? symbol = null, MarginMode? marginMode = null, CancellationToken ct = default);

        /// <summary>
        /// Close position
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/ClosePosition" /></para>
        /// </summary>
        /// <param name="positionId">The position id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition>> ClosePositionAsync(string positionId, CancellationToken ct = default);

        /// <summary>
        /// Get position settlement transaction history
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionSettlementTransactionList" /></para>
        /// </summary>
        /// <param name="sinceId">Filter by since id</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get position settlement transaction history for a symbol
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionSettlementTransactionList" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="sinceId">Filter by since id</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(string symbol, string? sinceId = null, string? sort = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Update position collateral
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/AdjustPositionCollateral" /></para>
        /// </summary>
        /// <param name="positionId">The position id</param>
        /// <param name="newCollateralQuantity">New collateral quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> UpdatePositionCollateralAsync(string positionId, decimal newCollateralQuantity, CancellationToken ct = default);

    }
}

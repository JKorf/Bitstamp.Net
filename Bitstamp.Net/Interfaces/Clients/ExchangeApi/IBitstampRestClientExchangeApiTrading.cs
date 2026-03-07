using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitstampRestClientExchangeApiTrading
    {
        /// <summary>
        /// Place a new limit order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenLimitBuyOrder" /><br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenLimitSellOrder" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/buy/{symbol}/<br />
        /// POST /api/v2/sell/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
        /// <param name="side">Order side</param>
        /// <param name="price">["<c>price</c>"] Order price</param>
        /// <param name="orderType">["<c>subtype</c>"] Order type (for derivatives)</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="limitPrice">["<c>limit_price</c>"] Limit price for triggered order</param>
        /// <param name="dailyOrder">["<c>daily_order</c>"] Whether to cancel order at end of the day</param>
        /// <param name="iocOrder">["<c>ioc_order</c>"] Immediate or cancel order</param>
        /// <param name="fokOrder">["<c>fok_order</c>"] Fill or kill order</param>
        /// <param name="mocOrder">["<c>moc_order</c>"] Maker or cancel order</param>
        /// <param name="gtdOrder">["<c>gtd_order</c>"] Good till date order</param>
        /// <param name="reduceOnly">["<c>trigger</c>"] Reduce only order</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="stopPrice">["<c>stop_price</c>"] Stop price</param>
        /// <param name="triggerType">["<c>trigger</c>"] Trigger price type</param>
        /// <param name="activationPrice">["<c>activation_price</c>"] Activation price</param>
        /// <param name="trailingDelta">["<c>trailing_delta</c>"] Trailing delta</param>
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="expireTime">["<c>expire_time</c>"] Expire time for GTD orders</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
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
        /// Place a new market order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenMarketBuyOrder" /><br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/OpenMarketSellOrder" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/buy/market/{symbol}/<br />
        /// POST /api/v2/sell/market/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
        /// <param name="side">Order side</param>
        /// <param name="orderType">["<c>subtype</c>"] Order type (for derivatives)</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="reduceOnly">["<c>trigger</c>"] Reduce only</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="stopPrice">["<c>stop_price</c>"] Stop price</param>
        /// <param name="triggerType">["<c>trigger</c>"] Trigger price type</param>
        /// <param name="activationPrice">["<c>activation_price</c>"] Activation price</param>
        /// <param name="trailingDelta">["<c>trailing_delta</c>"] Trailing delta</param>
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
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
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/CancelOrder" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/cancel_order/
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>id</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCancelOrderResponse>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/AccountOrderData" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/account_order_data/
        /// </para>
        /// </summary>
        /// <param name="orderSource">["<c>order_source</c>"] Order source</param>
        /// <param name="symbol">["<c>market</c>"] Symbol</param>
        /// <param name="fromEventId">["<c>since_id</c>"] Filter by from event id</param>
        /// <param name="toEventId">["<c>until_id</c>"] Filter by to event id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOrderEvent[]>> GetOrderHistoryAsync(OrderSource orderSource, string symbol, string? fromEventId = null, string? toEventId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/CancelAllOrders" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/cancel_all_order/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/CancelOrdersForMarket" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/cancel_all_order/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCancelAllOrderResponse>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/GetOrderStatus" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/order_status/
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>id</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="includeTrades">["<c>omit_transactions</c>"] Whether to include trades</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, bool? includeTrades = null, CancellationToken ct = default);

        /// <summary>
        /// Replace an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/ReplaceOrder" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/replace_order/
        /// </para>
        /// </summary>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="quantity">["<c>amount</c>"] New quantity</param>
        /// <param name="id">["<c>id</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>orig_client_order_id</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="newClientOrderId">["<c>client_order_id</c>"] New client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampReplaceResponse>> ReplaceOrderAsync(
            decimal price,
            decimal quantity,
            long? id = null,
            string? clientOrderId = null,
            string? newClientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/GetAllOpenOrders" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/open_orders/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/GetOpenOrdersForMarket" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/open_orders/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampOpenOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get user trades for derivatives
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetTradeHistory" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/trade_history/
        /// </para>
        /// </summary>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="orderId">["<c>order_id</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="afterId">["<c>after_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(
            long? orderId = null,
            long? afterId = null, 
            SortOrder? sort = null, 
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user trades for derivatives for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetTradeHistoryByMarket" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/trade_history/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="orderId">["<c>order_id</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="afterId">["<c>after_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampUserTrade[]>> GetDerivativesUserTradesAsync(
            string symbol,
            long? orderId = null,
            long? afterId = null,
            SortOrder? sort = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);


        /// <summary>
        /// Get open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetOpenPositionList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/open_positions/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open positions for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetOpenPositionMarketList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/open_positions/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition[]>> GetOpenPositionsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get position status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionStatus" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position_status/{positionId}/
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition>> GetPositionStatusAsync(string positionId, CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionHistoryList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position_history/
        /// </para>
        /// </summary>
        /// <param name="sinceId">["<c>since_id</c>"] Since id</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(
            string? sinceId = null, 
            SortOrder? sort = null, 
            int? limit = null,
            int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get position history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionHistoryList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position_history/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="sinceId">["<c>since_id</c>"] Since id</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPositionHistory[]>> GetPositionHistoryAsync(
            string symbol,
            string? sinceId = null,
            SortOrder? sort = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Close positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/ClosePositions" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/close_positions/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>market</c>"] The symbol, for example `ETHUSD-PERP`</param>
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampClosePositionsResponse>> ClosePositionsAsync(string? symbol = null, MarginMode? marginMode = null, CancellationToken ct = default);

        /// <summary>
        /// Close position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/ClosePosition" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/close_position/
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>position_id</c>"] The position id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampPosition>> ClosePositionAsync(string positionId, CancellationToken ct = default);

        /// <summary>
        /// Get position settlement transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionSettlementTransactionList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position_settlement_transactions/
        /// </para>
        /// </summary>
        /// <param name="sinceId">["<c>since_id</c>"] Filter by since id</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(
            string? sinceId = null,
            string? sort = null, 
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null, 
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get position settlement transaction history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetPositionSettlementTransactionList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position_settlement_transactions/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="sinceId">["<c>since_id</c>"] Filter by since id</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampSettleTransaction[]>> GetPositionSettlementTransactionsAsync(
            string symbol, 
            string? sinceId = null,
            string? sort = null,
            DateTime? startTime = null, 
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Update position collateral
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/AdjustPositionCollateral" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/adjust_position_collateral/
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>position_id</c>"] The position id</param>
        /// <param name="newCollateralQuantity">["<c>new_account</c>"] New collateral quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> UpdatePositionCollateralAsync(string positionId, decimal newCollateralQuantity, CancellationToken ct = default);

    }
}

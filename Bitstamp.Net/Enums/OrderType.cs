using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit
        /// </summary>
        [Map("LIMIT", "0")]
        Limit,
        /// <summary>
        /// ["<c>MARKET</c>"] Market
        /// </summary>
        [Map("MARKET", "2")]
        Market,
        /// <summary>
        /// ["<c>INSTANT</c>"] Instant
        /// </summary>
        [Map("INSTANT")]
        Instant,
        /// <summary>
        /// ["<c>CASH</c>"] Cash
        /// </summary>
        [Map("CASH")]
        Cash,
        /// <summary>
        /// ["<c>STOP_MARKET</c>"] Stop market
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// ["<c>STOP_LIMIT</c>"] Stop limit
        /// </summary>
        [Map("STOP_LIMIT")]
        StopLimit,
        /// <summary>
        /// ["<c>STOP_LOSS</c>"] Stop loss market
        /// </summary>
        [Map("STOP_LOSS")]
        StopLoss,
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit market
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// ["<c>STOP_LOSS_LIMIT</c>"] Stop loss limit
        /// </summary>
        [Map("STOP_LOSS_LIMIT")]
        StopLossLimit,
        /// <summary>
        /// ["<c>TAKE_PROFIT_LIMIT</c>"] Take profit limit
        /// </summary>
        [Map("TAKE_PROFIT_LIMIT")]
        TakeProfitLimit,
        /// <summary>
        /// ["<c>TRAILING_STOP_LOSS</c>"] Trailling stop loss market
        /// </summary>
        [Map("TRAILING_STOP_LOSS")]
        TrailingStopLoss,
        /// <summary>
        /// ["<c>TRAILING_TAKE_PROFIT</c>"] Trailing take profit market
        /// </summary>
        [Map("TRAILING_TAKE_PROFIT")]
        TrailingTakeProfit,
        /// <summary>
        /// ["<c>TRAILING_STOP_LOSS_LIMIT</c>"] Trailing stop loss limit
        /// </summary>
        [Map("TRAILING_STOP_LOSS_LIMIT")]
        TrailingStopLossLimit,
        /// <summary>
        /// ["<c>TRAILING_TAKE_PROFIT_LIMIT</c>"] Trailing take profit limit
        /// </summary>
        [Map("TRAILING_TAKE_PROFIT_LIMIT")]
        TrailingTakeProfitLimit
    }
}

using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Limit
        /// </summary>
        [Map("LIMIT", "0")]
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        [Map("MARKET", "2")]
        Market,
        /// <summary>
        /// Instant
        /// </summary>
        [Map("INSTANT")]
        Instant,
        /// <summary>
        /// Cash
        /// </summary>
        [Map("CASH")]
        Cash,
        /// <summary>
        /// Stop market
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// Stop limit
        /// </summary>
        [Map("STOP_LIMIT")]
        StopLimit,
        /// <summary>
        /// Stop loss market
        /// </summary>
        [Map("STOP_LOSS")]
        StopLoss,
        /// <summary>
        /// Take profit market
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// Stop loss limit
        /// </summary>
        [Map("STOP_LOSS_LIMIT")]
        StopLossLimit,
        /// <summary>
        /// Take profit limit
        /// </summary>
        [Map("TAKE_PROFIT_LIMIT")]
        TakeProfitLimit,
        /// <summary>
        /// Trailling stop loss market
        /// </summary>
        [Map("TRAILING_STOP_LOSS")]
        TrailingStopLoss,
        /// <summary>
        /// Trailing take profit market
        /// </summary>
        [Map("TRAILING_TAKE_PROFIT")]
        TrailingTakeProfit,
        /// <summary>
        /// Trailing stop loss limit
        /// </summary>
        [Map("TRAILING_STOP_LOSS_LIMIT")]
        TrailingStopLossLimit,
        /// <summary>
        /// Trailing take profit limit
        /// </summary>
        [Map("TRAILING_TAKE_PROFIT_LIMIT")]
        TrailingTakeProfitLimit
    }
}

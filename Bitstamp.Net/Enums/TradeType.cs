using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeType>))]
    public enum TradeType
    {
        /// <summary>
        /// ["<c>TRADE</c>"] Market trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// ["<c>LIQUIDATION</c>"] Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// ["<c>ADL</c>"] Auto deleverage
        /// </summary>
        [Map("ADL")]
        Adl,
        /// <summary>
        /// ["<c>ASSIGNMENT_PROGRAM</c>"] Assignment program
        /// </summary>
        [Map("ASSIGNMENT_PROGRAM")]
        AssignmentProgram,
        /// <summary>
        /// ["<c>MARKET_WIDE_POSITION_CLOSURE</c>"] Market wide position closure
        /// </summary>
        [Map("MARKET_WIDE_POSITION_CLOSURE")]
        MarketWidePositionClosure,
    }
}

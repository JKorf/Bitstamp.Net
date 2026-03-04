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
        /// Market trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// Auto deleverage
        /// </summary>
        [Map("ADL")]
        Adl,
        /// <summary>
        /// Assignment program
        /// </summary>
        [Map("ASSIGNMENT_PROGRAM")]
        AssignmentProgram,
        /// <summary>
        /// Market wide position closure
        /// </summary>
        [Map("MARKET_WIDE_POSITION_CLOSURE")]
        MarketWidePositionClosure,
    }
}

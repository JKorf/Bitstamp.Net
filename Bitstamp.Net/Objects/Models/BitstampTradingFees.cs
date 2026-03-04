using System.Text.Json.Serialization;
using System.Diagnostics;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Trading fee
    /// </summary>
    [DebuggerDisplay("{CurrencyPair,nq}")]
    public record BitstampTradingFees
    {
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string CurrencyPair { get; set; } = string.Empty;
        /// <summary>
        /// Fee info
        /// </summary>
        [JsonPropertyName("fees")]
        public TradingFeeEntry Fees { get; set; } = null!;
    }

    /// <summary>
    /// Fee percentages
    /// </summary>
    public record TradingFeeEntry
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal TakerFee { get; set; }
    }
}

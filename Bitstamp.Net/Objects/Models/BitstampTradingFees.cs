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
        /// ["<c>currency_pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string CurrencyPair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fees</c>"] Fee info
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
        /// ["<c>maker</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal TakerFee { get; set; }
    }
}

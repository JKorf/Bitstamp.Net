using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Conversion rate
    /// </summary>
    public record BitstampConversionRate
    {
        /// <summary>
        /// ["<c>buy</c>"] Buy price
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal BuyPrice { get; set; }
        /// <summary>
        /// ["<c>sell</c>"] Sell price
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal SellPrice { get; set; }
    }
}

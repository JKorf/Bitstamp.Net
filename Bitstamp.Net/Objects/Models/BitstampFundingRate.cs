using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Funding rate
    /// </summary>
    public record BitstampFundingRate
    {
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Market
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonPropertyName("next_funding_time")]
        public DateTime NextFundingTime { get; set; }
    }
}

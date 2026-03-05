using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Funding rate update
    /// </summary>
    public record BitstampFundingRateUpdate
    {
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Next funding timestamp
        /// </summary>
        [JsonPropertyName("next_funding_time")]
        public DateTime NextFundingTime { get; set; }
    }
}

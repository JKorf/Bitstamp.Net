using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    internal record BitstampFundingRateHistoryWrapper
    {
        [JsonPropertyName("funding_rate_history")]
        public BitstampFundingRateHistory[] History { get; set; } = [];
    }

    /// <summary>
    /// Funding rate
    /// </summary>
    public record BitstampFundingRateHistory
    {
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

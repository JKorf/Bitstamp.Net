using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

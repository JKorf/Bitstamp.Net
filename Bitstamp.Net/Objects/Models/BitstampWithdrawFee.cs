using System.Text.Json.Serialization;
using System.Diagnostics;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Withdraw fee
    /// </summary>
    public record BitstampWithdrawFee
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
    }
}

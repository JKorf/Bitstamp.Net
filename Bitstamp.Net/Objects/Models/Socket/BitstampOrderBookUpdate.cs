using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order book
    /// </summary>
    public record BitstampOrderBookUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("microtimestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public BitstampOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public BitstampOrderBookEntry[] Asks { get; set; } = [];
    }
}

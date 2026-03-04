using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Trade
    /// </summary>
    public record BitstampTrade
    {
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Timestamp { get; set; }

        [JsonInclude, JsonPropertyName("microtimestamp")]
        internal DateTime TimestampInt { set => Timestamp = value; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tid")]
        public long TradeId { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
    }
}

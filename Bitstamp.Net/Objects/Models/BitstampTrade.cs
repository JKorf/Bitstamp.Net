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
        /// ["<c>amount</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>date</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Timestamp { get; set; }

        [JsonInclude, JsonPropertyName("microtimestamp")]
        internal DateTime TimestampInt { set => Timestamp = value; }
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>tid</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tid")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
    }
}

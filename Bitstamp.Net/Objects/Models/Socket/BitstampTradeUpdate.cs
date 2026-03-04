using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade update
    /// </summary>
    public record BitstampTradeUpdate
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
        [JsonPropertyName("id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Buy order id
        /// </summary>
        [JsonPropertyName("buy_order_id")]
        public long BuyOrderId { get; set; }
        /// <summary>
        /// Sell order id
        /// </summary>
        [JsonPropertyName("sell_order_id")]
        public long SellOrderId { get; set; }
    }
}

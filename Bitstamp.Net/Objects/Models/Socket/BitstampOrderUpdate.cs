using System.ComponentModel;
using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    public record BitstampOrderUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("microtimestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("amount_at_create")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Last trade quantity
        /// </summary>
        [JsonPropertyName("amount_traded")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ID of related trade account (0 for main account, Unique ID for sub accounts).
        /// </summary>
        [JsonPropertyName("trade_account_id")]
        public long AccountId { get; set; }
    }
}

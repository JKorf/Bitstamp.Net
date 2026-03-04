using Bitstamp.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Replace result
    /// </summary>
    public record BitstampReplaceResponse
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long Id { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Original order id
        /// </summary>
        [JsonPropertyName("orig_order_id")]
        public long OriginalOrderId { get; set; }
        /// <summary>
        /// Original client order id
        /// </summary>
        [JsonPropertyName("orig_client_order_id")]
        public string? OriginalClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }
}

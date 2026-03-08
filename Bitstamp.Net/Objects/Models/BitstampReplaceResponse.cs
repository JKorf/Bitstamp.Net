using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Replace result
    /// </summary>
    public record BitstampReplaceResponse
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order side
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>datetime</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>orig_order_id</c>"] Original order id
        /// </summary>
        [JsonPropertyName("orig_order_id")]
        public long OriginalOrderId { get; set; }
        /// <summary>
        /// ["<c>orig_client_order_id</c>"] Original client order id
        /// </summary>
        [JsonPropertyName("orig_client_order_id")]
        public string? OriginalClientOrderId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }
}

using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Open order  info
    /// </summary>
    public record BitstampOpenOrder
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
        /// Create time
        /// </summary>
        [JsonPropertyName("datetime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Original Quantity
        /// </summary>
        [JsonPropertyName("amount_at_create")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
    }
}

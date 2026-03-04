using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record BitstampOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("subtype")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Trading pair
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string? Pair { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("amount_remaining")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("limit_price")]
        public decimal? LimitPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Reserved margin
        /// </summary>
        [JsonPropertyName("reserved_margin")]
        public decimal? ReservedMargin { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Trigger type
        /// </summary>
        [JsonPropertyName("trigger")]
        public TriggerType? TriggerType { get; set; }
        /// <summary>
        /// Activation price
        /// </summary>
        [JsonPropertyName("activation_price")]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// Trailing delta
        /// </summary>
        [JsonPropertyName("trailing_delta")]
        public decimal? TrailingDelta { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("transactions")]
        public BitstampOrderTrade[]? Trades { get; set; }
    }
}

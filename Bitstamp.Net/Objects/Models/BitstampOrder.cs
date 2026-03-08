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
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>datetime</c>"] Creation time
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order side
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>subtype</c>"] Order type
        /// </summary>
        [JsonPropertyName("subtype")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>currency_pair</c>"] Trading pair
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string? Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount_remaining</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("amount_remaining")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>limit_price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("limit_price")]
        public decimal? LimitPrice { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>reserved_margin</c>"] Reserved margin
        /// </summary>
        [JsonPropertyName("reserved_margin")]
        public decimal? ReservedMargin { get; set; }
        /// <summary>
        /// ["<c>stop_price</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>trigger</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("trigger")]
        public TriggerType? TriggerType { get; set; }
        /// <summary>
        /// ["<c>activation_price</c>"] Activation price
        /// </summary>
        [JsonPropertyName("activation_price")]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// ["<c>trailing_delta</c>"] Trailing delta
        /// </summary>
        [JsonPropertyName("trailing_delta")]
        public decimal? TrailingDelta { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>transactions</c>"] Trades
        /// </summary>
        [JsonPropertyName("transactions")]
        public BitstampOrderTrade[]? Trades { get; set; }
    }
}

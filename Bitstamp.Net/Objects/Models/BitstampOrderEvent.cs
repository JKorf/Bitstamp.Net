using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Order event
    /// </summary>
    public record BitstampOrderEvent
    {
        /// <summary>
        /// ["<c>event</c>"] Order event
        /// </summary>
        [JsonPropertyName("event")]
        public OrderEvent Event { get; set; }
        /// <summary>
        /// ["<c>event_id</c>"] Event id
        /// </summary>
        [JsonPropertyName("event_id")]
        public string EventId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public OrderSource OrderSource { get; set; }
        /// <summary>
        /// ["<c>trade_account_id</c>"] Trade account id
        /// </summary>
        [JsonPropertyName("trade_account_id")]
        public long TradeAccountId { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public BitstampOrderEventData Data { get; set; } = null!;
    }

    /// <summary>
    /// Order event data
    /// </summary>
    public record BitstampOrderEventData
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order side
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>order_subtype</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_subtype")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>microtimestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("microtimestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>amount_traded</c>"] Quantity traded
        /// </summary>
        [JsonPropertyName("amount_traded")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>amount_at_create</c>"] Quantity at create
        /// </summary>
        [JsonPropertyName("amount_at_create")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>is_liquidation</c>"] Is liquidation
        /// </summary>
        [JsonPropertyName("is_liquidation")]
        public bool IsLiquidation { get; set; }
        /// <summary>
        /// ["<c>trade_account_id</c>"] Trade account id
        /// </summary>
        [JsonPropertyName("trade_account_id")]
        public long? TradeAccountId { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>stop_price</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
    }


}

using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// User trade info
    /// </summary>
    public record BitstampUserTrade
    {
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>self_trade_order_id</c>"] Self trade order id
        /// </summary>
        [JsonPropertyName("self_trade_order_id")]
        public long? SelfTradeOrderId { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>liquidation_fee</c>"] Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidation_fee")]
        public decimal? LiquidationFee { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public TradeType Type { get; set; }
        /// <summary>
        /// ["<c>self_trade_type</c>"] Self trade type
        /// </summary>
        [JsonPropertyName("self_trade_type")]
        public TradeType? SelfTradeType { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}

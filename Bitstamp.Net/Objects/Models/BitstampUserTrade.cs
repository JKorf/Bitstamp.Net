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
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Self trade order id
        /// </summary>
        [JsonPropertyName("self_trade_order_id")]
        public long? SelfTradeOrderId { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidation_fee")]
        public decimal? LiquidationFee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public TradeType Type { get; set; }
        /// <summary>
        /// Self trade type
        /// </summary>
        [JsonPropertyName("self_trade_type")]
        public TradeType? SelfTradeType { get; set; }
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
    }
}

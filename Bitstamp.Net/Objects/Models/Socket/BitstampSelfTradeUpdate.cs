using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order quantities offset by self-trading prevention, without a trade execution.
    /// </summary>
    public record BitstampSelfTradeUpdate
    {
        /// <summary>
        /// ["<c>buy_order_id</c>"] Buy order id.
        /// </summary>
        [JsonPropertyName("buy_order_id")]
        public long BuyOrderId { get; set; }

        /// <summary>
        /// ["<c>sell_order_id</c>"] Sell order id.
        /// </summary>
        [JsonPropertyName("sell_order_id")]
        public long SellOrderId { get; set; }

        /// <summary>
        /// ["<c>amount</c>"] Quantity prevented on each order.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>amount_str</c>"] Quantity represented as a string.
        /// </summary>
        [JsonPropertyName("amount_str")]
        public string QuantityString { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>price</c>"] Price at which the orders crossed.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>price_str</c>"] Price represented as a string.
        /// </summary>
        [JsonPropertyName("price_str")]
        public string PriceString { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>type</c>"] Order side (0 for buy, 1 for sell).
        /// </summary>
        [JsonPropertyName("type")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] Event time with second precision.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime TimestampSeconds { get; set; }

        /// <summary>
        /// ["<c>microtimestamp</c>"] Event time with microsecond precision.
        /// </summary>
        [JsonPropertyName("microtimestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>sellers_trade_account_id</c>"] Seller's trade account (0 for the main account).
        /// </summary>
        [JsonPropertyName("sellers_trade_account_id")]
        public long SellerAccountId { get; set; }

        /// <summary>
        /// ["<c>buyers_trade_account_id</c>"] Buyer's trade account (0 for the main account).
        /// </summary>
        [JsonPropertyName("buyers_trade_account_id")]
        public long BuyerAccountId { get; set; }
    }
}

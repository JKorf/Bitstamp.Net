using System.Text.Json.Serialization;
using Bitstamp.Net.Converters;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// User transaction
    /// </summary>
    [JsonConverter(typeof(UserTransactionConverter))]
    public record BitstampUserTransaction
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("tid")]
        public long Id { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction type
        /// </summary>
        [JsonPropertyName("side")]
        public TransactionType Type { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Sent quantity
        /// </summary>
        public decimal SentQuantity { get; set; }
        /// <summary>
        /// Sent asset
        /// </summary>
        public string SentAsset { get; set; } = string.Empty;
        /// <summary>
        /// Received quantity
        /// </summary>
        public decimal ReceivedQuantity { get; set; }
        /// <summary>
        /// Received asset
        /// </summary>
        public string ReceivedAsset { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public long? OrderId { get; set; }
        /// <summary>
        /// Self trade
        /// </summary>
        public bool SelfTrade { get; set; }
        /// <summary>
        /// Self trade order id
        /// </summary>
        public long? SelfTradeOrderId { get; set; }
    }
}

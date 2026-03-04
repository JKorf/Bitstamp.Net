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
    /// Crypto transactions
    /// </summary>
    public record BitstampCryptoTransactions
    {
        /// <summary>
        /// Ripple iou transactions
        /// </summary>
        [JsonPropertyName("ripple_iou_transactions")]
        public BitstampCryptoTransaction[] RippleIouTransactions { get; set; } = [];
        /// <summary>
        /// Deposits
        /// </summary>
        [JsonPropertyName("deposits")]
        public BitstampCryptoDeposit[] Deposits { get; set; } = [];
        /// <summary>
        /// Withdrawals
        /// </summary>
        [JsonPropertyName("withdrawals")]
        public BitstampCryptoTransaction[] Withdrawals { get; set; } = [];
    }

    /// <summary>
    /// Crypto transaction
    /// </summary>
    public record BitstampCryptoTransaction
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Destination address
        /// </summary>
        [JsonPropertyName("destinationAddress")]
        public string DestinationAddress { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Deposit
    /// </summary>
    public record BitstampCryptoDeposit
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Pending reason
        /// </summary>
        [JsonPropertyName("pending_reason")]
        public DepositPendingReason PendingReason { get; set; }
        /// <summary>
        /// Destination address
        /// </summary>
        [JsonPropertyName("destinationAddress")]
        public string DestinationAddress { get; set; } = string.Empty;
    }
}

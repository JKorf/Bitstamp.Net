using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Crypto transactions
    /// </summary>
    public record BitstampCryptoTransactions
    {
        /// <summary>
        /// ["<c>ripple_iou_transactions</c>"] Ripple iou transactions
        /// </summary>
        [JsonPropertyName("ripple_iou_transactions")]
        public BitstampCryptoTransaction[] RippleIouTransactions { get; set; } = [];
        /// <summary>
        /// ["<c>deposits</c>"] Deposits
        /// </summary>
        [JsonPropertyName("deposits")]
        public BitstampCryptoDeposit[] Deposits { get; set; } = [];
        /// <summary>
        /// ["<c>withdrawals</c>"] Withdrawals
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
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>destinationAddress</c>"] Destination address
        /// </summary>
        [JsonPropertyName("destinationAddress")]
        public string DestinationAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txid</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>datetime</c>"] Timestamp
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txid</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>datetime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>pending_reason</c>"] Pending reason
        /// </summary>
        [JsonPropertyName("pending_reason")]
        public DepositPendingReason PendingReason { get; set; }
        /// <summary>
        /// ["<c>destinationAddress</c>"] Destination address
        /// </summary>
        [JsonPropertyName("destinationAddress")]
        public string DestinationAddress { get; set; } = string.Empty;

        [JsonPropertyName("destination_address"), JsonInclude]
        internal string DestinationAddressInt { set { DestinationAddress = value; } }
    }
}

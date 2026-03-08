using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public record BitstampDepositAddress
    {
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>memo_id</c>"] Memo id
        /// </summary>
        [JsonPropertyName("memo_id")]
        public string? MemoId { get; set; }
        /// <summary>
        /// ["<c>destination_tag</c>"] Destination tag
        /// </summary>
        [JsonPropertyName("destination_tag")]
        public long? DestinationTag { get; set; }
        /// <summary>
        /// ["<c>transfer_id</c>"] Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public long? TransferId { get; set; }
    }


}

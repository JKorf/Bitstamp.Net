using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public record BitstampDepositAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Memo id
        /// </summary>
        [JsonPropertyName("memo_id")]
        public string? MemoId { get; set; }
        /// <summary>
        /// Destination tag
        /// </summary>
        [JsonPropertyName("destination_tag")]
        public long? DestinationTag { get; set; }
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public long? TransferId { get; set; }
    }


}

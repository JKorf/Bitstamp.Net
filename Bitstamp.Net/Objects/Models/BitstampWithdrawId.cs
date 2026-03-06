using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Withdraw id
    /// </summary>
    public record BitstampWithdrawId
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        [JsonPropertyName("withdrawal_id")]
        public long WithdrawalId { get; set; }

        [JsonPropertyName("id"), JsonInclude]
        internal long Id { set => WithdrawalId = value; }
    }
}

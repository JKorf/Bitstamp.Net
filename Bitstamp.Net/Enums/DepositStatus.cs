using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>FINALIZED</c>"] Finalized
        /// </summary>
        [Map("FINALIZED")]
        Finalized,
        /// <summary>
        /// ["<c>IN_PROCESSING</c>"] In progress
        /// </summary>
        [Map("IN_PROCESSING")]
        InProgress,
        /// <summary>
        /// ["<c>REVERTED</c>"] Reverted
        /// </summary>
        [Map("REVERTED")]
        Reverted,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
    }
}

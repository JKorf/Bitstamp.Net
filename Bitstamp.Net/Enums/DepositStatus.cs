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
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Finalized
        /// </summary>
        [Map("FINALIZED")]
        Finalized,
        /// <summary>
        /// In progress
        /// </summary>
        [Map("IN_PROCESSING")]
        InProgress,
        /// <summary>
        /// Reverted
        /// </summary>
        [Map("REVERTED")]
        Reverted,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
    }
}

using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("0")]
        Open,
        /// <summary>
        /// In process
        /// </summary>
        [Map("1")]
        InProcess,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("2")]
        Finished,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("3")]
        Canceled,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("4")]
        Failed,
        /// <summary>
        /// Reversed
        /// </summary>
        [Map("11")]
        Reversed
    }
}

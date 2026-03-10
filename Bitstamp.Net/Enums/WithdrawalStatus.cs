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
        /// ["<c>0</c>"] Open
        /// </summary>
        [Map("0")]
        Open,
        /// <summary>
        /// ["<c>1</c>"] In process
        /// </summary>
        [Map("1")]
        InProcess,
        /// <summary>
        /// ["<c>2</c>"] Finished
        /// </summary>
        [Map("2")]
        Finished,
        /// <summary>
        /// ["<c>3</c>"] Canceled
        /// </summary>
        [Map("3")]
        Canceled,
        /// <summary>
        /// ["<c>4</c>"] Failed
        /// </summary>
        [Map("4")]
        Failed,
        /// <summary>
        /// ["<c>11</c>"] Reversed
        /// </summary>
        [Map("11")]
        Reversed
    }
}

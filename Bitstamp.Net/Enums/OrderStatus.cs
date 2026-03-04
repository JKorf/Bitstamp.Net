using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("Open")]
        Open,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("Finished")]
        Finished,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("Expired")]
        Expired,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("Canceled")]
        Canceled,
        /// <summary>
        /// Pending cancel
        /// </summary>
        [Map("Cancel pending")]
        CancelPending
    }
}

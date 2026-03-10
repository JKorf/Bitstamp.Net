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
        /// ["<c>Open</c>"] Open
        /// </summary>
        [Map("Open")]
        Open,
        /// <summary>
        /// ["<c>Finished</c>"] Finished
        /// </summary>
        [Map("Finished")]
        Finished,
        /// <summary>
        /// ["<c>Expired</c>"] Expired
        /// </summary>
        [Map("Expired")]
        Expired,
        /// <summary>
        /// ["<c>Canceled</c>"] Canceled
        /// </summary>
        [Map("Canceled")]
        Canceled,
        /// <summary>
        /// ["<c>Cancel pending</c>"] Pending cancel
        /// </summary>
        [Map("Cancel pending")]
        CancelPending
    }
}

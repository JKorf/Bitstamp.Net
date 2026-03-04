using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderEvent>))]
    public enum OrderEvent
    {
        /// <summary>
        /// Order created
        /// </summary>
        [Map("order_created")]
        OrderCreated,
        /// <summary>
        /// Order replaced
        /// </summary>
        [Map("order_replaced")]
        OrderReplaced,
        /// <summary>
        /// Order changed
        /// </summary>
        [Map("order_changed")]
        OrderChanged,
        /// <summary>
        /// Order deleted
        /// </summary>
        [Map("order_deleted")]
        OrderDeleted,
        /// <summary>
        /// Stop active
        /// </summary>
        [Map("stop_active")]
        StopActive,
        /// <summary>
        /// Stop inactive
        /// </summary>
        [Map("stop_inactive")]
        StopInactive
    }
}

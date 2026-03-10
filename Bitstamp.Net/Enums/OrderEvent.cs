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
        /// ["<c>order_created</c>"] Order created
        /// </summary>
        [Map("order_created")]
        OrderCreated,
        /// <summary>
        /// ["<c>order_replaced</c>"] Order replaced
        /// </summary>
        [Map("order_replaced")]
        OrderReplaced,
        /// <summary>
        /// ["<c>order_changed</c>"] Order changed
        /// </summary>
        [Map("order_changed")]
        OrderChanged,
        /// <summary>
        /// ["<c>order_deleted</c>"] Order deleted
        /// </summary>
        [Map("order_deleted")]
        OrderDeleted,
        /// <summary>
        /// ["<c>stop_active</c>"] Stop active
        /// </summary>
        [Map("stop_active")]
        StopActive,
        /// <summary>
        /// ["<c>stop_inactive</c>"] Stop inactive
        /// </summary>
        [Map("stop_inactive")]
        StopInactive
    }
}

using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order source
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSource>))]
    public enum OrderSource
    {
        /// <summary>
        /// ["<c>orderbook</c>"] Orderbook/standard orders
        /// </summary>
        [Map("orderbook")]
        Orderbook,
        /// <summary>
        /// ["<c>stop_order</c>"] Stop orders
        /// </summary>
        [Map("stop_order")]
        StopOrder
    }
}

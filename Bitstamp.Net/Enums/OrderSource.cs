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
        /// Orderbook/standard orders
        /// </summary>
        [Map("orderbook")]
        Orderbook,
        /// <summary>
        /// Stop orders
        /// </summary>
        [Map("stop_order")]
        StopOrder
    }
}

using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginMode>))]
    public enum MarginMode
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}

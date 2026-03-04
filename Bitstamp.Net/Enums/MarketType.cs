using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Type of market
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarketType>))]
    public enum MarketType
    {
        /// <summary>
        /// Perpetual futures
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Spot
        /// </summary>
        [Map("SPOT")]
        Spot
    }
}

using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Settlement type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SettlementType>))]
    public enum SettlementType
    {
        /// <summary>
        /// Periodic settlement
        /// </summary>
        [Map("PERIODIC")]
        Periodic,
        /// <summary>
        /// Close settlement
        /// </summary>
        [Map("CLOSED")]
        Closed
    }
}

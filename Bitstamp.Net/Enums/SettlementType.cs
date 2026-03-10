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
        /// ["<c>PERIODIC</c>"] Periodic settlement
        /// </summary>
        [Map("PERIODIC")]
        Periodic,
        /// <summary>
        /// ["<c>CLOSED</c>"] Close settlement
        /// </summary>
        [Map("CLOSED")]
        Closed
    }
}

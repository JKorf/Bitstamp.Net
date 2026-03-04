using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

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
    /// Enabled status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EnabledStatus>))]
    public enum EnabledStatus
    {
        /// <summary>
        /// Enabled
        /// </summary>
        [Map("ENABLED")]
        Enabled,
        /// <summary>
        /// Disabled
        /// </summary>
        [Map("DISABLED")]
        Disabled
    }
}

using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

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

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
        /// ["<c>ENABLED</c>"] Enabled
        /// </summary>
        [Map("ENABLED")]
        Enabled,
        /// <summary>
        /// ["<c>DISABLED</c>"] Disabled
        /// </summary>
        [Map("DISABLED")]
        Disabled
    }
}

using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Period>))]
    public enum Period
    {
        /// <summary>
        /// Day
        /// </summary>
        [Map("day")]
        Day,
        /// <summary>
        /// Hour
        /// </summary>
        [Map("hour")]
        Hour,
        /// <summary>
        /// Minute
        /// </summary>
        [Map("minute")]
        Minute
    }
}

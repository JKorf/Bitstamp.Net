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
        /// ["<c>day</c>"] Day
        /// </summary>
        [Map("day")]
        Day,
        /// <summary>
        /// ["<c>hour</c>"] Hour
        /// </summary>
        [Map("hour")]
        Hour,
        /// <summary>
        /// ["<c>minute</c>"] Minute
        /// </summary>
        [Map("minute")]
        Minute
    }
}

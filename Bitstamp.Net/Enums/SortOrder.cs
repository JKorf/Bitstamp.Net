using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SortOrder>))]
    public enum SortOrder
    {
        /// <summary>
        /// Ascending order
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// Descending order
        /// </summary>
        [Map("desc")]
        Descending
    }
}

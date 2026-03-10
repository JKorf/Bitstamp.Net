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
        /// ["<c>asc</c>"] Ascending order
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// ["<c>desc</c>"] Descending order
        /// </summary>
        [Map("desc")]
        Descending
    }
}

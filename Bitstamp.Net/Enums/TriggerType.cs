using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// Last trade price
        /// </summary>
        [Map("LAST_TRADED_PRICE")]
        LastTradePrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("INDEX_PRICE")]
        IndexPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice
    }
}

using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("0", "BUY")]
        Buy = 0,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("1", "SELL")]
        Sell = 1,
        /// <summary>
        /// Self trade
        /// </summary>
        [Map("SELF")]
        SelfTrade,
    }
}

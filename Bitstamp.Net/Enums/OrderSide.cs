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
        /// ["<c>0</c>"] Buy
        /// </summary>
        [Map("0", "BUY")]
        Buy = 0,
        /// <summary>
        /// ["<c>1</c>"] Sell
        /// </summary>
        [Map("1", "SELL")]
        Sell = 1,
        /// <summary>
        /// ["<c>SELF</c>"] Self trade
        /// </summary>
        [Map("SELF")]
        SelfTrade,
    }
}

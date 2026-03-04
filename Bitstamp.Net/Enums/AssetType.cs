using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// Fiat
        /// </summary>
        [Map("fiat")]
        Fiat,
        /// <summary>
        /// Crypto
        /// </summary>
        [Map("crypto")]
        Crypto
    }
}

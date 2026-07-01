using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Asset class
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetClass>))]
    public enum AssetClass
    {
        /// <summary>
        /// ["<c>CRYPTO</c>"] Crypto
        /// </summary>
        [Map("CRYPTO")]
        Crypto,
        /// <summary>
        /// ["<c>COMMODITIES</c>"] Commodities
        /// </summary>
        [Map("COMMODITIES")]
        Commodities,
        /// <summary>
        /// ["<c>ETF</c>"] ETF
        /// </summary>
        [Map("ETF")]
        Etf,
        /// <summary>
        /// ["<c>FX</c>"] FX
        /// </summary>
        [Map("FX")]
        Fx
    }
}

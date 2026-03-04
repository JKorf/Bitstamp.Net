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
    /// Withdraw type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawType>))]
    public enum WithdrawType
    {
        /// <summary>
        /// Sepa
        /// </summary>
        [Map("sepa")]
        Sepa,
        /// <summary>
        /// International
        /// </summary>
        [Map("international")]
        International
    }
}

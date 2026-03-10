using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Withdraw type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawType>))]
    public enum WithdrawType
    {
        /// <summary>
        /// ["<c>sepa</c>"] Sepa
        /// </summary>
        [Map("sepa")]
        Sepa,
        /// <summary>
        /// ["<c>international</c>"] International
        /// </summary>
        [Map("international")]
        International
    }
}

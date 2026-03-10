using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Deposit pending reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositPendingReason>))]
    public enum DepositPendingReason
    {
        /// <summary>
        /// ["<c>ADDRESS_VERIFICATION_NEEDED</c>"] Address verification needed
        /// </summary>
        [Map("ADDRESS_VERIFICATION_NEEDED")]
        AddressVerificationNeeded,
        /// <summary>
        /// ["<c>ADDITIONAL_INFORMATION_MISSING</c>"] Additional information missing
        /// </summary>
        [Map("ADDITIONAL_INFORMATION_MISSING")]
        AdditionalInfoMissing,
    }
}

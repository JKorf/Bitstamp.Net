using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Collateral asset
    /// </summary>
    public record BitstampCollateralAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>haircut</c>"] Haircut
        /// </summary>
        [JsonPropertyName("haircut")]
        public decimal Haircut { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Collateral asset
    /// </summary>
    public record BitstampCollateralAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Haircut
        /// </summary>
        [JsonPropertyName("haircut")]
        public decimal Haircut { get; set; }
    }


}

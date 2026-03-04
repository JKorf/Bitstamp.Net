using Bitstamp.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Leverage setting
    /// </summary>
    public record BitstampLeverageSetting
    {
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage current
        /// </summary>
        [JsonPropertyName("leverage_current")]
        public decimal LeverageCurrent { get; set; }
        /// <summary>
        /// Leverage max
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal LeverageMax { get; set; }
    }


}

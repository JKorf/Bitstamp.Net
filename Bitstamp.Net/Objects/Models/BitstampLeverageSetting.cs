using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Leverage setting
    /// </summary>
    public record BitstampLeverageSetting
    {
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage_current</c>"] Leverage current
        /// </summary>
        [JsonPropertyName("leverage_current")]
        public decimal LeverageCurrent { get; set; }
        /// <summary>
        /// ["<c>leverage_max</c>"] Leverage max
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal LeverageMax { get; set; }
    }


}

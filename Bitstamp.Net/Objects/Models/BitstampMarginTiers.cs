using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Margin tiers
    /// </summary>
    public record BitstampMarginTiers
    {
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tiers</c>"] Tiers
        /// </summary>
        [JsonPropertyName("tiers")]
        public BitstampMarginTier[] Tiers { get; set; } = [];
    }

    /// <summary>
    /// Margin tier
    /// </summary>
    public record BitstampMarginTier
    {
        /// <summary>
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>size_limit_low</c>"] Quantity limit low
        /// </summary>
        [JsonPropertyName("size_limit_low")]
        public decimal QuantityLimitLow { get; set; }
        /// <summary>
        /// ["<c>size_limit_high</c>"] Quantity limit high
        /// </summary>
        [JsonPropertyName("size_limit_high")]
        public decimal QuantityLimitHigh { get; set; }
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>initial_margin_rate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initial_margin_rate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_rate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenance_margin_rate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>close_out_margin_rate</c>"] Close out margin rate
        /// </summary>
        [JsonPropertyName("close_out_margin_rate")]
        public decimal CloseOutMarginRate { get; set; }
        /// <summary>
        /// ["<c>initial_margin_previous_level_max</c>"] Initial margin previous level max
        /// </summary>
        [JsonPropertyName("initial_margin_previous_level_max")]
        public decimal InitialMarginPreviousLevelMax { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_previous_level_max</c>"] Maintenance margin previous level max
        /// </summary>
        [JsonPropertyName("maintenance_margin_previous_level_max")]
        public decimal MaintenanceMarginPreviousLevelMax { get; set; }
        /// <summary>
        /// ["<c>close_out_margin_previous_level_max</c>"] Close out margin previous level max
        /// </summary>
        [JsonPropertyName("close_out_margin_previous_level_max")]
        public decimal CloseOutMarginPreviousLevelMax { get; set; }
    }


}

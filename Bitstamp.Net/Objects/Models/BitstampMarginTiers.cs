using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Margin tiers
    /// </summary>
    public record BitstampMarginTiers
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Tiers
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
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Quantity limit low
        /// </summary>
        [JsonPropertyName("size_limit_low")]
        public decimal QuantityLimitLow { get; set; }
        /// <summary>
        /// Quantity limit high
        /// </summary>
        [JsonPropertyName("size_limit_high")]
        public decimal QuantityLimitHigh { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("initial_margin_rate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenance_margin_rate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Close out margin rate
        /// </summary>
        [JsonPropertyName("close_out_margin_rate")]
        public decimal CloseOutMarginRate { get; set; }
        /// <summary>
        /// Initial margin previous level max
        /// </summary>
        [JsonPropertyName("initial_margin_previous_level_max")]
        public decimal InitialMarginPreviousLevelMax { get; set; }
        /// <summary>
        /// Maintenance margin previous level max
        /// </summary>
        [JsonPropertyName("maintenance_margin_previous_level_max")]
        public decimal MaintenanceMarginPreviousLevelMax { get; set; }
        /// <summary>
        /// Close out margin previous level max
        /// </summary>
        [JsonPropertyName("close_out_margin_previous_level_max")]
        public decimal CloseOutMarginPreviousLevelMax { get; set; }
    }


}

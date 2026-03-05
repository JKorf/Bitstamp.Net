using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Margin info
    /// </summary>
    public record BitstampMarginInfo
    {
        /// <summary>
        /// Account margin
        /// </summary>
        [JsonPropertyName("account_margin")]
        public decimal AccountMargin { get; set; }
        /// <summary>
        /// Account margin available
        /// </summary>
        [JsonPropertyName("account_margin_available")]
        public decimal AccountMarginAvailable { get; set; }
        /// <summary>
        /// Account margin reserved
        /// </summary>
        [JsonPropertyName("account_margin_reserved")]
        public decimal AccountMarginReserved { get; set; }
        /// <summary>
        /// Account margin asset
        /// </summary>
        [JsonPropertyName("account_margin_currency")]
        public string AccountMarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BitstampMarginInfoAsset[] Assets { get; set; } = [];
        /// <summary>
        /// Initial margin ratio
        /// </summary>
        [JsonPropertyName("initial_margin_ratio")]
        public decimal InitialMarginRatio { get; set; }
        /// <summary>
        /// Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maintenance_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// Implied leverage
        /// </summary>
        [JsonPropertyName("implied_leverage")]
        public decimal ImpliedLeverage { get; set; }
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public record BitstampMarginInfoAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("total_amount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Reserved
        /// </summary>
        [JsonPropertyName("reserved")]
        public decimal Reserved { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
    }


}

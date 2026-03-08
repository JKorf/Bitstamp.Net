using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Margin info
    /// </summary>
    public record BitstampMarginInfo
    {
        /// <summary>
        /// ["<c>account_margin</c>"] Account margin
        /// </summary>
        [JsonPropertyName("account_margin")]
        public decimal AccountMargin { get; set; }
        /// <summary>
        /// ["<c>account_margin_available</c>"] Account margin available
        /// </summary>
        [JsonPropertyName("account_margin_available")]
        public decimal AccountMarginAvailable { get; set; }
        /// <summary>
        /// ["<c>account_margin_reserved</c>"] Account margin reserved
        /// </summary>
        [JsonPropertyName("account_margin_reserved")]
        public decimal AccountMarginReserved { get; set; }
        /// <summary>
        /// ["<c>account_margin_currency</c>"] Account margin asset
        /// </summary>
        [JsonPropertyName("account_margin_currency")]
        public string AccountMarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assets</c>"] Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BitstampMarginInfoAsset[] Assets { get; set; } = [];
        /// <summary>
        /// ["<c>initial_margin_ratio</c>"] Initial margin ratio
        /// </summary>
        [JsonPropertyName("initial_margin_ratio")]
        public decimal InitialMarginRatio { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_ratio</c>"] Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maintenance_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// ["<c>implied_leverage</c>"] Implied leverage
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
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total_amount</c>"] Total quantity
        /// </summary>
        [JsonPropertyName("total_amount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>reserved</c>"] Reserved
        /// </summary>
        [JsonPropertyName("reserved")]
        public decimal Reserved { get; set; }
        /// <summary>
        /// ["<c>margin_available</c>"] Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
    }


}

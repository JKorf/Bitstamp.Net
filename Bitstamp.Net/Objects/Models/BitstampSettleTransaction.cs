using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Settlement transaction
    /// </summary>
    public record BitstampSettleTransaction
    {
        /// <summary>
        /// ["<c>transaction_id</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_id</c>"] Position id
        /// </summary>
        [JsonPropertyName("position_id")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settlement_time</c>"] Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        public DateTime SettleTime { get; set; }
        /// <summary>
        /// ["<c>settlement_type</c>"] Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]
        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// ["<c>settlement_price</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market_type</c>"] Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
        /// <summary>
        /// ["<c>pnl_currency</c>"] Pnl asset
        /// </summary>
        [JsonPropertyName("pnl_currency")]
        public string PnlAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pnl_settled</c>"] Pnl settled
        /// </summary>
        [JsonPropertyName("pnl_settled")]
        public decimal PnlSettled { get; set; }
        /// <summary>
        /// ["<c>pnl_component_price</c>"] Pnl component price
        /// </summary>
        [JsonPropertyName("pnl_component_price")]
        public decimal PnlComponentPrice { get; set; }
        /// <summary>
        /// ["<c>pnl_component_fees</c>"] Pnl component fees
        /// </summary>
        [JsonPropertyName("pnl_component_fees")]
        public decimal PnlComponentFees { get; set; }
        /// <summary>
        /// ["<c>pnl_component_funding</c>"] Pnl component funding
        /// </summary>
        [JsonPropertyName("pnl_component_funding")]
        public decimal PnlComponentFunding { get; set; }
        /// <summary>
        /// ["<c>pnl_component_socialized_loss</c>"] Pnl component socialized loss
        /// </summary>
        [JsonPropertyName("pnl_component_socialized_loss")]
        public decimal PnlComponentSocializedLoss { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>strike_price</c>"] Strike price
        /// </summary>
        [JsonPropertyName("strike_price")]
        public decimal StrikePrice { get; set; }
        /// <summary>
        /// ["<c>fees_component_trading</c>"] Fees component trading
        /// </summary>
        [JsonPropertyName("fees_component_trading")]
        public decimal FeesComponentTrading { get; set; }
        /// <summary>
        /// ["<c>fees_component_liquidation</c>"] Fees component liquidation
        /// </summary>
        [JsonPropertyName("fees_component_liquidation")]
        public decimal FeesComponentLiquidation { get; set; }
    }


}

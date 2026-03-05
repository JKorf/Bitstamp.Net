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
        /// Transaction id
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("position_id")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        public DateTime SettleTime { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]
        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
        /// <summary>
        /// Pnl asset
        /// </summary>
        [JsonPropertyName("pnl_currency")]
        public string PnlAsset { get; set; } = string.Empty;
        /// <summary>
        /// Pnl settled
        /// </summary>
        [JsonPropertyName("pnl_settled")]
        public decimal PnlSettled { get; set; }
        /// <summary>
        /// Pnl component price
        /// </summary>
        [JsonPropertyName("pnl_component_price")]
        public decimal PnlComponentPrice { get; set; }
        /// <summary>
        /// Pnl component fees
        /// </summary>
        [JsonPropertyName("pnl_component_fees")]
        public decimal PnlComponentFees { get; set; }
        /// <summary>
        /// Pnl component funding
        /// </summary>
        [JsonPropertyName("pnl_component_funding")]
        public decimal PnlComponentFunding { get; set; }
        /// <summary>
        /// Pnl component socialized loss
        /// </summary>
        [JsonPropertyName("pnl_component_socialized_loss")]
        public decimal PnlComponentSocializedLoss { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Strike price
        /// </summary>
        [JsonPropertyName("strike_price")]
        public decimal StrikePrice { get; set; }
        /// <summary>
        /// Fees component trading
        /// </summary>
        [JsonPropertyName("fees_component_trading")]
        public decimal FeesComponentTrading { get; set; }
        /// <summary>
        /// Fees component liquidation
        /// </summary>
        [JsonPropertyName("fees_component_liquidation")]
        public decimal FeesComponentLiquidation { get; set; }
    }


}

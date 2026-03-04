using Bitstamp.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public record BitstampPosition
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType? MarketType { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Settlement asset
        /// </summary>
        [JsonPropertyName("settlement_currency")]
        public string SettlementAsset { get; set; } = string.Empty;
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Pnl percentage
        /// </summary>
        [JsonPropertyName("pnl_percentage")]
        public decimal PnlPercentage { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("pnl_realized")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Pnl settled since inception
        /// </summary>
        [JsonPropertyName("pnl_settled_since_inception")]
        public decimal PnlSettledSinceInception { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// Cumulative price profit and loss
        /// </summary>
        [JsonPropertyName("cumulative_price_pnl")]
        public decimal CumulativePricePnl { get; set; }
        /// <summary>
        /// Cumulative trading fees
        /// </summary>
        [JsonPropertyName("cumulative_trading_fees")]
        public decimal CumulativeTradingFees { get; set; }
        /// <summary>
        /// Cumulative liquidation fees
        /// </summary>
        [JsonPropertyName("cumulative_liquidation_fees")]
        public decimal CumulativeLiquidationFees { get; set; }
        /// <summary>
        /// Cumulative funding
        /// </summary>
        [JsonPropertyName("cumulative_funding")]
        public decimal CumulativeFunding { get; set; }
        /// <summary>
        /// Cumulative socialized loss
        /// </summary>
        [JsonPropertyName("cumulative_socialized_loss")]
        public decimal CumulativeSocializedLoss { get; set; }
        /// <summary>
        /// Open quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("pnl_unrealized")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Profit and loss in settlement
        /// </summary>
        [JsonPropertyName("pnl_in_settlement")]
        public decimal PnlInSettlement { get; set; }
        /// <summary>
        /// Implied leverage
        /// </summary>
        [JsonPropertyName("implied_leverage")]
        public decimal ImpliedLeverage { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Initial margin ratio
        /// </summary>
        [JsonPropertyName("initial_margin_ratio")]
        public decimal InitialMarginRatio { get; set; }
        /// <summary>
        /// Current margin
        /// </summary>
        [JsonPropertyName("current_margin")]
        public decimal CurrentMargin { get; set; }
        /// <summary>
        /// Collateral reserved
        /// </summary>
        [JsonPropertyName("collateral_reserved")]
        public decimal CollateralReserved { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maintenance_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// Estimated liquidation price
        /// </summary>
        [JsonPropertyName("estimated_liquidation_price")]
        public decimal EstimatedLiquidationPrice { get; set; }
        /// <summary>
        /// Estimated closing fee quantity
        /// </summary>
        [JsonPropertyName("estimated_closing_fee_amount")]
        public decimal EstimatedClosingFeeQuantity { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Current value
        /// </summary>
        [JsonPropertyName("current_value")]
        public decimal CurrentValue { get; set; }
        /// <summary>
        /// Entry value
        /// </summary>
        [JsonPropertyName("entry_value")]
        public decimal EntryValue { get; set; }
        /// <summary>
        /// Strike price
        /// </summary>
        [JsonPropertyName("strike_price")]
        public decimal StrikePrice { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide Side { get; set; }
        /// <summary>
        /// Margin tier
        /// </summary>
        [JsonPropertyName("margin_tier")]
        public string MarginTier { get; set; } = string.Empty;
    }


}

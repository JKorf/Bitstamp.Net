using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public record BitstampPosition
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market_type</c>"] Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType? MarketType { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>settlement_currency</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settlement_currency")]
        public string SettlementAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>entry_price</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>pnl_percentage</c>"] Pnl percentage
        /// </summary>
        [JsonPropertyName("pnl_percentage")]
        public decimal PnlPercentage { get; set; }
        /// <summary>
        /// ["<c>pnl_realized</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("pnl_realized")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>pnl_settled_since_inception</c>"] Pnl settled since inception
        /// </summary>
        [JsonPropertyName("pnl_settled_since_inception")]
        public decimal PnlSettledSinceInception { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>pnl</c>"] Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// ["<c>cumulative_price_pnl</c>"] Cumulative price profit and loss
        /// </summary>
        [JsonPropertyName("cumulative_price_pnl")]
        public decimal CumulativePricePnl { get; set; }
        /// <summary>
        /// ["<c>cumulative_trading_fees</c>"] Cumulative trading fees
        /// </summary>
        [JsonPropertyName("cumulative_trading_fees")]
        public decimal CumulativeTradingFees { get; set; }
        /// <summary>
        /// ["<c>cumulative_liquidation_fees</c>"] Cumulative liquidation fees
        /// </summary>
        [JsonPropertyName("cumulative_liquidation_fees")]
        public decimal CumulativeLiquidationFees { get; set; }
        /// <summary>
        /// ["<c>cumulative_funding</c>"] Cumulative funding
        /// </summary>
        [JsonPropertyName("cumulative_funding")]
        public decimal CumulativeFunding { get; set; }
        /// <summary>
        /// ["<c>cumulative_socialized_loss</c>"] Cumulative socialized loss
        /// </summary>
        [JsonPropertyName("cumulative_socialized_loss")]
        public decimal CumulativeSocializedLoss { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Open quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>pnl_unrealized</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("pnl_unrealized")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>pnl_in_settlement</c>"] Profit and loss in settlement
        /// </summary>
        [JsonPropertyName("pnl_in_settlement")]
        public decimal PnlInSettlement { get; set; }
        /// <summary>
        /// ["<c>implied_leverage</c>"] Implied leverage
        /// </summary>
        [JsonPropertyName("implied_leverage")]
        public decimal ImpliedLeverage { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>initial_margin_ratio</c>"] Initial margin ratio
        /// </summary>
        [JsonPropertyName("initial_margin_ratio")]
        public decimal InitialMarginRatio { get; set; }
        /// <summary>
        /// ["<c>current_margin</c>"] Current margin
        /// </summary>
        [JsonPropertyName("current_margin")]
        public decimal CurrentMargin { get; set; }
        /// <summary>
        /// ["<c>collateral_reserved</c>"] Collateral reserved
        /// </summary>
        [JsonPropertyName("collateral_reserved")]
        public decimal CollateralReserved { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_ratio</c>"] Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maintenance_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// ["<c>estimated_liquidation_price</c>"] Estimated liquidation price
        /// </summary>
        [JsonPropertyName("estimated_liquidation_price")]
        public decimal EstimatedLiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>estimated_closing_fee_amount</c>"] Estimated closing fee quantity
        /// </summary>
        [JsonPropertyName("estimated_closing_fee_amount")]
        public decimal EstimatedClosingFeeQuantity { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>current_value</c>"] Current value
        /// </summary>
        [JsonPropertyName("current_value")]
        public decimal CurrentValue { get; set; }
        /// <summary>
        /// ["<c>entry_value</c>"] Entry value
        /// </summary>
        [JsonPropertyName("entry_value")]
        public decimal EntryValue { get; set; }
        /// <summary>
        /// ["<c>strike_price</c>"] Strike price
        /// </summary>
        [JsonPropertyName("strike_price")]
        public decimal StrikePrice { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide Side { get; set; }
        /// <summary>
        /// ["<c>margin_tier</c>"] Margin tier
        /// </summary>
        [JsonPropertyName("margin_tier")]
        public string MarginTier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time_opened</c>"] Open time
        /// </summary>
        [JsonPropertyName("time_opened")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ["<c>time_closed</c>"] Close time
        /// </summary>
        [JsonPropertyName("time_closed")]
        public DateTime? CloseTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public PositionStatus? Status { get; set; }
        /// <summary>
        /// ["<c>exit_price</c>"] Exit price
        /// </summary>
        [JsonPropertyName("exit_price")]
        public decimal? ExitPrice { get; set; }
        /// <summary>
        /// ["<c>settlement_price</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal? SettlementPrice { get; set; }
        /// <summary>
        /// ["<c>amount_delta</c>"] Quantity delta
        /// </summary>
        [JsonPropertyName("amount_delta")]
        public decimal? QuantityDelta { get; set; }
        /// <summary>
        /// ["<c>pnl_currency</c>"] Profit and loss asset
        /// </summary>
        [JsonPropertyName("pnl_currency")]
        public string? PnlAsset { get; set; }
        /// <summary>
        /// ["<c>pnl_settled</c>"] Settled profit and loss
        /// </summary>
        [JsonPropertyName("pnl_settled")]
        public decimal? Settled { get; set; }
        /// <summary>
        /// ["<c>closing_fee_amount</c>"] Closing fee
        /// </summary>
        [JsonPropertyName("closing_fee_amount")]
        public decimal? ClosingFee { get; set; }
    }


}

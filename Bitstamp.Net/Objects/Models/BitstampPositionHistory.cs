using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Position history
    /// </summary>
    public record BitstampPositionHistory
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
        public MarketType MarketType { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>pnl_currency</c>"] Profit and loss asset
        /// </summary>
        [JsonPropertyName("pnl_currency")]
        public string PnlAsset { get; set; } = string.Empty;
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
        /// ["<c>pnl_settled</c>"] Settled profit and loss
        /// </summary>
        [JsonPropertyName("pnl_settled")]
        public decimal Settled { get; set; }
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
        /// ["<c>cumulative_price_pnl</c>"] Cumulative price pnl
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
        /// ["<c>amount_delta</c>"] Quantity delta
        /// </summary>
        [JsonPropertyName("amount_delta")]
        public decimal QuantityDelta { get; set; }
        /// <summary>
        /// ["<c>time_opened</c>"] Open time
        /// </summary>
        [JsonPropertyName("time_opened")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>time_closed</c>"] Close time
        /// </summary>
        [JsonPropertyName("time_closed")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public PositionStatus Status { get; set; }
        /// <summary>
        /// ["<c>exit_price</c>"] Exit price
        /// </summary>
        [JsonPropertyName("exit_price")]
        public decimal ExitPrice { get; set; }
        /// <summary>
        /// ["<c>settlement_price</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
    }


}

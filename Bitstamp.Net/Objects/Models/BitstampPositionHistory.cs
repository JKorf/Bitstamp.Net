using Bitstamp.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Position history
    /// </summary>
    public record BitstampPositionHistory
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
        public MarketType MarketType { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Profit and loss asset
        /// </summary>
        [JsonPropertyName("pnl_currency")]
        public string PnlAsset { get; set; } = string.Empty;
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
        /// Settled profit and loss
        /// </summary>
        [JsonPropertyName("pnl_settled")]
        public decimal Settled { get; set; }
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
        /// Cumulative price pnl
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
        /// Quantity delta
        /// </summary>
        [JsonPropertyName("amount_delta")]
        public decimal QuantityDelta { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("time_opened")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonPropertyName("time_closed")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public PositionStatus Status { get; set; }
        /// <summary>
        /// Exit price
        /// </summary>
        [JsonPropertyName("exit_price")]
        public decimal ExitPrice { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
    }


}

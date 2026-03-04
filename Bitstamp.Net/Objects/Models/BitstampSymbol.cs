using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public record BitstampSymbol
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("market_symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset decimal places
        /// </summary>
        [JsonPropertyName("base_decimals")]
        public int BaseDecimals { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("counter_currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset decimal places
        /// </summary>
        [JsonPropertyName("counter_decimals")]
        public int QuoteDecimals { get; set; }
        /// <summary>
        /// Min order value
        /// </summary>
        [JsonPropertyName("minimum_order_value")]
        public decimal MinimumOrderValue { get; set; }
        /// <summary>
        /// Max order value
        /// </summary>
        [JsonPropertyName("maximum_order_value")]
        public decimal MaximumOrderValue { get; set; }
        /// <summary>
        /// Min order quantity
        /// </summary>
        [JsonPropertyName("minimum_order_amount")]
        public decimal MinimumOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("maximum_order_amount")]
        public decimal MaximumOrderQuantity { get; set; }
        /// <summary>
        /// Symbol status
        /// </summary>
        [JsonPropertyName("trading")]
        public EnabledStatus Status { get; set; }
        /// <summary>
        /// Instant buy/sell quote asset decimal places
        /// </summary>
        [JsonPropertyName("instant_order_counter_decimals")]
        public int InstantOrderCounterDecimals { get; set; }
        /// <summary>
        /// Status of whether instance/market orders are allowanced
        /// </summary>
        [JsonPropertyName("instant_and_market_orders")]
        public EnabledStatus InstantAndMarketOrderStatus { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
        /// <summary>
        /// Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_asset")]
        public string? UnderlyingAsset { get; set; }
        /// <summary>
        /// Payoff type
        /// </summary>
        [JsonPropertyName("payoff_type")]
        public string? PayoffType { get; set; }
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal? ContractSize { get; set; }
        /// <summary>
        /// International Securities Identification Number for derivatives
        /// </summary>
        [JsonPropertyName("isin")]
        public string? Isin { get; set; }
    }
}

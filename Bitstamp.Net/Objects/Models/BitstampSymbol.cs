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
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market_symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market_symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_currency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_decimals</c>"] Base asset decimal places
        /// </summary>
        [JsonPropertyName("base_decimals")]
        public int BaseDecimals { get; set; }
        /// <summary>
        /// ["<c>counter_currency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("counter_currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>counter_decimals</c>"] Quote asset decimal places
        /// </summary>
        [JsonPropertyName("counter_decimals")]
        public int QuoteDecimals { get; set; }
        /// <summary>
        /// ["<c>minimum_order_value</c>"] Min order value
        /// </summary>
        [JsonPropertyName("minimum_order_value")]
        public decimal MinimumOrderValue { get; set; }
        /// <summary>
        /// ["<c>maximum_order_value</c>"] Max order value
        /// </summary>
        [JsonPropertyName("maximum_order_value")]
        public decimal MaximumOrderValue { get; set; }
        /// <summary>
        /// ["<c>minimum_order_amount</c>"] Min order quantity
        /// </summary>
        [JsonPropertyName("minimum_order_amount")]
        public decimal MinimumOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maximum_order_amount</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("maximum_order_amount")]
        public decimal MaximumOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>trading</c>"] Symbol status
        /// </summary>
        [JsonPropertyName("trading")]
        public EnabledStatus Status { get; set; }
        /// <summary>
        /// ["<c>instant_order_counter_decimals</c>"] Instant buy/sell quote asset decimal places
        /// </summary>
        [JsonPropertyName("instant_order_counter_decimals")]
        public int InstantOrderCounterDecimals { get; set; }
        /// <summary>
        /// ["<c>instant_and_market_orders</c>"] Status of whether instance/market orders are allowanced
        /// </summary>
        [JsonPropertyName("instant_and_market_orders")]
        public EnabledStatus InstantAndMarketOrderStatus { get; set; }
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>market_type</c>"] Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
        /// <summary>
        /// ["<c>underlying_asset</c>"] Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_asset")]
        public string? UnderlyingAsset { get; set; }
        /// <summary>
        /// ["<c>payoff_type</c>"] Payoff type
        /// </summary>
        [JsonPropertyName("payoff_type")]
        public string? PayoffType { get; set; }
        /// <summary>
        /// ["<c>contract_size</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal? ContractSize { get; set; }
        /// <summary>
        /// ["<c>isin</c>"] International Securities Identification Number for derivatives
        /// </summary>
        [JsonPropertyName("isin")]
        public string? Isin { get; set; }
    }
}

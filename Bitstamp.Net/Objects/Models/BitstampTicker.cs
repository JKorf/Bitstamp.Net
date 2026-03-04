using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    public record BitstampTicker
    {
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// 24h high price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24h low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Day open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// 24h open price
        /// </summary>
        [JsonPropertyName("open_24")]
        public decimal? OpenPrice24Hrs { get; set; }
        /// <summary>
        /// Asset pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("market")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Price percentage change 24h
        /// </summary>
        [JsonPropertyName("percent_change_24")]
        public decimal? PercentageChange24Hrs { get; set; }
        /// <summary>
        /// Last trade side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Last hour volume weighted average price
        /// </summary>
        [JsonPropertyName("vwap")]
        public decimal Vwap { get; set; }
        /// <summary>
        /// Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal? OpenInterestValue { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
    }
}

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
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>high</c>"] 24h high price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] 24h low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>last</c>"] Last price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>open</c>"] Day open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>open_24</c>"] 24h open price
        /// </summary>
        [JsonPropertyName("open_24")]
        public decimal? OpenPrice24Hrs { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Asset pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("market")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>percent_change_24</c>"] Price percentage change 24h
        /// </summary>
        [JsonPropertyName("percent_change_24")]
        public decimal? PercentageChange24Hrs { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Last trade side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>vwap</c>"] Last hour volume weighted average price
        /// </summary>
        [JsonPropertyName("vwap")]
        public decimal Vwap { get; set; }
        /// <summary>
        /// ["<c>open_interest_value</c>"] Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal? OpenInterestValue { get; set; }
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>market_type</c>"] Market type
        /// </summary>
        [JsonPropertyName("market_type")]
        public MarketType MarketType { get; set; }
    }
}

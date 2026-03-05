using System.Text.Json.Serialization;
using Bitstamp.Net.Converters;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Order trade
    /// </summary>
    [JsonConverter(typeof(OrderTradeConverter))]
    public record BitstampOrderTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tid")]
        public long TradeId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Quantities
        /// </summary>
        [JsonPropertyName("amounts")]
        public Dictionary<string, decimal> Quantities { get; set; } = new(2);
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonIgnore]
        public decimal BaseQuantity => Price > 1 ? Quantities.OrderBy(x => x.Value).First().Value : Quantities.OrderByDescending(x => x.Value).First().Value;
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonIgnore]
        public decimal QuoteQuantity => Price > 1 ? Quantities.OrderByDescending(x => x.Value).First().Value : Quantities.OrderBy(x => x.Value).First().Value;
    }
}

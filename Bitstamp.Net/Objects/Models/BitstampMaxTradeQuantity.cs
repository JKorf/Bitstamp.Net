using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Max trade quantity
    /// </summary>
    public record BitstampMaxTradeQuantity
    {
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("maximum_order_amount")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Max order value
        /// </summary>
        [JsonPropertyName("maximum_order_value")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// Asset the max quantity is in
        /// </summary>
        [JsonPropertyName("maximum_order_amount_currency")]
        public string MaxOrderQuantityAsset { get; set; } = string.Empty;
        /// <summary>
        /// Asset the max value is in
        /// </summary>
        [JsonPropertyName("maximum_order_value_currency")]
        public string MaxOrderValueAsset { get; set; } = string.Empty;
    }
}

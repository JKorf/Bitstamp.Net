using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Max trade quantity
    /// </summary>
    public record BitstampMaxTradeQuantity
    {
        /// <summary>
        /// ["<c>maximum_order_amount</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("maximum_order_amount")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maximum_order_value</c>"] Max order value
        /// </summary>
        [JsonPropertyName("maximum_order_value")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// ["<c>maximum_order_amount_currency</c>"] Asset the max quantity is in
        /// </summary>
        [JsonPropertyName("maximum_order_amount_currency")]
        public string MaxOrderQuantityAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maximum_order_value_currency</c>"] Asset the max value is in
        /// </summary>
        [JsonPropertyName("maximum_order_value_currency")]
        public string MaxOrderValueAsset { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Conversion rate
    /// </summary>
    public record BitstampConversionRate
    {
        /// <summary>
        /// Buy price
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal BuyPrice { get; set; }
        /// <summary>
        /// Sell price
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal SellPrice { get; set; }
    }
}

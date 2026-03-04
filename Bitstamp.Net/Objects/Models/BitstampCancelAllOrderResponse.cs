using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Cancel all orders response
    /// </summary>
    public record BitstampCancelAllOrderResponse
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Canceled
        /// </summary>
        [JsonPropertyName("canceled")]
        public BitstampCancelOrderResponse[] Canceled { get; set; } = [];
    }
}

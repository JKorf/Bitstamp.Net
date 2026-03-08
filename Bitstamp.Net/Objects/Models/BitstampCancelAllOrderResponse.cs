using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Cancel all orders response
    /// </summary>
    public record BitstampCancelAllOrderResponse
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [JsonPropertyName("canceled")]
        public BitstampCancelOrderResponse[] Canceled { get; set; } = [];
    }
}

using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Close positions response
    /// </summary>
    public record BitstampClosePositionsResponse
    {
        /// <summary>
        /// Closed positions
        /// </summary>
        [JsonPropertyName("closed")]
        public BitstampPosition[] Closed { get; set; } = [];
        /// <summary>
        /// Failed
        /// </summary>
        [JsonPropertyName("failed")]
        public BitstampPosition[] Failed { get; set; } = [];
    }

}

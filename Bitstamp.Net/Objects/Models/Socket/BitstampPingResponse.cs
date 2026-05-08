using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    /// <summary>
    /// Result of a ping request
    /// </summary>
    internal class BitstampPingResponse
    {
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Account tradable symbol
    /// </summary>
    public record BitstampAccountSymbol
    {
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url_symbol</c>"] Url symbol
        /// </summary>
        [JsonPropertyName("url_symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}

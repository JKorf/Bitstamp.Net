using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Account tradable symbol
    /// </summary>
    public record BitstampAccountSymbol
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Url symbol
        /// </summary>
        [JsonPropertyName("url_symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}

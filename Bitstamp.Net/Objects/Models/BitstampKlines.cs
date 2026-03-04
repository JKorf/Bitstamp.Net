using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    internal record BitstampKlinesResult
    {
        [JsonPropertyName("data")]
        public BitstampKlines Data { get; set; } = null!;
    }

    internal record BitstampKlines
    {
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        [JsonPropertyName("ohlc")]
        public BitstampKline[] KLines { get; set; } = [];
    }
}

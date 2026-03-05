using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    internal record BitstampSocketAuthToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("valid_sec")]
        public int ValidSeconds { get; set; }
    }
}

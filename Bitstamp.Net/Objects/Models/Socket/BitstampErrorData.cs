using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    public record BitstampErrorData
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}

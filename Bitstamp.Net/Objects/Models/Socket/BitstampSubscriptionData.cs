using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models.Socket
{
    public record BitstampSubscriptionData
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("auth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }

        [JsonPropertyName("code"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Code { get; set; }

        [JsonPropertyName("message"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; } = string.Empty;

        public BitstampSubscriptionData(string channel, string? token = null)
        {
            Channel = channel;
            Token = token;
        }
    }
}

using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;

namespace Bitstamp.Net.Objects.Models.Socket
{
    internal record BitstampSocketData<T>
    {
        [JsonPropertyName("event")]
        public SocketEventType Event { get; set; }

        [JsonPropertyName("channel"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Channel { get; set; }

        [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public BitstampSocketData()
        { }

        public BitstampSocketData(SocketEventType type, T? data)
        {
            Event = type;
            Data = data;
        }
    }
}

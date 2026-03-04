using System.Text.Json;
using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;

namespace Bitstamp.Net.Converters
{
    internal class OrderTradeConverter : JsonConverter<BitstampOrderTrade>
    {
        public override BitstampOrderTrade? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var tradeId = root.GetProperty("tid").GetInt64()!;
                var priceString = root.GetProperty("price").GetString();
                var price = decimal.Parse(priceString ?? "0", System.Globalization.CultureInfo.InvariantCulture);
                var feeString = root.GetProperty("fee").GetString();
                var fee = decimal.Parse(feeString ?? "0", System.Globalization.CultureInfo.InvariantCulture);
                var timestampString = root.GetProperty("datetime").GetString();
                var timestamp = DateTime.Parse(timestampString ?? "", System.Globalization.CultureInfo.InvariantCulture);

                var firstAmountName = root.EnumerateObject().ElementAt(5).Name;
                var firstAmountString = root.EnumerateObject().ElementAt(5).Value.GetString();
                var firstAmountValue = decimal.Parse(firstAmountString ?? "0", System.Globalization.CultureInfo.InvariantCulture);
                var secondAmountName = root.EnumerateObject().ElementAt(6).Name;
                var secondAmountString = root.EnumerateObject().ElementAt(6).Value.GetString();
                var secondAmountValue = decimal.Parse(secondAmountString ?? "0", System.Globalization.CultureInfo.InvariantCulture);

                var amounts = new Dictionary<string, decimal>
                {
                    { firstAmountName,firstAmountValue },
                    { secondAmountName,secondAmountValue }
                };
                return new BitstampOrderTrade
                {
                    TradeId = tradeId,
                    Price = price,
                    Fee = fee,
                    Timestamp = timestamp,
                    Quantities = amounts
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, BitstampOrderTrade value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}

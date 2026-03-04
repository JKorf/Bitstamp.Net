using System.Text.Json;
using System.Text.Json.Serialization;
using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;

namespace Bitstamp.Net.Converters
{
    internal class UserTransactionConverter : JsonConverter<BitstampUserTransaction>
    {
        private decimal ParseDecimalValue(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.String)
            {
                var valueString = element.GetString();
                if (valueString is null or "None")
                    return 0m;
                else
                    return ExchangeHelpers.ParseDecimal(valueString) ?? 0;
            }
            else
            {
                return element.GetDecimal();
            }
        }

        public override BitstampUserTransaction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var id = root.GetProperty("id").GetInt64()!;
                var datetime = DateTime.SpecifyKind(DateTime.Parse(root.GetProperty("datetime").GetString()!), DateTimeKind.Utc);
                var type = Enum.Parse<TransactionType>(root.GetProperty("type").GetString()!);
                var fee = ParseDecimalValue(root.GetProperty("fee"));
                var assetA = root.EnumerateObject().ElementAt(4).Name;
                var amountA = ParseDecimalValue(root.EnumerateObject().ElementAt(4).Value);
                var assetB = root.EnumerateObject().ElementAt(5).Name;
                var amountB = ParseDecimalValue(root.EnumerateObject().ElementAt(5).Value);
                var symbol = root.EnumerateObject().ElementAt(6).Name;
                var price = ParseDecimalValue(root.EnumerateObject().ElementAt(6).Value);

                var orderId = root.TryGetProperty("order_id", out var orderIdProp) ? orderIdProp.GetInt64() : (long?)null;
                var selfTrade = root.TryGetProperty("self_trade", out var selfTradeProp) ? selfTradeProp.GetBoolean() : false;
                var selfTradeOrderId = root.TryGetProperty("self_trade_order_id", out var selfTradeOrderIdElement) ? (long?)selfTradeOrderIdElement.GetInt64() : null;

                var result = new BitstampUserTransaction()
                {
                    Id = id,
                    Timestamp = datetime,
                    Type = type,
                    Fee = fee,
                    Symbol = symbol,
                    Price = price,
                    OrderId = orderId,
                    SelfTrade = selfTrade,
                    SelfTradeOrderId = selfTradeOrderId
                };

                if (amountA < 0)
                {
                    result.SentAsset = assetA;
                    result.SentQuantity = amountA;
                    result.ReceivedAsset = assetB;
                    result.ReceivedQuantity = amountB;
                }
                else if (amountA > 0)
                {
                    result.SentAsset = assetB;
                    result.SentQuantity = amountB;
                    result.ReceivedAsset = assetA;
                    result.ReceivedQuantity = amountA;
                }
                else
                {

                }

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, BitstampUserTransaction value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
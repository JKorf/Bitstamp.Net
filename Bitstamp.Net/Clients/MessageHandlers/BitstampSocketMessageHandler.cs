using System.Text.Json;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Bitstamp.Net.Clients.MessageHandlers
{
    internal class BitstampSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitstampExchange._serializerContext);

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [
            
            // "bts:x" events with no channel, e.g. bts:request_reconnect or bts:error
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel").WithCustomConstraint(c => string.IsNullOrEmpty(c)),
                    new PropertyFieldReference("event").WithStartsWithConstraint("bts")],
                TypeIdentifierCallback = x => x.FieldValue("event")!,
            },

            // subscription updates
             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("event").WithCustomConstraint(c => c?.StartsWith("bts") != true)],
                TypeIdentifierCallback = x => x.FieldValue("channel")!,
            },

            // query responses
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("event").WithStartsWithConstraint("bts")],
                TypeIdentifierCallback = x => "query_" + x.FieldValue("channel")!,
            },
        ];
    }
}

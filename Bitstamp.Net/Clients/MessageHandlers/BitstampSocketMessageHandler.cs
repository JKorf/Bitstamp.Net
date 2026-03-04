using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Bitstamp.Net.Clients.MessageHandlers
{
    internal class BitstampSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitstampExchange._serializerContext);

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [
             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("event").WithCustomConstraint(c => c?.StartsWith("bts") != true)],
                TypeIdentifierCallback = x => x.FieldValue("channel")!,
            },
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("event").WithStartsWithConstraint("bts")],
                TypeIdentifierCallback = x => "query_" + x.FieldValue("channel")!,
            },
        ];
    }
}

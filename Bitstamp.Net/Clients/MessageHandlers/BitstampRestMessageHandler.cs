using System.Net.Http.Headers;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;

namespace Bitstamp.Net.Clients.MessageHandlers
{
    internal class BitstampRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;
        public override bool RequiresSeekableStream => true;
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitstampExchange._serializerContext);

        public BitstampRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        private Error ParseErrorInternal(JsonElement rootElement)
        {
            var code = (rootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetString() : null)
                ?? (rootElement.TryGetProperty("response_code", out var responseCodeProp) ? responseCodeProp.GetString() : null)
                ?? "?";

            string? reason = null;
            if (rootElement.TryGetProperty("reason", out var reasonProp))
            {
                if (reasonProp.ValueKind is JsonValueKind.Object)
                {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
                    var allErrors = reasonProp.Deserialize<Dictionary<string, string[]>>(Options);
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
                    if (allErrors != null)
                    {
                        if (allErrors.Count == 1 && allErrors.TryGetValue("__all__", out var genericErrors))
                            // Single error
                            reason = String.Join("; ", genericErrors);
                        else
                            // Multiple errors
                            reason = String.Join("; ", allErrors.Select(e => e.Key + ": " + String.Join(",", e.Value)));
                    }
                }
                else
                {
                    reason = reasonProp.GetRawText();
                }
            }

            reason ??= (rootElement.TryGetProperty("error", out var errorProp) ? errorProp.GetString() : null);
            reason ??= (rootElement.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : null);
            reason ??= (rootElement.TryGetProperty("errors", out var errorsProp) ? errorsProp.GetRawText() : null);

            return new ServerError(code, _errorMapping.GetErrorInfo(code, reason));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            return ParseErrorInternal(document!.RootElement);
        }

        public override async ValueTask<Error?> CheckForErrorResponse(RequestDefinition request, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            if (document!.RootElement.ValueKind is JsonValueKind.Array)
                return null;

            var status = document!.RootElement.TryGetProperty("status", out var codeProp) && codeProp.ValueKind == JsonValueKind.String ? codeProp.GetString() : null;
            var error = document!.RootElement.TryGetProperty("error", out var errorProp) ? errorProp.GetString() : null;
            if (status == "error" || error?.Length > 0)
                return ParseErrorInternal(document!.RootElement);

            return await base.CheckForErrorResponse(request, responseHeaders, responseStream).ConfigureAwait(false);

        }
    }
}

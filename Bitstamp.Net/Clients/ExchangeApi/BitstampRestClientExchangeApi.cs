using Bitstamp.Net.Clients.MessageHandlers;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    /// <inheritdoc cref="IBitstampRestClientExchangeApi" />
    internal partial class BitstampRestClientExchangeApi : RestApiClient, IBitstampRestClientExchangeApi
    {
        #region fields
        /// <inheritdoc />
        public new BitstampRestOptions ClientOptions => (BitstampRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping { get; } = BitstampErrors.RestErrorMapping;

        protected override IRestMessageHandler MessageHandler { get; } = new BitstampRestMessageHandler(BitstampErrors.RestErrorMapping);
        #endregion

        /// <inheritdoc />
        public string ExchangeName => "Bitstamp";


        /// <inheritdoc />
        public IBitstampRestClientExchangeApiAccount Account { get; }
        /// <inheritdoc />
        public IBitstampRestClientExchangeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitstampRestClientExchangeApiTrading Trading { get; }

        public BitstampRestClientExchangeApi(ILogger logger, HttpClient? httpClient, BitstampRestOptions options) :
            base(logger, httpClient, options.Environment.RestBaseAddress, options, options.ApiOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";
            RequestBodyContentEncoding = null;
            OmitContentTypeHeaderWithoutContent = true;

            Account = new BitstampRestClientExchangeApiAccount(this);
            ExchangeData = new BitstampRestClientExchangeApiExchangeData(this);
            Trading = new BitstampRestClientExchangeApiTrading(this);
        }

        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitstampExchange._serializerContext));

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitstampAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitstampExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public IBitstampRestClientExchangeApiShared SharedClient => this;

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }


        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            return await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }
    }
}

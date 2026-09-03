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
    internal partial class BitstampRestClientExchangeApi : RestApiClient<BitstampEnvironment, BitstampAuthenticationProvider, BitstampCredentials>, IBitstampRestClientExchangeApi
    {
        #region fields
        private readonly BitstampRestClientExchangeSharedApi _sharedApi;

        protected override ErrorMapping ErrorMapping { get; } = BitstampErrors.RestErrorMapping;

        protected override IRestMessageHandler MessageHandler { get; } = new BitstampRestMessageHandler(BitstampErrors.RestErrorMapping);
        #endregion


        /// <inheritdoc />
        public IBitstampRestClientExchangeApiAccount Account { get; }
        /// <inheritdoc />
        public IBitstampRestClientExchangeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitstampRestClientExchangeApiTrading Trading { get; }

        public BitstampRestClientExchangeApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, BitstampRestOptions options) :
            base(loggerFactory, BitstampExchange.Metadata.Id, httpClient, options.Environment.RestBaseAddress, options, options.ApiOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";
            RequestBodyContentEncoding = null;
            OmitContentTypeHeaderWithoutContent = true;

            Account = new BitstampRestClientExchangeApiAccount(this);
            ExchangeData = new BitstampRestClientExchangeApiExchangeData(this);
            Trading = new BitstampRestClientExchangeApiTrading(this);

            _sharedApi = new BitstampRestClientExchangeSharedApi(this);
        }

        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitstampExchange._serializerContext));

        protected override BitstampAuthenticationProvider CreateAuthenticationProvider(BitstampCredentials credentials)
            => new BitstampAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitstampExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public IBitstampRestClientExchangeApiShared SharedClient => _sharedApi;
        /// <inheritdoc />
        public IBitstampRestClientExchangeSharedApi SharedApi => _sharedApi;

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            return await base.SendAsync<Unit>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }
    }
}

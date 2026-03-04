using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System.Net;

namespace Bitstamp.Net
{
    internal class BitstampAuthenticationProvider : AuthenticationProvider<ApiCredentials>
    {
        #region Statics
        private static IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitstampExchange._serializerContext));
        #endregion

        #region Fields
        private readonly INonceProvider _nonceProvider;
        #endregion

        public override ApiCredentialsType[] SupportedCredentialTypes { get; } = [ApiCredentialsType.Hmac];

        #region Constructors
        public BitstampAuthenticationProvider(ApiCredentials credentials, INonceProvider nonceProvider) : base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");
            _nonceProvider = nonceProvider ?? throw new ArgumentNullException(nameof(nonceProvider));
        }
        #endregion

        #region Methods
        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration requestConfig)
        {
            if (!requestConfig.Authenticated)
                return;

            var key = "BITSTAMP " + _credentials.Key;
            var nonce = Guid.NewGuid().ToString();
            var timestamp = GetMillisecondTimestamp(apiClient);

            string bodyContent = "";
            if (requestConfig.BodyParameters?.Any() == true)
            {
                if (requestConfig.BodyFormat is RequestBodyFormat.Json)
                    bodyContent = _serializer.Serialize(requestConfig.BodyParameters);
                else if (requestConfig.BodyFormat is RequestBodyFormat.FormData)
                    bodyContent = requestConfig.BodyParameters.ToFormData();

                requestConfig.SetBodyContent(bodyContent);
            }

            requestConfig.Headers ??= new Dictionary<string, string>();
            requestConfig.Headers["X-Auth"] = key;
            requestConfig.Headers["X-Auth-Nonce"] = nonce;
            requestConfig.Headers["X-Auth-Timestamp"] = timestamp;
            requestConfig.Headers["X-Auth-Version"] = "v2";

            var contentType = "";
            if (requestConfig.BodyParameters?.Any() == true)
            {
                if (requestConfig.BodyFormat is RequestBodyFormat.Json)
                    contentType = Constants.JsonContentHeader;
                else if (requestConfig.BodyFormat is RequestBodyFormat.FormData)
                    contentType = Constants.FormContentHeader;
            }

            var pathAndQuery = requestConfig.Path + requestConfig.GetQueryString(true);
            if (!pathAndQuery.EndsWith("/"))
                pathAndQuery += "/";

            var signatureText = key +
                requestConfig.Method +
                new Uri(requestConfig.BaseAddress).Host +
                pathAndQuery +
                contentType +
                nonce +
                timestamp +
                "v2" +
                bodyContent;

            requestConfig.Headers["X-Auth-Signature"] = SignHMACSHA256(signatureText, SignOutputType.Hex);
        }
        #endregion
    }
}

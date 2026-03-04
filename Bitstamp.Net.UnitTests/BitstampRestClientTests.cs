using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using Bitstamp.Net.Clients;

namespace Bitstamp.Net.UnitTests
{
    [TestFixture()]
    public class BitstampRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BitstampAuthenticationProvider(new ApiCredentials("XXX", "XXX"), new BitstampNonceProvider());
            var client = (RestApiClient)new BitstampRestClient().ExchangeApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return uriParams["signature"].ToString();
                },
                "c2a5ec39e186f05cf65000aac8c6707c6541337bda16bc1edaea13ea35d1eb0e",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitstampRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitstampSocketClient>();
        }
    }
}

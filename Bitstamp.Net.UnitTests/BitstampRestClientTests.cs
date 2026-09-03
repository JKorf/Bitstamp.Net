using Bitstamp.Net;
using Bitstamp.Net.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;

namespace Bitstamp.Net.UnitTests
{
    [TestFixture()]
    public class BitstampRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BitstampAuthenticationProvider(new BitstampCredentials("XXX", "XXX"), "123");
            var client = (RestApiClient)new BitstampRestClient().ExchangeApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v2/buy/ethusd/",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["X-Auth-Signature"].ToString();
                },
                "6DCA01A0817446CBC44CD792EF743262A7342A15C2DF805BC89D30DBE099EEFA",
                new Parameters(BitstampExchange._parameterSerializationSettings)
                {
                    { "side", 0 },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitstampRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitstampSocketClient>();
        }


        [Test]
        public void TestRestSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new BitstampRestClient().ExchangeApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSocketSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new BitstampSocketClient().ExchangeApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }
    }
}

using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Objects.Options;

namespace Bitstamp.Net.UnitTests
{
    [NonParallelizable]
    public class BitstampRestIntegrationTests : RestIntegrationTest<BitstampRestClient>
    {
        public override bool Run { get; set; } = false;

        public override BitstampRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BitstampRestClient(null, loggerFactory, Options.Create(new BitstampRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().ExchangeApi.ExchangeData.GetTickerAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("-100011"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestAccount()
        {
        }

        [Test]
        public async Task TestExchangeData()
        {
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(default), false);
        }

        [Test]
        public async Task TestTrading()
        {
        }
    }
}

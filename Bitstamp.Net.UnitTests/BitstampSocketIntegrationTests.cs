using Bitstamp.Net.Clients;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bitstamp.Net.Objects.Models;

namespace Bitstamp.Net.UnitTests
{
    internal class BitstampSocketIntegrationTests : SocketIntegrationTest<BitstampSocketClient>
    {
        public override bool Run { get; set; } = true;

        public BitstampSocketIntegrationTests()
        {
        }

        public override BitstampSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BitstampSocketClient(Options.Create(new BitstampSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private BitstampRestClient GetRestClient()
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BitstampRestClient(x =>
            {
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestSubscriptions()
        {
            //await RunAndCheckUpdate<BitstampAccountUpdate>((client, updateHandler) => client.SpotApi.SubscribeToUserDataUpdatesAsync(listenKey.Data, updateHandler, default, default, default), false, true);
            
            //await RunAndCheckUpdate<BitstampTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
            //await RunAndCheckUpdate<BitstampTickerUpdate>((client, updateHandler) => client.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-SWAP-USDT", updateHandler, default), true, false);
        }
    }
}

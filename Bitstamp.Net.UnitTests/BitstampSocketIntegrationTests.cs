using Bitstamp.Net.Clients;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bitstamp.Net.Objects.Models.Socket;

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
                ApiCredentials = Authenticated ? new BitstampCredentials(key, sec) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            // Not reliable
            //await RunAndCheckUpdate<BitstampTradeUpdate>((client, updateHandler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD", updateHandler, default), true, false);
            //await RunAndCheckUpdate<BitstampTradeUpdate>((client, updateHandler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD-PERP", updateHandler, default), true, false);
        }
    }
}

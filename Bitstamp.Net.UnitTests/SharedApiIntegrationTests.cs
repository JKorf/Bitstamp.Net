using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Bitstamp.Net.UnitTests
{
    [TestFixture]
    public class BitstampSharedApiRestIntegrationTest : SharedApiRestIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            await ExecuteAsync();
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBitstamp();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBitstampSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedRest client)
        {
            if (client is IBitstampRestClientExchangeSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USD"), "ETH");

            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class BitstampSharedApiSocketIntegrationTest : SharedApiSocketIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            var result = await ExecuteAsync();
            foreach (var item in result)
                Assert.Warn(item.Message);
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBitstamp();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBitstampSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedSocket client)
        {
            if (client is IBitstampSocketClientExchangeSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USD"), "ETH");

            throw new NotImplementedException();
        }
    }
}

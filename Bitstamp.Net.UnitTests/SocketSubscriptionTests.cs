using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Options;
using Bitstamp.Net.Objects.Models.Socket;

namespace Bitstamp.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateConcurrentSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitstampSocketClient(Options.Create(new BitstampSocketOptions
            {
                OutputOriginalData = true

            }), logger);

            var tester = new SocketSubscriptionValidator<BitstampSocketClient>(client, "Subscriptions/ExchangeApi", "wss://ws.bitstamp.com");
            await tester.ValidateConcurrentAsync<BitstampTradeUpdate>(
                (client, handler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD", handler),
                (client, handler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("BTC/USD", handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateSubscriptions()
        {
            var client = new BitstampSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BitstampSocketClient>(client, "Subscriptions/ExchangeApi", "wss://ws.bitstamp.com");
            await tester.ValidateAsync<BitstampTradeUpdate>((client, handler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD", handler), "Trades", ignoreProperties: ["amount_str", "price_str", "timestamp"], nestedJsonProperty: "data");
            await tester.ValidateAsync<BitstampOrderBookUpdate>((client, handler) => client.ExchangeApi.SubscribeToFullOrderBookUpdatesAsync("ETH/USD", handler), "DiffBook", ignoreProperties:["timestamp"], nestedJsonProperty: "data");
            await tester.ValidateAsync<BitstampOrderBookUpdate>((client, handler) => client.ExchangeApi.SubscribeToOrderBookSnapshotUpdatesAsync("ETH/USD", handler), "OrderBook", ignoreProperties: ["timestamp"], nestedJsonProperty: "data");
        }

    }
}

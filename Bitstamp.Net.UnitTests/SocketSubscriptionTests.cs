//using CryptoExchange.Net.Objects;
//using CryptoExchange.Net.Testing;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using NUnit.Framework;
//using System.Threading.Tasks;
//using Bitstamp.Net.Clients;
//using Bitstamp.Net.Objects.Models;
//using Bitstamp.Net.Objects.Options;

//namespace Bitstamp.Net.UnitTests
//{
//    [TestFixture]
//    public class SocketSubscriptionTests
//    {
//        [Test]
//        public async Task ValidateConcurrentFuturesSubscriptions()
//        {
//            var logger = new LoggerFactory();
//            logger.AddProvider(new TraceLoggerProvider());

//            var client = new BitstampSocketClient(Options.Create(new BitstampSocketOptions
//            {
//                OutputOriginalData = true

//            }), logger);

//            var tester = new SocketSubscriptionValidator<BitstampSocketClient>(client, "Subscriptions/Spot", "wss://stream.Bitstamp.com");
//            await tester.ValidateConcurrentAsync<BitstampKlineUpdate>(
//                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
//                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
//                "Concurrent");
//        }

//        [Test]
//        public async Task ValidateSpotSubscriptions()
//        {
//            var client = new BitstampSocketClient(opts =>
//            {
//                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
//            });
//            var tester = new SocketSubscriptionValidator<BitstampSocketClient>(client, "Subscriptions/Spot", "wss://stream.Bitstamp.com");
//            await tester.ValidateAsync<BitstampTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", ignoreProperties: ["v"], nestedJsonProperty: "data");
//            await tester.ValidateAsync<BitstampKlineUpdate>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Klines", ignoreProperties: ["sn"], nestedJsonProperty: "data");
//            await tester.ValidateAsync<BitstampTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Tickers", nestedJsonProperty: "data");
//            await tester.ValidateAsync<BitstampOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", handler), "PartialBook", nestedJsonProperty: "data");
//            await tester.ValidateAsync<BitstampOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", handler), "OrderBook", nestedJsonProperty: "data");
//            await tester.ValidateAsync<BitstampAccountUpdate>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", handler), "UserAccount");
//            await tester.ValidateAsync<BitstampOrderUpdate[]>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", null, handler), "UserOrder", ignoreProperties: ["u"]);
//            await tester.ValidateAsync<BitstampUserTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", null, null, handler), "UserTrade");
//        }

//        [Test]
//        public async Task ValidateFuturesSubscriptions()
//        {
//            var client = new BitstampSocketClient(opts =>
//            {
//                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
//            });
//            var tester = new SocketSubscriptionValidator<BitstampSocketClient>(client, "Subscriptions/UsdtFutures", "wss://stream.Bitstamp.com");

//            await tester.ValidateAsync<BitstampAccountUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", handler), "UserAccount", useFirstUpdateItem: true);
//            await tester.ValidateAsync<BitstampFuturesOrderUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, handler), "UserOrder", ignoreProperties: ["u"]);
//            await tester.ValidateAsync<BitstampPositionUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, null, handler), "UserPosition");
//            await tester.ValidateAsync<BitstampUserTradeUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, null, null, handler), "UserTrade");
//        }
//    }
//}

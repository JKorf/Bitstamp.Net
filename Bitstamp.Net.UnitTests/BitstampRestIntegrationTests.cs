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
        }

        [Test]
        public async Task TestAccount()
        {
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAccountBalancesAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAccountBalanceAsync("usd", default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetWithdrawFeesAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetWithdrawFeesAsync("usdc", default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAllFeesAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetFeesAsync("ethusd", default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetUserTransactionsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetUserTransactionsAsync("ethusd", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetSymbolsAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetWithdrawalsAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetDepositAddressAsync("usdc", default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetCryptoTransactionsAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetDepositsAsync(default, default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Account.GetMarginInfoAsync(default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Account.GetLeverageSettingsAsync(default, default, default), true);
        }

        [Test]
        public async Task TestExchangeData()
        {
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetAllTickersAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetTickerAsync("ethusd", default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetHourTickerAsync("ethusd", default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetKlinesAsync("ethusd", Enums.KlineInterval.OneHour, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("ethusd", default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetTradesAsync("ethusd", default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetEurUsdConversionRateAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetFundingRateAsync("ethusd-perp", default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetFundingRateHistoryAsync("ethusd-perp", default, default, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetMarginTiersAsync(default), false);
            //await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetCollateralAssetsAsync(default), false);
        }

        [Test]
        public async Task TestTrading()
        {
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetOrderHistoryAsync(Enums.OrderSource.Orderbook, "ETH/USD", default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetOpenOrdersAsync(default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Trading.GetDerivativesUserTradesAsync(default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Trading.GetOpenPositionsAsync(default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Trading.GetPositionHistoryAsync(default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.ExchangeApi.Trading.GetPositionSettlementTransactionsAsync(default, default, default, default, default, default, default), true);
        }
    }
}

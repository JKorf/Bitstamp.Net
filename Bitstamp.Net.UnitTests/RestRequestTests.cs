using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Enums;
using System.Linq;

namespace Bitstamp.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateAccountCalls()
        {
            var client = new BitstampRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitstampRestClient>(client, "Endpoints/ExchangeApi/Account", "https://www.bitstamp.net", IsAuthenticated);
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetWithdrawalsAsync(), "GetWithdrawals");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.WithdrawFiatAsync(0.1m, "123", "123", "123", "123", "123", "123", "123", "123",WithdrawType.Sepa), "WithdrawFiat");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.CancelWithdrawalAsync("123"), "CancelWithdrawal");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetFiatWithdrawalStatusAsync("123"), "GetFiatWithdrawalStatus");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.WithdrawCryptoAsync("eth", 0.1m, "123"), "WithdrawCrypto");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetDepositAddressAsync("eth", "123"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetCryptoTransactionsAsync(), "GetCryptoTransactions");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetDepositsAsync(), "GetDeposits");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetMarginInfoAsync(), "GetMarginInfo");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetLeverageSettingsAsync(), "GetLeverageSettings");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.SetLeverageAsync(MarginMode.Isolated, "123", 0.1m), "SetLeverage");
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            var client = new BitstampRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitstampRestClient>(client, "Endpoints/ExchangeApi/ExchangeData", "https://www.bitstamp.net", IsAuthenticated);
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("ethusd"), "GetOrderBook", ignoreProperties: [ "timestamp" ]);
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetMarginTiersAsync(), "GetMarginTiers");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetCollateralAssetsAsync(), "GetCollateralAssets");
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            var client = new BitstampRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitstampRestClient>(client, "Endpoints/ExchangeApi/Trading", "https://www.bitstamp.net", IsAuthenticated);
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetOpenPositionsAsync(), "GetOpenPositions");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetPositionStatusAsync("123"), "GetPositionStatus");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetPositionHistoryAsync(), "GetPositionHistory");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.ClosePositionsAsync(), "ClosePositions");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.ClosePositionAsync("123"), "ClosePosition");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetPositionSettlementTransactionsAsync(), "GetPositionSettlementTransactions");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.UpdatePositionCollateralAsync("123", 0.1m), "UpdatePositionCollateral");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetOrderHistoryAsync(OrderSource.Orderbook, "123"), "GetOrderHistory", ignoreProperties: ["id_str", "datetime", "amount_str", "price_str"]);
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.SingleOrDefault(x => x.Key == "X-Auth-Signature").Key != null;
        }
    }
}

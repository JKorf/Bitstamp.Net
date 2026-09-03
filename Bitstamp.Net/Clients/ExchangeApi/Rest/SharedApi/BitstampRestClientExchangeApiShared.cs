using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampRestClientExchangeSharedApi :
        SharedApiBase,
        IBitstampRestClientExchangeApiShared,
        IBitstampRestClientExchangeSharedApi
    {
        private readonly BitstampRestClientExchangeApi _api;

        private const string _topicSpotId = "BitstampSpot";
        private const string _topicFuturesId = "BitstampFutures";
        private const string _exchangeName = "Bitstamp";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitstampExchange.Metadata, this);

        private static HashSet<string> _exchangeSupportedFiat = ["EUR", "USD", "GBP", "SGD"];

        public BitstampRestClientExchangeSharedApi(BitstampRestClientExchangeApi api)
           : base(
                 SharedTransport.Rest,
                 api.Exchange,
                 [TradingMode.Spot, TradingMode.PerpetualLinear],
                 () => api.Authenticated,
                 api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                GetBalancesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetOrderBookOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetFeeOptions,
                GetFundingRateHistoryOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOpenInterestOptions,
                GetPositionHistoryOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions
                );
        }
    }
}

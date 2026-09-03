using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Shared interface for rest API usage
    /// </summary>
    public interface IBitstampRestClientExchangeApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        IFeeRestClient,
        ISpotOrderClientIdRestClient,
        //ISpotTriggerOrderRestClient,
        IBookTickerRestClient,
        IFundingRateRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        ILeverageRestClient,
        IOpenInterestRestClient,
        IPositionHistoryRestClient,
        IFuturesOrderRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBitstampRestClientExchangeSharedApi :
        IGetAssetRest,
        IGetAllAssetsRest,
        IGetBalancesRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetKlinesRest,
        IGetOrderBookRest,
        IGetRecentTradesRest,
        IGetSpotSymbolsRest,
        IPlaceSpotOrder,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        ICancelSpotOrderRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        IGetFeesRest,
        IGetSpotOrderByClientOrderIdRest,
        ICancelSpotOrderByClientOrderIdRest,
        IGetBookTickerRest,
        IGetFundingRateHistoryRest,
        IGetFuturesSymbolsRest,
        IGetFuturesTickerRest,
        IGetAllFuturesTickersRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetOpenInterestRest,
        IGetPositionHistoryRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        ICancelFuturesOrderRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        IGetPositionsRest,
        IClosePositionRest
    { }
}

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
}

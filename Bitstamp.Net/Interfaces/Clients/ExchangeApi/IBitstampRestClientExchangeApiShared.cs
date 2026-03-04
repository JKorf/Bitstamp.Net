using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IBitstampRestClientExchangeApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        //IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        ////ITradeHistoryRestClient,
        //IWithdrawalRestClient,
        //IWithdrawRestClient,
        //IFeeRestClient,
        ISpotOrderClientIdRestClient,
        //ISpotTriggerOrderRestClient,
        IBookTickerRestClient
        //ITransferRestClient
    {
    }
}

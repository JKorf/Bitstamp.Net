using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
    /// </summary>
    public interface IBitstampSocketClientExchangeApiShared :
        ITradeSocketClient,
        IOrderBookSocketClient
    {
    }
}

using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampSocketClientExchangeSharedApi : 
        SharedApiBase,
        IBitstampSocketClientExchangeApiShared,
        IBitstampSocketClientExchangeSharedApi
    {
        private readonly BitstampSocketClientExchangeApi _api;

        private const string _exchangeName = "Bitstamp";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitstampExchange.Metadata, this);

        public BitstampSocketClientExchangeSharedApi(BitstampSocketClientExchangeApi api)
           : base(
                 SharedTransport.Socket,
                 api.Exchange,
                 [TradingMode.Spot, TradingMode.PerpetualLinear],
                 () => api.Authenticated,
                 api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTradeOptions,
                SubscribeOrderBookOptions
                );
        }
    }
}

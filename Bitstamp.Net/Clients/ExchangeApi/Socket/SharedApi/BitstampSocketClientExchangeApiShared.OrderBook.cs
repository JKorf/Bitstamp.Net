using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampSocketClientExchangeSharedApi
    {
        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, [100]);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderBookSnapshotUpdatesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                update => handler(update.ToType(new SharedOrderBook(SharedQuantityType.BaseAsset, null, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion
    }
}

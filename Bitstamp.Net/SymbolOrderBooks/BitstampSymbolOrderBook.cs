using Bitstamp.Net.Clients;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Models.Socket;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.SymbolOrderBooks
{
    /// <summary>
    /// Symbol order book implementation
    /// </summary>
    public class BitstampSymbolOrderBook: SymbolOrderBook
    {
        private readonly IBitstampRestClient _restClient;
        private readonly IBitstampSocketClient _socketClient;
        private readonly bool _clientOwner;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitstampSymbolOrderBook(string symbol, Action<BitstampOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public BitstampSymbolOrderBook(string symbol,
            Action<BitstampOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBitstampRestClient? restClient,
            IBitstampSocketClient? socketClient) : base(logger, "Bitstamp", "Exchange", symbol)
        {
            var options = BitstampOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = false;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);

            _restClient = restClient ?? new BitstampRestClient();
            _socketClient = socketClient ?? new BitstampSocketClient();
            _clientOwner = socketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.ExchangeApi.SubscribeToFullOrderBookUpdatesAsync(Symbol, HandleUpdate).ConfigureAwait(false);
            if (!subResult)
                return subResult;

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            
            // Wait 1000ms until the first update has been received
            await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(2000), ct).ConfigureAwait(false);

            var bookResult = await _restClient.ExchangeApi.ExchangeData.GetOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
            {
                await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(bookResult.Error!);
            }

            SetSnapshot(bookResult.Data.Timestamp.Ticks, bookResult.Data.Bids, bookResult.Data.Asks);            
            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<BitstampOrderBookUpdate> @event)
        {
            UpdateOrderBook(@event.Data.Timestamp.Ticks, @event.Data.Bids, @event.Data.Asks, @event.DataTime, @event.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            // Wait 1000ms until the first update has been received
            await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(2000), ct).ConfigureAwait(false);

            var bookResult = await _restClient.ExchangeApi.ExchangeData.GetOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetSnapshot(bookResult.Data.Timestamp.Ticks, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
                _socketClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}

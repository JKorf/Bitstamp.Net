using System.Text;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Interfaces.Clients;
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
    public class BitstampSpotSymbolOrderBook: SymbolOrderBook
    {
        private readonly IBitstampSocketClient _socketClient;
        private readonly bool _clientOwner;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitstampSpotSymbolOrderBook(string symbol, Action<BitstampOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null)
        {
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="socketClient">Socket client instance</param>
        public BitstampSpotSymbolOrderBook(string symbol,
            Action<BitstampOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBitstampSocketClient? socketClient) : base(logger, "Bitstamp", "Spot", symbol)
        {
            var options = BitstampOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = false;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);

            _socketClient = socketClient ?? new BitstampSocketClient();
            _clientOwner = socketClient == null;
            Levels = options?.Limit ?? 20;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            throw new Exception();
            //var result = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels!.Value, null, false, HandleUpdate).ConfigureAwait(false);
            //if (!result)
            //    return result;

            //if (ct.IsCancellationRequested)
            //{
            //    await result.Data.CloseAsync().ConfigureAwait(false);
            //    return result.AsError<UpdateSubscription>(new CancellationRequestedError());
            //}

            //Status = OrderBookStatus.Syncing;

            //var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            //return setResult ? result : new CallResult<UpdateSubscription>(setResult.Error!);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        //private void HandleUpdate(DataEvent<BitstampOrderBook> data)
        //{
        //    if (data.UpdateType == SocketUpdateType.Snapshot)
        //        SetSnapshot(DateTime.UtcNow.Ticks, data.Data.Data.Bids, data.Data.Data.Asks, data.DataTime, data.DataTimeLocal);
        //    else
        //        UpdateOrderBook(DateTime.UtcNow.Ticks, data.Data.Data.Bids, data.Data.Data.Asks, data.DataTime, data.DataTimeLocal);

        //    AddChecksum((int)data.Data.Data.Checksum);
        //}

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

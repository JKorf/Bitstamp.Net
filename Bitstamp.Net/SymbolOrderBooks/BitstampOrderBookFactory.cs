using Bitstamp.Net.Interfaces;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class BitstampOrderBookFactory : IBitstampOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => BitstampExchange.ExchangeName;

        /// <inheritdoc />
        public IOrderBookFactory<BitstampOrderBookOptions> ExchangeFactory { get; }


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitstampOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            ExchangeFactory = new OrderBookFactory<BitstampOrderBookOptions>(Create, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<BitstampOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(BitstampExchange.FormatSymbol);
            return Create(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<BitstampOrderBookOptions>? options = null)
            => new BitstampSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                        _serviceProvider.GetRequiredService<IBitstampRestClient>(),
                                        _serviceProvider.GetRequiredService<IBitstampSocketClient>());

    }
}

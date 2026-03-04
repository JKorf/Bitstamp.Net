using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;

namespace Bitstamp.Net.Interfaces
{
    /// <summary>
    /// Bitstamp order book factory
    /// </summary>
    public interface IBitstampOrderBookFactory : IExchangeService
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<BitstampOrderBookOptions> ExchangeFactory { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<BitstampOrderBookOptions>? options = null);

        /// <summary>
        /// Create a SymbolOrderBook for the Spot API
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Order book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(string symbol, Action<BitstampOrderBookOptions>? options = null);
    }
}
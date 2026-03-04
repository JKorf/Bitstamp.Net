using CryptoExchange.Net.Objects.Options;
using System;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Options for Bitstamp SymbolOrderBook
    /// </summary>
    public class BitstampOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for the Bitstamp SymbolOrderBook
        /// </summary>
        public static BitstampOrderBookOptions Default { get; set; } = new BitstampOrderBookOptions();

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

#warning todo
        /// <summary>
        /// The amount of rows. Should be one of: 5/10/20/50
        /// </summary>
        public int? Limit { get; set; }

        internal BitstampOrderBookOptions Copy()
        {
            var options = Copy<BitstampOrderBookOptions>();
            options.InitialDataTimeout = InitialDataTimeout;
            options.Limit = Limit;
            return options;
        }
    }
}

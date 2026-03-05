using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Bitstamp socket API options
    /// </summary>
    public class BitstampSocketApiOptions : SocketApiOptions
    {
        /// <summary>
        /// Interval to send ping message to server
        /// </summary>
        public TimeSpan PingInterval { get; set; } = TimeSpan.FromSeconds(30);

        internal BitstampSocketApiOptions Set(BitstampSocketApiOptions targetOptions)
        {
            base.Set(targetOptions);
            targetOptions.PingInterval = PingInterval;
            return targetOptions;
        }
    }
}

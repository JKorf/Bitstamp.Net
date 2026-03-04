using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Objects.Options
{
    public class BitstampSocketApiOptions : SocketApiOptions
    {
        public TimeSpan PingInterval { get; set; } = TimeSpan.FromSeconds(30);

        internal BitstampSocketApiOptions Set(BitstampSocketApiOptions targetOptions)
        {
            base.Set(targetOptions);
            targetOptions.PingInterval = PingInterval;
            return targetOptions;
        }
    }
}

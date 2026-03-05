using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitstampSocketClient
    /// </summary>
    public class BitstampSocketOptions : SocketExchangeOptions<BitstampEnvironment>
    {
        /// <summary>
        /// Default options for the BitstampRestClient
        /// </summary>
        internal static BitstampSocketOptions Default { get; set; } = new BitstampSocketOptions
        {
            Environment = BitstampEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Exchange API
        /// </summary>
        public BitstampSocketApiOptions ApiOptions { get; private set; } = new BitstampSocketApiOptions();

        internal BitstampSocketOptions Set(BitstampSocketOptions targetOptions)
        {
            targetOptions = base.Set<BitstampSocketOptions>(targetOptions);
            targetOptions.ApiOptions = ApiOptions.Set(targetOptions.ApiOptions);
            return targetOptions;
        }
    }
}

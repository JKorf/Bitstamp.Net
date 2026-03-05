using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitstampRestClient
    /// </summary>
    public class BitstampRestOptions : RestExchangeOptions<BitstampEnvironment>
    {
        /// <summary>
        /// Default options for the BitstampRestClient
        /// </summary>
        internal static BitstampRestOptions Default { get; set; } = new BitstampRestOptions
        {
            Environment = BitstampEnvironment.Live
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Exchange API
        /// </summary>
        public RestApiOptions ApiOptions { get; private set; } = new RestApiOptions();

        internal BitstampRestOptions Set(BitstampRestOptions targetOptions)
        {
            targetOptions = base.Set<BitstampRestOptions>(targetOptions);
            targetOptions.ApiOptions = ApiOptions.Set(targetOptions.ApiOptions);
            return targetOptions;
        }
    }
}

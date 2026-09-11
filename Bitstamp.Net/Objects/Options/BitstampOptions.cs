using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;

namespace Bitstamp.Net.Objects.Options
{
    /// <summary>
    /// Bitstamp options
    /// </summary>
    public class BitstampOptions : LibraryOptions<BitstampRestOptions, BitstampSocketOptions, BitstampCredentials, BitstampEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiClientOptions SharedApi { get; set; } = new();
    }
}

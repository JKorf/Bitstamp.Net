using CryptoExchange.Net.Authentication;

namespace Bitstamp.Net
{
    /// <summary>
    /// Bitstamp API credentials
    /// </summary>
    public class BitstampCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BitstampCredentials(string key, string secret) : base(key, secret)
        {
        }
    }
}

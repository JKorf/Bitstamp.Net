using CryptoExchange.Net.Authentication;

namespace Bitstamp.Net
{
    /// <summary>
    /// Bitstamp credentials
    /// </summary>
    public class BitstampCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public BitstampCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public BitstampCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BitstampCredentials(Hmac!);
    }
}

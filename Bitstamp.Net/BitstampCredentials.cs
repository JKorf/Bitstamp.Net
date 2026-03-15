using CryptoExchange.Net.Authentication;

namespace Bitstamp.Net
{
    /// <summary>
    /// Bitstamp credentials
    /// </summary>
    public class BitstampCredentials : ApiCredentials
    {
        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public BitstampCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key and secret
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public BitstampCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create WhiteBit credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public BitstampCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new BitstampCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}

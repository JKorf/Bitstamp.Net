namespace Bitstamp.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the Bitstamp clients
    /// </summary>
    public class BitstampApiAddresses
    {
        /// <summary>
        /// The address used by the BitstampRestClient for the rest API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitstampSocketClient for the socket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Bitstamp.com API
        /// </summary>
        public static BitstampApiAddresses Default = new BitstampApiAddresses
        {
            RestClientAddress = "https://www.bitstamp.net/",
            SocketClientAddress = "wss://ws.bitstamp.net/"
        };
    }
}

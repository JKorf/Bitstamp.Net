using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc />
    public class BitstampSharedApiClient : IBitstampSharedApiClient
    {
        /// <inheritdoc />
        public IBitstampRestClientExchangeSharedApi Rest { get; }
        /// <inheritdoc />
        public IBitstampSocketClientExchangeSharedApi Socket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampSharedApiClient(
            IBitstampRestClient restClient,
            IBitstampSocketClient socketClient)
        {
            Rest = restClient.ExchangeApi.SharedApi;
            Socket = socketClient.ExchangeApi.SharedApi;
        }
    }
}

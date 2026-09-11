using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace Bitstamp.Net.Clients
{
    /// <inheritdoc />
    public class BitstampSharedApiClient : SharedApiClientBase, IBitstampSharedApiClient
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
            IBitstampSocketClient socketClient,
            IOptions<BitstampOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                    restClient.ExchangeApi.SharedApi,
                    socketClient.ExchangeApi.SharedApi)
        {
            Rest = restClient.ExchangeApi.SharedApi;
            Socket = socketClient.ExchangeApi.SharedApi;
        }
    }
}

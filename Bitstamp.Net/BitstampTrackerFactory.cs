using Bitstamp.Net.Clients;
using Bitstamp.Net.Interfaces;
using Bitstamp.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Bitstamp.Net
{
    /// <inheritdoc />
    public class BitstampTrackerFactory : IBitstampTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitstampTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval) => false;

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null) => throw new NotImplementedException();

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitstampRestClient>() ?? new BitstampRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitstampSocketClient>() ?? new BitstampSocketClient();

            var sharedRestClient = restClient.ExchangeApi.SharedClient;
            var sharedSocketClient = socketClient.ExchangeApi.SharedClient;
            
            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitstampRestClient>() ?? new BitstampRestClient();

            return new BitstampUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitstampUserSpotDataTracker>>() ?? new NullLogger<BitstampUserSpotDataTracker>(),
                restClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, BitstampCredentials credentials, SpotUserDataTrackerConfig? config = null, BitstampEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitstampUserClientProvider>() ?? new BitstampUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);

            return new BitstampUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitstampUserSpotDataTracker>>() ?? new NullLogger<BitstampUserSpotDataTracker>(),
                restClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitstampRestClient>() ?? new BitstampRestClient();

            return new BitstampUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitstampUserFuturesDataTracker>>() ?? new NullLogger<BitstampUserFuturesDataTracker>(),
                restClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, BitstampCredentials credentials, FuturesUserDataTrackerConfig? config = null, BitstampEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitstampUserClientProvider>() ?? new BitstampUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);

            return new BitstampUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitstampUserFuturesDataTracker>>() ?? new NullLogger<BitstampUserFuturesDataTracker>(),
                restClient,
                userIdentifier,
                config
                );
        }
    }
}

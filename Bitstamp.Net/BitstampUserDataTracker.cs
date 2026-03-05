using Bitstamp.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net
{
    /// <inheritdoc/>
    public class BitstampUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BitstampUserSpotDataTracker(
            ILogger<BitstampUserSpotDataTracker> logger,
            IBitstampRestClient restClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                null,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class BitstampUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public BitstampUserFuturesDataTracker(
            ILogger<BitstampUserFuturesDataTracker> logger,
            IBitstampRestClient restClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                null,
                null,
                null,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}

using Bitstamp.Net.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using System.Text.Json.Serialization;

namespace Bitstamp.Net
{
    /// <summary>
    /// Bitstamp exchange information and configuration
    /// </summary>
    public static class BitstampExchange
    {
        internal static JsonSerializerContext _serializerContext = JsonSerializerContextCache.GetOrCreate<BitstampSourceGenerationContext>();

        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Bitstamp",
                "Bitstamp",
                "https://raw.githubusercontent.com/JKorf/Bitstamp.Net/master/Bitstamp.Net/Icon/icon.png",
                "https://www.bitstamp.com",
                ["https://www.bitstamp.net/api/"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized
                );

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Bitstamp";

        /// <summary>
        /// Display name
        /// </summary>
        public static string DisplayName => "Bitstamp";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Bitstamp.Net/master/Bitstamp.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.bitstamp.net/";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://www.bitstamp.net/api/"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        /// <summary>
        /// Aliases for CoinEx assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases = [
                new AssetAlias("usd", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to a CoinEx recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            if (tradingMode == TradingMode.Spot)
                return $"{baseAsset.ToUpper()}/{quoteAsset.ToUpper()}";

            return $"{baseAsset.ToUpper()}/{quoteAsset.ToUpper()}-PERP";
        }

        /// <summary>
        /// Convert a symbol name to a symbol accepted by the exchange in various endpoints by removing the `/` and converting to lower case 
        /// </summary>
        public static string SymbolToPathParameter(string symbol) => symbol.Replace("/", "").ToLower();

        /// <summary>
        /// Rate limiter configuration for the Bitstamp API
        /// </summary>
        public static BitstampRateLimiters RateLimiter { get; } = new BitstampRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Bitstamp API
    /// </summary>
    public class BitstampRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BitstampRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            Rest = new RateLimitGate("Rest")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 400, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed))
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 10_000, TimeSpan.FromMinutes(10), RateLimitWindowType.Fixed))
                ;
            Rest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Rest.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate Rest { get; private set; }
    }
}

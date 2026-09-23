using Bitstamp.Net;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Interfaces;
using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Options;
using Bitstamp.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IBitstampRestClient and IBitstampSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/Bitstamp.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBitstamp(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = BitstampOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return services.AddBitstampCore(options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IBitstampRestClient and IBitstampSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Bitstamp services</param>
        /// <returns></returns>
        public static IServiceCollection AddBitstamp(
            this IServiceCollection services,
            Action<BitstampOptions>? optionsDelegate = null)
        {
            var options = BitstampOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return services.AddBitstampCore(options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBitstampCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBitstampRestClient, BitstampRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BitstampRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new BitstampRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<BitstampRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<BitstampRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IBitstampSocketClient), x => { return new BitstampSocketClient(x.GetRequiredService<IOptions<BitstampSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IBitstampOrderBookFactory, BitstampOrderBookFactory>();
            services.AddTransient<IBitstampTrackerFactory, BitstampTrackerFactory>();
            services.AddTransient<ITrackerFactory, BitstampTrackerFactory>();
            services.AddSingleton<IBitstampUserClientProvider, BitstampUserClientProvider>(x =>
            new BitstampUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IBitstampRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<BitstampRestOptions>>(),
                x.GetRequiredService<IOptions<BitstampSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBitstampRestClient>().ExchangeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBitstampSocketClient>().ExchangeApi.SharedClient);

            services.RegisterSharedApiClient<
                IBitstampSharedApiClient,
                BitstampSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.Rest)
                    .Add(client => client.Socket)
                    );
            return services;
        }
    }
}

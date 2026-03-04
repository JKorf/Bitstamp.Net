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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IBitstampRestClient and IBitstampSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBitstamp(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new BitstampOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? BitstampEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? BitstampEnvironment.Live.Name;
            options.Rest.Environment = BitstampEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = BitstampEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
            var options = new BitstampOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? BitstampEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? BitstampEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
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

            return services;
        }
    }
}

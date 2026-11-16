// 04-shared-client.cs
//
// Demonstrates: accessing CryptoExchange.Net Shared API V2 capabilities from Bitstamp.Net.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net;
using Bitstamp.Net.Clients;
using CryptoExchange.Net.SharedApis;

var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});

var shared = client.ExchangeApi.SharedApi;
Console.WriteLine($"Exchange: {shared.Exchange}");

var info = shared.Discover();
var supportedFeatures = info.Features
    .Where(x => x.Supported)
    .Select(x => x.EndpointName);
Console.WriteLine($"{info.Exchange} {info.TypeName}: {string.Join(", ", supportedFeatures)}");

IGetSpotSymbolsRest symbols = shared;
var spotSymbols = await symbols.GetSpotSymbolsAsync(new GetSymbolsRequest());
if (!spotSymbols.Success)
{
    Console.WriteLine($"Shared spot symbol request failed: {spotSymbols.Error}");
    return;
}

foreach (var symbol in spotSymbols.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");

// Native Bitstamp endpoints remain available beside the shared abstraction.
var nativeTickers = await client.ExchangeApi.ExchangeData.GetAllTickersAsync();
if (nativeTickers.Success)
{
    foreach (var ticker in nativeTickers.Data.Take(5))
        Console.WriteLine($"{ticker.Symbol}: last={ticker.LastPrice}");
}

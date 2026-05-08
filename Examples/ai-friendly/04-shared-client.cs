// 04-shared-client.cs
//
// Demonstrates: accessing CryptoExchange.Net shared clients from Bitstamp.Net.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net;
using Bitstamp.Net.Clients;
using CryptoExchange.Net.SharedApis;

var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});

var shared = client.ExchangeApi.SharedClient;
Console.WriteLine($"Exchange: {shared.Exchange}");
Console.WriteLine($"Trading modes: {string.Join(", ", shared.SupportedTradingModes)}");

var spotSymbols = await shared.GetSpotSymbolsAsync(new GetSymbolsRequest());
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

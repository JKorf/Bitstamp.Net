// 01-market-and-account.cs
//
// Demonstrates: Bitstamp market data, account balances and result handling.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Enums;

var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETH/USD";

var symbols = await client.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (!symbols.Success)
{
    Console.WriteLine($"Symbols failed: {symbols.Error}");
    return;
}

var ethUsd = symbols.Data.SingleOrDefault(x => x.Name == symbol || x.Symbol == symbol);
if (ethUsd != null)
{
    Console.WriteLine($"{ethUsd.Name}: min quantity={ethUsd.MinimumOrderQuantity}, min value={ethUsd.MinimumOrderValue}");
}

var ticker = await client.ExchangeApi.ExchangeData.GetTickerAsync(symbol);
if (!ticker.Success)
{
    Console.WriteLine($"Ticker failed: {ticker.Error}");
    return;
}

Console.WriteLine($"{symbol} last={ticker.Data.LastPrice}, bid={ticker.Data.BestBidPrice}, ask={ticker.Data.BestAskPrice}");

var orderBook = await client.ExchangeApi.ExchangeData.GetOrderBookAsync(symbol);
if (!orderBook.Success)
{
    Console.WriteLine($"Order book failed: {orderBook.Error}");
    return;
}

Console.WriteLine($"{symbol} book levels: bids={orderBook.Data.Bids.Length}, asks={orderBook.Data.Asks.Length}");

var candles = await client.ExchangeApi.ExchangeData.GetKlinesAsync(
    symbol,
    KlineInterval.OneMinute,
    limit: 5,
    excludeCurrentCandle: true);

if (candles.Success)
{
    foreach (var candle in candles.Data)
        Console.WriteLine($"{candle.OpenTime:u} open={candle.OpenPrice} close={candle.ClosePrice}");
}

var balances = await client.ExchangeApi.Account.GetAccountBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Balances failed: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Where(x => x.Asset is "ETH" or "USD"))
    Console.WriteLine($"{balance.Asset}: total={balance.Total}, available={balance.Available}, reserved={balance.Reserved}");

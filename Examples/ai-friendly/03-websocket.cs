// 03-websocket.cs
//
// Demonstrates: Bitstamp public and private websocket subscriptions.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net;
using Bitstamp.Net.Clients;

var socketClient = new BitstampSocketClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});

const string spotSymbol = "ETH/USD";
const string derivativeSymbol = "ETH/USD-PERP";

var tradeSubscription = await socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync(
    spotSymbol,
    update =>
    {
        Console.WriteLine($"{spotSymbol} trade: {update.Data.Quantity} @ {update.Data.Price}");
    });

if (!tradeSubscription.Success)
{
    Console.WriteLine($"Trade subscription failed: {tradeSubscription.Error}");
    return;
}

var bookSubscription = await socketClient.ExchangeApi.SubscribeToOrderBookSnapshotUpdatesAsync(
    spotSymbol,
    update =>
    {
        var bestBid = update.Data.Bids.FirstOrDefault();
        var bestAsk = update.Data.Asks.FirstOrDefault();
        Console.WriteLine($"{spotSymbol} book: bid={bestBid?.Price}, ask={bestAsk?.Price}");
    });

if (!bookSubscription.Success)
{
    Console.WriteLine($"Book subscription failed: {bookSubscription.Error}");
    await socketClient.UnsubscribeAsync(tradeSubscription.Data);
    return;
}

var fundingSubscription = await socketClient.ExchangeApi.SubscribeToFundingRateUpdatesAsync(
    derivativeSymbol,
    update =>
    {
        Console.WriteLine($"{update.Data.Symbol} funding={update.Data.FundingRate}, mark={update.Data.MarkPrice}");
    });

if (!fundingSubscription.Success)
{
    Console.WriteLine($"Funding subscription failed: {fundingSubscription.Error}");
    await socketClient.UnsubscribeAsync(tradeSubscription.Data);
    await socketClient.UnsubscribeAsync(bookSubscription.Data);
    return;
}

Console.WriteLine("Listening. Press Enter to unsubscribe.");
Console.ReadLine();

await socketClient.UnsubscribeAsync(tradeSubscription.Data);
await socketClient.UnsubscribeAsync(bookSubscription.Data);
await socketClient.UnsubscribeAsync(fundingSubscription.Data);

// 05-error-handling.cs
//
// Demonstrates: reusable Bitstamp.Net REST and socket result handling.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

var restClient = new BitstampRestClient();
var socketClient = new BitstampSocketClient();

var book = await restClient.ExchangeApi.ExchangeData.GetOrderBookAsync("ETH/USD");
if (!EnsureSuccess(book, "load order book"))
    return;

Console.WriteLine($"Best bid: {book.Data.Bids.FirstOrDefault()?.Price}");

var subscription = await socketClient.ExchangeApi.SubscribeToFullOrderBookUpdatesAsync(
    "ETH/USD",
    update =>
    {
        Console.WriteLine($"Book update: bids={update.Data.Bids.Length}, asks={update.Data.Asks.Length}");
    });

if (!EnsureSuccess(subscription, "subscribe to order book"))
    return;

await socketClient.UnsubscribeAsync(subscription.Data);

static bool EnsureSuccess<T>(WebCallResult<T> result, string action)
{
    if (result.Success)
        return true;

    Console.WriteLine($"Could not {action}: {result.Error}");
    return false;
}

static bool EnsureSuccess<T>(CallResult<T> result, string action)
{
    if (result.Success)
        return true;

    Console.WriteLine($"Could not {action}: {result.Error}");
    return false;
}

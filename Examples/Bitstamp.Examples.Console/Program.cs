
using Bitstamp.Net.Clients;

// REST
var restClient = new BitstampRestClient();
var ticker = await restClient.ExchangeApi.ExchangeData.GetTickerAsync("ETH/USD");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETH/USD: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new BitstampSocketClient();
var subscription = await socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD", update =>
{
    Console.WriteLine($"Websocket client trade price for ETH/USD: {update.Data.Price}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to trade updates: {subscription.Error}");
    return;
}

Console.ReadLine();

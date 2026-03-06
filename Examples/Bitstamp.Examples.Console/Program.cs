
using Bitstamp.Net.Clients;

// REST
var restClient = new BitstampRestClient();
var ticker = await restClient.ExchangeApi.ExchangeData.GetTickerAsync("ETH/USD");
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

Console.ReadLine();

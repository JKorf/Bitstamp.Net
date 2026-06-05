// 02-trading-and-positions.cs
//
// Demonstrates: Bitstamp limit order placement, open orders, cancellation and derivatives positions.
//
// Setup: dotnet add package Bitstamp.Net

using Bitstamp.Net;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Enums;

var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});

const string spotSymbol = "ETH/USD";
const string derivativeSymbol = "ETH/USD-PERP";

var order = await client.ExchangeApi.Trading.PlaceLimitOrderAsync(
    symbol: spotSymbol,
    side: OrderSide.Buy,
    price: 1m,
    orderType: OrderType.Limit,
    quantity: 0.01m,
    clientOrderId: $"ai-example-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");

if (!order.Success)
{
    Console.WriteLine($"Order rejected: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id} on {order.Data.Symbol}");

var openOrders = await client.ExchangeApi.Trading.GetOpenOrdersAsync(spotSymbol);
if (openOrders.Success)
    Console.WriteLine($"Open orders on {spotSymbol}: {openOrders.Data.Length}");

var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.Id);
Console.WriteLine(cancel.Success
    ? $"Canceled order {order.Data.Id}"
    : $"Cancel failed: {cancel.Error}");

var positions = await client.ExchangeApi.Trading.GetOpenPositionsAsync(derivativeSymbol);
if (positions.Success)
{
    foreach (var position in positions.Data)
        Console.WriteLine($"{position.Symbol}: side={position.Side}, quantity={position.Quantity}, leverage={position.Leverage}");
}

var leverageSettings = await client.ExchangeApi.Account.GetLeverageSettingsAsync(
    MarginMode.Cross,
    derivativeSymbol);

if (leverageSettings.Success)
    Console.WriteLine($"Leverage settings returned: {leverageSettings.Data.Length}");

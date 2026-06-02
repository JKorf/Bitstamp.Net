using Bitstamp.Net;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Enums;

const string spotSymbol = "ETH/USD";
const string derivativesSymbol = "ETH/USD-PERP";

// Replace with valid credentials or order placement will always fail.
var apiKey = "API_KEY";
var apiSecret = "API_SECRET";

Console.WriteLine("Bitstamp.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceDerivativesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(BitstampRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.ExchangeApi.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);
    var order = await client.ExchangeApi.Trading.PlaceLimitOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        price: safePrice,
        orderType: OrderType.Limit,
        quantity: 0.01m,
        clientOrderId: $"example-spot-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.Id, includeTrades: true);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, remaining: {orderStatus.Data.QuantityRemaining}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.Id}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceDerivativesReduceOnlyOrderExampleAsync(BitstampRestClient client)
{
    Console.WriteLine($"Placing derivatives reduce-only limit sell order for {derivativesSymbol}...");

    var ticker = await client.ExchangeApi.ExchangeData.GetTickerAsync(derivativesSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get derivatives ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 1.05m, 2);
    var order = await client.ExchangeApi.Trading.PlaceLimitOrderAsync(
        symbol: derivativesSymbol,
        side: OrderSide.Sell,
        price: safePrice,
        orderType: OrderType.Limit,
        quantity: 0.01m,
        reduceOnly: true,
        clientOrderId: $"example-derivatives-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place derivatives order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed derivatives order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.Id, includeTrades: true);
    if (orderStatus.Success)
        Console.WriteLine($"Derivatives order status: {orderStatus.Data.Status}, remaining: {orderStatus.Data.QuantityRemaining}");
    else
        Console.WriteLine($"Failed to query derivatives order: {orderStatus.Error}");

    var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled derivatives order {order.Data.Id}"
        : $"Failed to cancel derivatives order: {cancel.Error}");
}

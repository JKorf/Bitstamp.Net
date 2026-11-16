# Usage examples

Each snippet is independent. Where a snippet uses `client`, reuse a configured `BitstampRestClient`. Authenticated operations require credentials with the corresponding account permissions.

## Imports

```csharp
using System;
using System.Threading.Tasks;
using Bitstamp.Net;
using Bitstamp.Net.Clients;
using Bitstamp.Net.Enums;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public market data and results

```csharp
using var client = new BitstampRestClient();
var ticker = await client.ExchangeApi.ExchangeData.GetTickerAsync("ETH/USD");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}
Console.WriteLine(ticker.Data.LastPrice);
```

## Authentication

```csharp
using var client = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials(
        Environment.GetEnvironmentVariable("BITSTAMP_API_KEY")!,
        Environment.GetEnvironmentVariable("BITSTAMP_API_SECRET")!);
});
```

## Account balances

```csharp
var balances = await client.ExchangeApi.Account.GetAccountBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine(balances.Error);
    return;
}
foreach (var balance in balances.Data)
    Console.WriteLine($"{balance.Asset}: available={balance.Available}, total={balance.Total}");
```

## Limit order and cancellation

This submits an order when executed. Choose a market, quantity, and price appropriate to the intended trade; review [execution considerations](safety.md#orders-and-cancellations).

```csharp
var order = await client.ExchangeApi.Trading.PlaceLimitOrderAsync(
    symbol: "ETH/USD", side: OrderSide.Buy, price: 1m,
    orderType: OrderType.Limit, quantity: 0.01m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine(order.Data.Id);
var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.Id);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

## Derivatives account reads

```csharp
var positions = await client.ExchangeApi.Trading.GetOpenPositionsAsync("ETH/USD-PERP");
if (!positions.Success)
{
    Console.WriteLine(positions.Error);
    return;
}
foreach (var position in positions.Data)
    Console.WriteLine($"{position.Symbol}: {position.Side}, quantity={position.Quantity}");
var leverage = await client.ExchangeApi.Account.GetLeverageSettingsAsync(
    MarginMode.Cross, "ETH/USD-PERP");
if (!leverage.Success)
    Console.WriteLine(leverage.Error);
```

## WebSocket lifecycle

```csharp
using var socketClient = new BitstampSocketClient();
var subscription = await socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync(
    "ETH/USD", update => Console.WriteLine(update.Data.Price));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

## Shared API V2

```csharp
IGetSpotSymbolsRest symbols = client.ExchangeApi.SharedApi;
var result = await symbols.GetSpotSymbolsAsync(new GetSymbolsRequest());
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var symbol in result.Data)
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");
var catalog = symbols.SpotSymbolCatalog;
```

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddBitstamp(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<Bitstamp.Net.Interfaces.Clients.IBitstampRestClient>();
var shared = provider.GetRequiredService<Bitstamp.Net.Interfaces.Clients.IBitstampSharedApiClient>();
```

## Complete examples (checkout-only)

These programs are compiled by the documentation test; running them can contact the exchange and execute their illustrated operations.

- [Market and account](../../../../Examples/ai-friendly/01-market-and-account.cs)
- [Trading and positions](../../../../Examples/ai-friendly/02-trading-and-positions.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Shared API](../../../../Examples/ai-friendly/04-shared-client.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)

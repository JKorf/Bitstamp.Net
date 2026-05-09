---
description: "AI assistant guide for Bitstamp.Net"
applyTo: "**/*"
---

# Bitstamp.Net AI Coding Guide

Bitstamp.Net is a CryptoExchange.Net-based client for the Bitstamp REST and websocket APIs. Use this guide when generating code or documentation for this repository.

## Package And Client Shape

- NuGet package id: `Bitstamp.Net`
- Root namespace: `Bitstamp.Net`
- REST client: `BitstampRestClient`
- Socket client: `BitstampSocketClient`
- Only API root: `ExchangeApi`
- REST sub-clients:
  - `restClient.ExchangeApi.ExchangeData`
  - `restClient.ExchangeApi.Account`
  - `restClient.ExchangeApi.Trading`
- Socket API:
  - `socketClient.ExchangeApi`
- Shared clients:
  - `restClient.ExchangeApi.SharedClient`
  - `socketClient.ExchangeApi.SharedClient`

Do not invent Binance/Bitget-style roots such as `SpotApi`, `SpotApiV3`, `UsdFuturesApi`, `FuturesApiV2`, `CoinFuturesApi`, or `PerpetualFuturesApi`. Bitstamp.Net exposes a single `ExchangeApi` root.

## Credentials And Options

Bitstamp credentials are HMAC key and secret only:

```csharp
var restClient = new BitstampRestClient(options =>
{
    options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
});
```

There is no passphrase/memo parameter.

Useful options:

- `BitstampRestOptions.Environment` defaults to `BitstampEnvironment.Live`
- `BitstampRestOptions.ApiOptions` configures REST API-level behavior
- `BitstampSocketOptions.Environment` defaults to `BitstampEnvironment.Live`
- `BitstampSocketOptions.SocketSubscriptionsCombineTarget` defaults to `10`
- `BitstampSocketOptions.ApiOptions.PingInterval` defaults to 30 seconds
- Use `BitstampRestClient.SetDefaultOptions(...)` and `BitstampSocketClient.SetDefaultOptions(...)` for application-wide defaults
- Use `services.AddBitstamp(...)` for dependency injection

## Symbol Rules

Bitstamp symbols use slash-separated market names:

- Spot example: `ETH/USD`
- Derivatives/perpetual example: `ETH/USD-PERP`

Do not rewrite symbols into Binance/Bitget/Bitfinex formats:

- `ETHUSDT`
- `ETH_USDT`
- `ETH-USDT`
- `ETH/USDT` when the Bitstamp market is `ETH/USD`
- `tETHUSD`

Use `ExchangeData.GetSymbolsAsync()` or account `GetSymbolsAsync()` to discover available symbols.

## REST Endpoint Routing

Market data and public exchange data:

- `GetSymbolsAsync()`
- `GetAssetsAsync()`
- `GetAllTickersAsync()`
- `GetTickerAsync(symbol)`
- `GetHourTickerAsync(symbol)`
- `GetKlinesAsync(symbol, KlineInterval.OneMinute, ...)`
- `GetOrderBookAsync(symbol)`
- `GetTradesAsync(symbol, Period? period = null)`
- `GetEurUsdConversionRateAsync()`
- `GetFundingRateAsync(symbol)`
- `GetFundingRateHistoryAsync(symbol, ...)`
- `GetMarginTiersAsync()`
- `GetCollateralAssetsAsync()`

Account endpoints:

- `GetAccountBalancesAsync()`
- `GetAccountBalanceAsync(asset)`
- `GetWithdrawFeesAsync()`
- `GetWithdrawFeesAsync(asset, network)`
- `GetAllFeesAsync()`
- `GetFeesAsync(symbol)`
- `GetUserTransactionsAsync(...)`
- `GetSymbolsAsync()` for markets tradable by the authenticated user
- `GetMaxTradeQuantityAsync(...)`
- `GetWithdrawalsAsync(...)`
- `WithdrawFiatAsync(...)`
- `CancelWithdrawalAsync(id)`
- `GetFiatWithdrawalStatusAsync(id)`
- `WithdrawCryptoAsync(asset, quantity, address, ...)`
- `GetDepositAddressAsync(asset, network)`
- `GetCryptoTransactionsAsync(...)`
- `GetDepositsAsync(...)`
- `GetMarginInfoAsync()`
- `GetLeverageSettingsAsync(...)`
- `SetLeverageAsync(marginMode, symbol, leverage)`

Trading endpoints:

- `PlaceLimitOrderAsync(symbol, side, price, orderType, quantity, ...)`
- `PlaceMarketOrderAsync(symbol, side, orderType, quantity, ...)`
- `CancelOrderAsync(orderId, clientOrderId)`
- `GetOrderHistoryAsync(orderSource, symbol, ...)`
- `CancelAllOrdersAsync()`
- `CancelAllOrdersAsync(symbol)`
- `GetOrderAsync(orderId, clientOrderId, includeTrades)`
- `ReplaceOrderAsync(price, quantity, id, clientOrderId, newClientOrderId)`
- `GetOpenOrdersAsync()`
- `GetOpenOrdersAsync(symbol)`
- `GetDerivativesUserTradesAsync(...)`
- `GetOpenPositionsAsync()`
- `GetOpenPositionsAsync(symbol)`
- `GetPositionStatusAsync(positionId)`
- `GetPositionHistoryAsync(...)`
- `ClosePositionsAsync(symbol, marginMode)`
- `ClosePositionAsync(positionId)`
- `GetPositionSettlementTransactionsAsync(...)`
- `UpdatePositionCollateralAsync(positionId, newCollateralQuantity)`

## Websocket Routing

Use `socketClient.ExchangeApi`.

Public socket subscriptions:

- `SubscribeToTradeUpdatesAsync(symbol, ...)`
- `SubscribeToFullOrderBookUpdatesAsync(symbol, ...)`
- `SubscribeToOrderBookSnapshotUpdatesAsync(symbol, ...)`
- `SubscribeToFundingRateUpdatesAsync(symbol, ...)`

Private socket subscriptions:

- `SubscribeToOrderUpdatesAsync(symbol, ...)`
- `SubscribeToUserTradeUpdatesAsync(symbol, ...)`

Always check `subscription.Success` before using `subscription.Data`. Unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Enums To Use

- Klines: `KlineInterval.OneMinute`, `ThreeMinutes`, `FiveMinutes`, `FifteenMinute`, `ThirtyMinutes`, `OneHour`, `OneDay`
- Orders: `OrderSide.Buy`, `OrderSide.Sell`
- Order types: `OrderType.Limit`, `Market`, `StopMarket`, `StopLimit`, `TakeProfit`, `TakeProfitLimit`, trailing variants
- Margin: `MarginMode.Cross`, `MarginMode.Isolated`
- Trigger price: `TriggerType.LastTradePrice`, `IndexPrice`, `MarkPrice`
- Public trade period: `Period`
- Order history source: `OrderSource.Orderbook`, `OrderSource.StopOrder`

## Result Handling

REST calls return `WebCallResult<T>` and socket subscriptions return `CallResult<UpdateSubscription>`.

```csharp
var result = await client.ExchangeApi.ExchangeData.GetTickerAsync("ETH/USD");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

Never assume `Data` is populated when `Success` is false.

## Local Order Book And Trackers

Bitstamp.Net includes:

- `BitstampOrderBookFactory`
- `BitstampSymbolOrderBook`
- `BitstampTrackerFactory`
- `BitstampUserSpotDataTracker`
- `BitstampUserFuturesDataTracker`

Use these when code needs maintained local order book state or user data tracking instead of manually combining snapshots and websocket deltas.

## Common Pitfalls

- Do not use `SpotApi`, `FuturesApi`, `UsdFuturesApi`, or versioned API roots.
- Do not add a credentials passphrase.
- Do not convert Bitstamp symbols to another exchange format.
- Do not use `BinPeriod`; Bitstamp.Net uses `KlineInterval`.
- Do not call `GetBalancesAsync`; Bitstamp account balances are `GetAccountBalancesAsync` or `GetAccountBalanceAsync`.
- Spot and derivatives share `ExchangeApi`; the symbol and method determine which Bitstamp endpoint is used.

## Source Files To Inspect Before Changing API Usage

- `Bitstamp.Net/Interfaces/Clients/ExchangeApi/IBitstampRestClientExchangeApiExchangeData.cs`
- `Bitstamp.Net/Interfaces/Clients/ExchangeApi/IBitstampRestClientExchangeApiAccount.cs`
- `Bitstamp.Net/Interfaces/Clients/ExchangeApi/IBitstampRestClientExchangeApiTrading.cs`
- `Bitstamp.Net/Interfaces/Clients/ExchangeApi/IBitstampSocketClientExchangeApi.cs`
- `Bitstamp.Net/Objects/Options/BitstampRestOptions.cs`
- `Bitstamp.Net/Objects/Options/BitstampSocketOptions.cs`
- `Bitstamp.Net/BitstampCredentials.cs`
- `Examples/ai-friendly`

# Bitstamp.Net Copilot Instructions

Generate code against the actual Bitstamp.Net client shape.

Before making API-shape decisions, also read:

- `AGENTS.md` for the full repository-specific AI coding guide
- `llms.txt` for concise Bitstamp.Net context
- `llms-full.txt` for detailed endpoint routing, pitfalls and examples
- `docs/ai-api-map.md` for intent-to-method mapping

## Correct Client Roots

```csharp
var restClient = new BitstampRestClient();
var socketClient = new BitstampSocketClient();

await restClient.ExchangeApi.ExchangeData.GetTickerAsync("ETH/USD");
await restClient.ExchangeApi.Account.GetAccountBalancesAsync();
await restClient.ExchangeApi.Trading.GetOpenOrdersAsync("ETH/USD");
await socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH/USD", update => { });
```

Bitstamp.Net does not expose `SpotApi`, `UsdFuturesApi`, `FuturesApiV2`, `SpotApiV3`, `CoinFuturesApi`, or `PerpetualFuturesApi`.

## Credentials

```csharp
options.ApiCredentials = new BitstampCredentials("API_KEY", "API_SECRET");
```

There is no passphrase.

## Bitstamp-Specific Rules

- Use `ExchangeApi.ExchangeData`, `ExchangeApi.Account`, and `ExchangeApi.Trading`.
- Use symbols such as `ETH/USD` and `ETH/USD-PERP`.
- Use `KlineInterval`, not `BinPeriod`.
- Check `result.Success` before reading `result.Data`.
- Use `Account.GetAccountBalancesAsync()` for balances.
- Use `Trading.PlaceLimitOrderAsync` and `Trading.PlaceMarketOrderAsync` for orders.

## Frequent Endpoint Mapping

| Intent | Bitstamp.Net member |
| --- | --- |
| Symbols | `restClient.ExchangeApi.ExchangeData.GetSymbolsAsync()` |
| Ticker | `restClient.ExchangeApi.ExchangeData.GetTickerAsync(symbol)` |
| Klines | `restClient.ExchangeApi.ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneMinute, ...)` |
| Order book | `restClient.ExchangeApi.ExchangeData.GetOrderBookAsync(symbol)` |
| Balances | `restClient.ExchangeApi.Account.GetAccountBalancesAsync()` |
| Fees | `restClient.ExchangeApi.Account.GetFeesAsync(symbol)` |
| Limit order | `restClient.ExchangeApi.Trading.PlaceLimitOrderAsync(...)` |
| Market order | `restClient.ExchangeApi.Trading.PlaceMarketOrderAsync(...)` |
| Open orders | `restClient.ExchangeApi.Trading.GetOpenOrdersAsync(...)` |
| Open positions | `restClient.ExchangeApi.Trading.GetOpenPositionsAsync(...)` |
| Trades stream | `socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync(...)` |
| Order book stream | `socketClient.ExchangeApi.SubscribeToFullOrderBookUpdatesAsync(...)` |
| Private order stream | `socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync(...)` |

## Avoid

- `ETHUSDT`, `ETH_USDT`, `ETH-USDT`, `tETHUSD`
- `new BitstampCredentials(key, secret, passphrase)`
- `BinPeriod.OneMinute`
- `GetBalancesAsync`
- Any `SpotApi` or `FuturesApi` root

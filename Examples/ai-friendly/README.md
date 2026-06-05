# Bitstamp.Net AI-Friendly Examples

These examples are intentionally small console programs that AI assistants can copy from safely. They are compiled by `Bitstamp.Net.UnitTests/Documentation/AiExampleCompileTests.cs`.

## Files

| File | Demonstrates |
| --- | --- |
| `01-market-and-account.cs` | Public market data, balances, symbols and result handling |
| `02-trading-and-positions.cs` | Limit orders, open orders, cancellation and derivatives positions |
| `03-websocket.cs` | Public and private websocket subscriptions |
| `04-shared-client.cs` | CryptoExchange.Net shared client access from Bitstamp.Net |
| `05-error-handling.cs` | Reusable REST/socket result handling helpers |

## Bitstamp Shape To Remember

Use:

```csharp
restClient.ExchangeApi.ExchangeData
restClient.ExchangeApi.Account
restClient.ExchangeApi.Trading
socketClient.ExchangeApi
```

Do not use exchange roots from other libraries such as `SpotApi`, `UsdFuturesApi`, `FuturesApiV2`, `SpotApiV3`, `CoinFuturesApi`, or `PerpetualFuturesApi`.

Bitstamp credentials use only API key and API secret:

```csharp
new BitstampCredentials("API_KEY", "API_SECRET")
```

Use Bitstamp symbols such as `ETH/USD` and `ETH/USD-PERP`. Use `KlineInterval` for candles.

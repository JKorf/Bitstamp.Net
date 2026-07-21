# Bitstamp.Net AI API Map

This map helps AI assistants route common user intents to the actual Bitstamp.Net methods.

## Client Roots

| Need | Use |
| --- | --- |
| REST client | `new BitstampRestClient(...)` |
| Socket client | `new BitstampSocketClient(...)` |
| Public market data | `restClient.ExchangeApi.ExchangeData` |
| Authenticated account data | `restClient.ExchangeApi.Account` |
| Orders and positions | `restClient.ExchangeApi.Trading` |
| Websocket subscriptions | `socketClient.ExchangeApi` |
| Shared REST abstraction | `restClient.ExchangeApi.SharedClient` |
| Shared socket abstraction | `socketClient.ExchangeApi.SharedClient` |
| Discover shared capabilities | `client.ExchangeApi.SharedClient.Discover()` |

## Market Data

| Intent | Method |
| --- | --- |
| Symbols | `ExchangeData.GetSymbolsAsync()` |
| Assets | `ExchangeData.GetAssetsAsync()` |
| All tickers | `ExchangeData.GetAllTickersAsync()` |
| Ticker | `ExchangeData.GetTickerAsync(symbol)` |
| Hour ticker | `ExchangeData.GetHourTickerAsync(symbol)` |
| Candles | `ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneMinute, ...)` |
| Order book | `ExchangeData.GetOrderBookAsync(symbol)` |
| Recent trades | `ExchangeData.GetTradesAsync(symbol, ...)` |
| EUR/USD conversion | `ExchangeData.GetEurUsdConversionRateAsync()` |
| Funding rate | `ExchangeData.GetFundingRateAsync(symbol)` |
| Funding history | `ExchangeData.GetFundingRateHistoryAsync(symbol, ...)` |
| Margin tiers | `ExchangeData.GetMarginTiersAsync()` |
| Collateral assets | `ExchangeData.GetCollateralAssetsAsync()` |

## Account

| Intent | Method |
| --- | --- |
| All balances | `Account.GetAccountBalancesAsync()` |
| Asset balance | `Account.GetAccountBalanceAsync(asset)` |
| Withdrawal fees | `Account.GetWithdrawFeesAsync(...)` |
| Trading fees | `Account.GetFeesAsync(symbol)` |
| All trading fees | `Account.GetAllFeesAsync()` |
| User transactions | `Account.GetUserTransactionsAsync(...)` |
| Tradable user markets | `Account.GetSymbolsAsync()` |
| Max order quantity | `Account.GetMaxTradeQuantityAsync(...)` |
| Withdrawals | `Account.GetWithdrawalsAsync(...)` |
| Crypto withdrawal | `Account.WithdrawCryptoAsync(...)` |
| Fiat withdrawal | `Account.WithdrawFiatAsync(...)` |
| Deposit address | `Account.GetDepositAddressAsync(asset, network)` |
| Crypto transactions | `Account.GetCryptoTransactionsAsync(...)` |
| Deposits | `Account.GetDepositsAsync(...)` |
| Margin info | `Account.GetMarginInfoAsync()` |
| Leverage settings | `Account.GetLeverageSettingsAsync(...)` |
| Set leverage | `Account.SetLeverageAsync(...)` |

## Trading

| Intent | Method |
| --- | --- |
| Place limit order | `Trading.PlaceLimitOrderAsync(...)` |
| Place market order | `Trading.PlaceMarketOrderAsync(...)` |
| Cancel one order | `Trading.CancelOrderAsync(orderId: ...)` or `Trading.CancelOrderAsync(clientOrderId: ...)` |
| Cancel all orders | `Trading.CancelAllOrdersAsync()` |
| Cancel symbol orders | `Trading.CancelAllOrdersAsync(symbol)` |
| Get order | `Trading.GetOrderAsync(...)` |
| Replace order | `Trading.ReplaceOrderAsync(...)` |
| Open orders | `Trading.GetOpenOrdersAsync(...)` |
| Order history | `Trading.GetOrderHistoryAsync(OrderSource.Orderbook, symbol, ...)` |
| Derivatives user trades | `Trading.GetDerivativesUserTradesAsync(...)` |
| Open positions | `Trading.GetOpenPositionsAsync(...)` |
| Position status | `Trading.GetPositionStatusAsync(positionId)` |
| Position history | `Trading.GetPositionHistoryAsync(...)` |
| Close positions | `Trading.ClosePositionsAsync(...)` |
| Update collateral | `Trading.UpdatePositionCollateralAsync(...)` |

## Sockets

| Intent | Method |
| --- | --- |
| Trades | `SubscribeToTradeUpdatesAsync(symbol, ...)` |
| Full/diff order book | `SubscribeToFullOrderBookUpdatesAsync(symbol, ...)` |
| Order book snapshots | `SubscribeToOrderBookSnapshotUpdatesAsync(symbol, ...)` |
| Funding rate | `SubscribeToFundingRateUpdatesAsync(symbol, ...)` |
| Private order updates | `SubscribeToOrderUpdatesAsync(symbol, ...)` |
| Private user trade updates | `SubscribeToUserTradeUpdatesAsync(symbol, ...)` |

## Shared APIs

Use SharedApis for exchange-agnostic code across Bitstamp.Net and other CryptoExchange.Net exchange libraries.

| Intent | Pattern |
| --- | --- |
| Shared REST client | `new BitstampRestClient().ExchangeApi.SharedClient` |
| Shared socket client | `new BitstampSocketClient().ExchangeApi.SharedClient` |
| Discover shared capabilities | `client.ExchangeApi.SharedClient.Discover()` |
| Shared spot symbols | `ISpotSymbolRestClient.GetSpotSymbolsAsync(new GetSymbolsRequest())` |
| Cached spot symbol catalog | `ISpotSymbolRestClient.SpotSymbolCatalog` after a successful spot symbol request |
| Shared spot ticker | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared futures symbols | `IFuturesSymbolRestClient.GetFuturesSymbolsAsync(new GetSymbolsRequest())` |
| Cached futures symbol catalog | `IFuturesSymbolRestClient.FuturesSymbolCatalog` after a successful futures symbol request |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

Shared REST calls return `HttpResult<T>` / `HttpResult`. Shared socket subscriptions return `WebSocketResult<UpdateSubscription>`. Shared non-I/O symbol/cache helpers such as symbol support checks return `ExchangeCallResult<T>`.

`GetSymbolsRequest` can filter spot and futures symbols by base/quote asset type and subtype. Results include `DisplayName`, asset types (`Crypto`, `Fiat`, or `TradFi`), and applicable subtypes (`StableCoin`, `Equity`, or `Commodity`).

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
| --- | --- |
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `WebSocketResult<UpdateSubscription> sub = await ...; if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Shared helper data | Read `ExchangeCallResult<T>.Data` only after `result.Success` |

## Avoid / Replace

| Avoid | Use |
| --- | --- |
| `SpotApi` | `ExchangeApi` |
| `UsdFuturesApi` | `ExchangeApi` |
| `FuturesApiV2` | `ExchangeApi` |
| `SpotApiV3` | `ExchangeApi` |
| `BinPeriod.OneMinute` | `KlineInterval.OneMinute` |
| `GetBalancesAsync` | `GetAccountBalancesAsync` |
| `ETHUSDT`, `ETH_USDT`, `ETH-USDT`, `tETHUSD` | Bitstamp-native symbols such as `ETH/USD` |
| `BitstampCredentials(key, secret, passphrase)` | `BitstampCredentials(key, secret)` |

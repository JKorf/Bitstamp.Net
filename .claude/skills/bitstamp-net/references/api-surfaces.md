# API surfaces

## Client roots and symbols

`client` below is a `BitstampRestClient`; `socketClient` is a `BitstampSocketClient`. Both expose `ExchangeApi` for spot and derivatives. There are no separate spot or futures client roots.

REST market data lives under `ExchangeData`, authenticated account data under `Account`, and orders and positions under `Trading`. Socket subscriptions live directly under `ExchangeApi`.

Native symbols use slash-separated assets, for example `ETH/USD` for spot and `ETH/USD-PERP` for a derivative. Request available symbols before selecting a market. Candles use `KlineInterval`, order sides use `OrderSide`, derivatives margin settings use `MarginMode.Cross` or `MarginMode.Isolated`, and order history requires an `OrderSource`.

## Market Data

| Intent | Method |
| --- | --- |
| Symbols | `client.ExchangeApi.ExchangeData.GetSymbolsAsync()` |
| Assets | `client.ExchangeApi.ExchangeData.GetAssetsAsync()` |
| All tickers | `client.ExchangeApi.ExchangeData.GetAllTickersAsync()` |
| Ticker | `client.ExchangeApi.ExchangeData.GetTickerAsync(symbol)` |
| Hour ticker | `client.ExchangeApi.ExchangeData.GetHourTickerAsync(symbol)` |
| Candles | `client.ExchangeApi.ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneMinute, ...)` |
| Order book | `client.ExchangeApi.ExchangeData.GetOrderBookAsync(symbol)` |
| Recent trades | `client.ExchangeApi.ExchangeData.GetTradesAsync(symbol, ...)` |
| EUR/USD conversion | `client.ExchangeApi.ExchangeData.GetEurUsdConversionRateAsync()` |
| Funding rate | `client.ExchangeApi.ExchangeData.GetFundingRateAsync(symbol)` |
| Funding history | `client.ExchangeApi.ExchangeData.GetFundingRateHistoryAsync(symbol, ...)` |
| Margin tiers | `client.ExchangeApi.ExchangeData.GetMarginTiersAsync()` |
| Collateral assets | `client.ExchangeApi.ExchangeData.GetCollateralAssetsAsync()` |

## Account

| Intent | Method |
| --- | --- |
| All balances | `client.ExchangeApi.Account.GetAccountBalancesAsync()` |
| Asset balance | `client.ExchangeApi.Account.GetAccountBalanceAsync(asset)` |
| Withdrawal fees | `client.ExchangeApi.Account.GetWithdrawFeesAsync(...)` |
| Trading fees | `client.ExchangeApi.Account.GetFeesAsync(symbol)` |
| All trading fees | `client.ExchangeApi.Account.GetAllFeesAsync()` |
| User transactions | `client.ExchangeApi.Account.GetUserTransactionsAsync(...)` |
| Tradable user markets | `client.ExchangeApi.Account.GetSymbolsAsync()` |
| Max order quantity | `client.ExchangeApi.Account.GetMaxTradeQuantityAsync(...)` |
| Withdrawals | `client.ExchangeApi.Account.GetWithdrawalsAsync(...)` |
| Crypto withdrawal | `client.ExchangeApi.Account.WithdrawCryptoAsync(...)` |
| Fiat withdrawal | `client.ExchangeApi.Account.WithdrawFiatAsync(...)` |
| Deposit address | `client.ExchangeApi.Account.GetDepositAddressAsync(asset, network)` |
| Crypto transactions | `client.ExchangeApi.Account.GetCryptoTransactionsAsync(...)` |
| Deposits | `client.ExchangeApi.Account.GetDepositsAsync(...)` |
| Margin info | `client.ExchangeApi.Account.GetMarginInfoAsync()` |
| Leverage settings | `client.ExchangeApi.Account.GetLeverageSettingsAsync(...)` |
| Set leverage | `client.ExchangeApi.Account.SetLeverageAsync(...)` |

## Trading

| Intent | Method |
| --- | --- |
| Place limit order | `client.ExchangeApi.Trading.PlaceLimitOrderAsync(...)` |
| Place market order | `client.ExchangeApi.Trading.PlaceMarketOrderAsync(...)` |
| Cancel one order | `client.ExchangeApi.Trading.CancelOrderAsync(orderId: ...)` or `client.ExchangeApi.Trading.CancelOrderAsync(clientOrderId: ...)` |
| Cancel all orders | `client.ExchangeApi.Trading.CancelAllOrdersAsync()` |
| Cancel symbol orders | `client.ExchangeApi.Trading.CancelAllOrdersAsync(symbol)` |
| Get order | `client.ExchangeApi.Trading.GetOrderAsync(...)` |
| Replace order | `client.ExchangeApi.Trading.ReplaceOrderAsync(...)` |
| Open orders | `client.ExchangeApi.Trading.GetOpenOrdersAsync(...)` |
| Order history | `client.ExchangeApi.Trading.GetOrderHistoryAsync(OrderSource.Orderbook, symbol, ...)` |
| Derivatives user trades | `client.ExchangeApi.Trading.GetDerivativesUserTradesAsync(...)` |
| Open positions | `client.ExchangeApi.Trading.GetOpenPositionsAsync(...)` |
| Position status | `client.ExchangeApi.Trading.GetPositionStatusAsync(positionId)` |
| Position history | `client.ExchangeApi.Trading.GetPositionHistoryAsync(...)` |
| Close one position | `client.ExchangeApi.Trading.ClosePositionAsync(...)` |
| Close positions | `client.ExchangeApi.Trading.ClosePositionsAsync(...)` |
| Update collateral | `client.ExchangeApi.Trading.UpdatePositionCollateralAsync(...)` |

## Sockets

| Intent | Method |
| --- | --- |
| Trades | `socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync(symbol, ...)` |
| Full/diff order book | `socketClient.ExchangeApi.SubscribeToFullOrderBookUpdatesAsync(symbol, ...)` |
| Order book snapshots | `socketClient.ExchangeApi.SubscribeToOrderBookSnapshotUpdatesAsync(symbol, ...)` |
| Funding rate | `socketClient.ExchangeApi.SubscribeToFundingRateUpdatesAsync(symbol, ...)` |
| Private order updates | `socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync(symbol, ...)` |
| Private user trade updates | `socketClient.ExchangeApi.SubscribeToUserTradeUpdatesAsync(symbol, ...)` |

## Shared API V2

Use `client.ExchangeApi.SharedApi` and `socketClient.ExchangeApi.SharedApi`. The DI aggregate `IBitstampSharedApiClient` exposes `Rest` and `Socket`.

| Intent | Capability |
| --- | --- |
| Spot symbol metadata and cached catalog | `IGetSpotSymbolsRest`, `SpotSymbolCatalog` |
| Futures symbol metadata and cached catalog | `IGetFuturesSymbolsRest`, `FuturesSymbolCatalog` |
| Ticker and all tickers | `IGetTickerRest`, `IGetAllTickersRest` |
| Candles, order books, recent trades | `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest` |
| Balances, assets, fees | `IGetBalancesRest`, `IGetAssetRest`, `IGetAllAssetsRest`, `IGetFeesRest` |
| Spot order placement and lifecycle | `IPlaceSpotOrder`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `ICancelSpotOrderRest` |
| Futures order placement and lifecycle | `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `ICancelFuturesOrderRest` |
| Futures positions and leverage | `IGetPositionsRest`, `ICloseFullPositionRest`, `IGetLeverageRest`, `ISetLeverageRest` |
| Funding history and open interest | `IGetFundingRateHistoryRest`, `IGetOpenInterestRest` |
| Trade subscriptions | `ISubscribeTradesSocket` |
| Order book subscriptions | `ISubscribeOrderBookSocket` |

The shared socket surface supports trades and order books; it does not expose ticker subscriptions. Private socket workflows use native subscriptions.

For runtime selection, call the aggregate's `GetCapability` with a descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode. Check for a missing match before accessing its `Capability` and `Options`. Use `GetCapabilities` to enumerate implementations and `Discover()` to inspect a surface's supported operations. Do not infer support merely from another exchange's implementation.

Call `GetSpotSymbolsAsync(new GetSymbolsRequest())` or `GetFuturesSymbolsAsync(new GetSymbolsRequest())` successfully before using the corresponding cached catalog. Requests can filter base/quote asset types and subtypes. Symbol metadata includes `DisplayName`, `Crypto`/`Fiat`/`TradFi` asset types, and applicable `StableCoin`/`Equity`/`Commodity` subtypes.

Shared orders use `SharedOrderQuantity`; choose the requested quantity representation supported by that capability. Shared REST and socket order books report `SharedQuantityType.BaseAsset`. Shared requests use shared symbol models rather than assuming a native string is accepted everywhere.

Shared REST results use `HttpResult<T>` / `HttpResult`, subscription results use `WebSocketResult<UpdateSubscription>`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`. Check `Success` in each case.

## Source navigation (checkout-only)

- [Public client interfaces](../../../../Bitstamp.Net/Interfaces/Clients/) contain exact overloads and optional arguments.
- [Symbol order books](../../../../Bitstamp.Net/SymbolOrderBooks/) maintain local books; use them when a synchronized book is required rather than treating a diff as a snapshot.
- [Tracker factory](../../../../Bitstamp.Net/BitstampTrackerFactory.cs) and [user data trackers](../../../../Bitstamp.Net/BitstampUserDataTracker.cs) provide higher-level tracking.

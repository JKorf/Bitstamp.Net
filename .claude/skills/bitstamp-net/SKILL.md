---
name: bitstamp-net
description: Use Bitstamp.Net to implement or review Bitstamp spot and derivatives REST calls, account and order workflows, WebSocket subscriptions, Shared API integration, and dependency injection in .NET applications.
---
# Bitstamp.Net

Install the library with `dotnet add package Bitstamp.Net`.

- Create `BitstampRestClient` and `BitstampSocketClient` from `Bitstamp.Net.Clients`. Reuse clients and dispose them when their owner ends.
- Both spot and derivatives use `ExchangeApi`. REST groups are `ExchangeData`, `Account`, and `Trading`; subscriptions are directly on the socket `ExchangeApi`.
- Configure `ApiCredentials = new BitstampCredentials(key, secret)` from `Bitstamp.Net`. No passphrase or customer ID is required.
- Use native symbols such as `ETH/USD` and `ETH/USD-PERP`; obtain supported symbols from the exchange rather than concatenating asset names.
- REST calls return `HttpResult<T>` or `HttpResult`; subscriptions return `WebSocketResult<UpdateSubscription>`. Check `Success` before accessing `Data`, and report `Error` on failure.
- Keep the concrete socket client for cleanup: unsubscribe successful subscriptions with `UnsubscribeAsync(subscription.Data)`.
- Use `ExchangeApi.SharedApi` for Shared API V2. Prefer capability interfaces for portable code; inspect capabilities and trading modes before selecting an implementation dynamically.

## References

- Read [API surfaces](references/api-surfaces.md) to select roots, methods, capabilities, symbols, and quantities.
- Read [usage](references/usage.md) for focused authentication, account, trading, subscription, Shared API, and DI snippets.
- Read [operational considerations](references/safety.md) when handling account changes, derivatives, cancellations, or retries.

Checkout-only: [repository development instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).

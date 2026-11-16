# Operational considerations

## Credentials and environments

Bitstamp uses an API key and secret, with no passphrase. Keep credentials outside source and give keys the permissions needed for the selected operations. Public market data does not require credentials; private REST and socket operations do. Configure credentials on the socket client for private subscriptions rather than inventing an external listen-key workflow.

`BitstampEnvironment.Live` is the named built-in environment. A custom URL does not by itself provide a sandbox or simulated execution.

## Orders and cancellations

Limit and market orders use separate placement methods. A limit order can fill immediately when its price crosses the book. An optional `clientOrderId` supports workflow correlation; do not assume the library creates one for you or that an identifier alone makes resubmission idempotent.

Cancellation can race with a fill. Check its result and reconcile order status and trades; submitting cancellation does not prove the order had no executions. Confirm the intended scope before using broad cancellation methods.

## Derivatives and funds

Spot and derivatives share `ExchangeApi`, but use different markets and account semantics. Select a supported derivative symbol and verify margin mode (`Cross` or `Isolated`), leverage, quantity, and reduce-only behavior for the intended operation. Leverage changes and collateral updates change account exposure. Distinguish closing one position from broader position-closing operations.

Withdrawals require the correct asset, network, destination, and applicable destination metadata. A successful request submission should be reconciled with its resulting withdrawal status.

## Ambiguous outcomes and retries

A timeout or lost response to an order, cancellation, leverage, collateral, or withdrawal request can occur after the exchange accepted it. Query the relevant order, transaction, or account state before deciding to retry. A transient transport error does not make a repeated account-changing request safe. Handle public read retries separately from submission recovery.

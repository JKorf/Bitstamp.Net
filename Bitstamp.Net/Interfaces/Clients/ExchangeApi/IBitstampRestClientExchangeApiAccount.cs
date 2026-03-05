using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitstampRestClientExchangeApiAccount
    {
        /// <summary>
        /// Get user account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Account-balances/operation/GetAccountBalances" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/account_balances/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampAccountBalance[]>> GetAccountBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get user account balance
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Account-balances/operation/GetAccountBalancesForCurrency" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/account_balances/{asset}/
        /// </para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampAccountBalance>> GetAccountBalanceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get withdraw fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Fees/operation/GetAllWithdrawalFees" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/fees/withdrawal/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawFee[]>> GetWithdrawFeesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get withdraw fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Fees/operation/GetWithdrawalFeeForCurrency" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/fees/withdrawal/{asset}/
        /// </para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawFee>> GetWithdrawFeesAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get trading fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Fees/operation/GetAllTradingFees" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/fees/trading/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTradingFees[]>> GetAllFeesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get trading fees for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Fees/operation/GetTradingFeesForCurrency" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/fees/trading/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampTradingFees>> GetFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get user transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetUserTransactions" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/user_transactions/
        /// </para>
        /// </summary>
        /// <param name="offset">Result offset</param>
        /// <param name="limit">Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="sinceId">Filter results since id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(
            SortOrder? sort = null,
            long? sinceId = null, 
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null, 
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user transaction history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetUserTransactionsForMarket" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/user_transactions/{symbol}/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD`</param>
        /// <param name="offset">Result offset</param>
        /// <param name="limit">Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="sinceId">Filter results since id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(
            string symbol,
            SortOrder? sort = null, 
            long? sinceId = null, 
            DateTime? startTime = null, 
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get symbols tradable for the user
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/GetUserTradingMarkets" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/my_markets/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampAccountSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get max tradable quantity for the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Orders/operation/MaxBuySellResource" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/get_max_order_amount/
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="orderType">Order type</param>
        /// <param name="side">Order side</param>
        /// <param name="price">Order price</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="activationPrice">Activation price</param>
        /// <param name="trailingDelta">Trailing delta</param>
        /// <param name="additionalCollateral">Additional collateral asset->quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampMaxTradeQuantity>> GetMaxTradeQuantityAsync(
            string symbol,
            MarginMode marginMode,
            decimal leverage,
            OrderType orderType,
            OrderSide side,
            decimal? price = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? trailingDelta = null,
            Dictionary<string, decimal>? additionalCollateral = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get withdrawals
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/GetWithdrawalRequests" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/withdrawal-requests/
        /// </para>
        /// </summary>
        /// <param name="id">Filter by id</param>
        /// <param name="maxAge">Max age in number of seconds to return</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawal[]>> GetWithdrawalsAsync(
            string? id = null, 
            long? maxAge = null, 
            int? limit = null, 
            int? offset = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Withdraw fiat to a bank account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/RequestFiatWithdrawal" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/withdrawal/open/
        /// </para>
        /// </summary>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="asset">The asset to withdraw, for example `USD`</param>
        /// <param name="name">Full user or company name</param>
        /// <param name="iban">IBAN to withdraw to</param>
        /// <param name="bic">Target bank BIC</param>
        /// <param name="address">User or company address</param>
        /// <param name="postalCode">User or company postal code</param>
        /// <param name="city">User or company city</param>
        /// <param name="country">User or company country</param>
        /// <param name="type">Withdraw type</param>
        /// <param name="bankName">Bank name for international withdrawal</param>
        /// <param name="bankAddress">Bank address for international withdrawal</param>
        /// <param name="bankPostalCode">Bank postal code for international withdrawal</param>
        /// <param name="bankCity">Bank city  for international withdrawal</param>
        /// <param name="bankCountry">Bank country for international withdrawal</param>
        /// <param name="currency">The currency in which the funds should be withdrawn</param>
        /// <param name="comment"></param>
        /// <param name="intermediateBankRouting">Intermediary bank routing number / bic</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawId>> WithdrawFiatAsync(
            decimal quantity,
            string asset,
            string name,
            string iban,
            string bic,
            string address,
            string postalCode, 
            string city, 
            string country,
            WithdrawType type,
            string? bankName = null,
            string? bankAddress = null,
            string? bankPostalCode = null,
            string? bankCity = null,
            string? bankCountry = null,
            string? currency = null,
            string? comment = null,
            string? intermediateBankRouting = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an in progress withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/CancelWithdrawal" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/withdrawal/cancel/
        /// </para>
        /// </summary>
        /// <param name="id">Withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCancelWithdrawResponse>> CancelWithdrawalAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get fiat withdrawal status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/GetFiatWithdrawalStatus" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/withdrawal/status/
        /// </para>
        /// </summary>
        /// <param name="id">The withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampFiatWithdrawalStatus>> GetFiatWithdrawalStatusAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Withdraw crypto. Only non-thirdparty withdrawals supported atm
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/RequestCryptoWithdrawal" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/{asset}_withdrawal/
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `eth`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="address">Address</param>
        /// <param name="network">Network</param>
        /// <param name="memoId">Memo</param>
        /// <param name="destinationTag">Destination tag</param>
        /// <param name="transferId">Transfer id</param>
        /// <param name="beneficiaryThirdparty">If the address you are withdrawing to is in your name (regardless of if this is a hosted or unhosted wallet), this should be set to False. If you are withdrawing to a third party, set it to True.</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawId>> WithdrawCryptoAsync(
            string asset,
            decimal quantity,
            string address,
            string? network = null,
            string? memoId = null,
            string? destinationTag = null,
            string? transferId = null,
            bool? beneficiaryThirdparty = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Deposits/operation/GetCryptoDepositAddress" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/{asset}_address/
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get crypto transactions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetCryptoUserTransactions" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/crypto-transactions/
        /// </para>
        /// </summary>
        /// <param name="includeIous">Include Ripple IOUs or not</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCryptoTransactions>> GetCryptoTransactionsAsync(
            bool? includeIous = null,
            DateTime? startTime = null, 
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetCryptoDepositsWithReviewStatus" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/crypto-transactions/deposits/
        /// </para>
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCryptoDeposit[]>> GetDepositsAsync(
            DepositStatus? status = null,
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get margin info/balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetMarginInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/margin_info/
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampMarginInfo>> GetMarginInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage settings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetLeverageSettingsList" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/leverage_settings/
        /// </para>
        /// </summary>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampLeverageSetting[]>> GetLeverageSettingsAsync(MarginMode? marginMode = null, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/UpdateLeverageSettingsList" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/leverage_settings/
        /// </para>
        /// </summary>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="leverage">Leverage setting</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampLeverageSetting>> SetLeverageAsync(MarginMode marginMode, string symbol, decimal leverage, CancellationToken ct = default);

    }
}

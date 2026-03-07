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
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
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
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
        /// <param name="network">["<c>network</c>"] Network</param>
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
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
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
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="limit">["<c>limit</c>"] Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">["<c>sort</c>"] Sorting direction</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="sinceId">["<c>since_id</c>"] Filter results since id</param>
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
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH/USD`</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="limit">["<c>limit</c>"] Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">["<c>sort</c>"] Sorting direction</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="sinceId">["<c>since_id</c>"] Filter results since id</param>
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
        /// <param name="symbol">["<c>market</c>"] Symbol, for example `ETH/USD-PERP`</param>
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="orderType">["<c>order_type</c>"] Order type</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="price">["<c>price</c>"] Order price</param>
        /// <param name="stopPrice">["<c>stop_price</c>"] Stop price</param>
        /// <param name="activationPrice">["<c>activation_price</c>"] Activation price</param>
        /// <param name="trailingDelta">["<c>trailing_delta</c>"] Trailing delta</param>
        /// <param name="additionalCollateral">["<c>additional_collateral</c>"] Additional collateral asset->quantity</param>
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
        /// <param name="id">["<c>id</c>"] Filter by id</param>
        /// <param name="maxAge">["<c>timedelta</c>"] Max age in number of seconds to return</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
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
        /// <param name="quantity">["<c>quantity</c>"] Quantity to withdraw</param>
        /// <param name="asset">["<c>account_currency</c>"] The asset to withdraw, for example `USD`</param>
        /// <param name="name">["<c>name</c>"] Full user or company name</param>
        /// <param name="iban">["<c>iban</c>"] IBAN to withdraw to</param>
        /// <param name="bic">["<c>bic</c>"] Target bank BIC</param>
        /// <param name="address">["<c>address</c>"] User or company address</param>
        /// <param name="postalCode">["<c>postal_code</c>"] User or company postal code</param>
        /// <param name="city">["<c>city</c>"] User or company city</param>
        /// <param name="country">["<c>country</c>"] User or company country</param>
        /// <param name="type">["<c>type</c>"] Withdraw type</param>
        /// <param name="bankName">["<c>bank_name</c>"] Bank name for international withdrawal</param>
        /// <param name="bankAddress">["<c>bank_address</c>"] Bank address for international withdrawal</param>
        /// <param name="bankPostalCode">["<c>bank_postal_code</c>"] Bank postal code for international withdrawal</param>
        /// <param name="bankCity">["<c>bank_city</c>"] Bank city  for international withdrawal</param>
        /// <param name="bankCountry">["<c>bank_country</c>"] Bank country for international withdrawal</param>
        /// <param name="currency">["<c>currency</c>"] The currency in which the funds should be withdrawn</param>
        /// <param name="comment">["<c>comment</c>"]</param>
        /// <param name="intermediateBankRouting">["<c>intermed_routing_num_or_bic</c>"] Intermediary bank routing number / bic</param>
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
        /// <param name="id">["<c>id</c>"] Withdrawal id</param>
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
        /// <param name="id">["<c>id</c>"] The withdrawal id</param>
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
        /// <param name="asset">["<c>asset</c>"] The asset, for example `eth`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="address">["<c>address</c>"] Address</param>
        /// <param name="network">["<c>network</c>"] Network</param>
        /// <param name="memoId">["<c>memo_id</c>"] Memo</param>
        /// <param name="destinationTag">["<c>destination_tag</c>"] Destination tag</param>
        /// <param name="transferId">["<c>transfer_id</c>"] Transfer id</param>
        /// <param name="beneficiaryThirdparty">["<c>beneficiary_thirdparty</c>"] If the address you are withdrawing to is in your name (regardless of if this is a hosted or unhosted wallet), this should be set to False. If you are withdrawing to a third party, set it to True.</param>
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
        /// <param name="asset">["<c>asset</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>network</c>"] Network</param>
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
        /// <param name="includeIous">["<c>include_ious</c>"] Include Ripple IOUs or not</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
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
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="startTime">["<c>since_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>until_timestamp</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
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
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="symbol">["<c>market</c>"] Symbol name</param>
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
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="symbol">["<c>market</c>"] Symbol</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage setting</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampLeverageSetting>> SetLeverageAsync(MarginMode marginMode, string symbol, decimal leverage, CancellationToken ct = default);

    }
}

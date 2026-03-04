using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitstamp.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Bitstamp account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitstampRestClientExchangeApiAccount
    {


        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Account-balances/operation/GetAccountBalances"/> 
        /// </summary>
        Task<WebCallResult<BitstampAccountBalance[]>> GetAccountBalancesAsync(CancellationToken ct = default);

        Task<WebCallResult<BitstampAccountBalance>> GetAccountBalanceAsync(string asset, CancellationToken ct = default);
        Task<WebCallResult<BitstampWithdrawFee[]>> GetWithdrawFeesAsync(CancellationToken cancellationToken = default);
        Task<WebCallResult<BitstampWithdrawFee>> GetWithdrawFeesAsync(string asset, string? network = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Fees/operation/GetAllTradingFees"/>"
        /// </summary>
        Task<WebCallResult<BitstampTradingFees[]>> GetAllFeesAsync(CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Fees/operation/GetTradingFeesForCurrency"/>"
        /// </summary>
        Task<WebCallResult<BitstampTradingFees>> GetFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetUserTransactions"/>
        /// </summary>
        /// <param name="offset">Skip that many transactions before returning results (default: 0, maximum: 200000). If you need to export older history contact support OR use combination of limit and since_id parameters.</param>
        /// <param name="limit">Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting by date and time: asc - ascending; desc - descending (default: desc).</param>
        /// <param name="startDate">(Optional) Show only transactions from unix timestamp (for max 30 days old).</param>
        /// <param name="endDate">Show only transactions to unix timestamp (for max 30 days old).</param>
        /// <param name="sinceId">(Optional) Show only transactions from specified transaction id. If since_id parameter is used, limit parameter is set to 1000.</param>
        Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// <seealso href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetUserTransactions"/>
        /// </summary>
        /// <param name="offset">Skip that many transactions before returning results (default: 0, maximum: 200000). If you need to export older history contact support OR use combination of limit and since_id parameters.</param>
        /// <param name="limit">Limit result to that many transactions (default: 100; maximum: 1000).</param>
        /// <param name="sort">Sorting by date and time: asc - ascending; desc - descending (default: desc).</param>
        /// <param name="startDate">(Optional) Show only transactions from unix timestamp (for max 30 days old).</param>
        /// <param name="endDate">Show only transactions to unix timestamp (for max 30 days old).</param>
        /// <param name="sinceId">(Optional) Show only transactions from specified transaction id. If since_id parameter is used, limit parameter is set to 1000.</param>
        Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(string symbol, SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        Task<WebCallResult<BitstampAccountSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

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
        /// <seealso href="https://www.bitstamp.net/api/#tag/Websocket/operation/GetWebsocketToken"/> 
        /// </summary>
        Task<WebCallResult<BitstampSocketAuthToken>> GenerateWebsocketAuthTokenAsync(CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal
        /// <para><a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/GetWithdrawalRequests" /></para>
        /// </summary>
        /// <param name="id">Filter by id</param>
        /// <param name="maxAge">Max age in number of seconds to return</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampWithdrawal>> GetWithdrawalsAsync(string? id = null, long? maxAge = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw fiat to a bank account
        /// <para><a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/RequestFiatWithdrawal" /></para>
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
        /// <para><a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/CancelWithdrawal" /></para>
        /// </summary>
        /// <param name="id">Withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCancelWithdrawResponse>> CancelWithdrawalAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get fiat withdrawal status
        /// <para><a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/GetFiatWithdrawalStatus" /></para>
        /// </summary>
        /// <param name="id">The withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampFiatWithdrawalStatus>> GetFiatWithdrawalStatusAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Withdraw crypto. Only non-thirdparty withdrawals supported atm
        /// <para><a href="https://www.bitstamp.net/api/#tag/Withdrawals/operation/RequestCryptoWithdrawal" /></para>
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
        /// <para><a href="https://www.bitstamp.net/api/#tag/Deposits/operation/GetCryptoDepositAddress" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get crypto transactions
        /// <para><a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetCryptoUserTransactions" /></para>
        /// </summary>
        /// <param name="includeIous">Include Ripple IOUs or not</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCryptoTransactions>> GetCryptoTransactionsAsync(bool? includeIous = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://www.bitstamp.net/api/#tag/Transactions-private/operation/GetCryptoDepositsWithReviewStatus" /></para>
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampCryptoDeposit[]>> GetDepositsAsync(DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get margin info/balances
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetMarginInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampMarginInfo>> GetMarginInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage settings
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/GetLeverageSettingsList" /></para>
        /// </summary>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampLeverageSetting[]>> GetLeverageSettingsAsync(string? marginMode = null, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para><a href="https://www.bitstamp.net/api/#tag/Derivatives-(Prelaunch)/operation/UpdateLeverageSettingsList" /></para>
        /// </summary>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="leverage">Leverage setting</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitstampLeverageSetting>> SetLeverageAsync(MarginMode marginMode, string symbol, decimal leverage, CancellationToken ct = default);

    }
}

using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class BitstampRestClientExchangeApiAccount : IBitstampRestClientExchangeApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitstampRestClientExchangeApi _baseClient;

        internal BitstampRestClientExchangeApiAccount(BitstampRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get All Fees

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTradingFees[]>> GetAllFeesAsync(CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/fees/trading/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampTradingFees[]>(request, null, cancellationToken);
        }

        #endregion

        #region Get Fees

        /// <inheritdoc />
        public Task<WebCallResult<BitstampTradingFees>> GetFeesAsync(string symbol, CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/fees/trading/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampTradingFees>(request, null, cancellationToken);
        }

        #endregion

        #region Get Withdraw Fees

        /// <inheritdoc />
        public Task<WebCallResult<BitstampWithdrawFee[]>> GetWithdrawFeesAsync(CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/fees/withdrawal/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampWithdrawFee[]>(request, null, cancellationToken);
        }

        #endregion

        #region Get Withdraw Fees

        /// <inheritdoc />
        public Task<WebCallResult<BitstampWithdrawFee>> GetWithdrawFeesAsync(string asset, string? network = null, CancellationToken cancellationToken = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/fees/withdrawal/{asset}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampWithdrawFee>(request, parameters, cancellationToken);
        }

        #endregion

        #region Generate Websocket Auth Token

        /// <inheritdoc />
        public Task<WebCallResult<BitstampSocketAuthToken>> GenerateWebsocketAuthTokenAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/websockets_token/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampSocketAuthToken>(request, null, ct);
        }

        #endregion

        #region Get Account Balances

        /// <inheritdoc />
        public Task<WebCallResult<BitstampAccountBalance[]>> GetAccountBalancesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/account_balances/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountBalance[]>(request, null, ct);
        }

        #endregion

        #region Get Account Balance

        /// <inheritdoc />
        public Task<WebCallResult<BitstampAccountBalance>> GetAccountBalanceAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/account_balances/{asset.ToLower()}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountBalance>(request, null, ct);
        }

        #endregion

        #region Get User Transactions

        /// <inheritdoc />
        public Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("offset", offset);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("sort", sort);
            parameters.AddOptionalSeconds("since_timestamp", startTime);
            parameters.AddOptionalSeconds("until_timestamp", endTime);
            parameters.AddOptional("since_id", sinceId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/user_transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTransaction[]>(request, parameters, ct);
        }

        #endregion

        #region Get User Transactions

        /// <inheritdoc />
        public Task<WebCallResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(string symbol, SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("offset", offset);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("sort", sort);
            parameters.AddOptionalSeconds("since_timestamp", startTime);
            parameters.AddOptionalSeconds("until_timestamp", endTime);
            parameters.AddOptional("since_id", sinceId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/user_transactions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTransaction[]>(request, parameters, ct);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public Task<WebCallResult<BitstampAccountSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/my_markets/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountSymbol[]>(request, null, ct);
        }

        #endregion

        #region Get Max Trade Quantity

        /// <inheritdoc />
        public Task<WebCallResult<BitstampMaxTradeQuantity>> GetMaxTradeQuantityAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", BitstampExchange.SymbolToPathParameter(symbol));
            parameters.AddEnum("margin_mode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.AddEnum("order_type", orderType);
            parameters.AddEnum("side", side);
            parameters.AddOptional("price", price);
            parameters.AddOptional("stop_price", price);
            parameters.AddOptional("activation_price", price);
            parameters.AddOptional("trailing_delta", price);
            parameters.AddOptional("activation_price", price);
            parameters.AddOptional("additional_collateral", additionalCollateral);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/get_max_order_amount/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampMaxTradeQuantity>(request, parameters, ct);
        }

        #endregion

        #region Get Withdraws

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampWithdrawal>> GetWithdrawalsAsync(string? id = null, long? maxAge = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("id", id);
            parameters.AddOptional("timedelta", maxAge);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/withdrawal-requests/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawal>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw Fiat

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampWithdrawId>> WithdrawFiatAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("quantity", quantity);
            parameters.Add("account_currency", asset);
            parameters.Add("name", name);
            parameters.Add("iban", iban);
            parameters.Add("bic", bic);
            parameters.Add("address", address);
            parameters.Add("postal_code", postalCode);
            parameters.Add("city", city);
            parameters.Add("country", country);
            parameters.AddEnum("type", type);
            parameters.AddOptional("bank_name", bankName);
            parameters.AddOptional("bank_address", bankAddress);
            parameters.AddOptional("bank_postal_code", bankPostalCode);
            parameters.AddOptional("bank_city", bankCity);
            parameters.AddOptional("bank_country", bankCountry);
            parameters.AddOptional("currency", currency);
            parameters.AddOptional("comment", comment);
            parameters.AddOptional("intermed_routing_num_or_bic", intermediateBankRouting);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/withdrawal/open/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampCancelWithdrawResponse>> CancelWithdrawalAsync(string id, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("id", id);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/withdrawal/cancel/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCancelWithdrawResponse>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fiat Withdrawal Status

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampFiatWithdrawalStatus>> GetFiatWithdrawalStatusAsync(string id, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("id", id);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/withdrawal/status/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampFiatWithdrawalStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw Crypto

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampWithdrawId>> WithdrawCryptoAsync(
            string asset,
            decimal quantity, 
            string address,
            string? network = null,
            string? memoId = null,
            string? destinationTag = null,
            string? transferId = null,
            bool? beneficiaryThirdparty = null,
            CancellationToken ct = default)
        {
            if (beneficiaryThirdparty == true)
                throw new NotImplementedException("Thirdparty not supported");

            var parameters = new ParameterCollection();
            parameters.AddString("amount", quantity);
            parameters.Add("address", address);
            parameters.AddOptional("network", network);
            parameters.AddOptional("memo_id", memoId);
            parameters.AddOptional("destination_tag", destinationTag);
            parameters.AddOptional("transfer_id", transferId);
            parameters.AddOptionalBoolString("beneficiary_thirdparty", beneficiaryThirdparty);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/{asset}_withdrawal/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v2/{asset.ToLower()}_address/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Crypto Transactions

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampCryptoTransactions>> GetCryptoTransactionsAsync(bool? includeIous = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("include_ious", includeIous);
            parameters.AddOptionalMillisecondsString("since_timestamp", startTime);
            parameters.AddOptionalMillisecondsString("until_timestamp", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/crypto-transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCryptoTransactions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampCryptoDeposit[]>> GetDepositsAsync(DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalMillisecondsString("since_timestamp", startTime);
            parameters.AddOptionalMillisecondsString("until_timestamp", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/crypto-transactions/deposits/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCryptoDeposit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Margin Info

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampMarginInfo>> GetMarginInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/margin_info/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampMarginInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Settings

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampLeverageSetting[]>> GetLeverageSettingsAsync(string? marginMode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("margin_mode", marginMode);
            parameters.AddOptional("market", BitstampExchange.SymbolToPathParameter(symbol));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/leverage_settings/", BitstampExchange.RateLimiter.Rest, 1, true);
            var result = await _baseClient.SendAsync<BitstampLeverageSetting[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<BitstampLeverageSetting>> SetLeverageAsync(MarginMode marginMode, string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("margin_mode", marginMode);
            parameters.Add("market", BitstampExchange.SymbolToPathParameter(symbol));
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/leverage_settings/", BitstampExchange.RateLimiter.Rest, 1, true);
            var result = await _baseClient.SendAsync<BitstampLeverageSetting>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}

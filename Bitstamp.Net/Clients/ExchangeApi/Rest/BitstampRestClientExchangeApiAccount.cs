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
        public Task<HttpResult<BitstampTradingFees[]>> GetAllFeesAsync(CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/fees/trading/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampTradingFees[]>(request, null, cancellationToken);
        }

        #endregion

        #region Get Fees

        /// <inheritdoc />
        public Task<HttpResult<BitstampTradingFees>> GetFeesAsync(string symbol, CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/fees/trading/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampTradingFees>(request, null, cancellationToken);
        }

        #endregion

        #region Get Withdraw Fees

        /// <inheritdoc />
        public Task<HttpResult<BitstampWithdrawFee[]>> GetWithdrawFeesAsync(CancellationToken cancellationToken = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/fees/withdrawal/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampWithdrawFee[]>(request, null, cancellationToken);
        }

        #endregion

        #region Get Withdraw Fees

        /// <inheritdoc />
        public Task<HttpResult<BitstampWithdrawFee>> GetWithdrawFeesAsync(string asset, string? network = null, CancellationToken cancellationToken = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/fees/withdrawal/{asset}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampWithdrawFee>(request, parameters, cancellationToken);
        }

        #endregion

        #region Generate Websocket Auth Token

        /// <inheritdoc />
        internal Task<HttpResult<BitstampSocketAuthToken>> GenerateWebsocketAuthTokenAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/websockets_token/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampSocketAuthToken>(request, null, ct);
        }

        #endregion

        #region Get Account Balances

        /// <inheritdoc />
        public Task<HttpResult<BitstampAccountBalance[]>> GetAccountBalancesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/account_balances/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountBalance[]>(request, null, ct);
        }

        #endregion

        #region Get Account Balance

        /// <inheritdoc />
        public Task<HttpResult<BitstampAccountBalance>> GetAccountBalanceAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/account_balances/{asset.ToLower()}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountBalance>(request, null, ct);
        }

        #endregion

        #region Get User Transactions

        /// <inheritdoc />
        public Task<HttpResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("offset", offset);
            parameters.Add("limit", limit);
            parameters.Add("sort", sort);
            parameters.Add("since_timestamp", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("until_timestamp", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("since_id", sinceId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/user_transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTransaction[]>(request, parameters, ct);
        }

        #endregion

        #region Get User Transactions

        /// <inheritdoc />
        public Task<HttpResult<BitstampUserTransaction[]>> GetUserTransactionsAsync(string symbol, SortOrder? sort = null, long? sinceId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("offset", offset);
            parameters.Add("limit", limit);
            parameters.Add("sort", sort);
            parameters.Add("since_timestamp", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("until_timestamp", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("since_id", sinceId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/user_transactions/{BitstampExchange.SymbolToPathParameter(symbol)}/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampUserTransaction[]>(request, parameters, ct);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public Task<HttpResult<BitstampAccountSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/my_markets/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampAccountSymbol[]>(request, null, ct);
        }

        #endregion

        #region Get Max Trade Quantity

        /// <inheritdoc />
        public Task<HttpResult<BitstampMaxTradeQuantity>> GetMaxTradeQuantityAsync(
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
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("market", BitstampExchange.SymbolToPathParameter(symbol));
            parameters.Add("margin_mode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.Add("order_type", orderType);
            parameters.Add("side", side);
            parameters.Add("price", price);
            parameters.Add("stop_price", price);
            parameters.Add("activation_price", price);
            parameters.Add("trailing_delta", price);
            parameters.Add("activation_price", price);
            parameters.AddRaw("additional_collateral", additionalCollateral);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/get_max_order_amount/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            return _baseClient.SendAsync<BitstampMaxTradeQuantity>(request, parameters, ct);
        }

        #endregion

        #region Get Withdraws

        /// <inheritdoc />
        public async Task<HttpResult<BitstampWithdrawal[]>> GetWithdrawalsAsync(string? id = null, long? maxAge = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            parameters.Add("timedelta", maxAge);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/withdrawal-requests/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw Fiat

        /// <inheritdoc />
        public async Task<HttpResult<BitstampWithdrawId>> WithdrawFiatAsync(
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
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("quantity", quantity);
            parameters.Add("account_currency", asset);
            parameters.Add("name", name);
            parameters.Add("iban", iban);
            parameters.Add("bic", bic);
            parameters.Add("address", address);
            parameters.Add("postal_code", postalCode);
            parameters.Add("city", city);
            parameters.Add("country", country);
            parameters.Add("type", type);
            parameters.Add("bank_name", bankName);
            parameters.Add("bank_address", bankAddress);
            parameters.Add("bank_postal_code", bankPostalCode);
            parameters.Add("bank_city", bankCity);
            parameters.Add("bank_country", bankCountry);
            parameters.Add("currency", currency);
            parameters.Add("comment", comment);
            parameters.Add("intermed_routing_num_or_bic", intermediateBankRouting);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/withdrawal/open/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<HttpResult<BitstampCancelWithdrawResponse>> CancelWithdrawalAsync(string id, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/withdrawal/cancel/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCancelWithdrawResponse>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fiat Withdrawal Status

        /// <inheritdoc />
        public async Task<HttpResult<BitstampFiatWithdrawalStatus>> GetFiatWithdrawalStatusAsync(string id, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/withdrawal/status/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampFiatWithdrawalStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw Crypto

        /// <inheritdoc />
        public async Task<HttpResult<BitstampWithdrawId>> WithdrawCryptoAsync(
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

            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("amount", quantity);
            parameters.Add("address", address);
            parameters.Add("network", network);
            parameters.Add("memo_id", memoId);
            parameters.Add("destination_tag", destinationTag);
            parameters.Add("transfer_id", transferId);
            parameters.Add("beneficiary_thirdparty", beneficiaryThirdparty);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/{asset}_withdrawal/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<BitstampDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v2/{asset.ToLower()}_address/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Crypto Transactions

        /// <inheritdoc />
        public async Task<HttpResult<BitstampCryptoTransactions>> GetCryptoTransactionsAsync(bool? includeIous = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("include_ious", includeIous);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/crypto-transactions/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCryptoTransactions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<HttpResult<BitstampCryptoDeposit[]>> GetDepositsAsync(DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("status", status);
            parameters.Add("since_timestamp", startTime);
            parameters.Add("until_timestamp", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/crypto-transactions/deposits/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampCryptoDeposit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Margin Info

        /// <inheritdoc />
        public async Task<HttpResult<BitstampMarginInfo>> GetMarginInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/margin_info/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampMarginInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Settings

        /// <inheritdoc />
        public async Task<HttpResult<BitstampLeverageSetting[]>> GetLeverageSettingsAsync(MarginMode? marginMode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("market", symbol == null ? null : BitstampExchange.SymbolToPathParameter(symbol));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/leverage_settings/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampLeverageSetting[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<BitstampLeverageSetting>> SetLeverageAsync(MarginMode marginMode, string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitstampExchange._parameterSerializationSettings);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("market", BitstampExchange.SymbolToPathParameter(symbol));
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/leverage_settings/", BitstampExchange.RateLimiter.Rest, 1, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<BitstampLeverageSetting>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}

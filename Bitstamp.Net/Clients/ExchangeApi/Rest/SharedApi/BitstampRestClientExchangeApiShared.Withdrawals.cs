using Bitstamp.Net.Enums;
using Bitstamp.Net.Interfaces.Clients.ExchangeApi;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Bitstamp.Net.Clients.ExchangeApi
{
    internal partial class BitstampRestClientExchangeSharedApi
    {
        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, nextPageToken, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, false, 1000);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalsAsync(
                request.Asset,
                limit: limit,
                offset: pageParams.Offset ?? 0,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(
                            x.Asset,
                            x.Address,
                            x.Quantity, 
                            x.Status == WithdrawalStatus.Finished,
                            x.Timestamp,
                            GetWithdrawalStatus(x))
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionId
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(BitstampWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Canceled || x.Status == WithdrawalStatus.Failed)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawalStatus.Finished)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawalStatus.InProcess || x.Status == WithdrawalStatus.Open)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawCryptoAsync(
                request.Asset,
                quantity: request.Quantity,
                address: request.Address,
                network: request.Network,
                memoId: request.AddressTag,
                destinationTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.WithdrawalId.ToString()));
        }

        #endregion
    }
}

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
        #region Deposit client
        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressAsync(request.Asset, request.Network!, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, new[] { new SharedDepositAddress(request.Asset, depositAddresses.Data.Address)
            {
                Network = request.Network,
                TagOrMemo = depositAddresses.Data.DestinationTag?.ToString() ?? depositAddresses.Data.MemoId
            }
            });
        }

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetDepositHistoryAsync(request, nextPageToken, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, false, 1000);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetDepositsAsync(
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                    result.Data.Length,
                    result.Data.Select(x => x.Timestamp),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Finalized,
                            x.Timestamp,
                            ParseTransferStatus(x.Status))
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionId
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Finalized)
                return SharedTransferStatus.Completed;
            if (status == DepositStatus.InProgress || status == DepositStatus.Pending)
                return SharedTransferStatus.InProgress;
            if (status == DepositStatus.Reverted || status == DepositStatus.Rejected)
                return SharedTransferStatus.Failed;

            return SharedTransferStatus.Unknown;
        }

        #endregion
    }
}

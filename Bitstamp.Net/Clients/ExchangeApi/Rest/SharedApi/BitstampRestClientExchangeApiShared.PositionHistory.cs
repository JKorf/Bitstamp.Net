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
        #region Position History client

        public GetPositionHistoryOptions GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, false, 1000)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetPositionHistoryRequest.Symbol), typeof(SharedSymbol), "The symbol to get position history for", "ETH-USDT")
            }
        };
        public async Task<HttpResult<SharedPositionHistory[]>> GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Trading.GetPositionHistoryAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                sinceId: pageParams.FromId,
                sort: SortOrder.Descending,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromId(result.Data.OrderBy(x => x.CloseTime).First().Id),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.QuantityDelta >= 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                            x.EntryPrice,
                            x.ExitPrice,
                            new SharedOrderQuantity(x.QuantityDelta),
                            x.RealizedPnl,
                            x.CloseTime)
                        {
                            Leverage = x.Leverage,
                            PositionId = x.Id
                        })
                    .ToArray(), nextPageRequest);
        }
        #endregion
    }
}

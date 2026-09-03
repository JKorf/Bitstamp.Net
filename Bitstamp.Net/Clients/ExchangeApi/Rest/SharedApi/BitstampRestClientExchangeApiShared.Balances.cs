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
        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot, AccountTypeFilter.Margin);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Spot || request.AccountType == null)
            {
                var result = await _api.Account.GetAccountBalancesAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedBalance(
                        TradingMode.Spot, 
                        x.Asset, 
                        x.Available,
                        x.Total)).ToArray());
            }
            else
            {
                var result = await _api.Account.GetMarginInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                var resultList = new List<SharedBalance>();
                foreach (var item in result.Data.Assets)
                {
                    resultList.Add(
                        new SharedBalance(
                            TradingMode.PerpetualLinear,
                            item.Asset,
                            item.Available,
                            item.TotalQuantity));
                }

                return HttpResult.Ok(result, resultList.ToArray());
            }
        }

        #endregion
    }
}

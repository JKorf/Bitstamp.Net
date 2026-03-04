using Bitstamp.Net.Interfaces.Clients;
using Bitstamp.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitstamp.Net.Objects.Sockets
{
    internal class BitstampSocketKeyGenerator
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IBitstampRestClient _client;
        private BitstampSocketAuthToken? _lastKey;
        private DateTime _lastKeyValidUntil = DateTime.MinValue;

        public BitstampSocketKeyGenerator(IBitstampRestClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CallResult<BitstampSocketAuthToken>> GenerateWebsocketKeyAsync()
        {
            if (ValidKeyExists())
                return new CallResult<BitstampSocketAuthToken>(_lastKey!);

            await _semaphore.WaitAsync();
            if (ValidKeyExists())
            {
                _semaphore.Release();
                return new CallResult<BitstampSocketAuthToken>(_lastKey!);
            }

            try
            {
                var newKey = await _client.ExchangeApi.Account.GenerateWebsocketAuthTokenAsync();
                _lastKey = newKey.Data;
                _lastKeyValidUntil = DateTime.UtcNow.AddSeconds((newKey.Data?.ValidSeconds ?? 0) - 10); // New key will be instant invalid if no valid response is returned
                return newKey;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private bool ValidKeyExists()
        {
            return _lastKey?.Token != null && _lastKeyValidUntil > DateTime.UtcNow;
        }
    }
}

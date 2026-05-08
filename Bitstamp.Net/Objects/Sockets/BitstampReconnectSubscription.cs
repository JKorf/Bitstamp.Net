using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.Objects.Sockets
{
    internal class BitstampReconnectSubsciption : SystemSubscription
    {
        private readonly BitstampSocketKeyGenerator _keyGenerator;
        
        public BitstampReconnectSubsciption(ILogger logger, BitstampSocketKeyGenerator keyGenerator) : base(logger, false)
        {
            _keyGenerator = keyGenerator;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitstampSocketData<BitstampErrorData>>("bts:request_reconnect", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitstampSocketData<BitstampErrorData> message)
        {
            _logger.LogWarning("Received reconnect request from server, reconnecting");

            // A forced reconnect will invalidate the previously used websocket auth token
            // So ensure the next connection attempt will get a fresh token
            _keyGenerator.InvalidateKey();

            Task.Run(() => connection.TriggerReconnectAsync().ContinueWith(r =>
            {
                if (r.Exception != null)
                    _logger.LogError(r.Exception, "Error while reconnecting");
            }));

            return CallResult.SuccessResult;
        }
    }
}

using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.Objects.Sockets
{
    internal class BitstampReconnectSubsciption : SystemSubscription
    {
        public BitstampReconnectSubsciption(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitstampSocketData<BitstampErrorData>>("bts:request_reconnect", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitstampSocketData<BitstampErrorData> message)
        {
            _logger.LogWarning("Received reconnect request from server, reconnecting");
            Task.Run(() => connection.TriggerReconnectAsync().ContinueWith(r =>
            {
                if (r.Exception != null)
                    _logger.LogError(r.Exception, "Error while reconnecting");
            }));

            return CallResult.SuccessResult;
        }
    }
}

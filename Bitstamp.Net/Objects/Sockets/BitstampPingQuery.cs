using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Bitstamp.Net.Objects.Sockets
{
    internal class BitstampPingQuery : Query<BitstampSocketData<BitstampPingResponse>>
    {
        public BitstampPingQuery()
            : base(new BitstampSocketData<BitstampPingResponse>(SocketEventType.Heartbeat, null), true)
        {
            MessageRouter = MessageRouter.CreateForQuery<BitstampSocketData<BitstampPingResponse>>("bts:heartbeat", HandleMessage);
        }

        public CallResult<BitstampSocketData<BitstampPingResponse>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitstampSocketData<BitstampPingResponse> message)
        {
            if (message.Data?.Status != "success")
                return CallResult<BitstampSocketData<BitstampPingResponse>>.Fail(new ServerError("Ping failed", new(ErrorType.Unknown, $"Ping failed. Status: {message.Data?.Status}")), originalData);

            return CallResult<BitstampSocketData<BitstampPingResponse>>.Ok(message, originalData);
        }
    }
}

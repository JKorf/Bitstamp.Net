using Bitstamp.Net.Enums;
using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace Bitstamp.Net.Objects.Sockets
{
    internal class BitstampQuery<T> : Query<BitstampSocketData<T>>
    {
        public BitstampQuery(SocketEventType type, string? channel, BitstampSocketAuthToken? token = null)
            : base(new BitstampSocketData<BitstampSubscriptionData>(type, channel == null ? null : new BitstampSubscriptionData(channel, token?.Token)), true)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitstampSocketData<T>>("query_" + channel, HandleMessage);
        }


        public CallResult<BitstampSocketData<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitstampSocketData<T> message)
        {
            if (message.Event == SocketEventType.Error && message.Data is BitstampSubscriptionData subData)
                return new CallResult<BitstampSocketData<T>>(new ServerError(subData.Code?.ToString() ?? string.Empty, new(ErrorType.Unknown, subData.Message)));

            return new CallResult<BitstampSocketData<T>>(message, originalData, null);
        }
    }
}

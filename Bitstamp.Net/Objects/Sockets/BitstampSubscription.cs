using Bitstamp.Net.Objects.Models;
using Bitstamp.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;

namespace Bitstamp.Net.Objects.Sockets
{
    public abstract class BitstampSubscription : Subscription
    {
        public BitstampSocketAuthToken? AuthToken { get; set; }
        public bool RequiresAuthentication { get; }

        public BitstampSubscription(ILogger logger, bool requiresAuthentication)
            : base(logger, requiresAuthentication)
        { }
    }

    public class BitstampSubscription<T> : BitstampSubscription
    {
        private readonly string _channel;
        private readonly Action<DateTime, string?, BitstampSocketData<T>> _handler;

        public BitstampSubscription(ILogger logger, string channel, string symbol, Action<DateTime, string?, BitstampSocketData<T>> handler, BitstampSocketAuthToken? authToken = null)
            : base(logger, authToken != null)
        {
            _channel = $"{channel}_{symbol}";
            if (authToken?.UserId != null)
                _channel += $"-{authToken.UserId}";

            _handler = handler;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitstampSocketData<T>>(_channel, DoHandleMessage);

            AuthToken = authToken;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitstampSocketData<T> message)
        {
            if (message != null && message.Event is not Enums.SocketEventType.SubscriptionSucceeded)
                _handler?.Invoke(receiveTime, originalData, message);

            return CallResult.SuccessResult;
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new BitstampQuery<BitstampSubscriptionData>(Enums.SocketEventType.Subscribe, _channel, AuthToken);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BitstampQuery<BitstampSubscriptionData>(Enums.SocketEventType.Unsubscribe, _channel);
    }
}

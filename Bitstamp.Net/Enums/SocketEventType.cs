using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    [JsonConverter(typeof(EnumConverter<SocketEventType>))]
    internal enum SocketEventType
    {
        [Map("bts:subscribe")]
        Subscribe,

        [Map("bts:unsubscribe")]
        Unsubscribe,

        [Map("bts:error")]
        Error,

        [Map("bts:heartbeat")]
        Heartbeat,

        [Map("bts:subscription_succeeded")]
        SubscriptionSucceeded,

        [Map("bts:unsubscription_succeeded")]
        UnsubscriptionSucceeded,

        /// <summary>
        /// Data was received for an active subscription
        /// </summary>
        [Map("data")]
        Data,

        [Map("order_created")]
        OrderCreated,

        [Map("order_changed")]
        OrderChanged,

        [Map("order_deleted")]
        OrderDeleted,

        [Map("trade")]
        Trade,

        [Map("funding_rate_saved")]
        FundingRateSaved
    }
}

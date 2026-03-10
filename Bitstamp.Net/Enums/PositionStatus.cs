using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Position status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionStatus>))]
    public enum PositionStatus
    {
        /// <summary>
        /// ["<c>OPEN</c>"] Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// ["<c>WAITING_SETTLEMENT</c>"] Waiting for settlement
        /// </summary>
        [Map("WAITING_SETTLEMENT")]
        WaitingSettlement,
        /// <summary>
        /// ["<c>SETTLED</c>"] Settled
        /// </summary>
        [Map("SETTLED")]
        Settled,
        /// <summary>
        /// ["<c>LIQUIDATING</c>"] Liquidation
        /// </summary>
        [Map("LIQUIDATING")]
        Liquidation
    }
}

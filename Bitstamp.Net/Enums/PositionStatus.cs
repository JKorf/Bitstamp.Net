using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Position status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionStatus>))]
    public enum PositionStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// Waiting for settlement
        /// </summary>
        [Map("WAITING_SETTLEMENT")]
        WaitingSettlement,
        /// <summary>
        /// Settled
        /// </summary>
        [Map("SETTLED")]
        Settled,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUIDATING")]
        Liquidation
    }
}

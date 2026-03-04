using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>

    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("60")]
        OneMinute = 60,
        /// <summary>
        /// Three minutes
        /// </summary>
        [Map("180")]
        ThreeMinutes = 180,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("300")]
        FiveMinutes = 300,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("900")]
        FifteenMinute = 900,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("1800")]
        ThirtyMinutes = 1800,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("3600")]
        OneHour = 3600,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("7200")]
        TwoHours = 7200,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("14400")]
        FourHours = 14400,
        /// <summary>
        /// Six hours
        /// </summary>
        [Map("21600")]
        SixHours = 21600,
        /// <summary>
        /// Twelve hours
        /// </summary>
        [Map("43200")]
        TwelveHours = 43200,
        /// <summary>
        /// One day
        /// </summary>
        [Map("86400")]
        OneDay = 86400,
        /// <summary>
        /// Three days
        /// </summary>
        [Map("259200")]
        ThreeDays = 259200
    }
}

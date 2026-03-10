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
        /// ["<c>60</c>"] One minute
        /// </summary>
        [Map("60")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>180</c>"] Three minutes
        /// </summary>
        [Map("180")]
        ThreeMinutes = 180,
        /// <summary>
        /// ["<c>300</c>"] Five minutes
        /// </summary>
        [Map("300")]
        FiveMinutes = 300,
        /// <summary>
        /// ["<c>900</c>"] Fifteen minutes
        /// </summary>
        [Map("900")]
        FifteenMinute = 900,
        /// <summary>
        /// ["<c>1800</c>"] Thirty minutes
        /// </summary>
        [Map("1800")]
        ThirtyMinutes = 1800,
        /// <summary>
        /// ["<c>3600</c>"] One hour
        /// </summary>
        [Map("3600")]
        OneHour = 3600,
        /// <summary>
        /// ["<c>7200</c>"] Two hours
        /// </summary>
        [Map("7200")]
        TwoHours = 7200,
        /// <summary>
        /// ["<c>14400</c>"] Four hours
        /// </summary>
        [Map("14400")]
        FourHours = 14400,
        /// <summary>
        /// ["<c>21600</c>"] Six hours
        /// </summary>
        [Map("21600")]
        SixHours = 21600,
        /// <summary>
        /// ["<c>43200</c>"] Twelve hours
        /// </summary>
        [Map("43200")]
        TwelveHours = 43200,
        /// <summary>
        /// ["<c>86400</c>"] One day
        /// </summary>
        [Map("86400")]
        OneDay = 86400,
        /// <summary>
        /// ["<c>259200</c>"] Three days
        /// </summary>
        [Map("259200")]
        ThreeDays = 259200
    }
}

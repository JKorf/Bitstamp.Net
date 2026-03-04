using Bitstamp.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Close positions response
    /// </summary>
    public record BitstampClosePositionsResponse
    {
        /// <summary>
        /// Closed positions
        /// </summary>
        [JsonPropertyName("closed")]
        public BitstampPosition[] Closed { get; set; } = [];
        /// <summary>
        /// Failed
        /// </summary>
        [JsonPropertyName("failed")]
        public BitstampPosition[] Failed { get; set; } = [];
    }

}

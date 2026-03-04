using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Withdraw id
    /// </summary>
    public record BitstampWithdrawId
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        [JsonPropertyName("withdrawal_id")]
        public long WithdrawalId { get; set; }
    }
}

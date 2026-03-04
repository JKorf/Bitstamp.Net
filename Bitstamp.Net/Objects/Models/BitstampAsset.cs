using Bitstamp.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record BitstampAsset
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset type
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType Type { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Decimal places
        /// </summary>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        [JsonPropertyName("logo")]
        public string Logo { get; set; } = string.Empty;
        /// <summary>
        /// Available supply
        /// </summary>
        [JsonPropertyName("available_supply")]
        public decimal? AvailableSupply { get; set; }
        /// <summary>
        /// Deposit
        /// </summary>
        [JsonPropertyName("deposit")]
        public EnabledStatus DepositStatus { get; set; }
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawal")]
        public EnabledStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("networks")]
        public List<BitstampAssetNetwork> Networks { get; set; } = new List<BitstampAssetNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    public class BitstampAssetNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Deposit
        /// </summary>
        [JsonPropertyName("deposit")]
        public EnabledStatus DepositStatus { get; set; }
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawal")]
        public EnabledStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// Withdraw decimal places
        /// </summary>
        [JsonPropertyName("withdrawal_decimals")]
        public int WithdrawalDecimals { get; set; }
        /// <summary>
        /// Withdraw min amount
        /// </summary>
        [JsonPropertyName("withdrawal_minimum_amount")]
        public decimal WithdrawalMinQuantity { get; set; }
    }
}

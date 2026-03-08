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
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Asset type
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType Type { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>decimals</c>"] Decimal places
        /// </summary>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }
        /// <summary>
        /// ["<c>logo</c>"] Logo
        /// </summary>
        [JsonPropertyName("logo")]
        public string Logo { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available_supply</c>"] Available supply
        /// </summary>
        [JsonPropertyName("available_supply")]
        public decimal? AvailableSupply { get; set; }
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [JsonPropertyName("deposit")]
        public EnabledStatus DepositStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawal</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawal")]
        public EnabledStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// ["<c>networks</c>"] Networks
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
        /// ["<c>network</c>"] Network name
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [JsonPropertyName("deposit")]
        public EnabledStatus DepositStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawal</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawal")]
        public EnabledStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawal_decimals</c>"] Withdraw decimal places
        /// </summary>
        [JsonPropertyName("withdrawal_decimals")]
        public int WithdrawalDecimals { get; set; }
        /// <summary>
        /// ["<c>withdrawal_minimum_amount</c>"] Withdraw min amount
        /// </summary>
        [JsonPropertyName("withdrawal_minimum_amount")]
        public decimal WithdrawalMinQuantity { get; set; }
    }
}

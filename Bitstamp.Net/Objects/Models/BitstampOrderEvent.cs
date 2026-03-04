using Bitstamp.Net.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitstamp.Net.Objects.Models
{
    /// <summary>
    /// Order event
    /// </summary>
    public record BitstampOrderEvent
    {
        /// <summary>
        /// Order event
        /// </summary>
        [JsonPropertyName("event")]
        public OrderEvent Event { get; set; }
        /// <summary>
        /// Event id
        /// </summary>
        [JsonPropertyName("event_id")]
        public string EventId { get; set; } = string.Empty;
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public OrderSource OrderSource { get; set; }
        /// <summary>
        /// Trade account id
        /// </summary>
        [JsonPropertyName("trade_account_id")]
        public long TradeAccountId { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public BitstampOrderEventData Data { get; set; } = null!;
    }

    /// <summary>
    /// Order event data
    /// </summary>
    public record BitstampOrderEventData
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_subtype")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("microtimestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quantity traded
        /// </summary>
        [JsonPropertyName("amount_traded")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Quantity at create
        /// </summary>
        [JsonPropertyName("amount_at_create")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Is liquidation
        /// </summary>
        [JsonPropertyName("is_liquidation")]
        public bool IsLiquidation { get; set; }
        /// <summary>
        /// Trade account id
        /// </summary>
        [JsonPropertyName("trade_account_id")]
        public long? TradeAccountId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
    }


}

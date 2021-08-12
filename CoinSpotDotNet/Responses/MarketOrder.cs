using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Market order record
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rotransaction" />
    /// </para>
    /// </summary>
    public class MarketOrder
    {
        /// <summary>
        /// Coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; }

        /// <summary>
        /// Market coin short name, example values 'AUD', 'USDT'
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; }

        /// <summary>
        /// Coin rate
        /// </summary>
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        /// <summary>
        /// Coin amount
        /// </summary>
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public double Total { get; set; }

        /// <summary>
        /// Sold date
        /// </summary>
        [JsonPropertyName("solddate")]
        public DateTime SoldDate { get; set; }

        /// <summary>
        /// CoinSpot fee in AUD ex GST
        /// </summary>
        [JsonPropertyName("audfeeExGst")]
        public double AudFeeExGst { get; set; }

        /// <summary>
        /// Fee GST in AUD
        /// </summary>
        [JsonPropertyName("audGst")]
        public double AudGst { get; set; }

        /// <summary>
        /// AUD total
        /// </summary>
        [JsonPropertyName("audtotal")]
        public double AudTotal { get; set; }

    }
}

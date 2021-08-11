using CoinSpotDotNet.Common;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Base Class for Order Details
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#openorders"/>
    /// </para>
    /// </summary>
    public abstract class Order
    {
        /// <summary>
        /// Amount of coin on order
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Amount { get; set; }
        
        /// <summary>
        /// Rate asked
        /// </summary>
        [JsonPropertyName("rate")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Rate { get; set; }
        
        /// <summary>
        /// Total cost of Order (Amount * Rate)
        /// </summary>
        [JsonPropertyName("total")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Total { get; set; }

        /// <summary>
        /// The coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; }

        /// <summary>
        /// Market short name
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; }
    }
}

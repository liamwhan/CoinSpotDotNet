using CoinSpotDotNet.Common;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Coin price
    /// </summary>
    public class CoinPrice
    {
        /// <summary>
        /// Latest bid price
        /// </summary>
        [JsonPropertyName("bid")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Bid { get; set; }

        /// <summary>
        /// Latest ask price
        /// </summary>
        [JsonPropertyName("ask")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Ask { get; set; }
        
        /// <summary>
        /// Latest price
        /// </summary>
        [JsonPropertyName("last")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Last { get; set; }

    }
}

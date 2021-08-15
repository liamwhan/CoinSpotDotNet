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
        /// Last price
        /// </summary>
        [JsonPropertyName("last")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Last { get; set; }

        /// <summary>
        /// Allows auto deconstruction of this class
        /// </summary>
        public void Deconstruct(out double bid, out double ask, out double last)
        {
            bid = Bid;
            ask = Ask;
            last = Last;
        }

    }
}

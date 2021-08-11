using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{

    /// <summary>
    /// Public API Latest Prices response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#latestprices"/>
    /// </para>
    /// </summary>
    public class LatestPricesResponse : CoinSpotResponse
    {
        /// <summary>
        /// Dictionary of prices where the key is the coin indentifier
        /// </summary>
        [JsonPropertyName("prices")]
        public Dictionary<string, CoinPrice> Prices { get; set; }
    }
}

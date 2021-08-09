using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// CoinSpot v2 API Latest Prices response
    /// <para>
    /// See <see href="https://coinspot.com.au/v2/api#latestprices"/>
    /// </para>
    /// </summary>
    public class LatestPricesV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Dictionary of prices where the key is the coin indentifier
        /// </summary>
        [JsonPropertyName("prices")]
        public Dictionary<string, CoinPrice> Prices { get; set; }
    }
}

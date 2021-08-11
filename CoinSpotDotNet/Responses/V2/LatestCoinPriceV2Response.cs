using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// CoinSpot v2 API Latest Coin Price Response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#latestpricescoin"/>
    /// </para>
    /// </summary>
    public class LatestCoinPriceV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Coin price for the specified coin type
        /// </summary>
        [JsonPropertyName("prices")]
        public CoinPrice Prices { get; set; }
    }
}

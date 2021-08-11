using CoinSpotDotNet.Common;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Response type for endpoints that display latest price responses
    /// <para>
    /// See:
    /// <list type="bullet">
    /// <item><description><see href="https://www.coinspot.com.au/v2/api#latestbuyprice"/></description></item>
    /// <item><description><see href="https://www.coinspot.com.au/v2/api#latestsellprice"/></description></item>
    /// <item><description><see href="https://www.coinspot.com.au/v2/api#latestbuypricenonaud"/></description></item>
    /// <item><description><see href="https://www.coinspot.com.au/v2/api#latestsellpricenonaud"/></description></item>
    /// <item><description>... etc.</description></item>
    /// </list>
    /// 
    /// </para>
    /// </summary>
    public class RateMarketPriceV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Latest buy price for that coin
        /// </summary>
        [JsonPropertyName("rate")]
        [JsonConverter(typeof(CoinSpotStringToDoubleJsonConverter))]
        public double Rate { get; set; }

        /// <summary>
        /// Market coin is trading in
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; }

    }
}

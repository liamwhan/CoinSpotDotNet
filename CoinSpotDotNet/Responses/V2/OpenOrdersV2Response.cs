using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Open Orders Response
    /// </summary>
    public class OpenOrdersV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// List of top 100 open buy orders for the given coin / market
        /// </summary>
        [JsonPropertyName("buyorders")]
        public IEnumerable<OpenOrder> BuyOrders { get; set; }

        /// <summary>
        /// List of top 100 open sell orders for the given coin / market
        /// </summary>
        [JsonPropertyName("sellorders")]
        public IEnumerable<OpenOrder> SellOrders { get; set; }
    }
}

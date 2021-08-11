using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Completed Orders Response
    /// </summary>
    public class CompletedOrdersV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// List of top 100 completed buy orders for the given coin / market
        /// </summary>
        [JsonPropertyName("buyorders")]
        public IEnumerable<CompletedOrder> BuyOrders { get; set; }

        /// <summary>
        /// List of top 100 completed sell orders for the given coin / market
        /// </summary>
        [JsonPropertyName("sellorders")]
        public IEnumerable<CompletedOrder> SellOrders { get; set; }
    }
}

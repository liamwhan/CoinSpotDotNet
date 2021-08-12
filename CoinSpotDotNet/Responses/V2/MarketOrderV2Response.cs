using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Market Order History response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rotransaction"/>
    /// </para>
    /// </summary>
    public class MarketOrderV2Response : CoinSpotV2Response
    {

        /// <summary>
        /// Array containing your buy order history
        /// </summary>
        [JsonPropertyName("buyorders")]
        public IEnumerable<MarketOrder> BuyOrders { get; set; }

        /// <summary>
        /// array containing your sell order history
        /// </summary>
        [JsonPropertyName("sellorders")]
        public IEnumerable<MarketOrder> SellOrders { get; set; }

    }
}

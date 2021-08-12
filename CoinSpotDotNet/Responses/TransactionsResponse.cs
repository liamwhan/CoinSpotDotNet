using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Response model for <c>/api/ro/my/transactions</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rotransaction"/>
    /// </para>
    /// </summary>
    public class TransactionsResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array containing your buy order history
        /// </summary>
        [JsonPropertyName("buyorders")]
        public IEnumerable<BuyOrder> BuyOrders { get; set; }

        /// <summary>
        /// Array containing your sell order history
        /// </summary>
        [JsonPropertyName("sellorders")]
        public IEnumerable<SellOrder> SellOrders { get; set; }
    }
}

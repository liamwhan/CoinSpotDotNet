using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Sell Transaction
    /// </summary>
    public class SellOrder : BuyOrder
    {
        /// <summary>
        /// Transaction total
        /// </summary>
        [JsonPropertyName("total")]
        public double Total { get; set; }
    }
}

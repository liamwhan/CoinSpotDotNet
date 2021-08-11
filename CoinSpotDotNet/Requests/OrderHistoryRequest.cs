using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Requests
{
    /// <summary>
    /// Request body for CoinSpot read-only API endpoint <c>/api/v2/ro/my/orders/completed</c>
    /// </summary>
    public class OrderHistoryRequest : DateRangeRequest
    {
        /// <summary>
        /// (Optional) coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("cointype")]
        public string CoinType { get; set; }


        /// <summary>
        /// (Optional, available markets only) market coin short name, example values 'AUD', 'USDT'
        /// </summary>
        [JsonPropertyName("markettype")]
        public string MarketType { get; set; }

    }
}

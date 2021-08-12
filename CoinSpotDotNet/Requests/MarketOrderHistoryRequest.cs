using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Requests
{
    /// <summary>
    /// Request body for v2 endpoint <c>/api/v2/ro/my/orders/market/completed</c>
    /// </summary>
    public class MarketOrderHistoryRequest : CoinSpotRequest
    {
        /// <summary>
        /// (optional) coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("cointype")]
        public string CoinType { get; set; }

        /// <summary>
        /// (optional, available markets only) market coin short name, example values 'AUD', 'USDT'
        /// </summary>
        [JsonPropertyName("markettype")]
        public string MarketType { get; set; }

        /// <summary>
        /// (optional, note: date is UTC date or UNIX EPOCH time) format 'YYYY-MM-DD' or e.g. 1614824116
        /// </summary>
        [JsonPropertyName("startdate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// (optional, note: date is UTC date or UNIX EPOCH time) format 'YYYY-MM-DD' or e.g. 1614824116
        /// </summary>
        [JsonPropertyName("enddate")]
        public DateTime? EndDate { get; set; }


    }
}

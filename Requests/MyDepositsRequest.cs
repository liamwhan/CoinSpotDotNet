using CoinSpotDotNet.Common;
using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Requests
{
    /// <summary>
    /// Request body for CoinSpot read-only API endpoint:
    /// <code>
    /// /api/ro/my/deposits
    /// </code>
    /// See <see href="https://www.coinspot.com.au/api#rodeposit"/>
    /// </summary>
    public class MyDepositsRequest : CoinSpotRequest
    {
        /// <summary>
        /// Optional. Start of date range for results
        /// </summary>
        [JsonConverter(typeof(CoinSpotDateTimeJsonConverter))]
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Optional. End of date range for results
        /// </summary>
        [JsonConverter(typeof(CoinSpotDateTimeJsonConverter))]
        public DateTime? EndDate { get; set; }
    }
}

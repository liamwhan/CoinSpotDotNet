using CoinSpotDotNet.Common;
using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Completed order details
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#historders"/>
    /// </para>
    /// </summary>
    public class CompletedOrder : Order
    {
        /// <summary>
        /// Order creation date
        /// </summary>
        [JsonPropertyName("solddate")]
        [JsonConverter(typeof(CoinSpotDateTimeResponseJsonConverter))]
        public DateTime? SoldDate { get; set; }
    }
}

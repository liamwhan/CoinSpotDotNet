using CoinSpotDotNet.Common;
using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Open Buy or Sell Order Details
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#openorders"/>
    /// </para>
    /// </summary>
    public class OpenOrder : Order
    {
        /// <summary>
        /// Order creation date
        /// <para>
        /// NOTE: Despite being in the CoinSpot API documentation, the beta v2 API currently does not return this property.
        /// </para>
        /// </summary>
        [JsonPropertyName("created")]
        [JsonConverter(typeof(CoinSpotDateTimeResponseJsonConverter))]
        public DateTime? Created { get; set; }

    }
}

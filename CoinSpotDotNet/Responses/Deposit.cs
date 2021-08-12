using CoinSpotDotNet.Responses.V2;
using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{

    /// <summary>
    /// Record of a deposit to CoinSpot
    /// </summary>
    public class Deposit : TransactionV2
    {

        /// <summary>
        /// Deposit type. e.g. "PayID", "POLi"
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Deposit reference id.
        /// </summary>
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

    }
}

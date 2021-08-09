using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Record of a deposit to CoinSpot
    /// </summary>
    public class Deposit
    {
        /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Deposit date
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Deposit status. e.g. "completed"
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Deposit type. e.g. "PayID"
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

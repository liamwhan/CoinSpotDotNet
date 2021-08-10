using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Base model for Deposits and Withdrawal API response types
    /// </summary>
    public abstract class Transaction
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
    }
}

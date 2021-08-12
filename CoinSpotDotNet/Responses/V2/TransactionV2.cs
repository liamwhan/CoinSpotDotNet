using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Base model for Deposits and Withdrawal API response types from the V2 API
    /// </summary>
    public abstract class TransactionV2 : Transaction
    {
        

        /// <summary>
        /// Deposit status. e.g. "completed"
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

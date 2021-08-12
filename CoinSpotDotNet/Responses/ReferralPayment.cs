using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Referral payment record
    /// </summary>
    public class ReferralPayment
    {
        /// <summary>
        /// Payment amount in coins
        /// </summary>
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; }

        /// <summary>
        /// Payment amount in AUD
        /// </summary>
        [JsonPropertyName("audamount")]
        public double AudAmount { get; set; }
        
        /// <summary>
        /// Payment timestamp 
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

    }
}

using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Affliate payment record
    /// </summary>
    public class AffiliatePayment
    {
        /// <summary>
        /// Amount paid
        /// </summary>
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Payment month
        /// </summary>
        [JsonPropertyName("month")]
        public DateTime Month { get; set; }
    }
}

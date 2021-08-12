using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Buy Transaction
    /// </summary>
    public class BuyOrder : Transaction
    {
        /// <summary>
        /// OTC transaction
        /// </summary>
        [JsonPropertyName("otc")]
        public bool Otc { get; set; }
        
        /// <summary>
        /// Market in which the transaction was placed 
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; }
        
        /// <summary>
        /// CoinSpot fee ex. GST
        /// </summary>
        [JsonPropertyName("audfeeExGst")]
        public double AudFeeExGst { get; set; }

        /// <summary>
        /// Fee GST
        /// </summary>
        [JsonPropertyName("audGst")]
        public double AudGst { get; set; }

        /// <summary>
        /// AUD total
        /// </summary>
        [JsonPropertyName("audtotal")]
        public double AudTotal { get; set; }

    }
}

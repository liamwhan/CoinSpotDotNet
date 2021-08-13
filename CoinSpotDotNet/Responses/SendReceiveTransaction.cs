using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Send or receive transaction record
    /// </summary>
    public class SendReceiveTransaction
    {
        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Amount of coin
        /// </summary>
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Coin short name, example value 'BTC', 'LTC', 'DOGE'
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; }

        /// <summary>
        /// Destination address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Value in AUD
        /// </summary>
        [JsonPropertyName("aud")]
        public double Aud { get; set; }

    }
}

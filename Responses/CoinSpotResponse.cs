using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Base class for all CoinSpot API responses
    /// </summary>
    public abstract class CoinSpotResponse
    {
        /// <summary>
        /// Response status e.g. "ok", "error"
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

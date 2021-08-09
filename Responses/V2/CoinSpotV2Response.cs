using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Base class for CoinSpot API v2 Responses
    /// </summary>
    public abstract class CoinSpotV2Response : CoinSpotResponse
    {
        /// <summary>
        /// Status message e.g. "ok" / description of error if error occurred
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}

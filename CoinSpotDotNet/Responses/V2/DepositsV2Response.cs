using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// CoinSpot read-only API v2 List Deposit History response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rodeposit"/>
    /// </para>
    /// </summary>
    public class DepositsV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Array containing your AUD <see cref="Deposit"/> history
        /// </summary>
        [JsonPropertyName("deposits")]
        public IEnumerable<Deposit> Deposits { get; set; }
    }
}

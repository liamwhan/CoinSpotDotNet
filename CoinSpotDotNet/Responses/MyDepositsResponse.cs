using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// CoinSpot read-only API List Deposit History response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rodeposit"/>
    /// </para>
    /// </summary>
    public class MyDepositsResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array containing your AUD <see cref="Deposit"/> history
        /// </summary>
        [JsonPropertyName("deposits")]
        public IEnumerable<Deposit> Deposits { get; set; }
    }
}

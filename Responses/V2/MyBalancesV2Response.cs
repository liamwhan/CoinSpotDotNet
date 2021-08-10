using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Response model for <c>v2/ro/my/balances</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#romybalances"/>
    /// </para>
    /// </summary>
    public class MyBalancesV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Array of currency balances identified with currency identifier
        /// </summary>
        [JsonPropertyName("balances")]
        public Dictionary<string, CoinBalance>[] Balances { get; set; }

        /// <summary>
        /// Helper getter to flatten the Balances that CoinSpot returns into a single dictionary
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, CoinBalance> BalancesFlattened => Balances.SelectMany(d => d).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

}

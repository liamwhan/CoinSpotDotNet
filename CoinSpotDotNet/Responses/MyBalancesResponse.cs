using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Response model for /ro/my/balances
    /// </summary>
    public class MyBalancesResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array of currency balances identified with currency identifier
        /// </summary>
        [JsonPropertyName("balances")]
        public Dictionary<string, CoinBalance>[] Balances { get; set; }

        /// <summary>
        /// Helper getter to flatten the Balances that CoinSpot returns
        /// </summary>
        public Dictionary<string, CoinBalance> BalancesFlattened => Balances.SelectMany(d => d).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

}

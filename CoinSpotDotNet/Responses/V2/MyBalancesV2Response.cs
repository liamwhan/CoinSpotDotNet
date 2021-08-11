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

    /// <summary>
    /// Response model for <c>/api/v2/ro/my/balance/{cointype}</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rocoinsbalance"/>
    /// </para>
    /// </summary>
    public class MyCoinBalanceV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// The coin balance
        /// </summary>
        [JsonPropertyName("balance")]
        public Dictionary<string, CoinBalance> Balance { get; set; }

        /// <summary>
        /// Gets the first value in the dictionary (for this endpoint there will only be one)
        /// </summary>
        [JsonIgnore]
        public CoinBalanceId CoinBalance => Balance.Any() ? new CoinBalanceId(Balance.FirstOrDefault().Key, Balance.FirstOrDefault().Value) : null;
    }

}

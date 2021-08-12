using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Response model for <c>/api/v2/ro/my/balance/{cointype}</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rocoinsbalance"/>
    /// </para>
    /// </summary>
    public class CoinBalanceV2Response : CoinSpotV2Response
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

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Response model for <c>/api/ro/my/balance/:cointype</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rocoinsbalance"/>
    /// </para>
    /// </summary>
    public class CoinBalanceResponse
    {
        /// <summary>
        /// The coin balance
        /// </summary>
        [JsonPropertyName("balance")]
        public KeyValuePair<string, CoinBalance> Balance { get; set; }

        /// <summary>
        /// Gets the first value in the dictionary (for this endpoint there will only be one)
        /// </summary>
        [JsonIgnore]
        public CoinBalanceId CoinBalance => !Balance.Equals(default(KeyValuePair<string, CoinBalance>)) ? new CoinBalanceId(Balance.Key, Balance.Value) : null;
    }
}

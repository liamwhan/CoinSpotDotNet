using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Represents a balance for a CoinSpot currency
    /// </summary>
    public class CoinBalance
    {
        /// <summary>
        /// Balance in currency units
        /// </summary>
        [JsonPropertyName("balance")]
        public double Balance { get; set; }

        /// <summary>
        /// Balance in AUD approx.
        /// </summary>
        [JsonPropertyName("audbalance")]
        public double AudBalance { get; set; }

        /// <summary>
        /// Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public double Rate { get; set; }
    }


    /// <summary>
    /// Convenience class that flattens a dictionary object returned from the API and returns the coin type in the <see cref="CoinType"/>
    /// </summary>
    public class CoinBalanceId : CoinBalance
    {
        /// <summary>
        /// The coin type identifier e.g. "BTC", "ETH" etc.
        /// </summary>
        [JsonIgnore]
        public string CoinType { get; set; }


        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public CoinBalanceId() { }

        /// <summary>
        /// Convenience constructor when flattening from dictionary
        /// </summary>
        /// <param name="coinType">The coin type identifier e.g. "BTC", "ETH" etc.</param>
        /// <param name="balance">The coin balance</param>
        public CoinBalanceId(string coinType, CoinBalance balance)
        {
            CoinType = coinType;
            Balance = balance.Balance;
            AudBalance = balance.AudBalance;
            Rate = balance.Rate;
        }

        /// <summary>
        /// Allows Deconstruction of this class
        /// </summary>
        public void Deconstruct(out string coinType, out double balance, out double audBalance, out double rate)
        {
            coinType = CoinType;
            balance = Balance;
            audBalance = AudBalance;
            rate = Rate;

        }
    }

}

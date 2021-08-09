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
        public double Balance { get; set; }

        /// <summary>
        /// Balance in AUD approx.
        /// </summary>
        public double AudBalance { get; set; }

        /// <summary>
        /// Rate
        /// </summary>
        public double Rate { get; set; }
    }

}

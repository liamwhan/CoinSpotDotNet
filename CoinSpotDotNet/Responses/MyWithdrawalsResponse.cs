using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// CoinSpot read-only API v1 My Withdrawl History response <c>/api/ro/my/withdrawals</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rowithdrawal"/>
    /// </para>
    /// </summary>
    public class MyWithdrawalsResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array containing your AUD <see cref="Withdrawal"/> history
        /// </summary>
        [JsonPropertyName("withdrawals")]
        public IEnumerable<Withdrawal> Withdrawals { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// Send and Receive Transaction History
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rosendreceive"/>
    /// </para>
    /// </summary>
    public class SendReceiveResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array containing your coin send transaction history
        /// </summary>
        [JsonPropertyName("sendtransactions")]
        public IEnumerable<SendReceiveTransaction> SendTransactions { get; set; }

        /// <summary>
        /// Array containing your coin receive transaction history
        /// </summary>
        [JsonPropertyName("receivetransactions")]
        public IEnumerable<SendReceiveTransaction> ReceiveTransactions { get; set; }
    }
}

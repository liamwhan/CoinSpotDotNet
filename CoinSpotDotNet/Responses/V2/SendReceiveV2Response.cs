using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// Send receive history response
    /// <para>
    /// See <see href="https://www.coinspot.com.au/v2/api#rosendreceive"/>
    /// </para>
    /// </summary>
    public class SendReceiveV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// array containing your coin send transaction history
        /// </summary>
        [JsonPropertyName("sendtransactions")]
        public IEnumerable<SendReceiveTransaction> SendTransactions { get; set; }

        /// <summary>
        /// array containing your coin receive transaction history
        /// </summary>
        [JsonPropertyName("receivetransactions")]
        public IEnumerable<SendReceiveTransaction> ReceiveTransactions { get; set; }

    }
}

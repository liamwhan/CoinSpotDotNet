using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Responses.V2
{
    /// <summary>
    /// CoinSpot read-only API response for endpoint <c>/api/ro/my/referralpayments</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#rorefpay"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: I have no referral payments on my coinspot account and there are no example responses in the 
    /// v1 CoinSpot API documentation, so this response shape is based on the Example Response for the v2 api
    /// which you can see here: <see href="https://www.coinspot.com.au/v2/api#rorefpay"/> but I have no way of
    /// testing that this is correct.
    /// </para>
    /// </remarks>
    public class ReferralPaymentV2Response : CoinSpotV2Response
    {
        /// <summary>
        /// Array containing one object for each completed referral payment
        /// </summary>
        [JsonPropertyName("payments")]
        public IEnumerable<ReferralPayment> Payments { get; set; }
    }
}

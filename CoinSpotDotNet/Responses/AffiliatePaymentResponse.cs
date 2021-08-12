using System.Collections.Generic;

namespace CoinSpotDotNet.Responses
{
    /// <summary>
    /// CoinSpot read-only API response for endpoint <c>/api/ro/my/affiliatepayments</c>
    /// <para>
    /// See <see href="https://www.coinspot.com.au/api#roaffpay"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: I have no affliate payments on my coinspot account and there are no example responses in the 
    /// v1 CoinSpot API documentation, so this response shape is based on the Example Response for the v2 api
    /// which you can see here: <see href="https://www.coinspot.com.au/v2/api#roaffpay"/> but I have no way of
    /// testing that this is correct.
    /// </para>
    /// </remarks>
    public class AffiliatePaymentResponse : CoinSpotResponse
    {
        /// <summary>
        /// Array containing one object for each completed affiliate payment
        /// </summary>
        public IEnumerable<AffiliatePayment> Payments { get; set; }

    }
}

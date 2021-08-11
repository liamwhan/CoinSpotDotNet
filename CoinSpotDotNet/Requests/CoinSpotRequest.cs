using CoinSpotDotNet.Common;
using System;

namespace CoinSpotDotNet.Requests
{
    /// <summary>
    /// Base class for the body content of all CoinSpot API requests
    /// </summary>
    public class CoinSpotRequest
    {
        /// <summary>
        /// The nonce to include. Defaults to Unix timestamp (seconds) of UTC now.
        /// <para>
        /// <see href="https://www.coinspot.com.au/api#security"/> for more information
        /// </para>
        /// </summary>
        public long Nonce { get; set; } = DateTime.UtcNow.ToUnixTimestamp();
    }
}

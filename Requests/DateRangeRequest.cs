using CoinSpotDotNet.Common;
using System;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Requests
{
    /// <summary>
    /// Request body for CoinSpot read-only API endpoint:
    /// See: 
    /// <list type="bullet">
    /// <item>
    ///     <description>
    ///         <see href="https://www.coinspot.com.au/api#rodeposit"/>
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         <see href="https://www.coinspot.com.au/api#rowithdrawal"/>
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         <see href="https://www.coinspot.com.au/v2/api#rodeposit"/>
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         <see href="https://www.coinspot.com.au/v2/api#rowithdrawal"/>
    ///     </description>
    /// </item>
    /// </list>
    /// </summary>
    public class DateRangeRequest : CoinSpotRequest
    {
        /// <summary>
        /// Optional. Start of date range for results. 
        /// <para>
        /// <see cref="CoinSpotDateTimeJsonConverter"/> converts value into CoinSpot supported date string.
        /// </para>
        /// </summary>
        [JsonConverter(typeof(CoinSpotDateTimeJsonConverter))]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional. End of date range for results
        /// <para>
        /// <see cref="CoinSpotDateTimeJsonConverter"/> converts value into CoinSpot supported date string.
        /// </para>
        /// </summary>
        [JsonConverter(typeof(CoinSpotDateTimeJsonConverter))]
        public DateTime? EndDate { get; set; }
    }
}

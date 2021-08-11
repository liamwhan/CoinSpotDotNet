using System;
using System.Collections.Generic;
using System.Text;

namespace CoinSpotDotNet.Common
{
    internal static class DateTimeExtensions
    {
        internal static long ToUnixTimestamp(this DateTime dt)
        {
            return (long)dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds; 
        }
    }
}

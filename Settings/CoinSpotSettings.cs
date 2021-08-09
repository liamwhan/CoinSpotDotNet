using Microsoft.Extensions.Options;

namespace CoinSpotDotNet.Settings
{
    /// <summary>
    /// CoinSpot Credentials. Common usage would be to configure this type as an <see cref="IOptionsMonitor{T}"/> 
    /// </summary>
    /// <example>
    /// In Startup.cs
    /// <code>
    ///     services.Configure&lt;CoinSpotSettings&gt;(Configuration.GetSection("CoinSpotSettings"));
    /// </code>
    /// </example>
    public class CoinSpotSettings
    {
        /// <summary>
        /// Your CoinSpot read-only API Key
        /// <para>
        /// See <see href="https://www.coinspot.com.au/my/api"/>
        /// </para>
        /// </summary> 
        public string ReadOnlyKey { get; set; }
        
        /// <summary>
        /// Your CoinSpot read-only API Secret
        /// <para>
        /// See <see href="https://www.coinspot.com.au/my/api"/>
        /// </para>
        /// </summary>
        public string ReadOnlySecret { get; set; }


    }
}

using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinSpotDotNet
{

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v1 calls and handles request signing.
    /// </summary>
    public interface ICoinSpotClient
    {
        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/balances</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#romybalances"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="MyBalancesResponse"/></returns>
        Task<MyBalancesResponse> ListMyBalances();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <code>/api/ro/my/deposits</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rodeposit"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="MyDepositsResponse"/></returns>
        Task<MyDepositsResponse> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null);


        /// <summary>
        /// Get Latest Prices from the CoinSpot public API v1
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#latestprices"/>
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task<LatestPricesResponse> LatestPrices();
    }

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v1 calls and handles request signing. 
    /// </summary>
    public class CoinSpotClient : BaseClient, ICoinSpotClient
    {
        private const string PathMyBalances = "/api/ro/my/balances";
        private const string PathMyDeposits = "/api/ro/my/deposits";

        private const string PublicPathLatestPrices = "/pubapi/latest";
        
        /// <summary>
        /// Constructor for use in non-ASP.NET scenarios where Dependency Injection is not available. Requires only a <see cref="CoinSpotSettings"/> object initialised with your API Credentials
        /// </summary>
        /// <param name="settings">An <see cref="CoinSpotSettings"/> containing your CoinSpot API credentials</param>
        public CoinSpotClient(CoinSpotSettings settings) : base(settings)
        { 
        }
        
        /// <summary>
        /// Constructor. For use in ASP.NET projects with Dependency Injection enabled. Requires a registered <see cref="IOptions{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/> containing your CoinSpot API credentials
        /// </summary>
        /// <param name="options">An <see cref="IOptionsMonitor{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/></param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> for error logging</param>
        /// <param name="client">The <see cref="HttpClient"/> injected by the ServiceProvider when registering this class with <see cref="IServiceCollection"/>.AddHttpClient&lt;TInterface, TImplementation&gt;() </param>
        public CoinSpotClient(IOptionsMonitor<CoinSpotSettings> options, ILogger<CoinSpotClient> logger, HttpClient client) : base(options, logger, client)
        { 
        }
        
        /// <inheritdoc />
        public async Task<LatestPricesResponse> LatestPrices()
        {

            using var response = await Get(new Uri(PublicPathLatestPrices, UriKind.Relative), null, null);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;

        }
        
        /// <inheritdoc/>
        public async Task<MyBalancesResponse> ListMyBalances()
        {
            var path = new Uri(PathMyBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest());
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var balance = await JsonSerializer.DeserializeAsync<MyBalancesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<MyDepositsResponse> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyDeposits, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            });
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var deposits = await JsonSerializer.DeserializeAsync<MyDepositsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;

        }


    }
}

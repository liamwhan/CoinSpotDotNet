using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Responses.V2;
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
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v2 calls and handles request signing.
    /// </summary>
    public interface ICoinSpotClientV2
    {
        /// <summary>
        /// Calls CoinSpot read-only API v2 Endpoint: <c>/api/v2/ro/my/balances</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#romybalances"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="MyBalancesV2Response"/></returns>
        Task<MyBalancesV2Response> ListMyBalances();

        /// <summary>
        /// Calls CoinSpot read-only API v2 Endpoint: <c>/api/v2/ro/my/deposits</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#rodeposit"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="MyDepositsV2Response"/></returns>
        Task<MyDepositsV2Response> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null);


        /// <summary>
        /// Get Latest Prices from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestprices"/>
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task<LatestPricesV2Response> LatestPrices();
    }

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v2 calls and handles request signing. 
    /// </summary>
    public class CoinSpotClientV2 : BaseClient, ICoinSpotClientV2
    {
       
        private const string PathMyBalances = "/api/v2/ro/my/balances";
        private const string PathMyDeposits = "/api/v2/ro/my/deposits";

        private const string PublicPathLatestPrices = "/pubapi/v2/latest";

        /// <summary>
        /// Constructor. Requires a registered <see cref="IOptions{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/> containing your CoinSpot API key and secret
        /// </summary>
        /// <param name="options">An <see cref="IOptionsMonitor{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/></param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> for error logging</param>
        /// <param name="client">The HttpClient injected by the ServiceProvider when registering this class with <see cref="IServiceCollection"/>.AddHttpClient </param>
        public CoinSpotClientV2(IOptionsMonitor<CoinSpotSettings> options, ILogger<CoinSpotClientV2> logger, HttpClient client) : base(options, logger, client)
        {
        }
        
        /// <inheritdoc />
        public async Task<LatestPricesV2Response> LatestPrices()
        {
            using var response = await Get(new Uri(PublicPathLatestPrices, UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }
       
        /// <inheritdoc/>
        public async Task<MyBalancesV2Response> ListMyBalances()
        {
            var path = new Uri(PathMyBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, options.CurrentValue.ReadOnlySecret);

            using var response = await Post(path, postData, sign);

            var balance = await JsonSerializer.DeserializeAsync<MyBalancesV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<MyDepositsV2Response> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyDeposits, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new MyDepositsRequest
            {
                StartDate = startDate,
                EndDate = endDate
            }, jsonOptions);
            var sign = SignUtility.Sign(postData, options.CurrentValue.ReadOnlySecret);

            using var response = await Post(path, postData, sign);

            var deposits = await JsonSerializer.DeserializeAsync<MyDepositsV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;

        }

        


    }
}

using CoinSpotDotNet.Common;
using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Responses.V2;
using CoinSpotDotNet.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinSpotDotNet
{
    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API calls and handles request signing. Requires a registered <see cref="IOptions{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/> containing your CoinSpot API key and secret
    /// </summary>
    public interface ICoinSpotClient
    {
        /// <summary>
        /// Calls CoinSpot read-only API Endpoint: <code>/api/ro/my/balances</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#romybalances"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="MyBalancesResponse"/></returns>
        Task<MyBalancesResponse> ListMyBalances();
        
        /// <summary>
        /// Calls CoinSpot read-only API Endpoint: <code>/api/ro/my/deposits</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rodeposit"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="MyDepositsResponse"/></returns>
        Task<MyDepositsResponse> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null);


        /// <summary>
        /// Get Latest Prices from the CoinSpot public API
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#latestprices"/>
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task<LatestPricesResponse> LatestPrices();
        
        /// <summary>
        /// Get Latest Prices from the CoinSpot v2 Public API
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestprices"/>
        /// </para>
        /// </summary>
        /// <returns></returns>
        Task<LatestPricesV2Response> LatestPricesV2();
    }

    /// <inheritdoc cref="ICoinSpotClient"/>
    public class CoinSpotClient : ICoinSpotClient
    {
        private readonly IOptionsMonitor<CoinSpotSettings> options;
        private readonly ILogger<CoinSpotClient> logger;
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonOptions;
        private const string PathMyBalances = "/api/ro/my/balances";
        private const string PathMyDeposits = "/api/ro/my/deposits";

        private const string PublicPathLatestPrices = "/pubapi/latest";
        private const string PublicPathLatestPricesV2 = "/pubapi/v2/latest";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">An <see cref="IOptionsMonitor{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/></param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> for error logging</param>
        /// <param name="client">The HttpClient injected by the ServiceProvider when registering this class with <see cref="IServiceCollection"/>.AddHttpClient </param>
        /// <param name="jsonOptions">Optional. Inject custom <see cref="JsonSerializerOptions"/></param>
        public CoinSpotClient(IOptionsMonitor<CoinSpotSettings> options, ILogger<CoinSpotClient> logger, HttpClient client, JsonSerializerOptions jsonOptions = null)
        {
            client.BaseAddress = new Uri("https://www.coinspot.com.au", UriKind.Absolute);
            this.options = options;
            this.logger = logger;
            this.client = client;
            this.jsonOptions = jsonOptions ?? new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = false,
                Converters =
                {
                    new CoinSpotDateTimeJsonConverter(),
                    new CoinSpotStringToDoubleJsonConverter()
                }
            };
        }
        /// <inheritdoc />
        public async Task<LatestPricesResponse> LatestPrices()
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = new Uri(PublicPathLatestPrices, UriKind.Relative),
                Method = HttpMethod.Get
            };

            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                logger.LogError("CoinSpot error response {statusCode}, message: {message}", response.StatusCode, responseBody);
                return null;
            }

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;

        }
        
        /// <inheritdoc />
        public async Task<LatestPricesV2Response> LatestPricesV2()
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = new Uri(PublicPathLatestPricesV2, UriKind.Relative),
                Method = HttpMethod.Get
            };

            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                logger.LogError("CoinSpot error response {statusCode}, message: {message}", response.StatusCode, responseBody);
                return null;
            }

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }

       
        /// <inheritdoc/>
        public async Task<MyBalancesResponse> ListMyBalances()
        {
            var path = new Uri(PathMyBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest());
            var sign = SignUtility.Sign(postData, options.CurrentValue.ReadOnlySecret);

            using var response = await Send(path, postData, sign);

            var balance = await JsonSerializer.DeserializeAsync<MyBalancesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<MyDepositsResponse> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyDeposits, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new MyDepositsRequest
            {
                StartDate = startDate,
                EndDate = endDate
            });
            var sign = SignUtility.Sign(postData, options.CurrentValue.ReadOnlySecret);

            using var response = await Send(path, postData, sign);

            var deposits = await JsonSerializer.DeserializeAsync<MyDepositsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;

        }

        private async Task<HttpResponseMessage> Send(Uri requestUri, string postData, string sign)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Post,
                Content = new StringContent(postData, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("key", options.CurrentValue.ReadOnlyKey);
            request.Headers.Add("sign", sign);

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                logger.LogError("CoinSpot error response on path '{path}'. Status: {statusCode}, message: {message}", requestUri.OriginalString, response.StatusCode, responseBody);
            }

            return response;
        }


    }
}

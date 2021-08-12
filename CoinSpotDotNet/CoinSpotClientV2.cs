using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Responses.V2;
using CoinSpotDotNet.Settings;
using CoinSpotDotNet.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CoinSpotDotNet
{

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v2 calls and handles request signing.
    /// </summary>
    public interface ICoinSpotClientV2
    {
        #region Read only API
        /// <summary>
        /// Calls the status check endpoint for the read-only API. Useful to validate your API Key / Secret values are correct
        /// </summary>
        /// <returns><see cref="CoinSpotResponse"/></returns>
        Task<CoinSpotResponse> ReadOnlyStatusCheck();

        /// <summary>
        /// Calls CoinSpot read-only API v2 Endpoint: <c>/api/v2/ro/my/balance/{cointype}</c> 
        /// </summary>
        /// <param name="coinType">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="MyCoinBalanceV2Response"/></returns>
        Task<MyCoinBalanceV2Response> MyCoinBalance(string coinType);

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
        /// Calls CoinSpot read-only API v2 Endpoint: <c>/api/v2/ro/my/deposits</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#rowithdrawal"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="MyWithdrawalsV2Response"/></returns>
        Task<MyWithdrawalsV2Response> ListMyWithdrawals(DateTime? startDate = null, DateTime? endDate = null);
        #endregion


        #region Public API
        /// <summary>
        /// Get Latest Prices from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestprices"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="LatestPricesV2Response"/></returns>
        Task<LatestPricesV2Response> LatestPrices();

        /// <summary>
        /// Get Latest Coin Price from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestpricescoin"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <returns><see cref="LatestCoinPriceV2Response"/></returns>
        Task<LatestCoinPriceV2Response> LatestCoinPrices(string coinType);

        /// <summary>
        /// Get Latest Coin/Market Price from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestpricescoinmarket"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="marketType">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <returns><see cref="LatestCoinPriceV2Response"/></returns>
        Task<LatestCoinPriceV2Response> LatestCoinMarketPrices(string coinType, string marketType);

        /// <summary>
        /// Get Latest Buy Price for <paramref name="coinType"/> from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestbuyprice"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <returns><see cref="RateMarketPriceV2Response"/></returns>
        Task<RateMarketPriceV2Response> LatestBuyPrice(string coinType);

        /// <summary>
        /// Get Latest Buy Price for <paramref name="coinType"/> in <paramref name="marketType"/> from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestbuypricenonaud"/>
        /// </para>
        /// </summary>
        /// <remarks>
        /// NOTE: The CoinSpot v2 API is in beta and, at time of writing, passing "AUD" as <paramref name="marketType"/> results in a 400 response from CoinSpot with message "Bad Market"
        /// </remarks>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="marketType">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <returns><see cref="RateMarketPriceV2Response"/></returns>
        Task<RateMarketPriceV2Response> LatestBuyPriceMarket(string coinType, string marketType);


        /// <summary>
        /// Get Latest Sell Price for <paramref name="coinType"/> from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestsellprice"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <returns><see cref="RateMarketPriceV2Response"/></returns>
        Task<RateMarketPriceV2Response> LatestSellPrice(string coinType);

        /// <summary>
        /// Get Latest Sell Price for <paramref name="coinType"/> in <paramref name="marketType"/> from the CoinSpot public API v2
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#latestsellpricenonaud"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="marketType">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <returns><see cref="RateMarketPriceV2Response"/></returns>
        Task<RateMarketPriceV2Response> LatestSellPriceMarket(string coinType, string marketType);

        /// <summary>
        /// Get open orders by <paramref name="coinType"/>
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#openorders"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <returns><see cref="OpenOrdersV2Response"/></returns>
        Task<OpenOrdersV2Response> OpenOrdersByCoin(string coinType);

        /// <summary>
        /// Get open orders by <paramref name="coinType"/> in <paramref name="marketType"/>
        /// <para>
        /// See <see href="https://www.coinspot.com.au/v2/api#openordersmarket"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="marketType">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <returns><see cref="OpenOrdersV2Response"/></returns>
        Task<OpenOrdersV2Response> OpenOrdersByCoinMarket(string coinType, string marketType);

        /// <summary>
        /// Get completed orders by <paramref name="coinType"/>
        /// <para>See <see href="https://www.coinspot.com.au/v2/api#historders"/></para>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <returns><see cref="CompletedOrdersV2Response"/></returns>
        Task<CompletedOrdersV2Response> CompletedOrdersByCoin(string coinType);

        /// <summary>
        /// Get completed orders by <paramref name="coinType"/> in <paramref name="marketType"/>
        /// </summary>
        /// <param name="coinType">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="marketType">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <returns><see cref="CompletedOrdersV2Response"/></returns>
        Task<CompletedOrdersV2Response> CompletedOrdersByCoinMarket(string coinType, string marketType);

        #endregion
    }

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v2 calls and handles request signing. 
    /// </summary>
    public class CoinSpotClientV2 : BaseClient, ICoinSpotClientV2
    {
       
        private const string PathMyBalances = "/api/v2/ro/my/balances";
        private const string PathMyDeposits = "/api/v2/ro/my/deposits";
        private const string PathMyWithdrawals = "/api/v2/ro/my/withdrawals";
        private const string PathReadOnlyStatusCheck = "/api/v2/ro/status";
        private const string PathMyCoinBalance = "/api/v2/ro/my/balance/{0}";

        private const string PublicPathLatestPrices = "/pubapi/v2/latest";
        private const string PublicPathLatestCoinPrices = "/pubapi/v2/latest/{0}";
        private const string PublicPathLatestCoinMarketPrices = "/pubapi/v2/latest/{0}/{1}";
        private const string PublicPathLatestBuyPrice = "/pubapi/v2/buyprice/{0}";
        private const string PublicPathLatestMarketBuyPrice = "/pubapi/v2/buyprice/{0}/{1}";
        private const string PublicPathLatestSellPrice = "/pubapi/v2/sellprice/{0}";
        private const string PublicPathLatestMarketSellPrice = "/pubapi/v2/sellprice/{0}/{1}";
        private const string PublicPathOpenOrdersByCoin = "/pubapi/v2/orders/open/{0}";
        private const string PublicPathOpenOrdersByCoinMarket = "/pubapi/v2/orders/open/{0}/{1}";
        private const string PublicPathCompletedOrdersByCoin = "/pubapi/v2/orders/completed/{0}";
        private const string PublicPathCompletedOrdersByCoinMarket = "/pubapi/v2/orders/completed/{0}/{1}";

        /// <summary>
        /// Constructor for use in non-ASP.NET and/or integration and unit testing scenarios where Dependency Injection is not available and this class must be manually constructed. Requires only a <see cref="CoinSpotSettings"/> object initialised with your API Credentials
        /// </summary>
        /// <param name="settings">An <see cref="CoinSpotSettings"/> containing your CoinSpot API credentials</param>
        public CoinSpotClientV2(CoinSpotSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// Constructor. Requires a registered <see cref="IOptions{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/> containing your CoinSpot API key and secret
        /// <para>If you use the <see cref="IServiceCollection"/> extension <see cref="ServiceCollectionExtensions.AddCoinSpotV1(IServiceCollection, Microsoft.Extensions.Configuration.IConfiguration)"/>
        /// or <see cref="ServiceCollectionExtensions.AddCoinSpotV2(IServiceCollection, Microsoft.Extensions.Configuration.IConfiguration)"/> this constructor will automatically be selected by the <see cref="IServiceProvider"/>
        /// </para>
        /// </summary>
        /// <param name="options">An <see cref="IOptionsMonitor{TOptions}"/> where TOptions == <see cref="CoinSpotSettings"/></param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> for error logging</param>
        /// <param name="client">The HttpClient injected by the ServiceProvider when registering this class with <see cref="IServiceCollection"/>.AddHttpClient </param>
        public CoinSpotClientV2(IOptionsMonitor<CoinSpotSettings> options, ILogger<CoinSpotClientV2> logger, HttpClient client) : base(options, logger, client)
        {
        }
        
        #region Public API
        /// <inheritdoc />
        public async Task<CompletedOrdersV2Response> CompletedOrdersByCoinMarket(string coinType, string marketType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            marketType = invalidCharRegex.Replace(marketType, string.Empty);
            using var response = await Get(new Uri(PublicPathCompletedOrdersByCoinMarket.Format(coinType, marketType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var orders = await JsonSerializer.DeserializeAsync<CompletedOrdersV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return orders;
        }

        /// <inheritdoc />
        public async Task<CompletedOrdersV2Response> CompletedOrdersByCoin(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            using var response = await Get(new Uri(PublicPathCompletedOrdersByCoin.Format(coinType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var orders = await JsonSerializer.DeserializeAsync<CompletedOrdersV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return orders;
        }

        /// <inheritdoc />
        public async Task<OpenOrdersV2Response> OpenOrdersByCoinMarket(string coinType, string marketType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            marketType = invalidCharRegex.Replace(marketType, string.Empty);
            using var response = await Get(new Uri(PublicPathOpenOrdersByCoinMarket.Format(coinType, marketType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var orders = await JsonSerializer.DeserializeAsync<OpenOrdersV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return orders;
        }

        /// <inheritdoc />
        public async Task<OpenOrdersV2Response> OpenOrdersByCoin(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            using var response = await Get(new Uri(PublicPathOpenOrdersByCoin.Format(coinType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var orders = await JsonSerializer.DeserializeAsync<OpenOrdersV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return orders;
        }


        /// <inheritdoc />
        public async Task<RateMarketPriceV2Response> LatestSellPriceMarket(string coinType, string marketType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            marketType = invalidCharRegex.Replace(marketType, string.Empty);

            using var response = await Get(new Uri(PublicPathLatestMarketSellPrice.Format(coinType, marketType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            
            var prices = await JsonSerializer.DeserializeAsync<RateMarketPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }

        /// <inheritdoc />
        public async Task<RateMarketPriceV2Response> LatestSellPrice(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            using var response = await Get(new Uri(PublicPathLatestSellPrice.Format(coinType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<RateMarketPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }

        /// <inheritdoc />
        public async Task<RateMarketPriceV2Response> LatestBuyPriceMarket(string coinType, string marketType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            marketType = invalidCharRegex.Replace(marketType, string.Empty);

            using var response = await Get(new Uri(PublicPathLatestMarketBuyPrice.Format(coinType, marketType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<RateMarketPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }

        /// <inheritdoc />
        public async Task<RateMarketPriceV2Response> LatestBuyPrice(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            using var response = await Get(new Uri(PublicPathLatestBuyPrice.Format(coinType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<RateMarketPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }

        /// <inheritdoc />
        public async Task<LatestCoinPriceV2Response> LatestCoinMarketPrices(string coinType, string marketType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            marketType = invalidCharRegex.Replace(marketType, string.Empty);

            using var response = await Get(new Uri(PublicPathLatestCoinMarketPrices.Format(coinType, marketType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<LatestCoinPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }
        
        /// <inheritdoc />
        public async Task<LatestCoinPriceV2Response> LatestCoinPrices(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);

            using var response = await Get(new Uri(PublicPathLatestCoinPrices.Format(coinType), UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<LatestCoinPriceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }
        
        /// <inheritdoc />
        public async Task<LatestPricesV2Response> LatestPrices()
        {
            using var response = await Get(new Uri(PublicPathLatestPrices, UriKind.Relative), null, null);
            if (!response.IsSuccessStatusCode) return null;
            

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;
        }
        #endregion


        #region Read only API
        /// <inheritdoc />
        public async Task<CoinSpotResponse> ReadOnlyStatusCheck()
        {
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);
            using var response = await Post(new Uri(PathReadOnlyStatusCheck, UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var status = await JsonSerializer.DeserializeAsync<CoinSpotResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return status;


        }

        /// <inheritdoc/>
        public async Task<MyCoinBalanceV2Response> MyCoinBalance(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);

            var path = new Uri(PathMyCoinBalance.Format(coinType), UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode) return null;
            return await JsonSerializer.DeserializeAsync<MyCoinBalanceV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
        }

       
        /// <inheritdoc/>
        public async Task<MyBalancesV2Response> ListMyBalances()
        {
            var path = new Uri(PathMyBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode) return null;

            var balance = await JsonSerializer.DeserializeAsync<MyBalancesV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }



        /// <inheritdoc/>
        public async Task<MyDepositsV2Response> ListMyDeposits(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyDeposits, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            }, jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var deposits = await JsonSerializer.DeserializeAsync<MyDepositsV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;

        }
        
        /// <inheritdoc/>
        public async Task<MyWithdrawalsV2Response> ListMyWithdrawals(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyWithdrawals, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            }, jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode) return null;

            var withdrawals = await JsonSerializer.DeserializeAsync<MyWithdrawalsV2Response>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return withdrawals;

        }

        #endregion

    }
}

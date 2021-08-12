using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Settings;
using CoinSpotDotNet.Internal;
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
        #region Read only API
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
        /// Calls CoinSpot read-only API v1 Endpoint: <code>/api/ro/my/withdrawals</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rowithdrawal"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="MyWithdrawalsResponse"/></returns>
        Task<MyWithdrawalsResponse> ListMyWithdrawals(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/balance/:cointype</c> 
        /// </summary>
        /// <param name="coinType">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="MyCoinBalanceResponse"/></returns>
        Task<MyCoinBalanceResponse> MyCoinBalance(string coinType);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions</c> 
        /// </summary>
        /// <returns><see cref="MyTransactionsResponse"/></returns>
        Task<MyTransactionsResponse> ListMyTransactionHistory();
        
        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/:cointype</c> 
        /// </summary>
        /// <param name="cointype">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="MyTransactionsResponse"/></returns>
        Task<MyTransactionsResponse> ListMyCoinTransactionHistory(string cointype);
        
        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/open</c> 
        /// </summary>
        /// <returns><see cref="MyTransactionsResponse"/></returns>
        Task<MyTransactionsResponse> ListMyOpenTransactions();
        
        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/:cointype/open</c> 
        /// </summary>
        /// <param name="cointype">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="MyTransactionsResponse"/></returns>
        Task<MyTransactionsResponse> ListMyOpenCoinTransactions(string cointype);
        
        
        
        #endregion


    }

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v1 calls and handles request signing. 
    /// </summary>
    public class CoinSpotClient : BaseClient, ICoinSpotClient
    {
        private const string PathMyBalances = "/api/ro/my/balances";
        private const string PathMyDeposits = "/api/ro/my/deposits";
        private const string PathMyWithdrawals = "/api/ro/my/withdrawls";
        private const string PathMyCoinBalance = "/api/ro/my/balances/{0}";
        private const string PathMyTransactions = "/api/ro/my/transactions";
        private const string PathMyCoinTransactions = "/api/ro/my/transactions/{0}";
        private const string PathMyOpenTransactions = "/api/ro/my/transactions/open";
        private const string PathMyOpenCoinTransactions = "/api/ro/my/transactions/{0}/open";

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

        #region Public API
        /// <inheritdoc />
        public async Task<LatestPricesResponse> LatestPrices()
        {

            using var response = await Get(new Uri(PublicPathLatestPrices, UriKind.Relative), null, null);

            if (!response.IsSuccessStatusCode) return null;

            var prices = await JsonSerializer.DeserializeAsync<LatestPricesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return prices;

        }
        #endregion

        #region Read Only API
        /// <inheritdoc/>
        public async Task<MyBalancesResponse> ListMyBalances()
        {
            var path = new Uri(PathMyBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest());
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode) return null;

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
            
            if (!response.IsSuccessStatusCode) return null;

            var deposits = await JsonSerializer.DeserializeAsync<MyDepositsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;
        }
        
        /// <inheritdoc/>
        public async Task<MyWithdrawalsResponse> ListMyWithdrawals(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathMyWithdrawals, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            });
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            
            if (!response.IsSuccessStatusCode) return null;

            var withdrawals = await JsonSerializer.DeserializeAsync<MyWithdrawalsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return withdrawals;
        }

        /// <inheritdoc/>
        public async Task<MyCoinBalanceResponse> MyCoinBalance(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathMyCoinBalance.Format(coinType), UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var balance = await JsonSerializer.DeserializeAsync<MyCoinBalanceResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<MyTransactionsResponse> ListMyTransactionHistory()
        {
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathMyTransactions, UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<MyTransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<MyTransactionsResponse> ListMyCoinTransactionHistory(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathMyCoinTransactions.Format(coinType), UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<MyTransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<MyTransactionsResponse> ListMyOpenTransactions()
        {
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathMyOpenTransactions, UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<MyTransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<MyTransactionsResponse> ListMyOpenCoinTransactions(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathMyOpenCoinTransactions.Format(coinType), UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<MyTransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }


        #endregion


    }
}

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
        /// <returns><see cref="BalancesResponse"/></returns>
        Task<BalancesResponse> ListBalances();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <code>/api/ro/my/deposits</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rodeposit"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="DepositsResponse"/></returns>
        Task<DepositsResponse> ListDeposits(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <code>/api/ro/my/withdrawals</code> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rowithdrawal"/>
        /// </para>
        /// </summary>
        /// <param name="startDate">Optional. Start of date range</param>
        /// <param name="endDate">Optional. End of date range</param>
        /// <returns><see cref="WithdrawalsResponse"/></returns>
        Task<WithdrawalsResponse> ListWithdrawals(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/balance/:cointype</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rocoinsbalance"/>
        /// </para>
        /// </summary>
        /// <param name="coinType">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="CoinBalanceResponse"/></returns>
        Task<CoinBalanceResponse> CoinBalance(string coinType);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rotransaction"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="TransactionsResponse"/></returns>
        Task<TransactionsResponse> ListTransactionHistory();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/:cointype</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rocointransaction"/>
        /// </para>
        /// </summary>
        /// <param name="cointype">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="TransactionsResponse"/></returns>
        Task<TransactionsResponse> ListCoinTransactionHistory(string cointype);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/open</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#roopentransactions"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="TransactionsResponse"/></returns>
        Task<TransactionsResponse> ListOpenTransactions();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/transactions/:cointype/open</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rocoinopentransactions"/>
        /// </para>
        /// </summary>
        /// <param name="cointype">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <returns><see cref="TransactionsResponse"/></returns>
        Task<TransactionsResponse> ListOpenCoinTransactions(string cointype);

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/sendreceive</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rosendreceive"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="SendReceiveResponse"/></returns>
        Task<SendReceiveResponse> ListSendReceiveTransactions();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/affiliatepayments</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#roaffpay"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="AffiliatePaymentResponse"/></returns>
        Task<AffiliatePaymentResponse> ListAffiliatePayments();

        /// <summary>
        /// Calls CoinSpot read-only API v1 Endpoint: <c>/api/ro/my/referralpayments</c> 
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#rorefpay"/>
        /// </para>
        /// </summary>
        /// <returns><see cref="AffiliatePaymentResponse"/></returns>
        Task<ReferralPaymentResponse> ListReferralPayments();
        #endregion


    }

    /// <summary>
    /// A typed <see cref="HttpClient"/> that abstracts CoinSpot API v1 calls and handles request signing. 
    /// </summary>
    public class CoinSpotClient : BaseClient, ICoinSpotClient
    {
        private const string PathBalances = "/api/ro/my/balances";
        private const string PathDeposits = "/api/ro/my/deposits";
        private const string PathWithdrawals = "/api/ro/my/withdrawls";
        private const string PathCoinBalance = "/api/ro/my/balances/{0}";
        private const string PathTransactions = "/api/ro/my/transactions";
        private const string PathCoinTransactions = "/api/ro/my/transactions/{0}";
        private const string PathOpenTransactions = "/api/ro/my/transactions/open";
        private const string PathOpenCoinTransactions = "/api/ro/my/transactions/{0}/open";
        private const string PathSendReceiveHistory = "/api/ro/my/sendreceive";
        private const string PathAffiliatePayments = "/api/ro/my/affiliatepayments";
        private const string PathReferralPayments = "/api/ro/my/referralpayments";

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
        public async Task<BalancesResponse> ListBalances()
        {
            var path = new Uri(PathBalances, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest());
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            if (!response.IsSuccessStatusCode) return null;

            var balance = await JsonSerializer.DeserializeAsync<BalancesResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<DepositsResponse> ListDeposits(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathDeposits, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            });
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            
            if (!response.IsSuccessStatusCode) return null;

            var deposits = await JsonSerializer.DeserializeAsync<DepositsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return deposits;
        }
        
        /// <inheritdoc/>
        public async Task<WithdrawalsResponse> ListWithdrawals(DateTime? startDate = null, DateTime? endDate = null)
        {
            var path = new Uri(PathWithdrawals, UriKind.Relative);
            var postData = SignUtility.CreatePostData(new DateRangeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            });
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(path, postData, sign);
            
            if (!response.IsSuccessStatusCode) return null;

            var withdrawals = await JsonSerializer.DeserializeAsync<WithdrawalsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return withdrawals;
        }

        /// <inheritdoc/>
        public async Task<CoinBalanceResponse> CoinBalance(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathCoinBalance.Format(coinType), UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var balance = await JsonSerializer.DeserializeAsync<CoinBalanceResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return balance;
        }

        /// <inheritdoc/>
        public async Task<TransactionsResponse> ListTransactionHistory()
        {
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathTransactions, UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<TransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<TransactionsResponse> ListCoinTransactionHistory(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathCoinTransactions.Format(coinType), UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<TransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<TransactionsResponse> ListOpenTransactions()
        {
            var postData = SignUtility.CreatePostData(new CoinSpotRequest(), jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            using var response = await Post(new Uri(PathOpenTransactions, UriKind.Relative), postData, sign);

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<TransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<TransactionsResponse> ListOpenCoinTransactions(string coinType)
        {
            coinType = invalidCharRegex.Replace(coinType, string.Empty);

            using var response = await Post(new Uri(PathOpenCoinTransactions.Format(coinType), UriKind.Relative), new CoinSpotRequest());

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<TransactionsResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<SendReceiveResponse> ListSendReceiveTransactions()
        {
            using var response = await Post(new Uri(PathSendReceiveHistory, UriKind.Relative), new CoinSpotRequest());

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<SendReceiveResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<AffiliatePaymentResponse> ListAffiliatePayments()
        {
            using var response = await Post(new Uri(PathAffiliatePayments, UriKind.Relative), new CoinSpotRequest());

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<AffiliatePaymentResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }

         /// <inheritdoc/>
        public async Task<ReferralPaymentResponse> ListReferralPayments()
        {
            using var response = await Post(new Uri(PathReferralPayments, UriKind.Relative), new CoinSpotRequest());

            if (!response.IsSuccessStatusCode) return null;

            var transactions = await JsonSerializer.DeserializeAsync<ReferralPaymentResponse>(await response.Content.ReadAsStreamAsync(), jsonOptions);
            return transactions;
        }



        #endregion


    }
}

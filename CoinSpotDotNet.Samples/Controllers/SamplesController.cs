using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Responses.V2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoinSpotDotNet.Samples.Controllers
{
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly ICoinSpotClient clientV1;
        private readonly ICoinSpotClientV2 clientV2;

        public SamplesController(
            ICoinSpotClient clientV1,
            ICoinSpotClientV2 clientV2
            )
        {
            this.clientV1 = clientV1;
            this.clientV2 = clientV2;
        }

        #region Public API v1
        #endregion

        #region Public API v2
        /// <summary>
        /// Public API - Latest prices
        /// </summary>
        [HttpGet("pubapi/v2/latest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LatestPricesV2Response))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestPrices()
        {
            var prices = await clientV2.LatestPrices();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Latest coin prices
        /// </summary>
        [HttpGet("pubapi/v2/latest/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LatestPricesV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestCoinPrices(string cointype)
        {
            var prices = await clientV2.LatestCoinPrices(cointype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Latest coin/market prices
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="markettype">Market coin short name, example value 'USDT', 'AUD' (only for available markets)</param>
        [HttpGet("pubapi/v2/latest/{cointype}/{markettype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LatestPricesV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestCoinMarketPrices(string cointype, string markettype)
        {
            var prices = await clientV2.LatestCoinMarketPrices(cointype, markettype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }


        /// <summary>
        /// Public API - Latest buy price for coin
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        [HttpGet("pubapi/v2/buyprice/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateMarketPriceV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestBuyPrice(string cointype)
        {
            var prices = await clientV2.LatestBuyPrice(cointype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Latest buy price for coin in market
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="markettype">Market coin short name, example value 'USDT' (only for available markets)</param>
        [HttpGet("pubapi/v2/buyprice/{cointype}/{markettype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateMarketPriceV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestBuyPriceMarket(string cointype, string markettype)
        {
            var prices = await clientV2.LatestBuyPriceMarket(cointype, markettype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Latest sell price for coin
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        [HttpGet("pubapi/v2/sellprice/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateMarketPriceV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestSellPrice(string cointype)
        {
            var prices = await clientV2.LatestSellPrice(cointype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Latest sell price for coin in market
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="markettype">Market coin short name, example value 'USDT' (only for available markets)</param>
        [HttpGet("pubapi/v2/sellprice/{cointype}/{markettype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateMarketPriceV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> LatestSellPriceMarket(string cointype, string markettype)
        {
            var prices = await clientV2.LatestSellPriceMarket(cointype, markettype);
            if (prices == null) return NotFound();
            return new JsonResult(prices);
        }

        /// <summary>
        /// Public API - Open orders by coin
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        [HttpGet("pubapi/v2/orders/open/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OpenOrdersV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> OpenOrdersByCoin(string cointype)
        {
            var orders = await clientV2.OpenOrdersByCoin(cointype);
            if (orders == null) return NotFound();
            return new JsonResult(orders);
        }

         /// <summary>
        /// Public API - Open orders by coin / market
        /// </summary>
        /// <param name="cointype">Coin short name, example value 'BTC', 'LTC', 'DOGE'</param>
        /// <param name="markettype">Market coin short name, example value 'USDT' (only for available markets)</param>
        [HttpGet("pubapi/v2/orders/open/{cointype}/{markettype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OpenOrdersV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> OpenOrdersByCoin(string cointype, string markettype)
        {
            var orders = await clientV2.OpenOrdersByCoinMarket(cointype, markettype);
            if (orders == null) return NotFound();
            return new JsonResult(orders);
        }
        #endregion


        #region Read only API v1
        /// <summary>
        /// Read Only API v1 - List My Balances
        /// </summary>
        [HttpGet("ro/my/balances")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalancesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> Balances()
        {
            var balances = await clientV1.ListBalances();
            if (balances == null) return NotFound();
            return new JsonResult(balances);
        }


        /// <summary>
        /// Read Only API v1 - Balance by coin type
        /// </summary>
        /// <remarks>
        /// NOTE: The V1 API currently returns a 404 Not Found for this request, and I suppose, as v2 is currently in beta this may not ever be fixed
        /// </remarks>
        [HttpGet("ro/my/balances/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoinBalanceResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> CoinBalance(string cointype)
        {
            var balances = await clientV1.CoinBalance(cointype);
            if (balances == null) return NotFound();
            return new JsonResult(balances);
        }

        /// <summary>
        /// Read Only API v1 - List My Deposits
        /// </summary>
        /// <param name="startDate">Optional. Specify date range start</param>
        /// <param name="endDate">Optional. Specify date range end</param>
        [HttpGet("ro/my/deposits")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepositsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> Deposits([FromQuery(Name = "startdate")] DateTime? startDate = null, [FromQuery(Name = "enddate")] DateTime? endDate = null)
        {
            var deposits = await clientV1.ListDeposits(startDate, endDate);
            if (deposits == null) return NotFound();
            return new JsonResult(deposits);
        }

        /// <summary>
        /// Read Only API v1 - My Transactions
        /// </summary>
        [HttpGet("ro/my/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> Transactions()
        {
            var transactions = await clientV1.ListTransactionHistory();
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }

        /// <summary>
        /// Read Only API v1 - My Coin Transactions
        /// </summary>
        [HttpGet("ro/my/transactions/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> CoinTransactions(string cointype)
        {
            var transactions = await clientV1.ListCoinTransactionHistory(cointype);
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }
        
        /// <summary>
        /// Read Only API v1 - My Open Transactions
        /// </summary>
        [HttpGet("ro/my/transactions/open")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> OpenTransactions()
        {
            var transactions = await clientV1.ListOpenTransactions();
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }
        
        /// <summary>
        /// Read Only API v1 - My Open Coin Transactions
        /// </summary>
        [HttpGet("ro/my/transactions/{cointype}/open")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> OpenCoinTransactions(string cointype)
        {
            var transactions = await clientV1.ListOpenCoinTransactions(cointype);
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }
        
        /// <summary>
        /// Read Only API v1 - My Send/Receive Transactions
        /// </summary>
        [HttpGet("ro/my/sendreceive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendReceiveResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> SendReceiveHistory()
        {
            var transactions = await clientV1.ListSendReceiveTransactions();
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }

        /// <summary>
        /// Read Only API v1 - My Affiliate Payments
        /// </summary>
        /// <remarks>https://www.coinspot.com.au/api#roaffpay</remarks>
        [HttpGet("ro/my/affiliatepayments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AffiliatePaymentV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> AffiliatePayments()
        {
            var transactions = await clientV1.ListAffiliatePayments();
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }

        /// <summary>
        /// Read Only API v1 - My Referral Payments
        /// </summary>
        /// <remarks>https://www.coinspot.com.au/api#rorefpay</remarks>
        [HttpGet("ro/my/referralpayments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReferralPaymentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> ReferralPayments()
        {
            var transactions = await clientV1.ListReferralPayments();
            if (transactions == null) return NotFound();
            return new JsonResult(transactions);
        }



        #endregion

        #region Read only API v2
        /// <summary>
        /// Read Only API v2 - Status Check - Useful for confirming your Key and Secret are working correctly
        /// </summary>
        [HttpGet("v2/ro/status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoinSpotResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> ReadOnlyStatusCheck()
        {
            var status = await clientV2.ReadOnlyStatusCheck();
            return new JsonResult(status);
        }
        
        /// <summary>
        /// Read Only API v2 - List My Balances
        /// </summary>
        [HttpGet("v2/ro/my/balances")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalancesV2Response)) ]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> BalancesV2()
        {
            var balances = await clientV2.ListBalances();
            return new JsonResult(balances);
        }
        
        
        /// <summary>
        /// Read Only API - Balance by coin type
        /// </summary>
        [HttpGet("v2/ro/my/balances/{cointype}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoinBalanceV2Response)) ]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> CoinBalanceV2(string cointype)
        {
            var balances = await clientV2.CoinBalance(cointype);
            return new JsonResult(balances);
        }

        /// <summary>
        /// Read Only API v2 - List My Deposits
        /// </summary>
        /// <param name="startDate">Optional. Specify date range start</param>
        /// <param name="endDate">Optional. Specify date range end</param>
        [HttpGet("v2/ro/my/deposits")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepositsV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> DepositsV2([FromQuery(Name = "startdate")]DateTime? startDate = null, [FromQuery(Name = "enddate")]DateTime? endDate = null)
        {
            var deposits = await clientV2.ListDeposits(startDate, endDate);
            return new JsonResult(deposits);
        }
        
        /// <summary>
        /// Read Only API v2 - List My Withdrawals
        /// </summary>
        /// <param name="startDate">Optional. Specify date range start</param>
        /// <param name="endDate">Optional. Specify date range end</param>
        [HttpGet("v2/ro/my/withdrawls")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WithdrawalsV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> WithdrawalsV2([FromQuery(Name = "startdate")]DateTime? startDate = null, [FromQuery(Name = "enddate")]DateTime? endDate = null)
        {
            var deposits = await clientV2.ListWithdrawals(startDate, endDate);
            return new JsonResult(deposits);
        }

        /// <summary>
        /// Read Only API v2 - My Market Order History
        /// </summary>
        /// <param name="cointype">Coin short name e.g. "ETH", "BTC" etc. used as the <c>cointype</c> url parameter</param>
        /// <param name="markettype">Market coin short name, example value 'USDT' (only for available markets)</param>
        /// <param name="startDate">Optional. Specify date range start format 'yyyy-MM-dd'</param>
        /// <param name="endDate">Optional. Specify date range end format 'yyyy-MM-dd'</param>
        [HttpGet("v2/ro/my/orders/market/completed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MarketOrderV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> MarketOrdersV2([FromQuery]string cointype = null, [FromQuery]string markettype = null, [FromQuery(Name = "startdate")]DateTime? startDate = null, [FromQuery(Name = "enddate")]DateTime? endDate = null)
        {
            var orders = await clientV2.MarketOrderHistory(cointype, markettype, startDate, endDate);
            return new JsonResult(orders);
        }

        /// <summary>
        /// Read Only API v2 - My Send and Receive History
        /// </summary>
        [HttpGet("v2/ro/my/sendreceive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendReceiveV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> SendReceiveV2()
        {
            var transactions = await clientV2.SendReceiveHistory();
            return new JsonResult(transactions);
        }

        /// <summary>
        /// Read Only API v2 - Affiliate payments
        /// </summary>
        [HttpGet("v2/ro/my/affiliatepayments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AffiliatePaymentV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> AffiliatePaymentsV2()
        {
            var transactions = await clientV2.AffiliatePayments();
            return new JsonResult(transactions);
        }

        /// <summary>
        /// Read Only API v2 - Referral payments
        /// </summary>
        [HttpGet("v2/ro/my/referralpayments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReferralPaymentV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> ReferralPaymentsV2()
        {
            var transactions = await clientV2.ReferralPayments();
            return new JsonResult(transactions);
        }

        
        #endregion


    }
}

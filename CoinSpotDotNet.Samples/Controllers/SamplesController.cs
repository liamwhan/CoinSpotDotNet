using CoinSpotDotNet.Responses;
using CoinSpotDotNet.Responses.V2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinSpotDotNet.Samples.Controllers
{
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly ICoinSpotClientV2 clientV2;

        public SamplesController(ICoinSpotClientV2 clientV2)
        {
            this.clientV2 = clientV2;
        }

        /// <summary>
        /// Public API - Latest prices
        /// </summary>
        [HttpGet("pubapi/v2/latest")]
        [ProducesResponseType(200, Type = typeof(LatestPricesV2Response))]
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
        [ProducesResponseType(200, Type = typeof(LatestPricesV2Response))]
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
        [ProducesResponseType(200, Type = typeof(RateMarketPriceV2Response))]
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
        [ProducesResponseType(200, Type = typeof(RateMarketPriceV2Response))]
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
        [ProducesResponseType(200, Type = typeof(RateMarketPriceV2Response))]
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
        [ProducesResponseType(200, Type = typeof(RateMarketPriceV2Response))]
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
        [ProducesResponseType(200, Type = typeof(OpenOrdersV2Response))]
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
        [ProducesResponseType(200, Type = typeof(OpenOrdersV2Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [Produces("application/json")]
        public async Task<IActionResult> OpenOrdersByCoin(string cointype, string markettype)
        {
            var orders = await clientV2.OpenOrdersByCoinMarket(cointype, markettype);
            if (orders == null) return NotFound();
            return new JsonResult(orders);
        }




        /// <summary>
        /// Read Only API - Status Check - Useful for confirming your Key and Secret are working correctly
        /// </summary>
        [HttpGet("v2/ro/status")]
        [ProducesResponseType(200, Type = typeof(CoinSpotResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> ReadOnlyStatusCheck()
        {
            var status = await clientV2.ReadOnlyStatusCheck();
            return new JsonResult(status);
        }
        
        /// <summary>
        /// Read Only API - List My Balances
        /// </summary>
        [HttpGet("v2/ro/my/balances")]
        [ProducesResponseType(200, Type = typeof(MyBalancesV2Response)) ]
        [Produces("application/json")]
        public async Task<IActionResult> MyBalancesV2()
        {
            var balances = await clientV2.ListMyBalances();
            return new JsonResult(balances);
        }
        
       

        
    }
}

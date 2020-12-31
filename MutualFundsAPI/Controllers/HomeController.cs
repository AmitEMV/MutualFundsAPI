using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutualFundsAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDashboardService dashboardService, ILogger<HomeController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        /// <summary>
        ///  Get the current total value of the portfolio
        /// </summary>
        /// <returns>Portfolio value as a string</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/totalvalue")]
        public async Task<ActionResult<string>> GetTotalValueAsync()
        {
            _logger.LogDebug("In HomeController:GetTotalValueAsync");

            string totalValue = await _dashboardService.GetTotalValueAsync();

            _logger.LogDebug("Returning from HomeController:GetTotalValueAsync");

            return Ok(totalValue);
        }

        /// <summary>
        /// Get the value trend for the specified number of months. This is currently limited to 12 months.
        /// </summary>
        /// <param name="numOfMonths">Number of months of historical data to fetch</param>
        /// <returns>Historical data trend as a pair of date and amount pair</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/valuetrend/{numOfMonths?}")]
        public async Task<ActionResult<List<ValueTrend>>> GetPortfolioValueTrendAsync(string numOfMonths)
        {
            _logger.LogDebug("In HomeController:GetPortfolioValueTrendAsync");

            List<ValueTrend> valueTrend = await _dashboardService.GetPortfolioValueTrendAsync(numOfMonths);

            _logger.LogDebug("Returning from HomeController:GetPortfolioValueTrendAsync");

            return Ok(valueTrend);
        }


        /// <summary>
        /// Get the top 6 performing funds for the last 12 months and the % change
        /// </summary>
        /// <returns>List of funds and their corresponding % change</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/topgainers")]
        public async Task<ActionResult<List<FundPerformance>>> GetTopGainersAsync()
        {
            _logger.LogDebug("In HomeController:GetTopGainersAsync");

            List<FundPerformance> fundPerf = await _dashboardService.GetTopGainersAsync();

            _logger.LogDebug("Returning from HomeController:GetTopGainersAsync");

            return Ok(fundPerf);
        }

        /// <summary>
        /// Get the worst 6 performing funds for the last 12 months and the % change
        /// </summary>
        /// <returns>List of funds and their corresponding % change</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/toplosers")]
        public async Task<ActionResult<List<FundPerformance>>> GetTopLosersAsync()
        {
            _logger.LogDebug("In HomeController:GetTopLosersAsync");

            List<FundPerformance> fundPerf = await _dashboardService.GetTopLosersAsync();

            _logger.LogDebug("Returning from HomeController:GetTopLosersAsync");

            return Ok(fundPerf);
        }
    }
}

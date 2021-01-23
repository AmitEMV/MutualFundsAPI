using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsAPI.Controllers
{
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioController(IPortfolioService portfolioService, ILogger<PortfolioController> logger)
        {
            _portfolioService = portfolioService;
            _logger = logger;
        }

        /// <summary>
        ///  Get the investment distribution split across different fund types
        /// </summary>
        /// <returns>Portfolio split as a list of fund type and value pairs</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/funddistribution")]
        public async Task<ActionResult<List<FundDistribution>>> GetFundDistributionAsync()
        {
            _logger.LogDebug("In PortfolioController:GetFundDistributionAsync");

            List<FundDistribution> fundDistributions = await _portfolioService.GetFundDistributionAsync();

            _logger.LogDebug("Returning from PortfolioController:GetFundDistributionAsync");

            return Ok(fundDistributions);
        }

        /// <summary>
        ///  Get the current portfolio snapshot across invested funds and their returns 
        /// </summary>
        /// <returns>List of data representing invested fund details and their returns</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/portfoliosnapshot")]
        public async Task<ActionResult<List<PortfolioSnapshot>>> GetPortfolioSnapshotAsync()
        {
            _logger.LogDebug("In PortfolioController:GetPortfolioSnapshotAsync");

            List<PortfolioSnapshot> portfolioSnapshot = await _portfolioService.GetPortfolioSnapshotAsync();

            _logger.LogDebug("Returning from PortfolioController:GetPortfolioSnapshotAsync");

            return Ok(portfolioSnapshot);
        }


        /// <summary>
        ///  Check the growth of our investment by checking the current value
        /// </summary>
        /// <returns>Invested amount and the current value of the investment</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/investmentvalue")]
        public async Task<ActionResult<InvestmentStatus>> GetInvestmentReturnsValueAsync()
        {
            _logger.LogDebug("In PortfolioController:GetInvestmentAndReturnsValueAsync");

            InvestmentStatus investmentStatus = await _portfolioService.GetInvestmentReturnsValueAsync();

            _logger.LogDebug("Returning from PortfolioController:GetInvestmentAndReturnsValueAsync");

            return Ok(investmentStatus);
        }

        /// <summary>
        /// Add a new fund in the portfolio to track
        /// </summary>
        /// <returns>Success or failure status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/addfund")]
        public async Task<ActionResult<bool>> AddFundToPortfolioAsync(FundInfo fundInfo)
        {
            _logger.LogDebug("In PortfolioController:AddFundToPortfolioAsync");

            bool status = await _portfolioService.AddFundToPortfolioAsync(fundInfo);

            _logger.LogDebug("Returning from PortfolioController:AddFundToPortfolioAsync");

            return Ok(status);
        }

        /// <summary>
        /// Delete a fund from our tracking portfolio
        /// </summary>
        /// <returns>Success or failure status</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/deletefund")]
        public async Task<ActionResult<bool>> DeleteFundFromPortfolioAsync(PortfolioSnapshot portfolioSnapshot)
        {
            _logger.LogDebug("In PortfolioController:DeleteFundFromPortfolioAsync");

            bool status = await _portfolioService.DeleteFundFromPortfolioAsync(portfolioSnapshot.FundId, portfolioSnapshot.PortfolioId);

            _logger.LogDebug("Returning from PortfolioController:DeleteFundFromPortfolioAsync");

            return Ok(status);
        }

    }
}

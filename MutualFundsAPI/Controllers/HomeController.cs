using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutualFundsAPI.Helpers;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDb appDb;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDb db, ILogger<HomeController> logger)
        {
            appDb = db;
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
            _logger.LogDebug("HomeController::GetTotalValueAsync: Fetching total portolio value");

            string totalValue = string.Empty;
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOTAL_PORTFOLIO_VALUE;
                var result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    totalValue = result.ToString();
                }
            }

            _logger.LogDebug("HomeController::GetTotalValueAsync: Fetching total portolio value successful");

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
            _logger.LogDebug("HomeController::GetPortfolioValueTrendAsync: Fetching portolio value trend");
            List<ValueTrend> valueTrend = new List<ValueTrend>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.PORTFOLIO_VALUE_TREND;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        valueTrend.Add(new ValueTrend()
                        {
                            Date = reader.GetValue(1).ToString(),
                            Amount = Double.Parse(reader.GetValue(2).ToString())
                        });
                    }
                }
            }

            _logger.LogDebug("HomeController::GetPortfolioValueTrendAsync: Fetching portolio value trend successful");
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
            _logger.LogDebug("HomeController::GetTopGainersAsync: Fetching top gainers");

            List<FundPerformance> fundPerf = new List<FundPerformance>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOP_GAINERS;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fundPerf.Add(new FundPerformance
                        {
                            FundName = reader.GetValue(1).ToString(),
                            Return = reader.GetValue(2).ToString()
                        });
                    }

                }
            }

            _logger.LogDebug("HomeController::GetTopGainersAsync: Fetching top gainers successful");

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
            _logger.LogDebug("HomeController::GetTopLosersAsync: Fetching top losers");

            List<FundPerformance> fundPerf = new List<FundPerformance>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOP_LOSERS;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fundPerf.Add(new FundPerformance
                        {
                            FundName = reader.GetValue(1).ToString(),
                            Return = reader.GetValue(2).ToString()
                        });
                    }
                }
            }

            _logger.LogDebug("HomeController::GetTopLosersAsync: Fetching top losers successful");

            return Ok(fundPerf);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(AppDb db)
        {
            appDb = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/TotalValue")]
        public async Task<ActionResult<string>> GetTotalValueAsync()
        {
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

            return Ok(totalValue);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/ValueTrend/{numOfMonths?}")]
        public async Task<ActionResult<List<ValueTrend>>> GetPortfolioValueTrendAsync(string numOfMonths)
        {
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

            return Ok(valueTrend);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/TopGainers")]
        public async Task<ActionResult<List<FundPerformance>>> GetTopGainersAsync()
        {
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

            return Ok(fundPerf);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/[controller]/TopLosers")]
        public async Task<ActionResult<List<FundPerformance>>> GetTopLosersAsync()
        {
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

            return Ok(fundPerf);
        }
    }
}

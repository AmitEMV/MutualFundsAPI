using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutualFundsAPI.Helpers;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly MySqlConnection mySqlConnection;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MySqlConnection connection)
        {
            _logger = logger;
            mySqlConnection = connection;
        }

        [HttpGet]
        [Route("api/[controller]/GetTotalValue")]
        public string GetTotalValue()
        {
            string totalValue = string.Empty;
            mySqlConnection.Open();

            using (var cmd = mySqlConnection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOTAL_PORTFOLIO_VALUE;
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    totalValue = result.ToString();
                }
            }

            return totalValue;
        }

        [HttpGet]
        [Route("api/[controller]/GetValueTrend")]
        public List<ValueTrend> GetPortfolioValueTrend(string numOfMonths)
        {
            List<ValueTrend> valueTrend = new List<ValueTrend>();
            mySqlConnection.Open();

            using (var cmd = mySqlConnection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.PORTFOLIO_VALUE_TREND;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    valueTrend.Add(new ValueTrend()
                    {
                        Date = reader.GetValue(1).ToString(),
                        Amount = Double.Parse(reader.GetValue(2).ToString())
                    });
                }

                reader.Close();
            }

            return valueTrend;
        }


        [HttpGet]
        [Route("api/[controller]/GetTopGainers")]
        public List<FundPerformance> GetTopGainers(string numOfMonths)
        {
            List<FundPerformance> fundPerf = new List<FundPerformance>();
            mySqlConnection.Open();

            using (var cmd = mySqlConnection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOP_GAINERS;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fundPerf.Add(new FundPerformance
                    {
                        FundName = reader.GetValue(1).ToString(),
                        Return = reader.GetValue(2).ToString()
                    });
                }

                reader.Close();
            }

            return fundPerf;
        }

        [HttpGet]
        [Route("api/[controller]/GetTopLosers")]
        public List<FundPerformance> GetTopLosers(string numOfMonths)
        {
            List<FundPerformance> fundPerf = new List<FundPerformance>();
            mySqlConnection.Open();

            using (var cmd = mySqlConnection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.TOP_LOSERS;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fundPerf.Add(new FundPerformance
                    {
                        FundName = reader.GetValue(1).ToString(),
                        Return = reader.GetValue(2).ToString()
                    });
                }

                reader.Close();
            }

            return fundPerf;
        }
    }
}

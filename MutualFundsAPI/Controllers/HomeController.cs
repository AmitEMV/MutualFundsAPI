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
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly MySqlConnection mySqlConnection;
        private readonly ILogger<WeatherForecastController> _logger;

        public HomeController(ILogger<WeatherForecastController> logger, MySqlConnection connection)
        {
            _logger = logger;
            mySqlConnection = connection;
        }

        [HttpGet]
        public string GetTotalValue()
        {
            string totalValue = string.Empty;
            mySqlConnection.Open();

            using (var cmd = mySqlConnection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_TOTAL_PORTFOLIO_VALUE;
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    totalValue = result.ToString();
                }
            }

            return totalValue;
        }
    }
}

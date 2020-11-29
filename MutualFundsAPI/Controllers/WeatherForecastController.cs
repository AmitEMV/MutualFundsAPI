using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace MutualFundsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        MySqlConnection mySqlConnection;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MySqlConnection connection)
        {
            _logger = logger;
            mySqlConnection = connection;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            mySqlConnection.Open();
            var cmd = mySqlConnection.CreateCommand();
            cmd.CommandText = @"SELECT SUM(ROUND(afv.nav * p.units,2)) AS TOTAL_PORTFOLIO_VALUE
		FROM mutualfunds_db.portfolio p
        INNER JOIN mutualfunds_db.amc_fund_nav afv ON p.amc_fund_id = afv.amc_fund_id
        INNER JOIN amc_funds af ON afv.amc_fund_id = af.id
WHERE afv.`date` = CURRENT_DATE;";
            var result = cmd.ExecuteReader();
            double val;
            int count = result.FieldCount;
            while (result.Read())
            {
                for (int i = 0; i < count; i++)
                {
                    val = (double)result.GetValue(i);
                    System.Diagnostics.Debug.WriteLine(val);
                }
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

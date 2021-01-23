using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Helpers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Implementation
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DashboardService : IDashboardService
    {
        private readonly AppDb appDb;
        private readonly ILogger<HomeController> _logger;

        public DashboardService(IDBConnector db, ILogger<HomeController> logger)
        {
            appDb = (AppDb)db;
            _logger = logger;
        }

        public async Task<List<ValueTrend>> GetPortfolioValueTrendAsync(string numOfMonths)
        {
            _logger.LogDebug("DashboardService:GetPortfolioValueTrendAsync: Fetching portolio value trend");

            List<ValueTrend> valueTrend = new List<ValueTrend>();

            await appDb.Connection.OpenAsync();

            // Get current value
            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_PORTFOLIO_VALUE_TREND;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        valueTrend.Add(new ValueTrend()
                        {
                            Date = reader.GetValue(1).ToString(),
                            CurrentValue = reader.GetValue(2).GetDoubleValue()
                        });
                    }
                }
            }

            // Get invested value
            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_INVESTED_VALUE_TREND;
                cmd.Parameters.Add("@numofmonths", MySqlDbType.Int32).Value = numOfMonths;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                int i = 0;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        valueTrend[i].InvestedValue = reader.GetValue(1).GetDoubleValue();
                        i++;
                    }
                }
            }

            _logger.LogDebug("DashboardService:GetPortfolioValueTrendAsync: Fetching portolio value trend successful");

            return valueTrend;
        }

        public async Task<List<FundPerformance>> GetTopGainersAsync()
        {
            _logger.LogDebug("DashboardService:GetTopGainersAsync: Fetching top gainers");

            List<FundPerformance> fundPerf = new List<FundPerformance>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_TOP_GAINERS;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fundPerf.Add(new FundPerformance
                        {
                            FundName = reader.GetValue(1).ToString(),
                            Return = reader.GetValue(2).GetDoubleValue()
                        });
                    }

                }
            }

            _logger.LogDebug("DashboardService:GetTopGainersAsync: Fetching top gainers successful");

            return fundPerf;
        }

        public async Task<List<FundPerformance>> GetTopLosersAsync()
        {
            _logger.LogDebug("In DashboardService:GetTopLosersAsync");

            List<FundPerformance> fundPerf = new List<FundPerformance>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_TOP_LOSERS;
                cmd.Parameters.Add("@dayofweek", MySqlDbType.Int32).Value = 1 + (int)DateTime.Now.DayOfWeek;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fundPerf.Add(new FundPerformance
                        {
                            FundName = reader.GetValue(1).ToString(),
                            Return = reader.GetValue(2).GetDoubleValue()
                        });
                    }
                }
            }

            _logger.LogDebug("Returning from DashboardService:GetTopLosersAsync");

            return fundPerf;
        }

        public async Task<double> GetTotalValueAsync()
        {
            _logger.LogDebug("In DashboardService:GetTotalValueAsync");
            
            double totalValue = 0;
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_TOTAL_PORTFOLIO_VALUE;
                var result = await cmd.ExecuteScalarAsync();
                totalValue = result.GetDoubleValue();
            }

            _logger.LogDebug("Returning from DashboardService:GetTotalValueAsync");

            return totalValue;
        } 

        public async Task<List<Funds>> GetAvailableFundsAsync()
        {
            _logger.LogDebug("In DashboardService:GetAvailableFundsAsync");

            List<Funds> funds = new List<Funds>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_AVAILABLE_FUNDS;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        funds.Add(new Funds
                        {
                            Id = reader.GetValue(0).ToString(),
                            FundName = reader.GetValue(1).ToString()
                        });
                    }
                }
            }

            _logger.LogDebug("Returning from DashboardService:GetAvailableFundsAsync");

            return funds;
        }
    }
}

using MutualFundsAPI.Controllers;
using MutualFundsAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Test
{
    public class TestDashboardService : IDashboardService
    {
        public async Task<List<ValueTrend>> GetPortfolioValueTrendAsync(string numOfMonths)
        {
            List<ValueTrend> ValueTrend = new List<ValueTrend>()
            {
                new ValueTrend()
                {
                    Amount = 100,
                    Date = "01-01-2019"
                },
                new ValueTrend()
                {
                    Amount = 150,
                    Date = "10-10-2019"
                },
            };

            return await Task.FromResult<List<ValueTrend>>(ValueTrend);
        }

        public async Task<List<FundPerformance>> GetTopGainersAsync()
        {
            List<FundPerformance> fundPerformance = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = "10"
                }
            };

            return await Task.FromResult<List<FundPerformance>>(fundPerformance);
        }

        public async Task<List<FundPerformance>> GetTopLosersAsync()
        {
            List<FundPerformance> fundPerformance = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = "-10"
                }
            };

            return await Task.FromResult<List<FundPerformance>>(fundPerformance);
        }

        public async Task<string> GetTotalValueAsync()
        {
            return await Task.FromResult<string>("1000");
        }
    }
}
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Test
{
    public class DummyDashboardService : IDashboardService
    {

        public async Task<List<ValueTrend>> GetPortfolioValueTrendAsync(string numOfMonths)
        {
            List<ValueTrend> ValueTrend = new List<ValueTrend>()
            {
                new ValueTrend()
                {
                    CurrentValue = 100,
                    InvestedValue = 80,
                    Date = "01-01-2019"
                },
                new ValueTrend()
                {
                    CurrentValue = 150,
                    InvestedValue = 80,
                    Date = "10-10-2019"
                },
            };

            return await Task.FromResult(ValueTrend);
        }

        public async Task<List<FundPerformance>> GetTopGainersAsync()
        {
            List<FundPerformance> fundPerformance = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = 10
                }
            };

            return await Task.FromResult(fundPerformance);
        }

        public async Task<List<FundPerformance>> GetTopLosersAsync()
        {
            List<FundPerformance> fundPerformance = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = -10
                }
            };

            return await Task.FromResult(fundPerformance);
        }

        public async Task<double> GetTotalValueAsync()
        {
            return await Task.FromResult(1000);
        }

        public async Task<List<Funds>> GetAvailableFundsAsync()
        {
            List<Funds> funds = new List<Funds>()
            {
                new Funds()
                {
                    FundName = "HDFC Fund",
                    Id = "1"
                }
            };

            return await Task.FromResult(funds);
        }

    }
}
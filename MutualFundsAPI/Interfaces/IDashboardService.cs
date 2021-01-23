using MutualFundsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<double> GetTotalValueAsync();

        Task<List<ValueTrend>> GetPortfolioValueTrendAsync(string numOfMonths);

        Task<List<FundPerformance>> GetTopGainersAsync();

        Task<List<FundPerformance>> GetTopLosersAsync();

        Task<List<Funds>> GetAvailableFundsAsync();
    }
}

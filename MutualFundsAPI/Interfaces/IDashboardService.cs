using Microsoft.AspNetCore.Mvc;
using MutualFundsAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<string> GetTotalValueAsync();

        Task<List<ValueTrend>> GetPortfolioValueTrendAsync(string numOfMonths);

        Task<List<FundPerformance>> GetTopGainersAsync();

        Task<List<FundPerformance>> GetTopLosersAsync();
    }
}

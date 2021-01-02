using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutualFundsAPI.Test.Services
{
    public class DummyPortfolioService : IPortfolioService
    {
        public async Task<List<FundDistribution>> GetFundDistributionAsync()
        {
            List<FundDistribution> fundDistributions = new List<FundDistribution>()
            {
                new FundDistribution()
                {
                    FundType = "Equity",
                    FundValue = 1000
                }
            };

            return await Task.FromResult<List<FundDistribution>>(fundDistributions);
        }

        public async Task<(string, string)> GetInvestmentReturnsValueAsync()
        {
            return await Task.FromResult<(string, string)>(("1000", "1500"));
        }

        public async Task<List<PortfolioSnapshot>> GetPortfolioSnapshotAsync()
        {
            List<PortfolioSnapshot> portfolioSnapshots = new List<PortfolioSnapshot>()
            {
                new PortfolioSnapshot()
                {
                    FundName = "HDFC Fund",
                    InvestmentValue = "1000",
                    CurrentValue = "1500",
                    Return = "50"
                }
            };

            return await Task.FromResult<List<PortfolioSnapshot>>(portfolioSnapshots);
        }
    }
}

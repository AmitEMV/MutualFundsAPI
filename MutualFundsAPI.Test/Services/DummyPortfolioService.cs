﻿using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using System.Collections.Generic;
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

            return await Task.FromResult(fundDistributions);
        }

        public async Task<InvestmentStatus> GetInvestmentReturnsValueAsync()
        {
            return await Task.FromResult(new InvestmentStatus(){
                InvestmentValue = 1000,
                CurrentValue = 1500
            });
        }

        public async Task<List<PortfolioSnapshot>> GetPortfolioSnapshotAsync()
        {
            List<PortfolioSnapshot> portfolioSnapshots = new List<PortfolioSnapshot>()
            {
                new PortfolioSnapshot()
                {
                    FundName = "HDFC Fund",
                    InvestmentValue = 1000,
                    CurrentValue = 1500,
                    Return = 50
                }
            };

            return await Task.FromResult(portfolioSnapshots);
        }
    }
}

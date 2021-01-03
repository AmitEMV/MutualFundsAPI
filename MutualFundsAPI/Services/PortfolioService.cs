using Microsoft.Extensions.Logging;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Helpers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsAPI.Implementation
{
    public class PortfolioService : IPortfolioService
    {
        private readonly AppDb appDb;
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioService(IDBConnector db, ILogger<PortfolioController> logger)
        {
            appDb = (AppDb)db;
            _logger = logger;
        }

        public async Task<InvestmentStatus> GetInvestmentReturnsValueAsync()
        {
            _logger.LogDebug("PortfolioService:GetInvestmentAndReturnsValueAsync: Fetching invested value and current value");

            InvestmentStatus investmentStatus = new InvestmentStatus();

            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_INVESTMENT_RETURNS_VALUE;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        investmentStatus.InvestmentValue = double.Parse(reader.GetValue(0).ToString());
                        investmentStatus.CurrentValue = double.Parse(reader.GetValue(1).ToString());
                    }
                }
            }

            _logger.LogDebug("PortfolioService:GetInvestmentAndReturnsValueAsync: Investment values fetched successfully");

            return investmentStatus;
        }

        public async Task<List<FundDistribution>> GetFundDistributionAsync()
        {
            _logger.LogDebug("PortfolioService:GetFundDistribution: Fetching portfolio investment distribution across fund types");

            List<FundDistribution> fundDistributions = new List<FundDistribution>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_PORTFOLIO_DISTRIBUTION;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fundDistributions.Add(new FundDistribution
                        {
                            FundType = reader.GetValue(0).ToString(),
                            FundValue = Double.Parse(reader.GetValue(1).ToString())
                        });
                    }

                }
            }

            _logger.LogDebug("PortfolioService:GetFundDistribution: Portfolio distribution fetch successful");

            return fundDistributions;
        }

        public async Task<List<PortfolioSnapshot>> GetPortfolioSnapshotAsync()
        {
            _logger.LogDebug("PortfolioService:GetPortfolioSnapshotAsync: Fetching the investment portfolio snapshot");

            List<PortfolioSnapshot> portfolioSnapshot = new List<PortfolioSnapshot>();
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.GET_PORTFOLIO_SNAPSHOT;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        portfolioSnapshot.Add(new PortfolioSnapshot
                        {
                            FundName = reader.GetValue(0).ToString(),
                            InvestmentValue = double.Parse(reader.GetValue(1).ToString()),
                            CurrentValue = double.Parse(reader.GetValue(2).ToString()),
                            Return = double.Parse(reader.GetValue(3).ToString())
                        });
                    }

                }
            }

            _logger.LogDebug("PortfolioService:GetPortfolioSnapshotAsync: Portfolio snapshot fetched successfully");

            return portfolioSnapshot;
        }
    }
}

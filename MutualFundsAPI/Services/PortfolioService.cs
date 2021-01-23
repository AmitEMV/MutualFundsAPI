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
                        investmentStatus.InvestmentValue = reader.GetValue(0).GetDoubleValue();
                        investmentStatus.CurrentValue = reader.GetValue(1).GetDoubleValue();
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
                            FundValue = reader.GetValue(1).GetDoubleValue()
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
                            FundId = int.Parse(reader.GetValue(0).ToString()),
                            PortfolioId = long.Parse(reader.GetValue(1).ToString()),
                            FundName = reader.GetValue(2).ToString(),
                            InvestmentValue = reader.GetValue(3).GetDoubleValue(),
                            CurrentValue = reader.GetValue(4).GetDoubleValue(),
                            Return = reader.GetValue(5).GetDoubleValue()
                        });
                    }
                }
            }

            _logger.LogDebug("PortfolioService:GetPortfolioSnapshotAsync: Portfolio snapshot fetched successfully");

            return portfolioSnapshot;
        }

        public async Task<bool> DeleteFundFromPortfolioAsync(int fundID, long portfolioId)
        {
            _logger.LogDebug($"PortfolioService:DeleteFundFromPortfolioAsync: Deleting {fundID} from portfolio {portfolioId}");

            var status = false;
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.DELETE_FUND;
                cmd.Parameters.Add("@portfolioId", MySqlConnector.MySqlDbType.Int64).Value = portfolioId;
                cmd.Parameters.Add("@fundId", MySqlConnector.MySqlDbType.Int32).Value = fundID;
                await cmd.ExecuteNonQueryAsync();
                status = true;
            }

            _logger.LogDebug("PortfolioService:DeleteFundFromPortfolioAsync: Deleted fund from portfolio successfuly");

            return status;
        }

        public async Task<bool> AddFundToPortfolioAsync(FundInfo fundInfo)
        {
            _logger.LogDebug($"PortfolioService:AddFundToPortfolioAsync: Adding new fund {fundInfo.FundName} to portfolio");

            var status = false;
            await appDb.Connection.OpenAsync();

            using (var cmd = appDb.Connection.CreateCommand())
            {
                cmd.CommandText = SqlQueries.ADD_FUND;
                cmd.Parameters.Add("@amcfundid", MySqlConnector.MySqlDbType.Int32).Value = fundInfo.FundId;
                cmd.Parameters.Add("@units", MySqlConnector.MySqlDbType.Double).Value = fundInfo.NumberOfUnits;
                cmd.Parameters.Add("@purchasetype", MySqlConnector.MySqlDbType.VarChar).Value = fundInfo.PurchaseType;
                cmd.Parameters.Add("@purchasedate", MySqlConnector.MySqlDbType.Date).Value = fundInfo.PurchaseDate;
                await cmd.ExecuteNonQueryAsync();
                status = true;
            }

            _logger.LogDebug("PortfolioService:AddFundToPortfolioAsync: Added new fund to the portfolio successfuly");

            return status;
        }
    }
}

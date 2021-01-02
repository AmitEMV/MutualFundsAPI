using MutualFundsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Interfaces
{
    public interface IPortfolioService
    {
        Task<InvestmentStatus> GetInvestmentReturnsValueAsync();

        Task<List<FundDistribution>> GetFundDistributionAsync();

        Task<List<PortfolioSnapshot>> GetPortfolioSnapshotAsync();
    }
}

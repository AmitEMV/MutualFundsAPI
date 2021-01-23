using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class PortfolioSnapshot
    {
        [Required]
        public int FundId { get; set; }

        [Required]
        public long PortfolioId { get; set; }

        [Required]
        public string FundName { get; set; }

        [Required]
        public double InvestmentValue { get; set; }

        [Required]
        public double CurrentValue { get; set; }

        [Required]
        public double Return { get; set; }
    }
}

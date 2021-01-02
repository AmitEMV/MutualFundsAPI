using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class PortfolioSnapshot
    {
        [Required]
        public string FundName { get; set; }

        [Required]
        public string InvestmentValue { get; set; }

        [Required]
        public string CurrentValue { get; set; }

        [Required]
        public string Return { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class InvestmentStatus
    {
        [Required]
        public string InvestmentValue { get; set; }

        [Required]
        public string CurrentValue { get; set; }
    }
}

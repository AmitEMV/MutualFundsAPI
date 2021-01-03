using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class InvestmentStatus
    {
        [Required]
        public double InvestmentValue { get; set; }

        [Required]
        public double CurrentValue { get; set; }
    }
}

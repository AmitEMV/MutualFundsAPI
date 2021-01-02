using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class FundDistribution
    {
        [Required]
        public double FundValue { get; set; }

        [Required]
        public string FundType { get; set; }
    }
}

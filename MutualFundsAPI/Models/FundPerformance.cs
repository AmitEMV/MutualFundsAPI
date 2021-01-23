using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class FundPerformance
    {
        [Required]
        public string FundName { get; set; }

        [Required]
        public double Return { get; set; }
    }
}

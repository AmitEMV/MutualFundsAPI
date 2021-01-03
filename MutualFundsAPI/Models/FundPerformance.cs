using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Controllers
{
    public class FundPerformance
    {
        [Required]
        public string FundName { get; set; }

        [Required]
        public double Return { get; set; }
    }
}

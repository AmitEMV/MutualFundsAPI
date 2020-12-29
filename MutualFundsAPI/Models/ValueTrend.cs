using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Controllers
{
    public class ValueTrend
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        public string Date { get; set; }
    }
}

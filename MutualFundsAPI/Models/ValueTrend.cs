using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class ValueTrend
    {
        [Required]
        public double CurrentValue { get; set; }

        [Required]
        public double InvestedValue { get; set; }

        [Required]
        public string Date { get; set; }
    }
}

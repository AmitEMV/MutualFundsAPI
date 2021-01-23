using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class Funds
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FundName { get; set; }
    }
}

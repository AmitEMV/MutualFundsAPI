using System;
using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class FundInfo
    {
        [Required]
        public string FundName { get; set; }
        [Required]
        public string PurchaseType { get; set; }
        [Required]
        public string InvestmentType { get; set; }
        [Required]
        public string PlanType { get; set; }
        [Required]
        public string FundType { get; set; }
        [Required]
        public string FundCategory { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public string PurchaseAmount { get; set; }
        [Required]
        public string NumberOfUnits { get; set; }
        [Required]
        public string NAV { get; set; }
    }
}

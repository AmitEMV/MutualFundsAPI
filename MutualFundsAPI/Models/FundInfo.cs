using System;
using System.ComponentModel.DataAnnotations;

namespace MutualFundsAPI.Models
{
    public class FundInfo
    {
        public string FundName { get; set; }
        [Required]
        public string PurchaseType { get; set; }
        public string InvestmentType { get; set; }
        public string PlanType { get; set; }
        public string FundType { get; set; }
        public string FundCategory { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        public string PurchaseAmount { get; set; }
        [Required]
        public string NumberOfUnits { get; set; }
        public string NAV { get; set; }
        [Required]
        public string FundId { get; set; }
    }
}

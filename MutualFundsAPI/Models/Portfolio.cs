using System;

namespace MutualFundsAPI.Models
{
    public class Portfolio
    {
        public string AMCFundId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double Units { get; set; }

        public string PurchaseType { get; set; }
    }
}

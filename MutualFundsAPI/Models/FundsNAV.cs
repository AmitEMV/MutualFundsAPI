using System;

namespace MutualFundsAPI.Models
{
    public class FundsNAV
    {
        public string AMCFundId { get; set; }

        public DateTime Date { get; set; }

        public double NAV { get; set; }
    }
}

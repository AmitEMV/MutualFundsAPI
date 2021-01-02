using MutualFundsAPI.Controllers;
using MutualFundsAPI.Models;

namespace MutualFundsAPI.Test.Helpers
{
    public class ValueTrendComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            ValueTrend valueTrendX = x as ValueTrend;
            ValueTrend valueTrendY = y as ValueTrend;

            if ((valueTrendX.Amount == valueTrendY.Amount && valueTrendX.Date == valueTrendY.Date))
                return 0;
            return 1;
        }
    }

    public class FundPerformanceComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            FundPerformance fundPerformanceX = x as FundPerformance;
            FundPerformance fundPerformanceY = y as FundPerformance;

            if ((fundPerformanceX.FundName == fundPerformanceY.FundName && fundPerformanceX.Return == fundPerformanceY.Return))
                return 0;
            return 1;
        }
    }

    public class FundDistributionComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            FundDistribution fundPerformanceX = x as FundDistribution;
            FundDistribution fundPerformanceY = y as FundDistribution;

            if ((fundPerformanceX.FundType == fundPerformanceY.FundType && fundPerformanceX.FundValue == fundPerformanceY.FundValue))
                return 0;
            return 1;
        }
    }

    public class PortfolioSnapshotComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            PortfolioSnapshot fundPerformanceX = x as PortfolioSnapshot;
            PortfolioSnapshot fundPerformanceY = y as PortfolioSnapshot;

            if ((fundPerformanceX.FundName == fundPerformanceY.FundName && fundPerformanceX.CurrentValue == fundPerformanceY.CurrentValue)
                && (fundPerformanceX.InvestmentValue == fundPerformanceY.InvestmentValue && fundPerformanceX.Return == fundPerformanceY.Return))
                return 0;
            return 1;
        }
    }
}

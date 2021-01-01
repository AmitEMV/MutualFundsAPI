using MutualFundsAPI.Controllers;

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
}

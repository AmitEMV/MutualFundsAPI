namespace MutualFundsAPI.Helpers
{
    public class SqlQueries
    {
        public static readonly string GET_TOTAL_PORTFOLIO_VALUE = @"SELECT SUM(ROUND(afv.nav * p.units,2)) AS TOTAL_PORTFOLIO_VALUE
		FROM mutualfunds_db.portfolio p
        INNER JOIN mutualfunds_db.amc_fund_nav afv ON p.amc_fund_id = afv.amc_fund_id
        INNER JOIN amc_funds af ON afv.amc_fund_id = af.id 
        WHERE afv.`date` = CURRENT_DATE;";
    }
}

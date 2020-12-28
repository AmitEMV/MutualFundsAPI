namespace MutualFundsAPI.Helpers
{
    public class SqlQueries
    {
        public static readonly string TOTAL_PORTFOLIO_VALUE = @"SELECT 
		SUM(ROUND(NAV.nav * PF.units,0)) as portfolio_value FROM 
		mutualfunds_db.portfolio PF
        INNER JOIN amc_funds FUNDS on PF.amc_fund_id = FUNDS.id
        INNER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id  AND NAV.date = current_date
        order by PF.purchase_date, PF.amc_fund_id;";

        public static readonly string PORTFOLIO_VALUE_TREND = @"SELECT  FUNDS.id,  DATE_FORMAT(NAV.date, '%Y-%m-%d'),
        SUM(ROUND(NAV.nav * PF.units,0)) as amount FROM 
		mutualfunds_db.portfolio PF
        LEFT OUTER JOIN amc_funds FUNDS on PF.amc_fund_id = FUNDS.id
        LEFT OUTER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id WHERE  
		NAV.date <= current_date AND  NAV.date >= DATE_SUB(current_date,  INTERVAL @numofmonths MONTH) 
        AND DAYOFWEEK(NAV.date) = @dayofweek
        GROUP BY NAV.date
        ORDER by NAV.date DESC;";

        public static readonly string TOP_GAINERS = @"SELECT fund_id, fund_name, ROUND(((today_value - prev_value)/prev_value)*100,2)as 'absolute return %'
        FROM mutualfunds_db.view_fund_oneyear_range
        WHERE today_value > prev_value
        ORDER BY ROUND(((today_value - prev_value)/prev_value)*100,2) DESC
        LIMIT 6;";

        public static readonly string TOP_LOSERS = @"SELECT fund_id, fund_name, ROUND(((today_value - prev_value)/prev_value)*100,2) as 'absolute return %'
        FROM mutualfunds_db.view_fund_oneyear_range
        WHERE prev_value > today_value
        ORDER BY ROUND(((today_value - prev_value)/prev_value)*100,2) ASC
        LIMIT 6;";

    }
}

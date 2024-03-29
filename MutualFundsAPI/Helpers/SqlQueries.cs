﻿namespace MutualFundsAPI.Helpers
{
    public class SqlQueries
    {
        #region Dashboard Queries

        public static readonly string GET_TOTAL_PORTFOLIO_VALUE = @"SELECT 
		SUM(ROUND(NAV.nav * PF.units,0)) as portfolio_value FROM 
		mutualfunds_db.portfolio PF
        INNER JOIN amc_funds FUNDS on PF.amc_fund_id = FUNDS.id
        INNER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id  AND NAV.date = current_date
        order by PF.purchase_date, PF.amc_fund_id;";

        public static readonly string GET_PORTFOLIO_VALUE_TREND = @"SELECT  FUNDS.id,  DATE_FORMAT(NAV.date, '%Y-%m-%d'),
        SUM(ROUND(NAV.nav * PF.units,0)) as amount FROM 
		mutualfunds_db.portfolio PF
        LEFT OUTER JOIN amc_funds FUNDS on PF.amc_fund_id = FUNDS.id
        LEFT OUTER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id WHERE  
		NAV.date <= current_date AND  NAV.date >= GREATEST(DATE_SUB(CURRENT_DATE,  INTERVAL @numofmonths MONTH), PF.purchase_date)
        AND DAYOFWEEK(NAV.date) = @dayofweek
        GROUP BY NAV.date
        ORDER by NAV.date ASC;";

        public static readonly string GET_INVESTED_VALUE_TREND = @"SET @csum :=0;            
        SELECT  
			DATE_FORMAT(NAV.date,""%Y-%m-%d""), MAX(INV.investment_value_date)
        FROM
           ( SELECT amc_fund_id, purchase_date, ROUND(@csum := @csum + investment_value) AS investment_value_date
             FROM 
                invested_value
              ORDER BY purchase_date ASC)  AS INV
              INNER JOIN amc_fund_nav NAV ON NAV.amc_fund_id = INV.amc_fund_id
             WHERE
                NAV.date <= CURRENT_DATE AND
                NAV.date >= GREATEST(DATE_SUB(CURRENT_DATE, INTERVAL @numofmonths MONTH), INV.purchase_date)
        AND DAYOFWEEK(NAV.date) = @dayofweek     

        GROUP BY NAV.date
        ORDER BY NAV.date ASC;";

        public static readonly string GET_TOP_GAINERS = @"SELECT fund_id, fund_name, ROUND(((today_value - prev_value)/prev_value)*100,2)as 'absolute return %'
        FROM mutualfunds_db.view_fund_oneyear_range
        WHERE today_value > prev_value
        ORDER BY ROUND(((today_value - prev_value)/prev_value)*100,2) DESC
        LIMIT 6;";

        public static readonly string GET_TOP_LOSERS = @"SELECT fund_id, fund_name, ROUND(((today_value - prev_value)/prev_value)*100,2) as 'absolute return %'
        FROM mutualfunds_db.view_fund_oneyear_range
        WHERE prev_value > today_value
        ORDER BY ROUND(((today_value - prev_value)/prev_value)*100,2) ASC
        LIMIT 6;";

        public static readonly string GET_AVAILABLE_FUNDS = @"SELECT id, fund_name  FROM mutualfunds_db.amc_funds;";

        #endregion

        #region Portfolio Queries

        public static readonly string GET_PORTFOLIO_DISTRIBUTION = @"SELECT 
		FUNDS.fund_type , SUM(ROUND(NAV.nav * PF.units,0)) as current_value
        FROM mutualfunds_db.portfolio PF
        INNER JOIN amc_funds FUNDS ON PF.amc_fund_id = FUNDS.id
        INNER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id  AND NAV.date = CURRENT_DATE
        GROUP BY FUNDS.fund_type;";

        public static readonly string GET_PORTFOLIO_SNAPSHOT = @"SELECT  
		fund_id, portfolio_id,fund_name, investment_value, today_value AS 'current_value', ROUND((today_value - investment_value)/investment_value, 2) * 100 AS 'returns'
        FROM `mutualfunds_db`.`view_fund_oneyear_range`;";

        public static readonly string GET_INVESTMENT_RETURNS_VALUE = @"SELECT *  
        FROM ( SELECT 
		SUM(ROUND(NAV.nav * PF.units,0)) AS investment_value
        FROM 
		mutualfunds_db.portfolio PF
        INNER JOIN amc_funds FUNDS ON PF.amc_fund_id = FUNDS.id
        INNER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id  AND NAV.date = PF.purchase_date
        ORDER BY PF.purchase_date, PF.amc_fund_id) AS A
        JOIN ( SELECT 
		SUM(ROUND(NAV.nav * PF.units,0)) AS portfolio_value
        FROM 
		mutualfunds_db.portfolio PF
        INNER JOIN amc_funds FUNDS ON PF.amc_fund_id = FUNDS.id
        INNER JOIN amc_fund_nav NAV ON PF.amc_fund_id = NAV.amc_fund_id  AND NAV.date = CURRENT_DATE
        ORDER BY PF.purchase_date, PF.amc_fund_id) as B;";

        public static readonly string DELETE_FUND = "delete from mutualfunds_db.portfolio where id = @portfolioId AND amc_fund_id = @fundId;";

        public static readonly string ADD_FUND = @"INSERT INTO mutualfunds_db.portfolio(`amc_fund_id`,`units`,`purchase_type`,`purchase_date`) VALUES(@amcfundid,@units,@purchasetype,@purchasedate);";

        #endregion
    }
}

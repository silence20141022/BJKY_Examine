using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Examining.Model;

namespace Aim.Examining.Web
{
    public partial class YearResultByYearAndScore : ExamListPage
    {
        string scoreZone = "";
        string year = "";
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            scoreZone = RequestData.Get<string>("scoreZone");
            year = RequestData.Get<string>("year");
            switch (RequestActionString)
            {
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string where = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            switch (scoreZone)
            {
                case "95->100":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=95" + where;
                    break;
                case "90->94":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=90 and IntegrationScore < 95" + where;
                    break;
                case "85->89":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=85 and IntegrationScore < 90" + where;
                    break;
                case "80->84":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=80 and IntegrationScore < 85" + where;
                    break;
                case "75->79":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=75 and IntegrationScore < 80" + where;
                    break;
                case "70->74":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=70 and IntegrationScore < 75" + where;
                    break;
                case "0->69":
                    sql = @"select * from BJKY_Examine..ExamYearResult where Year='" + year + "' and IntegrationScore>=0 and IntegrationScore < 70" + where;
                    break;
            }
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "IntegrationScore";
            string asc = search.Orders.Count <= 0 || !search.Orders[0].Ascending ? " desc" : " asc";
            string pageSql = @"
		    WITH OrderedOrders AS
		    (SELECT *,
		    ROW_NUMBER() OVER (order by {0} {1})as RowNumber
		    FROM ({2}) temp ) 
		    SELECT * 
		    FROM OrderedOrders 
		    WHERE RowNumber between {3} and {4}";
            pageSql = string.Format(pageSql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pageSql);
            return dicts;
        }
    }
}


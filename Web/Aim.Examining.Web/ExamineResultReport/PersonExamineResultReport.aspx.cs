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
    public partial class PersonExamineResultReport : ExamListPage
    {
        string resultType = "year";
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (RequestActionString)
            {
                case "GetResultByUserId":
                    string userId = RequestData.Get<string>("userId");
                    IList<ExamYearResult> eyrEnts = ExamYearResult.FindAllByProperty("Year", "UserId", userId);
                    IList<EasyDictionary> dics = new List<EasyDictionary>();
                    foreach (ExamYearResult eyrEnt in eyrEnts)
                    {
                        EasyDictionary dic = new EasyDictionary();
                        dic.Add("YearQuarter", eyrEnt.Year + "-1");
                        dic.Add("Score", eyrEnt.FirstQuarterScore);
                        dics.Add(dic);
                        dic = new EasyDictionary();
                        dic.Add("YearQuarter", eyrEnt.Year + "-2");
                        dic.Add("Score", eyrEnt.SecondQuarterScore);
                        dics.Add(dic);
                        dic = new EasyDictionary();
                        dic.Add("YearQuarter", eyrEnt.Year + "-3");
                        dic.Add("Score", eyrEnt.ThirdQuarterScore);
                        dics.Add(dic);
                        dic = new EasyDictionary();
                        dic.Add("YearQuarter", eyrEnt.Year + "-4");
                        dic.Add("Score", eyrEnt.FourthQuarterScore);
                        dics.Add(dic);
                    }
                    PageState.Add("Result", dics);
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string where = "";
            int times = 0;
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        case "StartYear":
                            where += " and A.Year>=" + item.Value + " ";
                            break;
                        case "EndYear":
                            where += " and A.Year<=" + item.Value + " ";
                            break;
                        case "Times":
                            times = Convert.ToInt32(item.Value);
                            break;
                        case "IntegrationScore":
                            where += " and A." + item.PropertyName + " >=" + item.Value + " ";
                            break;
                        default:
                            where += " and A." + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            if (times > 0)
            {
                sql = @"select A.* from BJKY_Examine..ExamYearResult as A where 1=1" + where + " and (Select count(B.Id) from BJKY_Examine..ExamYearResult as B where UserId=A.UserId " + where.Replace("A.", "B.") + ")>=" + times;
            }
            else
            {
                sql = @"select * from BJKY_Examine..ExamYearResult as A where 1=1" + where;
            }
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("EnumYear", SysEnumeration.GetEnumDict("Year"));
            PageState.Add("EnumLevel", SysEnumeration.GetEnumDict("ExamineLevel"));
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "DeptName";
            string asc = search.Orders.Count <= 0 || !search.Orders[0].Ascending ? " asc" : " desc";
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


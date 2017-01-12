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

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class FeedbackList : ExamListPage
    {
        private IList<PersonConfig> pcEnts = null;
        string sql = "";
        string Index = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Feedback ent = null;
            Index = RequestData.Get<string>("Index");
            switch (RequestActionString)
            {
                case "delete":
                    ent = this.GetTargetData<Feedback>();
                    ent.DoDelete();
                    break;
                default:
                    DoSelect();
                    break;
            }

        }
        private void DoSelect()
        {
            string where = "";
            Index = RequestData.Get<String>("Index");
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        case "StartTime":
                            where += " and StartTime>'" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            if (Index == "0")
            {
                sql = @"select * from BJKY_Examine..Feedback  where 
                PatIndex('%" + UserInfo.UserID + "%',DirectLeaderIds)>0 and State=1" + where;
            }
            else
            {
                sql = @"select * from BJKY_Examine..Feedback  where 
                PatIndex('%" + UserInfo.UserID + "%',DirectLeaderIds)>0 and State>=2" + where;
            }
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "FeedbackTime";
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


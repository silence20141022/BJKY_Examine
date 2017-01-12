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
using Aim.Examining.Web;
using Aim.Examining.Model;

namespace Aim.Examining.Web
{
    public partial class IndicatorScoreByTask : ExamListPage
    {
        private string TaskId = string.Empty;
        private string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TaskId = RequestData.Get<string>("TaskId");
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
                        //case "StartTime":
                        //    where += " and StartTime>'" + item.Value + "' ";
                        //    break;
                        //case "EndTime":
                        //    where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                        //    break;
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            sql = @"select *,(select count(Id) from BJKY_Examine..CustomIndicator where IndicatorSecondId=IndicatorScore.IndicatorSecondId
            and Year='{0}' and StageType='{1}' and CreateId='{2}') as SubQuan
            from BJKY_Examine..IndicatorScore where ExamineTaskId='{3}' " + where;
            ExamineTask etEnt = ExamineTask.Find(TaskId);
            ExamineStage esEnt = ExamineStage.Find(etEnt.ExamineStageId);
            sql = string.Format(sql, esEnt.Year, esEnt.StageType, etEnt.BeUserId, TaskId);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("TaskInfo", etEnt);
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "FirstSortIndex ,SecondSortIndex";
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


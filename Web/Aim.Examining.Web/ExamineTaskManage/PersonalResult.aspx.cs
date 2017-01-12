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
    public partial class PersonalResult : ExamListPage
    {
        string Index = "";
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Index = RequestData.Get<string>("Index");
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
                        case "StartTime":
                            where += " and StartTime>'" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        case "StageName":
                            where += " and B." + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                        default:
                            where += " and A." + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            if (Index == "0")//年度结果
            {
                sql = @"select A.*,   B.StageName,B.ExamineType, datediff(dd,A.ApproveTime,getdate()) as Days, 
                C.Id as AppealId,C.State as AppealState,C.Result as AppealResult,D.Id as FeedbackId,D.State as FeedbackState,D.Result as FeedbackResult
                from BJKY_Examine..ExamYearResult as A                  
                left join BJKY_Examine..ExamineStage as B on A.ExamineStageId=B.Id              
                left join BJKY_Examine..ExamineAppeal as C on A.Id=C.ExamYearResultId
                left join BJKY_Examine..Feedback as D on A.Id=D.ExamYearResultId
                where A.UserId='" + UserInfo.UserID + "' and B.State=5  " + where;//已评定等级才能申诉
            }
            else//个人季度考核结果
            {
                sql = @"select A.*, B.StageName,B.ExamineType  from BJKY_Examine..ExamineStageResult as A  
                left join BJKY_Examine..ExamineStage as B on A.ExamineStageId=B.Id 
                where A.UserId='" + UserInfo.UserID + "'  and B.State>=3" + where;
            }
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("SysConfig", SysConfig.FindAll().First<SysConfig>());
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "CreateTime";
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


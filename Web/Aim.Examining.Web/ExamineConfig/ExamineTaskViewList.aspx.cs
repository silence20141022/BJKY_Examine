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

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineTaskViewList : ExamListPage
    {
        private string ExamineStageId = string.Empty;
        private string ToUserId = "";
        ExamineStage esEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            ToUserId = RequestData.Get<string>("ToUserId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            switch (RequestActionString)
            {
                case "delete":
                    IList<string> taskIds = RequestData.GetList<string>("taskIds");
                    foreach (string taskId in taskIds)
                    {
                        ExamineTask etEnt = ExamineTask.Find(taskId);
                        etEnt.DoDelete();
                    }
                    esEnt.TaskQuan = esEnt.TaskQuan - taskIds.Count;
                    esEnt.DoUpdate();
                    break;
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
                            where += " and StartTime>='" + item.Value + "' ";
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
            string sql = @"select * from BJKY_Examine..ExamineTask where ExamineStageId='" + ExamineStageId + "' and ToUserId='" + ToUserId + "'" + where + " order by ToDeptName,BeDeptName,BeUserName asc";
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
            var Obj = new
            {
                ExamineStageName = ExamineStage.Find(ExamineStageId).StageName,
                ToUserName = SysUser.Find(ToUserId).Name
            };
            PageState.Add("Obj", Obj);
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


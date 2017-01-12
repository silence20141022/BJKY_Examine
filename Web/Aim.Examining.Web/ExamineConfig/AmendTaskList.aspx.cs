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
    public partial class AmendTaskList : ExamListPage
    {
        private IList<ExamineTask> etEnts = null;
        private IList<TempTask> ttEnts = null;
        private string ExamineStageId = string.Empty;
        string state = "";
        ExamineStage esEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId); //如果考核阶段的状态是已生成  任务状态是0  如果是已启动  任务状态为1
                state = esEnt.State == 1 ? "0" : "1";
            }
            switch (RequestActionString)
            {
                case "AmendTask":
                    //需要添加的任务
                    ttEnts = TempTask.FindAllByProperties(TempTask.Prop_ExamineStageId, ExamineStageId, TempTask.Prop_AmendState, "+");
                    int addQuan = ttEnts.Count;
                    foreach (TempTask ttEnt in ttEnts)//特殊任务也连同判断了
                    {
                        ExamineTask etEnt = new ExamineTask("", ttEnt.ExamineStageId, ttEnt.ToUserId, ttEnt.ToUserName, ttEnt.ToDeptId, ttEnt.ToDeptName,
                            ttEnt.ToRoleCode, ttEnt.ToRoleName, ttEnt.BeUserId, ttEnt.BeUserName, ttEnt.BeDeptId, ttEnt.BeDeptName, ttEnt.BeRoleCode,
                            ttEnt.BeRoleName, null, state, ttEnt.Tag, null, UserInfo.UserID, UserInfo.Name, System.DateTime.Now,
                            ttEnt.ExamineIndicatorId, ttEnt.ExamineRelationId);
                        etEnt.DoCreate();
                        ttEnt.DoDelete();
                    }
                    etEnts = ExamineTask.FindAllByProperties(ExamineTask.Prop_ExamineStageId, ExamineStageId, ExamineTask.Prop_AmendState, "-");
                    int reduceQuan = etEnts.Count;
                    foreach (ExamineTask etEnt in etEnts)
                    {
                        etEnt.DoDelete();
                    }
                    esEnt.TaskQuan = esEnt.TaskQuan + addQuan - reduceQuan;
                    esEnt.DoUpdate();
                    PageState.Add("Result", "增补任务数量：【" + addQuan.ToString() + "】  删除任务数量：【" + reduceQuan.ToString() + "】!");
                    break;
                case "CancelAmendTask":
                    ttEnts = TempTask.FindAllByProperties(TempTask.Prop_ExamineStageId, ExamineStageId, TempTask.Prop_AmendState, "+");
                    foreach (TempTask ttEnt in ttEnts)//特殊任务也连同判断了
                    {
                        ttEnt.DoDelete();
                    }
                    etEnts = ExamineTask.FindAllByProperties(ExamineTask.Prop_ExamineStageId, ExamineStageId, ExamineTask.Prop_AmendState, "-");
                    foreach (ExamineTask etEnt in etEnts)
                    {
                        etEnt.AmendState = null;
                        etEnt.DoUpdate();
                    }
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
            string sql = @"select * from BJKY_Examine..TempTask where AmendState='+' and ExamineStageId='{0}'" + where + "  union select * from BJKY_Examine..ExamineTask where AmendState='-' and ExamineStageId='{1}'" + where + "";
            sql = string.Format(sql, ExamineStageId, ExamineStageId);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("StageName", esEnt.StageName);
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


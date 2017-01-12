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
using System.Data;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineTaskGroup : ExamListPage
    {
        string Index = "";
        string sql = "";
        string ExamineStageId = "";
        string BeRoleCode = "";
        string ToRoleCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Index = RequestData.Get<string>("Index");
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            BeRoleCode = RequestData.Get<string>("BeRoleCode");
            ToRoleCode = RequestData.Get<string>("ToRoleCode");
            switch (RequestActionString)
            {
                case "TakeBack":
                    IList<ExamineTask> etEnts = ExamineTask.FindAllByProperties("ExamineStageId", ExamineStageId, "ToUserId", UserInfo.UserID, "BeRoleCode", BeRoleCode, "ToRoleCode", ToRoleCode);
                    foreach (ExamineTask etEnt in etEnts)
                    {
                        etEnt.State = "1";
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
            switch (Index)
            {
                case "0"://考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2  考核阶段结束后考核任务状态变为3  如果没有提交的变为4  已作废
//                    sql = @"select  A.ExamineStageId, A.ExamineRelationId, A.BeRoleCode,A.BeRoleName,A.ToRoleCode,A.ToRoleName,count(*) as TaskQuan,
//                       B.ExamineType,B.StageType,B.StageName, B.CreateTime,C.BeUserNames from BJKY_Examine..ExamineTask as A
//                       left join BJKY_Examine..ExamineStage as B on B.Id=A.ExamineStageId
//                       left join BJKY_Examine..DeptExamineRelation as C on C.Id=A.ExamineRelationId
//                       where A.ToUserId='{0}' and A.State='1' 
//                       group by A.ExamineStageId, A.BeRoleCode,A.BeRoleName,A.ToRoleCode,A.ToRoleName,A.ExamineRelationId,B.ExamineType,B.StageType,B.StageName, B.CreateTime,C.BeUserNames";
                    sql = @"select  A.ExamineStageId, A.ExamineRelationId, A.TaskQuan,
                       B.ExamineType,B.StageType,B.StageName, B.CreateTime,C.BeUserNames,D.BeRoleName,D.BeRoleCode from 
                       (select ExamineStageId,ExamineRelationId,count(*) TaskQuan from  BJKY_Examine..ExamineTask where State='1' and ToUserId='{0}'group by ExamineStageId,ExamineRelationId) A
                       left join BJKY_Examine..ExamineStage as B on B.Id=A.ExamineStageId
                       left join BJKY_Examine..DeptExamineRelation as C on C.Id=A.ExamineRelationId
                       left join BJKY_Examine..ExamineRelation as D on D.Id=A.ExamineRelationId";                                                                                                                                                                                            break;
                case "1":
                    sql = @"select  A.ExamineStageId, A.ExamineRelationId, A.TaskQuan,
                       B.ExamineType,B.StageType,B.StageName, B.CreateTime,C.BeUserNames,D.BeRoleName,D.BeRoleCode  from 
                       (select ExamineStageId,ExamineRelationId,count(*) TaskQuan from  BJKY_Examine..ExamineTask where State='2' and ToUserId='{0}'group by ExamineStageId,ExamineRelationId) A
                       left join BJKY_Examine..ExamineStage as B on B.Id=A.ExamineStageId
                       left join BJKY_Examine..DeptExamineRelation as C on C.Id=A.ExamineRelationId
                       left join BJKY_Examine..ExamineRelation as D on D.Id=A.ExamineRelationId";         
                    break;
                default:
                    sql = @"select  A.ExamineStageId, A.ExamineRelationId, A.TaskQuan,
                       B.ExamineType,B.StageType,B.StageName, B.CreateTime,C.BeUserNames,D.BeRoleName,D.BeRoleCode  from 
                       (select ExamineStageId,ExamineRelationId,count(*) TaskQuan from  BJKY_Examine..ExamineTask where State='3' and ToUserId='{0}'group by ExamineStageId,ExamineRelationId) A
                       left join BJKY_Examine..ExamineStage as B on B.Id=A.ExamineStageId
                       left join BJKY_Examine..DeptExamineRelation as C on C.Id=A.ExamineRelationId
                       left join BJKY_Examine..ExamineRelation as D on D.Id=A.ExamineRelationId";         
                    break;
            }
            sql = string.Format(sql, UserInfo.UserID);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
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


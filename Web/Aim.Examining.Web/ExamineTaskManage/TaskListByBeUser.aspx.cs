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
    public partial class TaskListByBeUser : ExamListPage
    {
        private string ExamineStageResultId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageResultId = RequestData.Get<string>("ExamineStageResultId");
            switch (RequestActionString)
            {
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            ExamineStageResult esrEnt = ExamineStageResult.Find(ExamineStageResultId);
            string where = "";
            if (SearchCriterion.Orders.Count == 0)
            {
                SearchCriterion.SetOrder("ToUserName", true);
                where = " ExamineStageId='{0}' and State='3' and BeUserId='{1}'";
            }
            else
            {
                where = " ExamineStageId='{0}' and State='3' and BeUserId='{1}'";
            }
            where = string.Format(where, esrEnt.ExamineStageId, esrEnt.UserId);
            IList<ExamineTask> etEnts = ExamineTask.FindAll(SearchCriterion, Expression.Sql(where));
            PageState.Add("DataList", etEnts);
            if (!string.IsNullOrEmpty(esrEnt.ExamineStageId))//有些考核结果是手动填报的 因此需要加判断  有无考核阶段和明细
            {
                ExamineStage esEnt = ExamineStage.Find(esrEnt.ExamineStageId);
                PageState.Add("ExamineStage", esEnt);
            }
            PageState.Add("BeUserName", esrEnt.UserName);
        }
    }
}


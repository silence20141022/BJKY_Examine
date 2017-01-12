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
    public partial class QuarterResult : ExamListPage
    {
        private string ExamineStageId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            switch (RequestActionString)
            {
                default:
                    DoSelect();
                    break;
            }

        }
        private void DoSelect()
        {
            SearchCriterion.AddSearch(ExamineTask.Prop_ExamineStageId, ExamineStageId);
            IList<ExamineStageResult> esrEnts = ExamineStageResult.FindAll(SearchCriterion);
            PageState.Add("DataList", esrEnts);
        }
    }
}


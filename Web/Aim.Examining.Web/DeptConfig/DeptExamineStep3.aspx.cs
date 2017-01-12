using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Web;
using Aim.Examining.Model;

namespace Aim.Examining.Web
{
    public partial class DeptExamineStep3 : ExamBasePage
    {
        string id = String.Empty;   // 对象id
        ExamineStage ent = null;
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            switch (RequestActionString)
            {
                case "update":
                    string did = RequestData.Get<string>("did");
                    string ExamineIndicatorId = RequestData.Get<string>("ExamineIndicatorId");
                    ExamineStageDetail esdEnt = ExamineStageDetail.Find(did);
                    esdEnt.ExamineIndicatorId = ExamineIndicatorId;
                    esdEnt.DoUpdate();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            sql = @"select A.*,B.BeUserNames,B.UpLevelUserNames,B.SameLevelUserNames,B.DownLevelUserNames,B.RelationName,
                C.IndicatorName from BJKY_Examine..ExamineStageDetail as A 
                left join BJKY_Examine..DeptExamineRelation as B on A.ExamineRelationId=B.Id
                left join BJKY_Examine..ExamineIndicator as C on C.Id=A.ExamineIndicatorId
                where A.ExamineStageId='" + id + "' order by RelationName asc";
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
            ent = ExamineStage.Find(id);
            SetFormData(ent);
        }
        private void SaveExamineStageDetail(ExamineStage esEnt)
        {
            IList<string> entStrList = RequestData.GetList<string>("data");
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, id);
            foreach (ExamineStageDetail esdEnt in esdEnts)
            {
                esdEnt.DoDelete();
            }
            if (entStrList != null && entStrList.Count > 0)
            {
                esdEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamineStageDetail>(tent) as ExamineStageDetail).ToList();
                foreach (ExamineStageDetail esdEnt in esdEnts)
                {
                    esdEnt.ExamineStageId = esEnt.Id;
                    esdEnt.DoCreate();
                }
            }
        }
    }
}


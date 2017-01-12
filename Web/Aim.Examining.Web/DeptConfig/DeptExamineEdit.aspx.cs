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


namespace Aim.Examining.Web.DeptConfig
{
    public partial class DeptExamineEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        ExamineStage ent = null;
        string ExamineType = "";
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            ExamineType = Server.HtmlDecode(RequestData.Get<string>("ExamineType"));
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<ExamineStage>(); 
                    ent.DoUpdate();
                    SaveExamineStageDetail(ent);
                    break;
                case "create":
                    ent = GetPostedData<ExamineStage>();
                    ent.State = 0; 
                    ent.DoCreate();
                    SaveExamineStageDetail(ent);
                    break;
                case "delete":
                    ent = GetTargetData<ExamineStage>();
                    ent.DoDelete();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = ExamineStage.Find(id);
                }
                SetFormData(ent);
                sql = @"select A.*,B.BeUserNames,B.UpLevelUserNames,B.SameLevelUserNames,B.DownLevelUserNames,B.RelationName,
                C.IndicatorName from BJKY_Examine..ExamineStageDetail as A 
                left join BJKY_Examine..DeptExamineRelation as B on A.ExamineRelationId=B.Id
                left join BJKY_Examine..ExamineIndicator as C on C.Id=A.ExamineIndicatorId
                where A.ExamineStageId='" + id + "'";
                PageState.Add("DataList", DataHelper.QueryDictList(sql));
            }
            if (op == "c")
            {
                var obj = new
                     {
                         ExamineType = ExamineType,
                         LaunchUserName = UserInfo.Name,
                         LaunchUserId = UserInfo.UserID
                     };
                SetFormData(obj);
            }
            sql = @"select Id,GroupName  from BJKY_Examine..PersonConfig 
                      where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位')";
            sql = string.Format(sql, UserInfo.UserID);
            EasyDictionary dic1 = DataHelper.QueryDict(sql, "Id", "GroupName");
            PageState.Add("enumDept", dic1);
            PageState.Add("EnumYear", SysEnumeration.GetEnumDict("Year"));
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


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
    public partial class DeptExamineStep1 : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        ExamineStage ent = null;
        string sql = "";
        string JsonString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            JsonString = RequestData.Get<string>("JsonString");
            switch (RequestActionString)
            {
                case "update":
                    ent = JsonHelper.GetObject<ExamineStage>(JsonString);
                    ent.DoUpdate();
                    PageState.Add("Id", ent.Id);
                    break;
                case "create":
                    ent = JsonHelper.GetObject<ExamineStage>(JsonString);
                    ent.CreateId = UserInfo.UserID;
                    ent.CreateName = UserInfo.Name;
                    ent.CreateTime = DateTime.Now;
                    ent.State = 0;
                    ent.DoCreate();
                    PageState.Add("Id", ent.Id);
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            if (string.IsNullOrEmpty(id))
            {
                var obj = new
                     {
                         LaunchUserName = UserInfo.Name,
                         LaunchUserId = UserInfo.UserID
                     };
                SetFormData(obj);
            }
            else
            {
                ent = ExamineStage.Find(id);
                SetFormData(ent);
            }
            sql = @"select Id,GroupName from BJKY_Examine..PersonConfig 
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


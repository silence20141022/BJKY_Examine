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
    public partial class LevelApprove : ExamListPage
    {
        ExamineStage esEnt = null;
        string ExamineStageId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            IList<String> entStrList = RequestData.GetList<string>("data");
            switch (RequestActionString)
            {
                case "Submit":
                    if (entStrList.Count > 0)
                    {
                        IList<ExamYearResult> eyrEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamYearResult>(tent) as ExamYearResult).ToList();
                        foreach (ExamYearResult eyrEnt in eyrEnts)
                        {
                            eyrEnt.ApproveUserId = UserInfo.UserID;
                            eyrEnt.ApproveUserName = UserInfo.Name;
                            eyrEnt.ApproveTime = System.DateTime.Now;
                            eyrEnt.DoUpdate();
                        }
                    }
                    esEnt.State = 5;
                    esEnt.DoUpdate();
                    break;
                case "TempSave":
                    if (entStrList.Count > 0)
                    {
                        IList<ExamYearResult> eyrEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamYearResult>(tent) as ExamYearResult).ToList();
                        foreach (ExamYearResult eyrEnt in eyrEnts)
                        {
                            eyrEnt.DoUpdate();
                        }
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string sql = @"select *,(select top 1 Name from SysEnumeration where Code=ExamYearResult.BeRoleCode ) as BeRoleName 
            from BJKY_Examine..ExamYearResult where ExamineStageId='" + ExamineStageId + "' order by BeRoleCode desc";
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
            var obj = new
            {
                ExamineStageName = esEnt.StageName,
                State = esEnt.State,
                Year = esEnt.Year
            };
            PageState.Add("Obj", obj);
        }
    }
}


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
    public partial class ExamineStep2 : ExamBasePage
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
                    IList<string> entStrList = RequestData.GetList<string>("data");
                    IList<ExamineStageDeptDetail> esddEnts = ExamineStageDeptDetail.FindAllByProperty(ExamineStageDeptDetail.Prop_ExamineStageId, id);
                    foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                    {
                        esddEnt.DoDelete();
                    }
                    esddEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamineStageDeptDetail>(tent) as ExamineStageDeptDetail).ToList();
                    foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                    {
                        esddEnt.ExamineStageId = id;
                        esddEnt.DoCreate();
                    }
                    break; 
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            sql = @"select * from BJKY_Examine..ExamineStageDeptDetail where  ExamineStageId='" + id + "' order by GroupType desc,GroupName asc";
            //按时间倒序是为了和部门配置以及创建考核的时候部门显示的顺序一致
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
        } 
        private void SaveDeptDetail(ExamineStage esEnt)
        {
            IList<string> entStrList = RequestData.GetList<string>("data");
            IList<ExamineStageDeptDetail> esddEnts = ExamineStageDeptDetail.FindAllByProperty(ExamineStageDeptDetail.Prop_ExamineStageId, id);
            foreach (ExamineStageDeptDetail esddEnt in esddEnts)
            {
                esddEnt.DoDelete();
            }
            if (entStrList != null && entStrList.Count > 0)
            {
                esddEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamineStageDeptDetail>(tent) as ExamineStageDeptDetail).ToList();
                foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                {
                    esddEnt.ExamineStageId = esEnt.Id;
                    esddEnt.DoCreate();
                }
            }
        }
    }
}


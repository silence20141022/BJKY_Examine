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
using System.Data;

namespace Aim.Examining.Web
{
    public partial class DeptExamineStep6 : ExamBasePage
    {
        string id = String.Empty;   // 对象id
        ExamineStage esEnt = null;
        string sql = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            switch (RequestActionString)
            {
                case "launch":                    
                    if (!string.IsNullOrEmpty(id))
                    {
                        esEnt = ExamineStage.Find(id);                      
                        if (esEnt.State == 1)
                        {
                            esEnt.State = 2; 
                            esEnt.DoUpdate();
                            sql = "update BJKY_Examine..ExamineTask set State='1' where ExamineStageId='" + esEnt.Id + "'";
                            DataHelper.ExecSql(sql);
                            PageState.Add("Id", esEnt.Id);
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
            if (!String.IsNullOrEmpty(id))
            {
                esEnt = ExamineStage.Find(id);
            }
            SetFormData(esEnt);
            sql = @"select A.*,B.BeUserNames,B.UpLevelUserNames,B.SameLevelUserNames,B.DownLevelUserNames,B.RelationName,
                C.IndicatorName from BJKY_Examine..ExamineStageDetail as A 
                left join BJKY_Examine..DeptExamineRelation as B on A.ExamineRelationId=B.Id
                left join BJKY_Examine..ExamineIndicator as C on C.Id=A.ExamineIndicatorId
                where A.ExamineStageId='" + id + "'";
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
            sql = @"select Id,GroupName  from BJKY_Examine..PersonConfig 
                      where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位')";
            sql = string.Format(sql, UserInfo.UserID);
            EasyDictionary dic1 = DataHelper.QueryDict(sql, "Id", "GroupName");
            PageState.Add("enumDept", dic1);
            PageState.Add("EnumYear", SysEnumeration.GetEnumDict("Year"));
        }
    }
}


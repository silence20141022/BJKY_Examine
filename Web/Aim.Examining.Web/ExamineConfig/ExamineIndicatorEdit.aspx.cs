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
using Aim.Examining.Model;
using System.Data;

namespace Aim.Examining.Web
{
    public partial class ExamineIndicatorEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string sql = ""; // 对象类型 
        ExamineIndicator ent = null;
        IList<SysEnumeration> seEnts = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<ExamineIndicator>();
                    seEnts = SysEnumeration.FindAllByProperty(SysEnumeration.Prop_Value, ent.BeRoleCode);
                    if (seEnts.Count > 0)
                    {
                        ent.BeRoleName = seEnts[0].Name;
                    }
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<ExamineIndicator>();
                    seEnts = SysEnumeration.FindAllByProperty(SysEnumeration.Prop_Value, ent.BeRoleCode);
                    if (seEnts.Count > 0)
                    {
                        ent.BeRoleName = seEnts[0].Name;
                    }
                    ent.DoCreate();
                    break;
                case "copy":
                    ent = GetPostedData<ExamineIndicator>();
                    ExamineIndicator eiNEnt = new ExamineIndicator(null, ent.IndicatorName, ent.BeRoleCode, ent.BeRoleName, ent.BelongDeptId, ent.BelongDeptName, ent.Remark, null, null, null);
                    eiNEnt.DoCreate();
                    DoCopy(eiNEnt, id);
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            sql = @"select  Id,GroupName  from BJKY_Examine..PersonConfig where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' 
            or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位')"; 
            sql = string.Format(sql, UserInfo.UserID);
            EasyDictionary dic1 = DataHelper.QueryDict(sql, "Id", "GroupName");
            PageState.Add("enumDept", dic1);
            //sql = @"select Value,Name from SysEnumeration where Code='' ";
            //EasyDictionary dic2 = DataHelper.QueryDict(sql, "Value", "Name");
            PageState.Add("BeRoleEnum", SysEnumeration.GetEnumDict("BeExamineObject"));
            //            sql = @"select GroupID,Name  from SysGroup where PathLevel='4' and 
            //                PatIndex('%'+GroupID+'%',(select Path from SysGroup where GroupID=(select top 1 GroupID from SysUserGroup where UserID='" + UserInfo.UserID + "') ))>0";
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);//获取当前登录人部门
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = ExamineIndicator.Find(id);
                }
                SetFormData(ent);
            }
            else
            {
                var obj = new object();
                if (dics.Count > 0)
                {
                    obj = new
                    {
                        BelongDeptId = dics[0].Get<string>("Id"),
                        BelongDeptName = dics[0].Get<string>("GroupName")
                    };
                }
                SetFormData(obj);
            }  
        }
        private void DoCopy(ExamineIndicator eiNEnt, string oid)
        {
            IList<IndicatorFirst> ifOEnts = IndicatorFirst.FindAllByProperty(IndicatorFirst.Prop_SortIndex, IndicatorFirst.Prop_ExamineIndicatorId, oid);
            foreach (IndicatorFirst ifOEnt in ifOEnts)
            {
                IndicatorFirst ifNEnt = new IndicatorFirst(null, eiNEnt.Id, ifOEnt.IndicatorFirstName, "F", "F", ifOEnt.MaxScore, ifOEnt.SortIndex);
                ifNEnt.DoCreate();
                IList<IndicatorSecond> isOEnts = IndicatorSecond.FindAllByProperty(IndicatorSecond.Prop_SortIndex, IndicatorSecond.Prop_IndicatorFirstId, ifOEnt.Id);
                foreach (IndicatorSecond isOEnt in isOEnts)
                {
                    IndicatorSecond isNEnt = new IndicatorSecond(null, ifNEnt.Id, ifNEnt.IndicatorFirstName, isOEnt.IndicatorSecondName, isOEnt.MaxScore, isOEnt.SortIndex, isOEnt.ToolTip);
                    isNEnt.DoCreate();
                    IList<ScoreStandard> ssOEnts = ScoreStandard.FindAllByProperty(ScoreStandard.Prop_SortIndex, ScoreStandard.Prop_IndicatorSecondId, isOEnt.Id);
                    foreach (ScoreStandard ssOEnt in ssOEnts)
                    {
                        ScoreStandard ssNEnt = new ScoreStandard("", isNEnt.Id, ssOEnt.IndicatorThirdName, ssOEnt.MaxScore, ssOEnt.SortIndex);
                        ssNEnt.DoCreate();
                    }
                }
            }
        }
    }
}


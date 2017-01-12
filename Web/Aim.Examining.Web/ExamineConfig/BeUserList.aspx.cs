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

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class BeUserList : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id   
        string BeRoleCode = "";
        string ExamineStageId = ""; 
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            BeRoleCode = RequestData.Get<string>("BeRoleCode");
            ExamineStageId = RequestData.Get<string>("ExamineStageId"); 
            if (!string.IsNullOrEmpty(BeRoleCode))
            {
                string beUserIds = string.Empty;//存储被考核对象具体的人
                string beUserNames = string.Empty;
                string DeptNames = string.Empty;
                string DeptConstraint = "";
                IList<EasyDictionary> dics = null;
                IList<PersonConfig> pcEnts = null;
                if (!string.IsNullOrEmpty(ExamineStageId))//如果是新建 人员不加部门约束
                {
                    DeptConstraint = " and Id in (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + ExamineStageId + "')";
                }
                switch (BeRoleCode)
                {
                    case "BeDeputyDirector"://副院级领导
                        pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupCode, "DeputyDirector");
                        if (pcEnts.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(pcEnts[0].ClerkIds))
                            {
                                beUserIds = pcEnts[0].ClerkIds;
                                beUserNames = pcEnts[0].ClerkNames;
                                DeptNames = pcEnts[0].ClerkGroupNames;
                            }
                        }
                        break;
                    case "BeExecutiveDeptLeader"://职能服务部门正职   +分管工作的副职也被当做中层干部来考核  
                        sql = @"select (isnull(FirstLeaderIds,'')+','+isnull(ChargeSecondLeaderIds,'')) as beUserIds,
                        (isnull(FirstLeaderNames,'')+','+isnull(ChargeSecondLeaderNames,'')) as beUserNames ,
                        (isnull(FirstLeaderGroupNames,'')+','+isnull(ChargeSecondLeaderGroupNames,'')) as DeptNames
                        from BJKY_Examine..PersonConfig where GroupType='职能服务部门' " + DeptConstraint;
                        dics = DataHelper.QueryDictList(sql);
                        foreach (EasyDictionary dic in dics)
                        {
                            beUserIds += dic.Get<string>("beUserIds") + ",";
                            beUserNames += dic.Get<string>("beUserNames") + ",";
                            DeptNames += dic.Get<string>("DeptNames") + ",";
                        }
                        break;
                    case "BeBusinessDeptLeader"://经营目标单位正职  和负责工作的副职
                        sql = @"select (isnull(FirstLeaderIds,'')+','+isnull(ChargeSecondLeaderIds,'')) as beUserIds,
                        (isnull(FirstLeaderNames,'')+','+isnull(ChargeSecondLeaderNames,'')) as beUserNames,
                        (isnull(FirstLeaderGroupNames,'')+','+isnull(ChargeSecondLeaderGroupNames,'')) as DeptNames
                        from BJKY_Examine..PersonConfig where GroupType='经营目标单位' " + DeptConstraint;
                        dics = DataHelper.QueryDictList(sql);
                        foreach (EasyDictionary dic in dics)
                        {
                            beUserIds += dic.Get<string>("beUserIds") + ",";
                            beUserNames += dic.Get<string>("beUserNames") + ",";
                            DeptNames += dic.Get<string>("DeptNames") + ",";
                        }
                        break;
                    //case "BeDeptSecondLeader"://部门副职                        {
                    //    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_Id, LaunchDeptId);
                    //    if (pcEnts.Count > 0)
                    //    {
                    //        beUserIds = pcEnts[0].SecondLeaderIds;
                    //        beUserNames = pcEnts[0].SecondLeaderNames;
                    //    }
                    //    break;
                    //case "BeDeptClerk"://部门员工
                    //    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_Id, LaunchDeptId);
                    //    if (pcEnts.Count > 0)
                    //    {
                    //        beUserIds = pcEnts[0].ClerkIds;
                    //        beUserNames = pcEnts[0].ClerkNames;
                    //    }
                    //    break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(beUserIds))
                {
                    DataTable dt = new DataTable();
                    DataColumn dc = new DataColumn("Id");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("UserID");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("UserName");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("DeptName");
                    dt.Columns.Add(dc);
                    string[] userIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] userNameArray = beUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] deptNameArray = DeptNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < userIdArray.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = System.Guid.NewGuid();
                        dr["UserID"] = userIdArray[i];
                        dr["UserName"] = userNameArray[i];
                        dr["DeptName"] = deptNameArray[i];
                        dt.Rows.Add(dr);
                    }
                    PageState.Add("DataList", dt);
                }
            }
        }
    }
}


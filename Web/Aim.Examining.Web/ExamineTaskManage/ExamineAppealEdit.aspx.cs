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

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class ExamineAppealEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string ExamYearResultId = String.Empty;
        string id = string.Empty;// 对象id 
        ExamineAppeal ent = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            ExamYearResultId = RequestData.Get<string>("ExamYearResultId");
            string Action = RequestData.Get<string>("Action");
            int State = RequestData.Get<int>("State");
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<ExamineAppeal>();
                    if (Action == "Agree")
                    {
                        switch (ent.State)
                        {
                            case 1:
                                ent.AcceptSubmitTime = System.DateTime.Now;
                                if (ent.ExamineType == "院级考核")//如果是院级考核 提交后直接送达人力资源部负责人
                                {
                                    ent.State = 3;
                                }
                                else
                                {
                                    ent.State = 2;
                                }
                                break;
                            case 2:
                                ent.DeptLeaderSubmitTime = System.DateTime.Now;
                                ent.State = 3;
                                break;
                            case 3:
                                ent.HrSubmitTime = System.DateTime.Now;
                                ent.State = 4;//按正常流程走完了
                                ent.Result = "同意";
                                ExamYearResult eyrEnt = ExamYearResult.Find(ent.ExamYearResultId);
                                eyrEnt.AppealLevel = ent.ModifiedLevel;
                                eyrEnt.AppealScore = ent.ModifiedScore;
                                eyrEnt.DoUpdate();
                                break;
                            default:
                                ent.AppealTime = System.DateTime.Now;
                                ent.State = 1;
                                break;
                        }
                    }
                    if (Action == "Disagree")
                    {
                        ent.State = 4;//按正常流程走完了
                        ent.Result = "已打回";
                        ent.AcceptSubmitTime = System.DateTime.Now;
                    }
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<ExamineAppeal>();
                    if (Action == "Agree")//创建的时候提交
                    {
                        ent.State = 1;
                        ent.AppealTime = System.DateTime.Now;
                    }
                    ent.DoCreate();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            if (op == "c")
            {
                if (!String.IsNullOrEmpty(ExamYearResultId))
                {
                    ExamYearResult eyrEnt = ExamYearResult.Find(ExamYearResultId);
                    ExamineStage esEnt = ExamineStage.Find(eyrEnt.ExamineStageId);
                    IList<PersonConfig> pcEnts1 = PersonConfig.FindAllByProperty("GroupCode", "HRAppealAcceptor");
                    IList<PersonConfig> pcEnts2 = PersonConfig.FindAllByProperty("GroupCode", "HRAppealCharger");
                    IList<PersonConfig> pcEnts3 = PersonConfig.FindAllByProperty("Id", eyrEnt.DeptId);
                    var obj = new
                    {
                        ExamYearResultId = ExamYearResultId,
                        ExamineStageId = eyrEnt.ExamineStageId,
                        ExamineType = esEnt.ExamineType,
                        AppealUserId = eyrEnt.UserId,
                        AppealUserName = eyrEnt.UserName,
                        OriginalScore = eyrEnt.ApproveScore.HasValue ? eyrEnt.ApproveScore : eyrEnt.IntegrationScore,
                        OriginalLevel = String.IsNullOrEmpty(eyrEnt.ApproveLevel) ? eyrEnt.ApproveLevel : eyrEnt.AdviceLevel,
                        //有可能是部门级考核 没有角色编号和角色名称
                        BeRoleCode = !string.IsNullOrEmpty(eyrEnt.BeRoleCode) ? eyrEnt.BeRoleCode : "",
                        BeRoleName = !string.IsNullOrEmpty(eyrEnt.BeRoleCode) ? SysEnumeration.FindAllByProperty(SysEnumeration.Prop_Code, eyrEnt.BeRoleCode).First<SysEnumeration>().Name : "",
                        DeptId = eyrEnt.DeptId,
                        DeptName = eyrEnt.DeptName,
                        AppealTime = System.DateTime.Now.ToShortDateString(),
                        AcceptUserId = pcEnts1.Count > 0 ? pcEnts1[0].ClerkIds.Replace(",", "") : "",
                        AcceptUserName = pcEnts1.Count > 0 ? pcEnts1[0].ClerkNames : "",
                        DeptLeaderId = pcEnts3.Count > 0 ? pcEnts3[0].FirstLeaderIds.Replace(",", "") : "",
                        DeptLeaderName = pcEnts3.Count > 0 ? pcEnts3[0].FirstLeaderNames : "",
                        HrUserId = pcEnts2.Count > 0 ? pcEnts2[0].ClerkIds.Replace(",", "") : "",
                        HrUserName = pcEnts2.Count > 0 ? pcEnts2[0].ClerkNames : ""
                    };
                    SetFormData(obj);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ent = ExamineAppeal.Find(id);
                    SetFormData(ent);
                }
            }
            PageState.Add("ExamineLevel", SysEnumeration.GetEnumDict("ExamineLevel"));
        }
    }
}


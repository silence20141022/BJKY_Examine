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
using Aim.Examining.Web;

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class FeedbackEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string ExamYearResultId = string.Empty;
        Feedback ent = null;
        string Action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            ExamYearResultId = RequestData.Get<string>("ExamYearResultId");
            Action = RequestData.Get<string>("Action");
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<Feedback>();
                    if (Action == "Submit")
                    {
                        switch (ent.State)
                        {
                            case 1:
                                ent.State = 2;
                                ent.DirectLeaderSignDate = System.DateTime.Now;
                                ent.Result = "同意";
                                break;
                            default:
                                ent.State = 1;
                                ent.FeedbackTime = System.DateTime.Now;
                                break;
                        }
                    }
                    if (Action == "Disagree")
                    {
                        ent.Result = "不同意";//不同意后 状态初始化为空 让提交人还可以编辑。可以再次提交
                        ent.State = null;
                    }
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<Feedback>();
                    if (Action == "Submit")
                    {
                        ent.State = 1;
                        ent.FeedbackTime = System.DateTime.Now;
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
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = Feedback.Find(id);
                }
                SetFormData(ent);
            }
            else
            {
                ExamYearResult eyrEnt = ExamYearResult.Find(ExamYearResultId);
                string level = eyrEnt.AdviceLevel;
                decimal? score = eyrEnt.IntegrationScore;

                if (!string.IsNullOrEmpty(eyrEnt.ApproveLevel))
                {
                    level = eyrEnt.ApproveLevel;
                }
                if (!string.IsNullOrEmpty(eyrEnt.AppealLevel))
                {
                    level = eyrEnt.AppealLevel;
                }
                if (eyrEnt.ApproveScore > 0)
                {
                    score = eyrEnt.ApproveScore;
                }
                if (eyrEnt.AppealScore > 0)
                {
                    score = eyrEnt.AppealScore;
                }
                var obj = new
                {
                    ExamYearResultId = ExamYearResultId,
                    ExamineStageId = eyrEnt.ExamineStageId,
                    UserId = eyrEnt.UserId,
                    UserName = eyrEnt.UserName,
                    //因为部门级考核没有被考核角色 和名称  所以要判断一下
                    BeRoleCode = !string.IsNullOrEmpty(eyrEnt.BeRoleCode) ? eyrEnt.BeRoleCode : "",
                    BeRoleName = !string.IsNullOrEmpty(eyrEnt.BeRoleCode) ? SysEnumeration.FindAllByProperty(SysEnumeration.Prop_Code, eyrEnt.BeRoleCode).First<SysEnumeration>().Name : "",
                    DeptId = eyrEnt.DeptId,
                    DeptName = eyrEnt.DeptName,
                    Year = eyrEnt.Year,
                    ExamineGrade = level,
                    IntegrationScore = score,
                    FirstQuarterScore = eyrEnt.FirstQuarterScore,
                    SecondQuarterScore = eyrEnt.SecondQuarterScore,
                    ThirdQuarterScore = eyrEnt.ThirdQuarterScore,
                    YearScore = eyrEnt.FourthQuarterScore
                };
                SetFormData(obj);
            }
        }
    }
}


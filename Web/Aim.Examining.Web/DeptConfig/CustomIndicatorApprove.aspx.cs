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

namespace Aim.Examining.Web.DeptConfig
{
    public partial class CustomIndicatorApprove : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id            
        string sql = "";
        CustomIndicator ciEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                ciEnt = CustomIndicator.Find(id);
            }
            IList<string> entStrList = RequestData.GetList<string>("data");
            switch (RequestActionString)
            {
                case "agree":
                    ciEnt.Result = "同意";
                    ciEnt.ApproveTime = System.DateTime.Now; 
                    ciEnt.State = "2";
                    ciEnt.DoUpdate();
                    if (entStrList.Count > 0)
                    {
                        IList<CustomIndicatorOpinion> eiEnts = entStrList.Select(tent => JsonHelper.GetObject<CustomIndicatorOpinion>(tent) as CustomIndicatorOpinion).ToList();
                        eiEnts[0].DoCreate();
                    }
                    FormatOpinion();
                    break;
                case "disagree":
                    ciEnt.Result = "不同意";
                    ciEnt.ApproveTime = System.DateTime.Now; 
                    ciEnt.State = "0";//不同意后自定义指标的状态归0
                    ciEnt.DoUpdate();
                    if (entStrList.Count > 0)
                    {
                        IList<CustomIndicatorOpinion> eiEnts = entStrList.Select(tent => JsonHelper.GetObject<CustomIndicatorOpinion>(tent) as CustomIndicatorOpinion).ToList();
                        eiEnts[0].DoCreate();
                    }
                    FormatOpinion();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void FormatOpinion()
        {
            IList<CustomIndicatorOpinion> cioEnts = CustomIndicatorOpinion.FindAllByProperty("CreateTime", CustomIndicatorOpinion.Prop_CustomIndicatorId, ciEnt.Id);
            string temp = "<table style='font-size:12px'><tr style='font-weight:bold'><td width='12px'></td><td width='160px'>审批意见</td><td width='50px'>审批人</td><td width='70px'>审批时间</td></tr>";
            for (int i = 0; i < cioEnts.Count; i++)
            {
                temp += "<tr><td>" + (i + 1).ToString() + "</td><td>" + cioEnts[i].Opinion + "</td><td>" + cioEnts[i].CreateName + "</td><td>" + cioEnts[0].CreateTime.Value.ToShortDateString() + "</td></tr>";
            }
            ciEnt.Opinion = temp + "</table>";
            ciEnt.DoUpdate();
        }
        private void DoSelect()
        {
            sql = @"select A.Id, A.PersonSecondIndicatorName,A.Weight, A.ToolTip,A.SelfRemark,A.PersonFirstIndicatorId,
                  B.PersonFirstIndicatorName, B.Weight as FirstWeight,B.IndicatorType
                  from BJKY_Examine..PersonSecondIndicator as A 
                  left join BJKY_Examine..PersonFirstIndicator as B on A.PersonFirstIndicatorId=B.Id 
                  where B.CustomIndicatorId='{0}' order by B.IndicatorType desc,B.SortIndex asc";
            sql = string.Format(sql, id);
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
            SetFormData(ciEnt);
            PageState.Add("Entity", ciEnt);
            //加载审批意见表  因为有可能多次审批
            IList<CustomIndicatorOpinion> cioEnts = CustomIndicatorOpinion.FindAllByProperty(CustomIndicatorOpinion.Prop_CustomIndicatorId, id);
            PageState.Add("DataList1", cioEnts);
        }
    }
}


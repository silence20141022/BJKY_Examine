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

namespace Aim.Examining.Web
{
    public partial class IndicatorSecondEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id        
        IndicatorSecond ent = null;
        IList<ScoreStandard> ents = null;
        string IndicatorFirstId = "";
        string ExamineIndicatorId = "";
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            IndicatorFirstId = RequestData.Get<string>("IndicatorFirstId");
            ExamineIndicatorId = RequestData.Get<string>("ExamineIndicatorId");
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<IndicatorSecond>();
                    ent.DoUpdate();
                    SaveDetail(ent);
                    break;
                case "create":
                    ent = this.GetPostedData<IndicatorSecond>();
                    ent.DoCreate();
                    SaveDetail(ent);
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            sql = @"select Id as IndicatorFirstId ,IndicatorFirstName  from BJKY_Examine..IndicatorFirst 
                      where  ExamineIndicatorId = '" + ExamineIndicatorId + "' order by SortIndex asc ";
            EasyDictionary dic = DataHelper.QueryDict(sql, "IndicatorFirstId", "IndicatorFirstName");
            PageState.Add("IndictorFirstEnum", dic);//Combo数据集
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = IndicatorSecond.Find(id);
                }
                SetFormData(ent);
                SearchCriterion.AddSearch(ScoreStandard.Prop_IndicatorSecondId, id);
                SearchCriterion.SetOrder("SortIndex", true);
                ents = ScoreStandard.FindAll(SearchCriterion);
                PageState.Add("DataList", ents);
            }
            else
            {
                if (!string.IsNullOrEmpty(IndicatorFirstId))
                {
                    IndicatorFirst ifEnt = IndicatorFirst.Find(IndicatorFirstId);
                    sql = "select isnull(max(SortIndex),0) from BJKY_Examine..IndicatorSecond where IndicatorFirstId='" + IndicatorFirstId + "'";
                    var obj = new
                    {
                        SortIndex = DataHelper.QueryValue<int>(sql) + 1,
                        ExamineIndicatorId = ExamineIndicatorId
                    };
                    SetFormData(obj);
                }
            }
        }
        private void SaveDetail(IndicatorSecond isEnt)
        {
            IList<ScoreStandard> ssEnts = ScoreStandard.FindAllByProperty(ScoreStandard.Prop_IndicatorSecondId, id);
            foreach (ScoreStandard ssEnt in ssEnts)
            {
                ssEnt.DoDelete();
            }
            IList<string> entStrList = RequestData.GetList<string>("data");
            string temp = string.Empty;
            if (entStrList != null && entStrList.Count > 0)
            {
                IList<ScoreStandard> pfiEnts = entStrList.Select(tent => JsonHelper.GetObject<ScoreStandard>(tent) as ScoreStandard).ToList();

                foreach (ScoreStandard ifItem in pfiEnts)
                {
                    ifItem.IndicatorSecondId = isEnt.Id;
                    ifItem.IndicatorThirdName = ifItem.IndicatorThirdName.Replace("\"", "'");//去除双引号
                    temp += temp + ifItem.SortIndex.ToString() + "：" + ifItem.IndicatorThirdName + "@" + ifItem.MaxScore + "$";
                    ifItem.DoCreate();
                }
            }
            if (temp.Length > 0)
            {
                isEnt.ToolTip = temp.Substring(0, temp.Length - 1);
            }
            else
            {
                isEnt.ToolTip = temp;
            }

            isEnt.DoUpdate();
        }
    }
}


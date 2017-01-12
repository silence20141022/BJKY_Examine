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
using Aim.Examining.Web;
using Aim.Examining.Model;

namespace Aim.Examine.Web.ExamineConfig
{
    public partial class IndicatorFirstList : ExamListPage
    {
        private IList<IndicatorFirst> ents = null;
        private string ExamineIndicatorId = string.Empty;  //指标考核id
        private string IndicatorName = string.Empty;       //名称

        protected void Page_Load(object sender, EventArgs e)
        {

            ExamineIndicatorId = this.RequestData.Get<string>("ExamineIndicatorId");
            IndicatorName = this.RequestData.Get<string>("IndicatorName");

            switch (RequestActionString)
            {
                case "JudgeRepeat":
                    string indicatorName = RequestData.Get<string>("IndicatorFirstName");
                    if (!string.IsNullOrEmpty(indicatorName))
                    {
                        IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperty(IndicatorFirst.Prop_IndicatorFirstName, indicatorName);
                        if (ifEnts.Count > 0)
                        {
                            PageState.Add("Result", true);
                        }
                    }
                    break;
                case "Save":
                    IList<string> entStrList = RequestData.GetList<string>("data");
                    if (entStrList != null && entStrList.Count > 0)
                    {
                        IList<IndicatorFirst> pfiEnts = entStrList.Select(tent => JsonHelper.GetObject<IndicatorFirst>(tent) as IndicatorFirst).ToList();
                        foreach (IndicatorFirst ifItem in pfiEnts)
                        {
                            ifItem.DoSave();
                            PageState.Add("Id", ifItem.Id);
                        }
                    }
                    break;
                case "batchdelete":
                    DoBatchDelete();
                    break;
                default:

                    DoSelect();
                    break;
            }

        }
        private void DoSelect()
        {
            if (string.IsNullOrEmpty(ExamineIndicatorId)) return;
            SearchCriterion.SetSearch(IndicatorFirst.Prop_ExamineIndicatorId, ExamineIndicatorId);
            SearchCriterion.SetOrder("SortIndex", true);
            ents = IndicatorFirst.FindAll(SearchCriterion);
            PageState.Add("DataList", ents);
            PageState.Add("BeRoleName", SysEnumeration.GetEnumDict("BeExamineObject")); //被考核
        }
        [ActiveRecordTransaction]
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");
            if (idList != null && idList.Count > 0)
            {
                foreach (string item in idList)
                {
                    IList<IndicatorSecond> isEnts = IndicatorSecond.FindAllByProperty(IndicatorSecond.Prop_IndicatorFirstId, item);
                    foreach (IndicatorSecond isEnt in isEnts)
                    {
                        IList<ScoreStandard> ssEnts = ScoreStandard.FindAllByProperty(ScoreStandard.Prop_IndicatorSecondId, isEnt.Id);
                        foreach (ScoreStandard ssEnt in ssEnts)
                        {
                            ssEnt.DoDelete();
                        }
                        isEnt.DoDelete();
                    }
                }
                IndicatorFirst.DoBatchDelete(idList.ToArray());
            }
        }
    }
}


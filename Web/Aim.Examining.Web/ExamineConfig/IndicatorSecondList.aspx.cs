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
    public partial class IndicatorSecondList : ExamListPage
    {
        private IList<IndicatorSecond> ents = null;
        private string IndicatorFirstId = string.Empty;
        private string IndicatorFirstName = string.Empty;
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IndicatorFirstId = RequestData.Get<string>("IndicatorFirstId");
            IndicatorFirstName = RequestData.Get<string>("IndicatorFirstName");
            switch (RequestActionString)
            {
                case "JudgeRepeat":
                    string IndicatorSecondName = RequestData.Get<string>("IndicatorSecondName");
                    if (!string.IsNullOrEmpty(IndicatorSecondName))
                    {
                        IList<IndicatorSecond> pfiEnts = IndicatorSecond.FindAllByProperties(IndicatorSecond.Prop_IndicatorSecondName, IndicatorSecondName, IndicatorSecond.Prop_IndicatorFirstId, IndicatorFirstId);
                        if (pfiEnts.Count > 0)
                        {
                            PageState.Add("Result", true);
                        }
                    }
                    break;
                case "Save":
                    IList<string> entStrList = RequestData.GetList<string>("data");
                    if (entStrList != null && entStrList.Count > 0)
                    {
                        IList<IndicatorSecond> psiEnts = entStrList.Select(tent => JsonHelper.GetObject<IndicatorSecond>(tent) as IndicatorSecond).ToList();
                        foreach (IndicatorSecond isItem in psiEnts)
                        {
                            isItem.DoSave();
                            PageState.Add("Id", isItem.Id);
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
            if (!String.IsNullOrEmpty(IndicatorFirstId))
            {
                sql = @"select A.*,BJKY_Examine.dbo.fun_getScoreStandard(A.Id) as ScoreStandard 
                             from BJKY_Examine..IndicatorSecond as A  where A.IndicatorFirstId='" + IndicatorFirstId + "' order by A.SortIndex";
                PageState.Add("DataList", DataHelper.QueryDictList(sql));
            }
        }
        [ActiveRecordTransaction]
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");
            if (idList != null && idList.Count > 0)
            {
                foreach (string item in idList)
                {
                    IList<ScoreStandard> ssEnts = ScoreStandard.FindAllByProperty(ScoreStandard.Prop_IndicatorSecondId, item);
                    foreach (ScoreStandard ssEnt in ssEnts)
                    {
                        ssEnt.DoDelete();
                    }
                }
                IndicatorSecond.DoBatchDelete(idList.ToArray());
            }
        }
    }
}


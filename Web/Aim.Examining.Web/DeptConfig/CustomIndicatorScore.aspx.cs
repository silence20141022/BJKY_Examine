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
using Aim.Examining.Model;

namespace Aim.Examining.Web.DeptConfig
{
    public partial class CustomIndicatorScore : ExamListPage
    {
        string sql = "";
        string IndicatorSecondId = "";
        string ExamineTaskId = "";
        string PersonFirstIndicatorId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IndicatorSecondId = RequestData.Get<string>("IndicatorSecondId");
            ExamineTaskId = RequestData.Get<string>("ExamineTaskId");
            PersonFirstIndicatorId = RequestData.Get<string>("PersonFirstIndicatorId");
            switch (RequestActionString)
            {
                case "AutoSave":
                    decimal score = RequestData.Get<decimal>("Score");
                    if (!string.IsNullOrEmpty(PersonFirstIndicatorId))
                    {
                        CustomFirstIndicatorScore cfiEnt = null;
                        IList<CustomFirstIndicatorScore> cfiEnts = CustomFirstIndicatorScore.FindAllByProperties(CustomFirstIndicatorScore.Prop_ExamineTaskId, ExamineTaskId, CustomFirstIndicatorScore.Prop_PersonFirstIndicatorId, PersonFirstIndicatorId);
                        if (cfiEnts.Count > 0)
                        {
                            cfiEnt = cfiEnts[0];
                            cfiEnt.CustomScore = score;
                            cfiEnt.DoUpdate();
                        }
                        else
                        {
                            cfiEnt = new CustomFirstIndicatorScore();
                            cfiEnt.PersonFirstIndicatorId = PersonFirstIndicatorId;
                            cfiEnt.ExamineTaskId = ExamineTaskId;
                            cfiEnt.CustomScore = score;
                            cfiEnt.DoCreate();
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
            sql = @"select A.Id,A.PersonFirstIndicatorId, A.PersonSecondIndicatorName,A.Weight,A.SortIndex, A.ToolTip,A.SelfRemark,
            B.IndicatorType,B.PersonFirstIndicatorName, B.Weight as FirstWeight, B.SortIndex as FirstIndex ,C.Summary,
            (select top 1 CustomScore from BJKY_Examine..CustomFirstIndicatorScore where ExamineTaskId='{5}' and PersonFirstIndicatorId=B.Id) as Score
            from BJKY_Examine..PersonSecondIndicator as A 
            left join BJKY_Examine..PersonFirstIndicator as B on A.PersonFirstIndicatorId=B.Id   
            left join BJKY_Examine..CustomIndicator as C on B.CustomIndicatorId=C.Id    
            where C.CreateId='{0}' and C.Year='{1}' and C.StageType='{2}' and C.DeptId='{3}' and C.IndicatorSecondId='{4}' and C.Result='同意'
            order by B.IndicatorType desc ,B.Id asc,A.SortIndex asc";
            ExamineTask etEnt = ExamineTask.Find(ExamineTaskId);
            ExamineStage esEnt = ExamineStage.Find(etEnt.ExamineStageId);
            sql = string.Format(sql, etEnt.BeUserId, esEnt.Year, esEnt.StageType, esEnt.LaunchDeptId, IndicatorSecondId, ExamineTaskId);
            IList<EasyDictionary> dics0 = DataHelper.QueryDictList(sql);
            IList<EasyDictionary> dics1 = new List<EasyDictionary>();
            string temp = "";
            foreach (EasyDictionary dic0 in dics0)
            {
                EasyDictionary dic1 = new EasyDictionary();
                if (temp != dic0.Get<string>("PersonFirstIndicatorId"))
                {
                    temp = dic0.Get<string>("PersonFirstIndicatorId");
                    dic1.Add("PersonFirstIndicatorId", dic0.Get<string>("PersonFirstIndicatorId"));
                    dic1.Add("IndicatorType", dic0.Get<string>("IndicatorType"));
                    dic1.Add("PersonFirstIndicatorName", dic0.Get<string>("PersonFirstIndicatorName"));
                    dic1.Add("FirstWeight", dic0.Get<string>("FirstWeight"));
                    dic1.Add("Id", dic0.Get<string>("Id"));
                    dic1.Add("PersonSecondIndicatorName", dic0.Get<string>("PersonSecondIndicatorName"));
                    dic1.Add("Weight", dic0.Get<string>("Weight"));
                    dic1.Add("ToolTip", dic0.Get<string>("ToolTip"));
                    dic1.Add("SelfRemark", dic0.Get<string>("SelfRemark"));
                    dic1.Add("Score", dic0.Get<string>("Score"));
                    dic1.Add("Summary", dic0.Get<string>("Summary"));
                }
                else
                {
                    dic1.Add("PersonFirstIndicatorId", "");
                    dic1.Add("IndicatorType", "");
                    dic1.Add("PersonFirstIndicatorName", "");
                    dic1.Add("FirstWeight", "");
                    dic1.Add("Id", dic0.Get<string>("Id"));
                    dic1.Add("PersonSecondIndicatorName", dic0.Get<string>("PersonSecondIndicatorName"));
                    dic1.Add("Weight", dic0.Get<string>("Weight"));
                    dic1.Add("ToolTip", dic0.Get<string>("ToolTip"));
                    dic1.Add("SelfRemark", dic0.Get<string>("SelfRemark"));
                    dic1.Add("Score", "");
                    dic1.Add("Summary", dic0.Get<string>("Summary"));
                }
                dics1.Add(dic1);
            }
            PageState.Add("DataList", dics1);
            IndicatorSecond isEnt = IndicatorSecond.Find(IndicatorSecondId);
            var obj = new
            {
                DeptName = etEnt.BeDeptName,
                IndicatorSecondName = isEnt.IndicatorSecondName,
                MaxScore = isEnt.MaxScore,
                BeUserName = etEnt.BeUserName
            };
            PageState.Add("BaseInfo", obj);
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "CreateTime";
            string asc = search.Orders.Count <= 0 || !search.Orders[0].Ascending ? " desc" : " asc";
            string pageSql = @"
		    WITH OrderedOrders AS
		    (SELECT *,
		    ROW_NUMBER() OVER (order by {0} {1})as RowNumber
		    FROM ({2}) temp ) 
		    SELECT * 
		    FROM OrderedOrders 
		    WHERE RowNumber between {3} and {4}";
            pageSql = string.Format(pageSql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pageSql);
            return dicts;
        }
    }
}


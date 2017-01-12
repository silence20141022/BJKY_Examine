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

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class YearResult : ExamListPage
    {
        ExamineStage esEnt = null;
        string ExamineStageId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            IList<string> YearResultIds = RequestData.GetList<string>("YearResultIds");
            switch (RequestActionString)
            {
                case "Submit":
                    esEnt.State = 4;
                    esEnt.DoUpdate();
                    break;
                case "AutoSave":
                    string id = RequestData.Get<string>("id");
                    string AdviceLevel = RequestData.Get<string>("AdviceLevel");
                    if (!string.IsNullOrEmpty(id))
                    {
                        ExamYearResult eyrEnt = ExamYearResult.Find(id);
                        eyrEnt.AdviceLevel = AdviceLevel;
                        eyrEnt.ApproveLevel = AdviceLevel;
                        eyrEnt.DoUpdate();
                    }
                    break;
                case "FindExamineStageResultId":
                    IList<ExamineStageResult> esrEnts = ExamineStageResult.FindAllByProperties("Year", RequestData.Get<string>("Year"), "StageType", RequestData.Get<string>("StageType"), "UserId", RequestData.Get<string>("UserId"));
                    if (esrEnts.Count > 0)
                    {
                        PageState.Add("FindExamineStageResultId", esrEnts[0].Id);
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string where = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        case "StartTime":
                            where += " and StartTime>='" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        default:
                            where += " and A." + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            string sql = @"select A.*, B.SortIndex as Sequence
            from BJKY_Examine..ExamYearResult as A left join SysEnumeration as B on A.BeRoleCode=B.Code
            where A.ExamineStageId='" + ExamineStageId + "'" + where;
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            var obj = new
             {
                 ExamineStageName = esEnt.StageName,
                 Year = esEnt.Year
             };
            PageState.Add("Obj", obj);
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "Sequence";
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


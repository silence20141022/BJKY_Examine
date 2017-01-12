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
    public partial class ExamineIndicatorList : ExamListPage
    {
        private IList<ExamineIndicator> ents = null;
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (RequestActionString)
            {
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
            sql = @"select * from BJKY_Examine..ExamineIndicator where BelongDeptId in
               (select  Id from BJKY_Examine..PersonConfig 
                where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and 
                (GroupType='职能服务部门' or GroupType='经营目标单位'))";
            sql = string.Format(sql, UserInfo.UserID);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("BeRoleName", SysEnumeration.GetEnumDict("BeExamineObject")); //被考核
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
        [ActiveRecordTransaction]
        private void DoBatchDelete()  //批量删除
        {
            IList<object> idList = RequestData.GetList<object>("IdList");
            if (idList != null && idList.Count > 0)
            {
                foreach (string item in idList)
                {
                    IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperty(IndicatorFirst.Prop_ExamineIndicatorId, item);
                    foreach (IndicatorFirst ifEnt in ifEnts)
                    {
                        IList<IndicatorSecond> isEnts = IndicatorSecond.FindAllByProperty(IndicatorSecond.Prop_IndicatorFirstId, ifEnt.Id);
                        foreach (IndicatorSecond isEnt in isEnts)
                        {
                            IList<ScoreStandard> ssEnts = ScoreStandard.FindAllByProperty(ScoreStandard.Prop_IndicatorSecondId, isEnt.Id);
                            foreach (ScoreStandard ssEnt in ssEnts)
                            {
                                ssEnt.DoDelete();
                            }
                            isEnt.DoDelete();
                        }
                        ifEnt.DoDelete();
                    }
                }
                ExamineIndicator.DoBatchDelete(idList.ToArray());
            }
        }

    }
}


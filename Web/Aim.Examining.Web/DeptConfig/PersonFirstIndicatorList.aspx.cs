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
    public partial class PersonFirstIndicatorList : ExamListPage
    {
        private IList<PersonFirstIndicator> ents = null;
        string CustomIndicatorId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonFirstIndicator ent = null;
            CustomIndicatorId = RequestData.Get<string>("CustomIndicatorId");
            switch (RequestActionString)
            {
                case "delete":
                    IList<string> ids = RequestData.GetList<string>("ids");
                    foreach (string str in ids)
                    {
                        ent = PersonFirstIndicator.Find(str);
                        IList<PersonSecondIndicator> psiEnts = PersonSecondIndicator.FindAllByProperty(PersonSecondIndicator.Prop_PersonFirstIndicatorId, str);
                        foreach (PersonSecondIndicator psiEnt in psiEnts)
                        {
                            psiEnt.DoDelete();
                        }
                        ent.DoDelete();
                    }
                    break;
                case "AutoUpdate":
                    IList<string> entStrList = RequestData.GetList<string>("data");
                    if (entStrList.Count > 0)
                    {
                        ents = entStrList.Select(tent => JsonHelper.GetObject<PersonFirstIndicator>(tent) as PersonFirstIndicator).ToList();
                    }
                    if (ents.Count > 0)
                    {
                        ents[0].DoUpdate();
                    }
                    break;
                case "create":
                    PersonFirstIndicator pfiEnt = new PersonFirstIndicator();
                    CustomIndicator ciEnt = CustomIndicator.Find(CustomIndicatorId);
                    string sql = @"select max(SortIndex) from BJKY_Examine..PersonFirstIndicator where CustomIndicatorId='" + CustomIndicatorId + "'";
                    pfiEnt.SortIndex = DataHelper.QueryValue<int>(sql) + 1;
                    pfiEnt.CustomIndicatorId = ciEnt.Id;
                    pfiEnt.TotalWeight = ciEnt.Weight;
                    pfiEnt.DoCreate();
                    PageState.Add("Entity", pfiEnt);
                    break;
                default:
                    DoSelect();
                    break;
            }

        }
        private void DoSelect()
        {
            string sql = @"select A.* from BJKY_Examine..PersonFirstIndicator as A 
            where CustomIndicatorId='" + CustomIndicatorId + "'";
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "IndicatorType";
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
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");

            if (idList != null && idList.Count > 0)
            {
                for (int i = 0; i < idList.Count; i++)
                {
                    PersonSecondIndicator[] second = PersonSecondIndicator.FindAllByProperty(PersonSecondIndicator.Prop_PersonFirstIndicatorId, idList[i]);
                    for (int j = 0; j < second.Length; j++)
                    {
                        second[i].Delete();
                    }

                }
                PersonFirstIndicator.DoBatchDelete(idList.ToArray());
            }
        }
    }
}


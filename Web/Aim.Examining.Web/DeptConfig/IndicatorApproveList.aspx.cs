using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Model;
using Castle.ActiveRecord;

namespace Aim.Examining.Web.DeptConfig
{
    public partial class IndicatorApproveList : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string sql = "";
        string Index = "";
        CustomIndicator ciEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> entStrList = RequestData.GetList<string>("data");
            id = RequestData.Get<string>("id");
            Index = RequestData.Get<string>("Index");
            if (!string.IsNullOrEmpty(id))
            {
                ciEnt = CustomIndicator.Find(id);
            }
            switch (RequestActionString)
            {
                case "submit":
                    ciEnt.State = "1";
                    ciEnt.Result = "审批中";
                    ciEnt.DoUpdate();
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
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            if (Index == "0")
            {
                sql = @"select * from BJKY_Examine..CustomIndicator where State='1' and  ApproveUserId='" + UserInfo.UserID + "'" + where;
            }
            else
            {
                sql = @"select * from BJKY_Examine..CustomIndicator where  (State ='2' or State='3') and  ApproveUserId='" + UserInfo.UserID + "'" + where;
            }
            sql = string.Format(sql, UserInfo.UserID);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
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
        private void DoBatchDelete()
        {
            IList<object> ids = RequestData.GetList<object>("ids");
            foreach (string str in ids)
            {
                CustomIndicator ciEnt = CustomIndicator.Find(str);
                IList<PersonFirstIndicator> pfiEnts = PersonFirstIndicator.FindAllByProperty(PersonFirstIndicator.Prop_CustomIndicatorId, str);
                foreach (PersonFirstIndicator pfiEnt in pfiEnts)
                {
                    IList<PersonSecondIndicator> psiEnts = PersonSecondIndicator.FindAllByProperty(PersonSecondIndicator.Prop_PersonFirstIndicatorId, pfiEnt.Id);
                    foreach (PersonSecondIndicator psiEnt in psiEnts)
                    {
                        psiEnt.DoDelete();
                    }
                    pfiEnt.DoDelete();
                }
                ciEnt.DoDelete();
            }
        }
    }
}


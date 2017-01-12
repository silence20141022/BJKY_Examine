using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Examining.Model;
using Aim.Data;
using Aim.Portal.Web.UI;
using Aim.Utilities;
using System.Data.SqlClient;
using System.Configuration;
using Aim.Portal.Model;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineStageSelect : ExamBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string where = "";
            //string i = RequestData.Get<string>("2");
            //foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            //{
            //    if (item.Value.ToString() != "")
            //    {
            //        where += " and A." + item.PropertyName + " like '%" + item.Value + "%'";
            //    }
            //}
            //找到由登录人创建的 已生成或者已启动的考核阶段
            string sql = @"select Id ,StageName,ExamineType,StageType,LaunchDeptName,Year from BJKY_Examine..ExamineStage where (State=1 or State=2) 
            and CreateId='" + UserInfo.UserID + "'";
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
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

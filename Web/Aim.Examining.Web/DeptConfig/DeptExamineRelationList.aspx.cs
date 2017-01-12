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
    public partial class DeptExamineRelationList : ExamListPage
    {
        string sql = "";
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DeptExamineRelation ent = null;
            switch (RequestActionString)
            {
                case "delete":
                    id = RequestData.Get<string>("id");
                    if (!string.IsNullOrEmpty(id))
                    {
                        sql = @"select Count(A.Id) from BJKY_Examine..ExamineStage as A left join 
                        BJKY_Examine..ExamineStageDetail as B on A.Id=B.ExamineStageId where B.ExamineRelationId='" + id + "'";
                        int result = DataHelper.QueryValue<int>(sql);
                        if (result > 0)
                        {
                            PageState.Add("Allow", "F");
                        }
                        else
                        {
                            ent = DeptExamineRelation.Find(id);
                            ent.DoDelete();
                            PageState.Add("Allow", "T");
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
            string where = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        case "StartTime":
                            where += " and StartTime>'" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        case "BeUserNames":
                            where += " and (PatIndex('%" + item.Value + "%',BeUserNames)>0 or PatIndex('%" + item.Value + "%',UpLevelUserNames)>0 or PatIndex('%" + item.Value + "%',SameLevelUserNames)>0 or PatIndex('%" + item.Value + "%',DownLevelUserNames)>0 )";
                            break;
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            sql = @"select * from BJKY_Examine..DeptExamineRelation where GroupID in (select  Id from BJKY_Examine..PersonConfig 
                  where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位'))" + where;
            sql = string.Format(sql, UserInfo.UserID);
            //该部门的人只要能进入此模块 显示登录人部门的 考核关系
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
    }
}


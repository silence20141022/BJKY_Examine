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
using System.Data;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class TaskGroupByToUser : ExamListPage
    {
        private string ExamineStageId = string.Empty;
        ExamineStage esEnt = null;
        private string ToUserId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            ToUserId = RequestData.Get<string>("ToUserId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            switch (RequestActionString)
            {
                case "SendMessage":
                    SendMessage();
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
            string sql = @"select  A.ToUserId,A.ToUserName,(select Phone from SysUser where UserID=A.ToUserId) as Phone,
           (select count(Id) from  BJKY_Examine..ExamineTask where ExamineStageId='{0}'
            and State='1' and ToUserId=A.ToUserId) as UnSubmitQuan,count(*) as TaskQuan from BJKY_Examine..ExamineTask as A
            where A.ExamineStageId='{1}'" + where + "group by A.ToUserId ,A.ToUserName";
            sql = string.Format(sql, ExamineStageId, ExamineStageId);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }
        private void SendMessage()
        {
            //            string msg = @"请登录  http://192.168.1.42:7003  北京矿冶研究总院绩效考核系统 ！提交【" + esEnt.StageName + "】考核任务";
            //            string sql = @"select UserID,Name,WorkNo,Phone from BJKY_Portal..SysUser  
            //                        where UserID='" + ToUserId + "' and Phone is not null";
            //            DataTable dt = DataHelper.QueryDataTable(sql);
            //            foreach (DataRow row in dt.Rows)
            //            {
            //                sql = @"INSERT INTO [BJKY_Message].[dbo].[outbox]
            //               ([Sender],[ReceiverMobileNo],[Msg],[SendTime],[IsChinese],[CommPort] ,[NeedReport])
            //                VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,1 ,0 ,0)";
            //                sql = string.Format(sql, "", row["Phone"].ToString(), msg, DateTime.Now.ToString());
            //                DataHelper.ExecSql(sql);
            //            }
            //            PageState.Add("Result", "T");
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "ToUserName";
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


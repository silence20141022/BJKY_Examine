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
    public partial class CustomIndicatorList : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string sql = "";
        CustomIndicator ciEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> entStrList = RequestData.GetList<string>("data");
            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                ciEnt = CustomIndicator.Find(id);
            }
            switch (RequestActionString)
            {
                case "create":
                    ciEnt = new CustomIndicator();
                    ciEnt.IndicatorNo = DataHelper.QueryValue<string>("select BJKY_Examine.dbo.fun_getIndicatorNo()");
                    ciEnt.DeptId = RequestData.Get<string>("DeptId");
                    ciEnt.DeptName = RequestData.Get<string>("DeptName");
                    ciEnt.IndicatorSecondId = RequestData.Get<string>("IndicatorSecondId");
                    ciEnt.IndicatorSecondName = RequestData.Get<string>("IndicatorSecondName");
                    ciEnt.DeptIndicatorName = RequestData.Get<string>("DeptIndicatorName");
                    ciEnt.Weight = RequestData.Get<int>("Weight");
                    PersonConfig pcEnt = PersonConfig.Find(ciEnt.DeptId);
                    if (pcEnt != null)
                    {
                        ciEnt.Remark = pcEnt.GroupID;//备注字段用来存储足够机构的ID  方便选人
                    }
                    ciEnt.State = "0";
                    ciEnt.DoCreate();
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("originalId")))//如果改字段不为空  说明是复制操作
                    {
                        IList<PersonFirstIndicator> pfiEnts = PersonFirstIndicator.FindAllByProperty(PersonFirstIndicator.Prop_CustomIndicatorId, RequestData.Get<string>("originalId"));
                        foreach (PersonFirstIndicator pfiEnt in pfiEnts)
                        {
                            PersonFirstIndicator nEnt1 = new PersonFirstIndicator();
                            nEnt1.CustomIndicatorId = ciEnt.Id;
                            nEnt1.PersonFirstIndicatorName = pfiEnt.PersonFirstIndicatorName;
                            nEnt1.TotalWeight = pfiEnt.TotalWeight;
                            nEnt1.Weight = pfiEnt.Weight;
                            nEnt1.SortIndex = pfiEnt.SortIndex;
                            nEnt1.IndicatorType = pfiEnt.IndicatorType;
                            nEnt1.CreateId = UserInfo.UserID;
                            nEnt1.CreateName = UserInfo.Name;
                            nEnt1.CreateTime = DateTime.Now;
                            nEnt1.DoCreate();
                            IList<PersonSecondIndicator> psiEnts = PersonSecondIndicator.FindAllByProperty(PersonSecondIndicator.Prop_PersonFirstIndicatorId, pfiEnt.Id);
                            foreach (PersonSecondIndicator psiEnt in psiEnts)
                            {
                                PersonSecondIndicator NEnt2 = new PersonSecondIndicator();
                                NEnt2.PersonFirstIndicatorId = nEnt1.Id;
                                NEnt2.PersonFirstIndicatorName = nEnt1.PersonFirstIndicatorName;
                                NEnt2.PersonSecondIndicatorName = psiEnt.PersonSecondIndicatorName;
                                NEnt2.Weight = psiEnt.Weight;
                                NEnt2.SortIndex = psiEnt.SortIndex;
                                NEnt2.ToolTip = psiEnt.ToolTip;
                                NEnt2.SelfRemark = psiEnt.SelfRemark;
                                NEnt2.CreateId = UserInfo.UserID;
                                NEnt2.CreateName = UserInfo.Name;
                                NEnt2.CreateTime = DateTime.Now;
                                NEnt2.DoCreate();
                            }
                        }
                    }
                    PageState.Add("Entity", ciEnt);
                    break;
                case "AutoUpdate":
                    string ApproveUserId = RequestData.Get<string>("ApproveUserId");
                    string ApproveUserName = RequestData.Get<string>("ApproveUserName");
                    if (!string.IsNullOrEmpty(ApproveUserId))
                    {
                        ciEnt.ApproveUserId = ApproveUserId;
                        ciEnt.ApproveUserName = ApproveUserName;
                    }
                    ciEnt.DoUpdate();
                    break;
                case "submit":
                    sql = @"select isnull(sum(A.Weight),0) from BJKY_Examine..PersonSecondIndicator as A 
                    left join BJKY_Examine..PersonFirstIndicator as B on A.PersonFirstIndicatorId=B.Id where B.CustomIndicatorId='" + id + "'";
                    int useWeight = DataHelper.QueryValue<int>(sql);
                    if (ciEnt.Weight == useWeight)
                    {
                        ciEnt.State = "1";
                        ciEnt.Result = "审批中";
                        ciEnt.DoUpdate();
                        PageState.Add("Result", "T");
                    }
                    else
                    {
                        PageState.Add("Result", "F");
                    }
                    break;
                case "delete":
                    DoBatchDelete();
                    break;
                case "UpLoadSummary":
                    ciEnt.Summary = RequestData.Get<string>("Summary");
                    ciEnt.DoUpdate();
                    PageState.Add("Result", ciEnt.Summary);
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
            sql = @"select  A.*,B.StageName from BJKY_Examine..CustomIndicator A left join BJKY_Examine..ExamineStage B
            on A.ExamineStageId=B.Id where  A.CreateId='" + UserInfo.UserID + "' ";
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("EnumYear", SysEnumeration.GetEnumDict("Year"));
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


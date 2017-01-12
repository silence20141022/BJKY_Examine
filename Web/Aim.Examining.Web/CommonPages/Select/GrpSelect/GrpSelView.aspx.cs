using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Model;

namespace Aim.Portal.Web.CommonPages
{
    public partial class GrpSelView : BaseListPage
    {
        #region 变量

        string op = String.Empty;
        string id = String.Empty;
        string cid = String.Empty;   // 类型标识
        string type = String.Empty; // 查询类型
        string ctype = String.Empty; // 分类类型

        private IList<SysGroup> ents = new List<SysGroup>();

        #endregion

        #region 构造函数

        public GrpSelView()
        {
            SearchCriterion.CurrentPageIndex = 1;
            SearchCriterion.PageSize = 100; // 一次最多显示100个角色
        }

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            // 类型标识
            cid = RequestData.Get<string>("cid", String.Empty);
            type = RequestData.Get<string>("type", String.Empty).ToLower();
            ctype = RequestData.Get<string>("ctype", "role").ToLower();

            if (!IsAsyncRequest)
            {
                if (!String.IsNullOrEmpty(cid))
                {
                    try
                    {
                        int icid = Convert.ToInt32(cid);

                        SearchCriterion.SetOrder("ParentID");
                        SearchCriterion.SetOrder("SortIndex");
                        SearchCriterion.SetOrder("CreateDate");
                        SearchCriterion.AddSearch("Type", icid);

                        ents = SysGroupRule.FindAll(SearchCriterion);
                    }
                    catch { }

                    this.PageState.Add("DtList", ents);
                }
                else
                {
                    SysGroupType[] typeList = SysGroupTypeRule.FindAll();
                    this.PageState.Add("DtList", typeList);
                }
            }
            else
            {
                switch (this.RequestAction)
                {
                    case RequestActionEnum.Custom:
                        if (RequestActionString == "querychildren")
                        {
                            if (type == "gtype")
                            {
                                ents = SysGroup.FindAll("FROM SysGroup as ent WHERE ent.Type = ?", id);

                                this.PageState.Add("DtList", ents);
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}

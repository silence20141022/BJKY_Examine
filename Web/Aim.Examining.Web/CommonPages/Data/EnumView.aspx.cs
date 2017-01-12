using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;

using Aim.Data;
using Aim.Common;
using Aim.Web.UI;
using Aim.Portal.Entity;
using Aim.Portal.Rule;

namespace Aim.Portal.Web.CommonPages
{
    public partial class EnumView : BaseListPage
    {
        #region 变量

        private SysEnumeration[] ents = null;

        string id = String.Empty;   // 对象id
        IList<string> ids = null;   // 节点列表
        IList<string> pids = null;   // 父节点列表
        string code = String.Empty; // 对象编码
        string showroot = String.Empty;  // 是否显示根节点

        string tag = String.Empty;  // 扩展信息
        SearchModeEnum tagschmode = SearchModeEnum.Like;    // 扩展信息查询方式

        #endregion

        #region 构造函数

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id", String.Empty);
            ids = RequestData.GetList<string>("ids");
            pids = RequestData.GetList<string>("pids");
            code = RequestData.Get<string>("code", String.Empty);
            showroot = RequestData.Get<string>("showroot", String.Empty);

            tag = RequestData.Get<string>("tag", String.Empty);
            string tagschmodestr = RequestData.Get<string>("tagschmode", String.Empty);
            tagschmode = ObjectHelper.GetEnum<SearchModeEnum>(tagschmodestr, SearchModeEnum.Like);

            SysEnumeration ent = null;
            switch (this.RequestAction)
            {
                case RequestActionEnum.Update:
                    ent = this.GetMergedData<SysEnumeration>();
                    ent.ParentID = String.IsNullOrEmpty(ent.ParentID) ? null : ent.ParentID;
                    ent.Update();
                    this.SetMessage("更新成功！");
                    break;
                default:
                    if (RequestActionString == "batchdelete")
                    {
                        IList<object> idList = RequestData.GetList<object>("IdList");
                        if (idList != null && idList.Count > 0)
                        {
                            SysEnumeration.DoBatchDelete(idList.ToArray());
                        }
                    }
                    else
                    {
                        // 构建查询表达式
                        SearchCriterion sc = new HqlSearchCriterion();

                        // sc.SetOrder("SortIndex");
                        sc.SetOrder("CreatedDate");

                        ICriterion crit = null;

                        if (RequestActionString == "querychildren")
                        {
                            if (ids != null && ids.Count > 0 || pids != null && pids.Count > 0)
                            {
                                if (ids != null && ids.Count > 0)
                                {
                                    IEnumerable<string> distids = ids.Distinct().Where(tent => !String.IsNullOrEmpty(tent));
                                    crit = Expression.In(SysEnumeration.Prop_EnumerationID, distids.ToArray());
                                }

                                if (pids != null && pids.Count > 0)
                                {
                                    IEnumerable<string> dispids = pids.Distinct().Where(tent => !String.IsNullOrEmpty(tent));

                                    if (crit != null)
                                    {
                                        crit = SearchHelper.UnionCriterions(crit, Expression.In(SysEnumeration.Prop_ParentID, dispids.ToArray()));
                                    }
                                    else
                                    {
                                        crit = Expression.In(SysEnumeration.Prop_ParentID, dispids.ToArray());
                                    }
                                }
                            }
                        }
                        else
                        {
                            ICriterion tagCirt = null;

                            if (!String.IsNullOrEmpty(tag))
                            {
                                HqlCommonSearchCriterionItem tagCritItem = new HqlCommonSearchCriterionItem(SysEnumeration.Prop_Tag, tag, tagschmode);
                                tagCirt = tagCritItem.GetCriterion();
                            }

                            if (!String.IsNullOrEmpty(code))
                            {
                                SysEnumeration tent = SysEnumeration.FindFirstByProperties(SysEnumeration.Prop_Code, code);

                                crit = SearchHelper.IntersectCriterions(
                                    Expression.Eq(SysEnumeration.Prop_ParentID, tent.EnumerationID), tagCirt);

                                if (!String.IsNullOrEmpty(showroot) && showroot != "0" && showroot.ToLower() != "false")
                                {
                                    crit = SearchHelper.UnionCriterions(
                                        crit, Expression.Eq(SysEnumeration.Prop_EnumerationID, tent.EnumerationID));
                                }

                                this.PageState.Add("Root", tent);
                            }
                            else
                            {
                                crit = SearchHelper.UnionCriterions(Expression.IsNull(SysEnumeration.Prop_ParentID),
                                    Expression.Eq(SysEnumeration.Prop_ParentID, String.Empty));

                                crit = SearchHelper.IntersectCriterions(crit, tagCirt);
                            }
                        }

                        ents = SysEnumerationRule.FindAll(sc, crit);

                        this.PageState.Add("DtList", ents);
                    }

                    break;
            }
        }

        #endregion
    }
}

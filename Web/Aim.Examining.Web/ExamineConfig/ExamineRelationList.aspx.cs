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

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineRelationList : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> entStrList = RequestData.GetList<string>("data");
            id = RequestData.Get<string>("id");
            switch (RequestActionString)
            {
                case "save":
                    IList<ExamineRelation> pcEnts = entStrList.Select(tent => JsonHelper.GetObject<ExamineRelation>(tent) as ExamineRelation).ToList();
                    foreach (ExamineRelation pcEnt in pcEnts)
                    {
                        if (!string.IsNullOrEmpty(pcEnt.Id))
                        {
                            pcEnt.DoUpdate();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(pcEnt.BeRoleCode))
                                pcEnt.DoCreate();
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
            IList<ExamineRelation> ents = null;
            if (!string.IsNullOrEmpty(id))
            {
                ents = ExamineRelation.FindAllByProperty("Id", id);
            }
            else
            {
                ents = ExamineRelation.FindAll(SearchCriterion);
            }
            PageState.Add("DataList", ents);
            PageState.Add("BeRoleName", SysEnumeration.GetEnumDict("BeExamineObject")); //被考核
            PageState.Add("ToRoleName", SysEnumeration.FindAllByProperty(SysEnumeration.Prop_Code, "ExamineObject").First().ChildNodes);//多选下拉框
        }
        [ActiveRecordTransaction]
        private void DoBatchDelete()
        {
            IList<object> idList = RequestData.GetList<object>("IdList");
            if (idList != null && idList.Count > 0)
            {
                ExamineRelation.DoBatchDelete(idList.ToArray());
            }
        }
    }
}


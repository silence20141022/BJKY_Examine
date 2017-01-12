using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Model;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class UserBalanceList : ExamBasePage
    {
        string ExamineRelationId = String.Empty;   // 对象id   
        UserBalance ubEnt = null;
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineRelationId = RequestData.Get<string>("ExamineRelationId");
            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                ubEnt = UserBalance.Find(id);
            }
            switch (RequestActionString)
            {
                case "AutoSave":
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("ToRoleCode")))
                    {
                        ubEnt.ToRoleCode = RequestData.Get<string>("ToRoleCode");
                    }
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("ToRoleName")))
                    {
                        ubEnt.ToRoleName = RequestData.Get<string>("ToRoleName");
                    }
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("ToUserId")))
                    {
                        ubEnt.ToUserId = RequestData.Get<string>("ToUserId");
                    }
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("ToUserName")))
                    {
                        ubEnt.ToUserName = RequestData.Get<string>("ToUserName");
                    }
                    if (!string.IsNullOrEmpty(RequestData.Get<string>("Balance")))
                    {
                        ubEnt.Balance = RequestData.Get<int>("Balance");
                    }
                    ubEnt.DoUpdate();
                    break;
                case "delete":
                    IList<string> ids = RequestData.GetList<string>("ids");
                    foreach (string str in ids)
                    {
                        ubEnt = UserBalance.Find(str);
                        ubEnt.DoDelete();
                    }
                    break;
                case "Create":
                    ubEnt = new UserBalance();
                    ubEnt.ExamineRelationId = RequestData.Get<string>("ExamineRelationId");
                    ubEnt.RelationName = RequestData.Get<string>("RelationName");
                    ubEnt.SortIndex = RequestData.Get<int>("SortIndex");
                    ubEnt.DoCreate();
                    PageState.Add("ubEnt", ubEnt);
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            IList<UserBalance> ubEnts = UserBalance.FindAllByProperty("SortIndex", UserBalance.Prop_ExamineRelationId, ExamineRelationId);
            PageState.Add("DataList", ubEnts);
        }
    }
}


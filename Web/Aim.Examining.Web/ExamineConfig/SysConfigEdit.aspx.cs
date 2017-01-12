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
    public partial class SysConfigEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id    
        SysConfig scEnt = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id"); 
            switch (RequestActionString)
            {
                case "update":
                    scEnt = GetMergedData<SysConfig>(); 
                    scEnt.DoUpdate();
                    break;
                case "create":
                    scEnt = this.GetPostedData<SysConfig>(); 
                    scEnt.DoCreate();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            IList<SysConfig> scEnts = SysConfig.FindAll();
            if (scEnts.Count > 0)
            {
                SetFormData(scEnts[0]);
            }
        }
    }
}


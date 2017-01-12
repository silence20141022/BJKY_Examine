using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Aim.Common;
using Aim.Portal;
using Aim.Portal.Web;
using Aim.Portal.Model;
using Aim.Portal.Web.UI;
using Aim.Data;
using System.Configuration;

namespace Aim.Examining.Web
{
    public partial class Default : ExamBasePage
    {
        string db = ConfigurationManager.AppSettings["BJZJ_Examine"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tag"] != null && Request.QueryString["tag"] == "Refresh")
            {
                Response.Write("");
                Response.End();
            }
            IEnumerable<SysModule> topAuthExamMdls = new List<SysModule>();
            if (UserContext.AccessibleApplications.Count > 0)
            {
                SysApplication examApp = UserContext.AccessibleApplications.FirstOrDefault(tent => tent.Code == EXAMINING_APP_CODE);

                if (examApp != null && UserContext.AccessibleModules.Count > 0)
                {
                    topAuthExamMdls = UserContext.AccessibleModules.Where(tent => tent.ApplicationID == examApp.ApplicationID && String.IsNullOrEmpty(tent.ParentID));
                    topAuthExamMdls = topAuthExamMdls.OrderBy(tent => tent.SortIndex);
                }
            }
            //string IsPrompt = "False";
            //IList<EasyDictionary> dicAssConfig = DataHelper.QueryDictList("select * from " + db + "..P_AssConfig where CloseState='1'");
            //if (dicAssConfig.Count > 0)
            //{
            //    string assConfigId = dicAssConfig[0].Get<String>("Id");
            //    IList<EasyDictionary> dicList = DataHelper.QueryDictList("select * from " + db + "..P_StageSubmitUser where UserId='" + WebPortalService.CurrentUserInfo.UserID + "'  and P_AssConfigId='" + assConfigId + "'");
            //    if (dicList.Count == 0)
            //    {
            //        IsPrompt = "True";
            //    }
            //}
            this.PageState.Add("Modules", topAuthExamMdls);
            //this.PageState.Add("Prompt", IsPrompt);
        }

        protected void lnkRelogin_Click(object sender, EventArgs e)
        {
            WebPortalService.LogoutAndRedirect();
        }

        protected void lnkGoodway_Click(object sender, EventArgs e)
        {
            // string passcode = GwIntegrateService.GetGwPasscode();
            string passcode = String.Empty;

            string gwPortalUrl = ConfigurationHosting.SystemConfiguration.AppSettings["GoodwayPortalUrl"];
            gwPortalUrl = String.Format(gwPortalUrl + "?PassCode={0}", passcode);

            Response.Redirect(gwPortalUrl);
        }

        protected void lnkExit_Click(object sender, EventArgs e)
        {
            WebPortalService.Exit();
        }
    }
}

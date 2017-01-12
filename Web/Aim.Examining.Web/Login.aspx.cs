using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Aim.Portal.Model;
namespace Aim.Portal.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private bool asyncreq = false;

        protected string PrjName = String.Empty;

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {   
            asyncreq = ObjectHelper.ConvertValue<bool>(Request["asyncreq"], false);
            if (Request["reqaction"] == "login")
            {
                DoLogin();
            }
            else
            {
                string gwPassCode = Request["gwpasscode"];
                string workNo = Request["workno"];

                if (!String.IsNullOrEmpty(gwPassCode) && !String.IsNullOrEmpty(workNo))
                {
                    DoLoginByGwPassCodeAndWorkNo(gwPassCode, workNo);
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 由金慧Passcode登录
        /// </summary>
        /// <param name="passcode"></param>
        private void DoLoginByGwPassCodeAndWorkNo(string passcode, string workno)
        {
            bool stateflag = true;
            // bool stateflag = GwIntegrateService.CheckGwUserSession(passcode);

            if (stateflag)
            {
                SysUser usr = SysUser.FindFirstByProperties("WorkNo", workno);
                LoginUser(usr.LoginName, usr.Password, true);
            }
        }

        private void DoLogin()
        {
            string uname = Request["uname"];
            string pwd = Request["pwd"];

            if (!String.IsNullOrEmpty(uname))
            {
                LoginUser(uname, pwd, false);
            }
            else
            {
                Response.Write("请输入用户名！");
                Response.End();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        private void LoginUser(string uname, string pwd, bool pwdEncrypted)
        {
            try
            {
                string sid = PortalService.AuthUser(uname, pwd, pwdEncrypted);

                if (!String.IsNullOrEmpty(sid))
                {
                    string url = FormsAuthentication.GetRedirectUrl(uname, true);

                    if (asyncreq)
                    {
                        Response.Write(String.Format("success,{0}", url));
                    }
                    else
                    {
                        Response.Redirect(url);
                    }
                }
                else
                {
                    Response.Write("登陆失败，用户名或密码不正确！");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            Response.End();
        }

        #endregion
    }
}

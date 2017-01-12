using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Web;
using System.Web.Security;
using Aim.Common;
using Aim.Common.Authentication;
using Aim.Portal;

namespace Aim.Portal.ServicesProvider
{
    [PartMetadata(ContantValue.AIM_MEF_EXPORT_KEY, ContantValue.AIM_MEF_EXPORT)]
    [Export(typeof(IPortalServiceProvider))]
    public class WebPortalServiceProvider : PortalServiceProvider
    {
        #region PortalServiceProvider 成员

        /// <summary>
        /// 获取当前系统令牌
        /// </summary>
        /// <returns></returns>
        public override SysIdentity GetCurrentSysIdentity()
        {
            SysIdentity sysIdentity = WebHelper.GetSysIdentity(HttpContext.Current.User);

            if (sysIdentity != null && !String.IsNullOrEmpty(sysIdentity.UserSID))
            {
                if (sysIdentity.UserInfo == null)
                {
                    sysIdentity.UserInfo = GetUserInfo(sysIdentity.UserSID);
                }

                return sysIdentity;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="authPackage"></param>
        /// <returns></returns>
        public override string AuthenticateUser(IAuthPackage authPackage)
        {
            string sid = base.AuthenticateUser(authPackage);

            if (!String.IsNullOrEmpty(sid) && sid.Length >= 30)
            {
                // 判断返回的是否用户状态
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(authPackage.LoginName, false);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                    ticket.Version, ticket.Name, ticket.IssueDate,
                    ticket.Expiration, ticket.IsPersistent, sid);

                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }

            return sid;
        }

        /// <summary>
        /// 登出系统
        /// </summary>
        public override void Logout()
        {
            HttpContext.Current.Session.Abandon();

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        #endregion
    }
}

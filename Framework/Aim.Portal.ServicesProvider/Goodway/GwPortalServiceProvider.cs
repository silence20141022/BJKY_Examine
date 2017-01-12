using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Goodway.Data;
using Aim.Common;
using Aim.Common.Service;
using Aim.Common.Authentication;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.ServicesProvider.GwUSService;

namespace Aim.Portal.ServicesProvider
{
    public class GwPortalServiceProvider : IPortalServiceProvider
    {
        #region 成员属性

        public const string GW_PASSCODE = "PassCode";

        private static UserStateServiceSoapClient ussc;    // 金慧用户状态服务

        /// <summary>
        /// 用户状态服务
        /// </summary>
        private static UserStateServiceSoapClient USServiceClient
        {
            get
            {
                if (ussc == null)
                {
                    ussc = new UserStateServiceSoapClient();
                }

                return ussc;
            }
        }

        #endregion

        #region IPortalServiceProvider 成员

        public SysIdentity GetCurrentSysIdentity()
        {
            SysIdentity sysIdentity = WebHelper.GetSysIdentity(HttpContext.Current.User);

            string gwpasscode = HttpContext.Current.Request[GW_PASSCODE];

            if (sysIdentity == null && !String.IsNullOrEmpty(gwpasscode))
            {
                if (this.GetUserSessionState(gwpasscode) == UserSessionState.Valid)
                {
                    UserInfo ui = GetUserInfo(gwpasscode);

                    SetAuthenticationTicket(gwpasscode, ui.LoginName);

                    sysIdentity = new SysIdentity(gwpasscode);
                    sysIdentity.UserInfo = ui;

                    SysPrincipal sp = new SysPrincipal(sysIdentity);
                    HttpContext.Current.User = sp;
                }
            }
            
            if (sysIdentity != null && !String.IsNullOrEmpty(sysIdentity.UserSID))
            {
                if (sysIdentity.UserInfo == null)
                {
                    sysIdentity.UserInfo = GetUserInfo(sysIdentity.UserSID);
                }
            }

            return sysIdentity;
        }

        public UserInfo GetUserInfo(string sessionID)
        {
            UserInfo ui = null;

            string uistr = USServiceClient.GetUserInfo(sessionID);

            if (!String.IsNullOrEmpty(uistr))
            {
                DataForm df = new DataForm(uistr);
                string loginName = df.GetValue("SystemName") == null ? df.GetValue("LoginName") : df.GetValue("SystemName");

                SysUser user = SysUser.Get(loginName);

                ui = new GwUserInfo(user.UserID, loginName, user.Name);
            }

            return ui;
        }

        public UserSessionState GetUserSessionState(string sessionID)
        {
            bool stateflag = false;

            try
            {
                stateflag = USServiceClient.CheckUserState(sessionID);
            }
            finally { }

            if (stateflag)
            {
                return UserSessionState.Valid;
            }

            return UserSessionState.Faulted;
        }

        public void RefreshSession(string sessionID)
        {
            USServiceClient.RefreshUserState(sessionID);
        }

        public string AuthenticateUser(IAuthPackage authPackage)
        {
            DataForm df = new DataForm();
            df.SetValue("LoginName", authPackage.LoginName);
            df.SetValue("Password", authPackage.Password);

            string sid = USServiceClient.CreateUserState(df.ToString(), string.Empty, string.Empty, string.Empty, string.Empty);

            if (!String.IsNullOrEmpty(sid) && sid.Length >= 30)
            {
                SetAuthenticationTicket(sid, authPackage.LoginName);
            }

            return sid;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Abandon();

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置认证令牌
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="loginName"></param>
        private void SetAuthenticationTicket(string sessionId, string loginName)
        {
            SysIdentity si = null;
            SysPrincipal sp = null;

            // 判断返回的是否用户状态
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(loginName, true);
            authCookie.Expires.AddYears(99);  // 永不过期

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                ticket.Version, ticket.Name, ticket.IssueDate,
                ticket.Expiration, ticket.IsPersistent, sessionId);

            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        #endregion

        #region 内部类

        class GwUserInfo : UserInfo
        {
            #region 私有方法

            private string _UserID, _LoginName, _Name;

            #endregion

            #region 属性

            public override string UserID
            {
                get { return _UserID; }
            }

            public override string LoginName
            {
                get { return _LoginName; }
            }

            public override string Name
            {
                get { return _Name; }
            }

            #endregion


            #region 构造函数

            public GwUserInfo(string userID, string loginName, string name)
            {
                this._UserID = userID;
                this._LoginName = loginName;
                this._Name = name;
            }

            #endregion
        }

        #endregion
    }
}

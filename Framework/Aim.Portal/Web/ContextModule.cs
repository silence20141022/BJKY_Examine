using System;
using System.Threading;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Castle.ActiveRecord;
using Aim.Common;
using Aim.Common.Authentication;

namespace Aim.Portal.Web
{
    /// <summary>
    /// 处理用户的验证
    /// </summary>
    public class ContextModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);

            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        public void Dispose()
        {

        }

        #endregion

        /// <summary>
        /// 认证请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            AcquireRequestIdentity();
        }

        /// <summary>
        /// 请求开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            
        }

        #region 静态方法

        /// <summary>
        /// 获取登陆标识信息
        /// </summary>
        public static void AcquireRequestIdentity()
        {
            IPrincipal user = HttpContext.Current.User;
            string requestPath = HttpContext.Current.Request.FilePath.ToLower();

            // 只有aspx页面才需要验证
            if (!(requestPath.EndsWith("aspx") || requestPath.EndsWith("ashx")))
            {
                return;
            }

            // 用户认证
            if (user != null && user.Identity.IsAuthenticated && user.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity formIdentity = user.Identity as FormsIdentity;
                string sid = formIdentity.Ticket.UserData;

                if (!String.IsNullOrEmpty(sid))
                {
                    SysIdentity si = new SysIdentity(sid);
                    SysPrincipal sp = new SysPrincipal(si);
                    HttpContext.Current.User = sp;
                }
            }

        }

        #endregion
    }
    
}

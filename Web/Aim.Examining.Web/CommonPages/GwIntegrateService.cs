using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aim.Web;
using Aim.Web.UI;
using Aim.Portal.Entity;
using Aim.Portal.Web.GwUSService;

namespace Aim.Portal.Web
{
    /// <summary>
    /// 金慧集成服务
    /// </summary>
    public class GwIntegrateService
    {
        #region 成员属性

        public const string GW_PASSCODE = "PassCode";

        private static UserStateService ussc;    // 金慧用户状态服务

        /// <summary>
        /// 用户状态服务
        /// </summary>
        private static UserStateService USServiceClient
        {
            get
            {
                if (ussc == null)
                {
                    ussc = new UserStateService();
                }

                return ussc;
            }
        }

        #endregion

        #region 构造函数

        public GwIntegrateService()
        {
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 获取金慧系统PassCode
        /// </summary>
        /// <returns></returns>
        public static string GetGwPasscode()
        {
            string passcode = String.Empty;

            if (PortalService.CurrentUserInfo.ExtData.ContainsKey(GW_PASSCODE))
            {
                passcode = PortalService.CurrentUserInfo.ExtData[GW_PASSCODE];

                if (!USServiceClient.CheckUserState(passcode))
                {
                    passcode = String.Empty;
                }
            }

            if (String.IsNullOrEmpty(passcode))
            {
                SysUser user = SysUser.Find(PortalService.CurrentUserInfo.UserID);

                passcode = USServiceClient.CreateUserStateByWorkNo(user.WorkNo, HttpContext.Current.Request.UserHostAddress, "");
                PortalService.CurrentUserInfo.ExtData.Add(GW_PASSCODE, passcode);
            }

            return passcode;
        }

        /// <summary>
        /// 由金慧passcode获取用户
        /// </summary>
        /// <returns></returns>
        public static bool CheckGwUserSession(string passcode)
        {
            bool stateflag = false;

            try
            {
                stateflag = USServiceClient.CheckUserState(passcode);
            }
            finally { }

            return stateflag;
        }

        #endregion

        #region 私有函数

        #endregion
    }
}

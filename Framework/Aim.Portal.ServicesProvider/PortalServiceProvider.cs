using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ComponentModel.Composition;
using Aim.Common;
using Aim.Common.Service;
using Aim.Common.Authentication;
using Aim.Portal;

namespace Aim.Portal.ServicesProvider
{
    [PartMetadata(ContantValue.AIM_MEF_EXPORT_KEY, ContantValue.AIM_MEF_EXPORT)]
    [Export(typeof(IPortalServiceProvider))]
    public class PortalServiceProvider : IPortalServiceProvider
    {
        #region 成员

        private USService.UserSessionServiceClient ussc;

        #endregion

        #region 构造函数

        public PortalServiceProvider()
        {
            ussc = new USService.UserSessionServiceClient();
        }

        #endregion

        #region 属性

        protected USService.IUserSessionService USService
        {
            get
            {
                if (ussc == null)
                {
                    ussc = new USService.UserSessionServiceClient();
                }

                if (ussc.State == CommunicationState.Closed || ussc.State == CommunicationState.Faulted)
                {
                    ussc = new USService.UserSessionServiceClient();
                }

                return ussc;
            }
        }

        #endregion

        #region IPortalServiceProvider 成员

        /// <summary>
        /// 获取当前系统令牌
        /// </summary>
        /// <returns></returns>
        public virtual SysIdentity GetCurrentSysIdentity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public virtual UserInfo GetUserInfo(string sessionID)
        {
            return GetUserData<UserInfo>(sessionID, "getuserinfo");
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public virtual UserSessionState GetUserSessionState(string sessionID)
        {
            if (USService.CheckUserSession(sessionID))
            {
                return UserSessionState.Valid;
            }
            else
            {
                return UserSessionState.None;
            }
        }

        public virtual void RefreshSession(string sessionID)
        {
            USService.RefreshSession(sessionID);
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="authPackage"></param>
        /// <returns></returns>
        public virtual string AuthenticateUser(IAuthPackage authPackage)
        {
            AuthMessage am = new AuthMessage();
            am.LoginName = authPackage.LoginName;
            am.Password = authPackage.Password;
            am.PasswordEncrypted = authPackage.PasswordEncrypted;
            am.AuthType = authPackage.AuthType;
            am.IP = authPackage.IP;

            string sid = USService.AuthenticateUser(am.ToString());

            return sid;
        }

        /// <summary>
        /// 登出系统
        /// </summary>
        public virtual void Logout()
        {

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sid"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        protected T GetUserData<T>(string sid, string op)
        {
            return RetrieveUserData<T>(sid, op);
        }

        /// <summary>
        /// 获取系统数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="op"></param>
        /// <returns></returns>
        protected T GetSystemData<T>(string op)
        {
            return RetrieveSystemData<T>(op);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sid"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        protected T RetrieveUserData<T>(string sid, string operation)
        {
            OpMessage opMsg = new OpMessage();
            opMsg.SessionID = sid;
            opMsg.Operation = operation;

            T obj = ServiceHelper.DeserializeFromBytes<T>(USService.GetUserData(opMsg.ToString()));

            return obj;
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        protected T RetrieveSystemData<T>(string operation)
        {
            OpMessage opMsg = new OpMessage();
            opMsg.Operation = operation;

            T obj = ServiceHelper.DeserializeFromBytes<T>(USService.GetSystemData(opMsg.ToString()));

            return obj;
        }

        #endregion
    }
}

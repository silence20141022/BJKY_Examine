using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Common;
using Aim.Common.Service;
using Aim.Common.Authentication;
using Aim.Portal.Model;

namespace Aim.Portal
{
    /// <summary>
    /// 处理Portal服务相关信息
    /// </summary>
    public class PortalService
    {
        #region 成员

        private bool isInitialized = false;

        private IPortalServiceProvider _provider = null;

        private static PortalService ps = null;

        private Dictionary<string, UserContext> userContextList;
        public static object uclslocker = new object(); // 添加一个对象作为UserContextList的锁

        private SystemContext systemContext; 

        private IList<SysApplication> applications;
        private IList<SysModule> modules;
        private IList<SysAuth> auths;
        private IList<SysGroup> groups;
        private IList<SysRole> roles;

        /// <summary>
        /// 单体
        /// </summary>
        public static PortalService Instance
        {
            get
            {
                if (ps == null)
                {
                    ps = new PortalService();
                }

                return ps;
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 是否已初始化
        /// </summary>
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        /// <summary>
        /// 获取Provider
        /// </summary>
        private IPortalServiceProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    throw new Exception("请确认已初始化PortalService");
                }

                return _provider;
            }
        }

        /// <summary>
        /// 用户上下文列表
        /// </summary>
        private Dictionary<string, UserContext> UserContextList
        {
            get
            {
                if (userContextList == null)
                {
                    userContextList = new Dictionary<string, UserContext>();
                }

                return userContextList;
            }
        }

        /// <summary>
        /// 系统上下文
        /// </summary>
        private SystemContext SystemContext
        {
            get
            {
                if (systemContext == null)
                {
                    systemContext = new SystemContext();
                }

                return systemContext;
            }
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        public static SystemInfo SystemInfo
        {
            get
            {
                return Instance.SystemContext.SystemInfo;
            }
        }

        /// <summary>
        /// 当前SessionID
        /// </summary>
        public static string CurrentUserSID
        {
            get
            {
                SysIdentity sysIdentity = Instance.Provider.GetCurrentSysIdentity();

                if (sysIdentity != null)
                {
                    return sysIdentity.UserSID;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static UserInfo CurrentUserInfo
        {
            get
            {
                SysIdentity sysIdentity = Instance.Provider.GetCurrentSysIdentity();

                if (sysIdentity != null && !String.IsNullOrEmpty(sysIdentity.UserSID))
                {
                    if (sysIdentity.UserInfo == null)
                    {
                        sysIdentity.UserInfo = Instance.Provider.GetUserInfo(sysIdentity.UserSID);
                    }

                    return sysIdentity.UserInfo;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取当前用户上下文
        /// </summary>
        public static UserContext CurrentUserContext
        {
            get
            {
                try
                {
                    lock (uclslocker)
                    {
                        if (Instance.UserContextList.ContainsKey(CurrentUserInfo.LoginName))
                        {
                            return Instance.UserContextList[CurrentUserInfo.LoginName];
                        }
                        else if (!String.IsNullOrEmpty(CurrentUserSID) && !String.IsNullOrEmpty(CurrentUserInfo.LoginName))
                        {
                            UserContext uc = new UserContext(CurrentUserSID);

                            Instance.UserContextList.Add(CurrentUserInfo.LoginName, uc);

                            return uc;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception) { return null; }
            }
        }

        /// <summary>
        /// 检查当前用户是否登录
        /// </summary>
        public static void CheckLogon()
        {
            if (!IsUserLogon())
            {
                // 没有权限访问，先登出，然后抛出异常。
                Logout();

                throw new System.Security.SecurityException("登录超时或没有登录，请重新登录。");
            }
        }

        /// <summary>
        /// 检查当前页是否有权限
        /// </summary>
        /// <returns></returns>
        public static void CheckAuth(string pgKey)
        {
            string upperPgKey = pgKey.ToUpper();

            if (((CurrentUserContext.AccessibleApplications.Select(app => app.Code != null && app.Code.ToUpper() == upperPgKey).Count() > 0)
                    || (CurrentUserContext.AccessibleModules.Select(app => app.Code != null && app.Code.ToUpper() == upperPgKey).Count() > 0)))
            {
                Instance.Provider.RefreshSession(CurrentUserSID);
            }
            else
            {
                throw new System.Security.SecurityException("您没有使用该系统的权限，请联系管理员。");
            }
        }

        /// <summary>
        /// 获取系统所有应用
        /// </summary>
        public static IList<SysApplication> Applications
        {
            get
            {
                if (Instance.applications == null)
                {
                    Instance.RefreshModules();
                }

                return Instance.applications;
            }
        }

        /// <summary>
        /// 获取系统所有模块
        /// </summary>
        public static IList<SysModule> Modules
        {
            get
            {
                if (Instance.modules == null)
                {
                    Instance.RefreshModules();
                }

                return Instance.modules;
            }
        }

        /// <summary>
        /// 获取系统所有权限
        /// </summary>
        public static IList<SysAuth> Auths
        {
            get
            {
                if (Instance.auths == null)
                {
                    Instance.RefreshModules();
                }

                return Instance.auths;
            }
        }

        /// <summary>
        /// 获取系统所有角色
        /// </summary>
        public static IList<SysRole> Roles
        {
            get
            {
                if (Instance.roles == null)
                {
                    Instance.RefreshModules();
                }

                return Instance.roles;
            }
        }

        /// <summary>
        /// 获取系统所有组
        /// </summary>
        public static IList<SysGroup> Groups
        {
            get
            {
                if (Instance.groups == null)
                {
                    Instance.RefreshModules();
                }

                return Instance.groups;
            }
        }

        /// <summary>
        /// 刷新系统模块，组，人员等
        /// </summary>
        public static void RefreshSysModules()
        {
            Instance.RefreshModules();
        }

        #endregion

        #region 构造函数

        protected PortalService()
        {
            systemContext = new SystemContext();

            userContextList = new Dictionary<string, UserContext>();

            RefreshModules();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 初始化PortalService
        /// </summary>
        public static bool Initialize(IPortalServiceProvider provider)
        {
            if (Instance.isInitialized)
            {
                throw new Exception("PortalService已初始化");
            }
            else
            {
                Instance.isInitialized = true;
                Instance._provider = provider;
            }

            return Instance.isInitialized;
        }

        public static UserInfo GetUserInfo(string sid)
        {
            return Instance.Provider.GetUserInfo(sid);
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <returns></returns>
        public static void Logout()
        {
            if (!String.IsNullOrEmpty(CurrentUserSID))
            {
                Instance.Provider.RefreshSession(CurrentUserSID);
            }

            if (CurrentUserInfo != null)
            {
                string loginName = CurrentUserInfo.LoginName;

                lock (uclslocker)
                {
                    if (!String.IsNullOrEmpty(loginName) && Instance.UserContextList.ContainsKey(loginName))
                    {
                        Instance.UserContextList.Remove(loginName);
                    }
                }
            }

            Instance.Provider.Logout();
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public static bool IsUserLogon()
        {
            bool isLogon = (!String.IsNullOrEmpty(CurrentUserSID) && GetUserSessionState(CurrentUserSID) == UserSessionState.Valid);

            return isLogon;
        }

        /// <summary>
        /// 获取用户Session状态
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static UserSessionState GetUserSessionState(string sid)
        {
            return Instance.Provider.GetUserSessionState(sid);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string AuthUser(string uname, string pwd)
        {
            return AuthUser(uname, pwd, false);
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string AuthUser(string uname, string pwd, bool passwordEncrypted)
        {
            IAuthPackage authPackage = new AuthPackage { LoginName = uname, Password = pwd, PasswordEncrypted = passwordEncrypted };

            string sid = Instance.Provider.AuthenticateUser(authPackage);

            if (Instance.UserContextList.ContainsKey(uname))
            {
                Instance.UserContextList.Remove(uname);
            }

            UserContext context = Instance.GetUserContext(sid);
            Instance.UserContextList.Add(uname, context);

            return sid;
        }

        /// <summary>
        /// 检查用户状态
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool CheckUserSession()
        {
            if (!String.IsNullOrEmpty(CurrentUserSID))
            {
                return CheckUserSession(CurrentUserSID);
            }

            return false;
        }

        /// <summary>
        /// 检查用户状态
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>j
        public static bool CheckUserSession(string sid)
        {
            if (!String.IsNullOrEmpty(sid))
            {
                return Instance.Provider.GetUserSessionState(sid) == UserSessionState.Valid;
            }

            return false;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 刷新应用模块
        /// </summary>
        private void RefreshModules()
        {
            applications = new List<SysApplication>(SysApplication.FindAll());
            modules = new List<SysModule>(SysModule.FindAll(Expression.Eq(SysModule.Prop_Status, 1)));

            auths = new List<SysAuth>(SysAuth.FindAll());
            roles = new List<SysRole>(SysRole.FindAll());
            groups = new List<SysGroup>(SysGroup.FindAll());
        }

        /// <summary>
        /// 获取人员上下文
        /// </summary>
        /// <returns></returns>
        private UserContext GetUserContext(string sid)
        {
            UserContext context = new UserContext(sid);

            return context;
        }

        #endregion
    }

    // 系统信息
    public class SystemContext
    {
        #region 私有变量

        private Hashtable _tag;

        private SystemInfo _systemInfo;

        #endregion

        #region 属性

        /// <summary>
        /// 扩展数据
        /// </summary>
        public Hashtable ExtData
        {
            get
            {
                if (_tag == null)
                {
                    _tag = new Hashtable();
                }

                return _tag;
            }
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        public SystemInfo SystemInfo
        {
            get
            {
                return this._systemInfo;
            }
        }

        #endregion

        #region 构造函数

        public SystemContext()
        {
            _systemInfo = new SystemInfo();
        }

        #endregion
    }

    /// <summary>
    /// 用户上下文
    /// </summary>
    public class UserContext
    {
        #region 成员

        private UserInfo userInfo = null;

        private IList<SysAuth> auths = null;

        private IList<SysGroup> groups = null;
        private IList<SysRole> roles = null;

        private IList<SysApplication> apps = null;
        private IList<SysModule> mdls = null;

        private Hashtable _tag;

        #endregion

        #region 构造函数

        public UserContext(string sid)
        {
            if (!String.IsNullOrEmpty(sid))
            {
                userInfo = PortalService.GetUserInfo(sid);
            }

            if (userInfo == null)
            {
                throw new MessageException("获取用户上下文失败！请重新登录。");
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 系统用户
        /// </summary>
        private SysUser SysUser
        {
            get
            {
                return SysUser.Get(UserInfo.LoginName);
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get { return userInfo; }
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        public IList<SysAuth> Auths
        {
            get
            {
                if (auths == null)
                {
                    
                    IEnumerable<string> ids = SysUser.AllAuth.Select(v => v.AuthID);
                    auths = PortalService.Auths.Where(v => ids.Contains(v.AuthID)).ToList();
                }

                return auths;
            }
        }

        /// <summary>
        /// 获取用户所属组
        /// </summary>
        public IList<SysGroup> Groups
        {
            get
            {
                if (groups == null)
                {
                    IEnumerable<string> ids = SysUser.AllGroup.Select(v => v.GroupID);
                    groups = PortalService.Groups.Where(v => ids.Contains(v.GroupID)).ToList();
                }

                return groups;
            }
        }

        /// <summary>
        /// 获取用户所属角色
        /// </summary>
        public IList<SysRole> Roles
        {
            get
            {
                if (roles == null)
                {
                    IEnumerable<string> ids = SysUser.AllRole.Select(v => v.RoleID);
                    roles = PortalService.Roles.Where(v => ids.Contains(v.RoleID)).ToList();
                }

                return roles;
            }
        }

        /// <summary>
        /// 用户可访问的应用
        /// </summary>
        public IList<SysApplication> AccessibleApplications
        {
            get
            {
                if (apps == null)
                {
                    IEnumerable<string> ids = SysAuth.GetApplicationsByAuths(this.Auths).Select(v => v.ApplicationID);
                    apps = PortalService.Applications.Where(v => ids.Contains(v.ApplicationID)).ToList();
                }
                return apps;
            }
        }

        /// <summary>
        /// 用户可访问的模块
        /// </summary>
        public IList<SysModule> AccessibleModules
        {
            get
            {
                if (mdls == null)
                {
                    IEnumerable<string> ids = SysAuth.GetModulesByAuths(this.Auths).Select(v => v.ModuleID);
                    mdls = PortalService.Modules.Where(v => ids.Contains(v.ModuleID)).ToList();
                }

                return mdls;
            }
        }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public Hashtable ExtData
        {
            get
            {
                if (_tag == null)
                {
                    _tag = new Hashtable();
                }

                return _tag;
            }
        }

        #endregion

        #region 静态函数

        #endregion
    }
}

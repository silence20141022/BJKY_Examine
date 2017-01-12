// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;

namespace Aim.Portal.Model
{
    /// <summary>
    /// 系统用户类
    /// </summary>
    [Serializable]
    public partial class SysUser
    {
        #region 私有成员

        [NonSerialized]
        private IList<SysAuth> _AllAuth = null;

        [NonSerialized]
        private IList<SysGroup> _AllGroup = null;

        [NonSerialized]
        private IList<SysRole> _AllRole = null;

        [NonSerialized]
        private IList<SysGroup> _Group = new List<SysGroup>();

        [NonSerialized]
        private IList<SysAuth> _Auth = new List<SysAuth>();

        [NonSerialized]
        private IList<SysAuth> _AuthNo = new List<SysAuth>();

        [NonSerialized]
        private IList<SysRole> _Role = new List<SysRole>();

        #endregion

        #region 成员属性

        /// <summary>
        /// 所有属性
        /// </summary>
        [JsonIgnore]
        public IList<SysAuth> AllAuth
        {
            get
            {
                if (_AllAuth == null)
                {
                    _AllAuth = this.RetrieveAllAuth();
                }

                return _AllAuth;
            }
        }

        /// <summary>
        /// 所有属性
        /// </summary>
        [JsonIgnore]
        public IList<SysGroup> AllGroup
        {
            get
            {
                if (_AllGroup == null)
                {
                    _AllGroup = this.RetrieveAllGroup();
                }

                return _AllGroup;
            }
        }

        /// <summary>
        /// 所有角色
        /// </summary>
        [JsonIgnore]
        public IList<SysRole> AllRole
        {
            get
            {
                if (_AllRole == null)
                {
                    _AllRole = this.RetrieveAllRole();
                }

                return _AllRole;
            }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysGroup), Table = "SysUserGroup", ColumnRef = "GroupID", ColumnKey = "UserID", Lazy = true)]
        public IList<SysGroup> Group
        {
            get
            {
                int i = _Group.Count;
                return _Group;
            }
            set { _Group = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysAuth), Table = "SysUserPermission", ColumnRef = "AuthID", ColumnKey = "UserID", Lazy = true)]
        public IList<SysAuth> Auth
        {
            get
            {
                int i = _Auth.Count;
                return _Auth;
            }
            set { _Auth = value; }
        }


        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysAuth), Table = "SysUserPermissionNo", ColumnRef = "AuthID", ColumnKey = "UserID", Lazy = true)]
        public IList<SysAuth> AuthNo
        {
            get
            {
                int i = _AuthNo.Count;
                return _AuthNo;
            }
            set { _AuthNo = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysUserRole", ColumnRef = "RoleID", ColumnKey = "UserID", Lazy = true)]
        public IList<SysRole> Role
        {
            get
            {
                int i = _Role.Count;
                return _Role;
            }
            set { _Role = value; }
        }

        #endregion

        #region 重载

        public override void Create()
        {
            this.CreateDate = DateTime.Now;

            base.Create();
        }

        public override void CreateAndFlush()
        {
            this.CreateDate = DateTime.Now;

            base.CreateAndFlush();
        }

        public override void Update()
        {
            this.LastModifiedDate = DateTime.Now;

            base.Update();
        }

        public override void UpdateAndFlush()
        {
            this.LastModifiedDate = DateTime.Now;

            base.UpdateAndFlush();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("LoginName"))
            {
                throw new RepeatedKeyException("存在重复的 登陆名 “" + this.LoginName + "”");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            // 事务开始
            this.CreateAndFlush();
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            base.Delete();
        }

        /// <summary>
        /// 是否拥有某权限
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public bool HasPermission(SysAuth auth)
        {
            return AllAuth.Contains(auth);
        }

        /// <summary>
        /// 是否是某角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsRole(SysRole role)
        {
            return this.Role.Contains(role);
        }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            foreach (SysRole role in this.Role)
            {
                if (role.RoleID == "0")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否属于某个组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool IsGroup(SysGroup group)
        {
            return this.Group.Contains(group);
        }

        /// <summary>
        /// 获取用户所属组(/*当你属于某个子组是，你将属于所有父组（暂不考虑）*/)
        /// </summary>
        /// <returns></returns>
        public IList<SysGroup> RetrieveAllGroup()
        {
            IList<SysGroup> groups = this.Group;

            List<string> groupIDs = new List<string>();
            foreach (SysGroup sg in groups)
            {
                if (sg.Path != null)
                {
                    string[] paths = (sg.Path + "." + sg.GroupID).Split('.');
                    foreach (string p in paths)
                    {
                        if (!groupIDs.Contains(p))
                        {
                            groupIDs.Add(p);
                        }
                    }
                }
            }

            ICriterion hqlCriterion = Expression.In("GroupID", groupIDs);

            groups = (SysGroup[])SysGroup.FindAll(hqlCriterion);

            return groups;
        }

        /// <summary>
        /// 获取用户所属的所有角色(自带角色+组角色)
        /// </summary>
        /// <returns></returns>
        public IList<SysRole> RetrieveAllRole()
        {
            IList<SysRole> roles = new List<SysRole>();

            foreach (SysRole tRole in this.Role)
            {
                roles.Add(tRole);
            }
            //by huo 
            foreach (SysGroup tGroup in this.AllGroup)
            {
                if (tGroup.Type == 3)
                {
                    if (SysRole.FindAllByProperties(SysRole.Prop_Name, tGroup.Name).Length > 0)
                    {
                        SysRole role = SysRole.FindAllByProperties(SysRole.Prop_Name, tGroup.Name)[0];
                        roles.Add(role);
                    }
                }
                foreach (SysRole tRole in tGroup.Role)
                {
                    if (!roles.Contains(tRole))
                    {
                        roles.Add(tRole);
                    }
                }
            }

            return roles;
        }

        /// <summary>
        /// 获取用户所拥有所有权限(自带权限+自带角色权限+组权限)
        /// </summary>
        /// <returns></returns>
        public IList<SysAuth> RetrieveAllAuth()
        {
            IList<SysAuth> auths = new List<SysAuth>();

            foreach (SysAuth tAuth in this.Auth)
            {
                auths.Add(tAuth);
            }

            foreach (SysRole tRole in this.AllRole)
            {
                foreach (SysAuth tAuth in tRole.Auth)
                {
                    if (!auths.Contains(tAuth))
                    {
                        auths.Add(tAuth);
                    }
                }
            }
            foreach (SysGroup tGroup in this.AllGroup)
            {
                foreach (SysAuth tAuth in tGroup.Auth)
                {
                    if (!auths.Contains(tAuth))
                    {
                        auths.Add(tAuth);
                    }
                }
            }
            foreach (SysAuth nAuth in this.AuthNo)
            {
                if (auths.Contains(nAuth) && !nAuth.IsTop)
                    auths.Remove(nAuth);
            }

            return auths;
        }

        /// <summary>
        /// 获取可访问的应用
        /// </summary>
        /// <returns></returns>
        public IList<SysApplication> GetAccessibleApplications()
        {
            return SysAuth.GetApplicationsByAuths(this.AllAuth);
        }

        /// <summary>
        /// 获取可访问呢模块
        /// </summary>
        /// <returns></returns>
        public IList<SysModule> GetAccessibleModules()
        {
            return SysAuth.GetModulesByAuths(this.AllAuth);
        }

        #endregion

        #region 静态函数

        /// <summary>
        /// 根据登录名获取SysUser
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static SysUser Get(string loginName)
        {
            if (String.IsNullOrEmpty(loginName) || !SysUser.Exists(Expression.Eq(SysUser.Prop_LoginName, loginName)))
            {
                return null;
            }
            else
            {
                return SysUser.FindFirstByProperties(SysUser.Prop_LoginName, loginName);
            }
        }

        /// <summary>
        /// 由中文名生成登录名
        /// </summary>
        /// <returns></returns>
        public static string NewLoginNameFromChineseName(string chineseName)
        {
            string pyName = StringHelper.ConvertChineseToPY(chineseName);
            string loginName = pyName;

            // 已经存在此用户名 (因为重名概率较小，这里采用循环方式操作)
            if (SysUser.Exists("LoginName=?", loginName))
            {
                int i = 1;

                loginName = String.Format("{0}_{1}", loginName, i);
                while (SysUser.Exists("LoginName=?", loginName))
                {
                    loginName = String.Format("{0}_{1}", loginName, i);
                    i++;
                }
            }

            return loginName;
        }

        #endregion

    } // SysUser
}


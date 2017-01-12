// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [Serializable]
    public partial class SysAuth
    {
        #region 私有变量

        [NonSerialized]
        private SysAuth _Parent;

        [NonSerialized]
        private IList<SysAuth> _Children;

        private bool? _IsLeaf = null;

        [NonSerialized]
        private IList<SysGroup> _Group = new List<SysGroup>();

        [NonSerialized]
        private IList<SysRole> _Role = new List<SysRole>();

        [NonSerialized]
        private IList<SysUser> _User = new List<SysUser>();

        [NonSerialized]
        private IList<SysUser> _UserNo = new List<SysUser>();

        #endregion

        #region 成员属性

        /// <summary>
        /// 此组的所在组（父组）
        /// </summary>
        [JsonIgnore]
        public SysAuth Parent
        {
            get
            {
                if (!IsTop)
                {
                    _Parent = SysAuth.Find(this.ParentID);
                }

                return _Parent;
            }
        }

        /// <summary>
        /// 是否顶层组（没有父节点）
        /// </summary>
        public bool IsTop
        {
            get
            {
                return String.IsNullOrEmpty(this.ParentID);
            }
        }

        /// <summary>
        /// 是否根模块（没有子节点）
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                if (_IsLeaf == null)
                {
                    bool isLeaf = !SysAuth.Exists("ParentID = ?", this.AuthID);
                    _IsLeaf = isLeaf;
                }

                return _IsLeaf.GetValueOrDefault();
            }
        }

        /// <summary>
        /// 此组的所拥有的子模块
        /// </summary>
        [JsonIgnore]
        public IList<SysAuth> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = SysAuth.FindAllByProperties("ParentID", this.ModuleID);
                }

                return _Children;
            }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysGroup), Table = "SysGroupPermission", ColumnRef = "GroupID", ColumnKey = "AuthID", Lazy = true)]
        public IList<SysGroup> Group
        {
            get { return _Group; }
            set { _Group = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysRolePermission", ColumnRef = "RoleID", ColumnKey = "AuthID", Lazy = true)]
        public IList<SysRole> Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUserPermission", ColumnRef = "UserID", ColumnKey = "AuthID", Lazy = true)]
        public IList<SysUser> User
        {
            get { return _User; }
            set { _User = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUserPermissionNo", ColumnRef = "UserID", ColumnKey = "AuthID", Lazy = true)]
        public IList<SysUser> UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        #endregion

        #region 公共方法
        
        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            // 事务开始
            this.CreateAndFlush();
        }

        /// <summary>
        /// 修改模块(同时修改类型为2，对应权限的名称)
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        public void DoDelete()
        {
            base.Delete();
        }

        /// <summary>
        /// 由系统应用创建权限
        /// </summary>
        public void CreateByApplication(string appid)
        {
            SysApplication app = SysApplication.Find(appid);

            this.CreateByApplication(app);
        }

        /// <summary>
        /// 由系统应用创建权限
        /// </summary>
        public void CreateByApplication(SysApplication app)
        {
            this.SetDataByApplication(app);  // 根据对应的SysApplication设置数据

            this.DoCreate();
        }

        /// <summary>
        /// 由系统应用更新权限
        /// </summary>
        public void UpdateByApplication(string appid)
        {
            SysApplication app = SysApplication.Find(appid);

            this.UpdateByApplication(app);
        }

        /// <summary>
        /// 由系统应用更新权限
        /// </summary>
        public void UpdateByApplication(SysApplication app)
        {
            this.SetDataByApplication(app);  // 根据对应的SysApplication设置数据

            this.DoUpdate();
        }

        /// <summary>
        /// 由系统模块创建权限
        /// </summary>
        public void CreateByModule(string mdlid)
        {
            SysModule mdl = SysModule.Find(mdlid);

            this.CreateByModule(mdl);
        }

        /// <summary>
        /// 由系统模块创建权限
        /// </summary>
        public void CreateByModule(SysModule mdl)
        {
            this.SetDataByModule(mdl);  // 根据对应的SysModule设置数据

            this.DoCreate();
        }

        /// <summary>
        /// 由系统模块更新权限
        /// </summary>
        public void UpdateByModule(string mdlid)
        {
            SysModule mdl = SysModule.Find(mdlid);
            this.UpdateByModule(mdl);
        }

        /// <summary>
        /// 由系统模块更新权限
        /// </summary>
        public void UpdateByModule(SysModule mdl)
        {
            this.SetDataByModule(mdl);  // 根据对应的SysModule设置数据

            this.DoUpdate();
        }

        /// <summary>
        /// 获取相应层级子级
        /// </summary>
        /// <returns></returns>
        public SysAuth[] GetSubs()
        {
            int currLevel = this.PathLevel.GetValueOrDefault();

            return SysAuth.FindAll("PathLevel > ?", currLevel);
        }

        /// <summary>
        /// 获取相应层级子级
        /// </summary>
        /// <param name="level">当前层级向下层级</param>
        /// <returns></returns>
        public SysAuth[] GetSubs(int level)
        {
            int currLevel = this.PathLevel.GetValueOrDefault();
            int maxLevel = this.PathLevel.GetValueOrDefault() + level;

            return SysAuth.FindAll("PathLevel > ? AND PathLevel <= ?", currLevel, maxLevel);
        }

        /// <summary>
        /// 添加顶层模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsTops()
        {
            this.Path = null;
            this.PathLevel = 0;

            this.DoCreate();
        }

        /// <summary>
        /// 添加兄弟模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSibs(string sibID)
        {
            SysAuth sib = SysAuth.Find(sibID);

            CreateAsSib(sib);
        }

        /// <summary>
        /// 添加兄弟模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(SysAuth sib)
        {
            this.ParentID = sib.ParentID;
            this.Path = sib.Path;
            this.PathLevel = sib.PathLevel;

            this.DoCreate();
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(string parentID)
        {
            SysAuth parent = SysAuth.Find(parentID);

            CreateAsSub(parent);
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(SysAuth parent)
        {
            this.ParentID = parent.AuthID;
            this.Path = String.Format("{0}.{1}", (parent.Path == null ? String.Empty : parent.Path), parent.AuthID);
            this.PathLevel = parent.PathLevel + 1;

            this.DoCreate();
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

        /// <summary>
        /// 获取可访问应用
        /// </summary>
        /// <returns></returns>
        public static IList<SysApplication> GetApplicationsByAuths(IEnumerable<SysAuth> auths)
        {
            IEnumerable<string> appids = auths.Where(v => v.Type == 1 && String.IsNullOrEmpty(v.ModuleID) && !String.IsNullOrEmpty(v.Data)).Select(v => v.Data);

            return SysApplication.FindAllByPrimaryKeys(appids.ToArray());
        }

        /// <summary>
        /// 获取可访问模块
        /// </summary>
        /// <returns></returns>
        public static IList<SysModule> GetModulesByAuths(IList<SysAuth> auths)
        {
            IEnumerable<string> mdlids = auths.Where(v => v.Type == 1 && !String.IsNullOrEmpty(v.ModuleID)).Select(v => v.ModuleID);

            return SysModule.FindAllByPrimaryKeys(mdlids.ToArray());
        }

        #endregion

        #region 私有函数

        private void SetDataByApplication(SysApplication app)
        {
            this.Name = app.Name;
            this.Code = String.Format("AUTH_APP_{0}", app.Code);
            this.Data = app.ApplicationID;
            this.Path = ".";
            this.PathLevel = 0;
            this.ParentID = null;
            this.Type = 1;  // 1为系统入口权限
            this.Description = String.Format("应用 {0} 访问权限", app.Name);
        }

        private void SetDataByModule(SysModule mdl)
        {
            SysAuth[] pAuths;
            SysAuth pAuth = null;

            // 获取父权限（与系统模块对应）
            if (!String.IsNullOrEmpty(mdl.ParentID))
            {
                pAuths = SysAuth.FindAllByProperties("ModuleID", mdl.ParentID);
            }
            else
            {
                pAuths = SysAuth.FindAllByProperties("Data", mdl.ApplicationID);
            }

            if (pAuths.Length > 0)
            {
                pAuth = pAuths[0];
                this.ParentID = pAuth.AuthID;
                this.Path = String.Format("{0}.{1}", pAuth.Path, pAuth.AuthID);
                this.PathLevel = (pAuth.PathLevel == null ? 0 : (pAuth.PathLevel + 1));
            }

            this.Name = mdl.Name;
            this.Code = String.Format("AUTH_MDL_{0}", mdl.Code);
            this.Type = 1;  // 1为系统入口权限
            this.SortIndex = mdl.SortIndex;
            this.ModuleID = mdl.ModuleID;
            this.Description = String.Format("模块 {0} 访问权限", mdl.Name);
        }

        #endregion
    } // SysAuth
}


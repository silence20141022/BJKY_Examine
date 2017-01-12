// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [Serializable]
	public partial class SysRole
    {
        #region 私有成员

        [NonSerialized]
        private IList<SysGroup> _Group = new List<SysGroup>();

        [NonSerialized]
        private IList<SysAuth> _Auth = new List<SysAuth>();

        [NonSerialized]
        private IList<SysUser> _User = new List<SysUser>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 是否拥有某权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool HasPermission(SysAuth auth)
        {
            return this.Auth.Contains(auth);
        }

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("Code"))
            {
                throw new RepeatedKeyException("存在重复的 编号 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();
            this.CreateDate = DateTime.Now;

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
            this.LastModifiedDate = DateTime.Now;

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            base.Delete();
        }

        #endregion

        #region 成员属性

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysAuth), Table = "SysRolePermission", ColumnRef = "AuthID", ColumnKey = "RoleID", Lazy = true)]
        public IList<SysAuth> Auth
        {
            get { return _Auth; }
            set { _Auth = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysGroup), Table = "SysGroupRole", ColumnRef = "GroupID", ColumnKey = "RoleID", Lazy = true)]
        public IList<SysGroup> Group
        {
            get { return _Group; }
            set { _Group = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUserRole", ColumnRef = "UserID", ColumnKey = "RoleID", Lazy = true)]
        public IList<SysUser> User
        {
            get { return _User; }
            set { _User = value; }
        }

        #endregion

        #region 重载

        /// <summary>
        /// 创建模块(将同时生成一条Type为2的权限信息)(应当做事务处理)
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;

            base.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateAndFlush()
        {
            this.CreateDate = DateTime.Now;

            base.CreateAndFlush();
        }

        /// <summary>
        /// 创建模块(将同时生成一条Type为2的权限信息)(应当做事务处理)
        /// </summary>
        public override void Update()
        {
            this.LastModifiedDate = DateTime.Now;

            base.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void UpdateAndFlush()
        {
            this.LastModifiedDate = DateTime.Now;

            base.UpdateAndFlush();
        }

        /// <summary>
        /// 创建模块(将同时生成一条Type为2的权限信息)(应当做事务处理)
        /// </summary>
        public override void Save()
        {
            if (this.RoleID != null)
            {
                this.LastModifiedDate = DateTime.Now;
            }
            else
            {
                this.CreateDate = DateTime.Now;
            }

            base.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveAndFlush()
        {
            if (this.RoleID != null)
            {
                this.LastModifiedDate = DateTime.Now;
            }
            else
            {
                this.CreateDate = DateTime.Now;
            }

            base.SaveAndFlush();
        }

        #endregion

    } // SysUser
}


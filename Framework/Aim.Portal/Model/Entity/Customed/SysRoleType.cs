// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [Serializable]
    public partial class SysRoleType
    {
        #region 私有变量

        private bool? _HasRole = null;

        #endregion

        #region 成员属性

        /// <summary>
        /// 此类型是否拥有权限
        /// </summary>
        public bool HasRole
        {
            get
            {
                if (_HasRole == null)
                {
                    _HasRole = SysRole.Exists("Type = ?", this.RoleTypeID);
                }

                return _HasRole.GetValueOrDefault();
            }
        }

        #endregion

        #region 重载

        /// <summary>
        /// 创建
        /// </summary>
        public override void Create()
        {
            base.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateAndFlush()
        {
            base.CreateAndFlush();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void UpdateAndFlush()
        {
            base.UpdateAndFlush();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            base.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveAndFlush()
        {
            base.SaveAndFlush();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("Name"))
            {
                throw new RepeatedKeyException("已存在 角色名 “" + this.Name + "”");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

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
            ICriterion crit = Expression.Eq("Type", this.RoleTypeID);
            if (SysRole.Exists(crit))
            {
                throw new Exception("存在此类型的角色，不能执行删除操作。");
            }

            base.Delete();
        }

        #endregion

    } // SysUser
}


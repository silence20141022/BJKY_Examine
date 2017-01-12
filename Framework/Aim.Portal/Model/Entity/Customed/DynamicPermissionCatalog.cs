
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
    /// 自定义实体类
    /// </summary>
    [Serializable]
    public partial class DynamicPermissionCatalog
    {
        /// <summary>
        /// 系统固有的授权类型
        /// </summary>
        public enum SysCatalogEnum
        {
            SYS_USER,
            SYS_GROUP,
            SYS_ROLE
        }

        #region 成员

        #endregion

        #region 成员属性
        
        #endregion

        #region 重载

        public override void Create()
        {
            this.CreatedDate = DateTime.Now;

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                this.SetFullEditStatus();
            }

            base.Create();
        }

        public override void CreateAndFlush()
        {
            this.CreatedDate = DateTime.Now;

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                this.SetFullEditStatus();
            }

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

        public override void Save()
        {
            if (this.DynamicPermissionCatalogID != null)
            {
                this.LastModifiedDate = DateTime.Now;
            }
            else
            {
                this.CreatedDate = DateTime.Now;
            }

            base.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveAndFlush()
        {
            if (this.DynamicPermissionCatalogID != null)
            {
                this.LastModifiedDate = DateTime.Now;
            }
            else
            {
                this.CreatedDate = DateTime.Now;
            }

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
            if (!this.IsPropertyUnique("Code"))
            {
                throw new RepeatedKeyException("存在重复的 编码 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            bool isEditable = this.Editable;
            this.SetFullEditStatus();
            this.Editable = isEditable;

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
            // 同时删除相关授权
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    DynamicPermission[] permissions = DynamicPermission.FindAllByProperty("CatalogCode", this.Code);
                    foreach (DynamicPermission tpermission in permissions)
                    {
                        tpermission.DoDelete();
                    }

                    base.Delete();

                    trans.VoteCommit();
                }
                catch (Exception ex)
                {
                    trans.VoteRollBack();

                    throw ex;
                }
            }
        }

        #endregion

    } // DynamicPermissionCatalog
}



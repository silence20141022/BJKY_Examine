
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
	public partial class DynamicOperation
    {
        #region 成员变量

        #endregion

        #region 成员属性
        
        #endregion

        #region 重载

        public override void Create()
        {
            base.Create();
        }

        public override void CreateAndFlush()
        {
            base.CreateAndFlush();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void UpdateAndFlush()
        {
            base.UpdateAndFlush();
        }
        
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
            /*if (!this.IsPropertyUnique("UniqueKey"))
            {
                throw new RepeatedKeyException("存在重复的 UniqueKey “" + this.UniqueKey + "”");
            }*/
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                this.SetFullEditStatus();
            }
            else
            {
                bool isEditable = this.Editable;
                this.SetFullEditStatus();
                this.Editable = isEditable;
            }
            
            this.CreatedDate = DateTime.Now;

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

    } // DynamicOperation
}



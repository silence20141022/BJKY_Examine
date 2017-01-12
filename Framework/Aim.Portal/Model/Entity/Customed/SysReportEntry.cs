
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
	public partial class SysReportEntry
    {
        #region 私有成员

        #endregion

        #region 成员属性
        
        #endregion

        #region 重载

        public override void Create()
        {
            this.CreateTime = DateTime.Now;

            base.Create();
        }

        public override void CreateAndFlush()
        {
            this.CreateTime = DateTime.Now;

            base.CreateAndFlush();
        }

        public override void Update()
        {
            this.ModifyTime = DateTime.Now;

            base.Update();
        }

        public override void UpdateAndFlush()
        {
            this.ModifyTime = DateTime.Now;

            base.UpdateAndFlush();
        }
        
        public override void Save()
        {
            if (this.Id != null)
            {
                this.ModifyTime = DateTime.Now;
            }
			else
            {
                this.CreateTime = DateTime.Now;
            }

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

        #endregion

    } // SysReportEntry
}



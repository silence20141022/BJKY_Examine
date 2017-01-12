using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;

namespace Aim.Portal.Model
{
    [Serializable]
    public partial class SysApplication
    {
        #region 私有变量

        [NonSerialized]
        private IList _Module = new List<SysModule>();

        private bool? _HasModule = null;

        #endregion

        #region 成员属性

        /// <summary>
        /// 是否有子模块
        /// </summary>
        public bool HasModule
        {
            get
            {
                if (_HasModule == null)
                {
                    return SysModule.Exists("ApplicationID = ?", this.ApplicationID);
                }

                return _HasModule.GetValueOrDefault();
            }
        }

        [JsonIgnore]
        [HasMany(typeof(SysModule), Table = "SysModule", ColumnKey = "ApplicationID", Cascade = ManyRelationCascadeEnum.All)]
        public IList Module
        {
            get { return _Module; }
            set { _Module = value; }
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
        /// 获取指定应用区间级别的模块
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IList<SysModule> GetModulesByLevel(int from, int to)
        {
            IList<SysModule> mdls = new List<SysModule>();

            if (from >= 0)
            {
                if (from == to)
                {
                    return SysModule.FindAllByProperty("PathLevel", from);
                }
                else if (from < to)
                {
                    DetachedCriteria crits = DetachedCriteria.For<SysModule>();

                    crits.Add(Expression.Eq("ApplicationID", this.ApplicationID));
                    crits.Add(Expression.Ge("PathLevel", from));
                    crits.Add(Expression.Le("PathLevel", to));

                    return SysModule.FindAll(crits);
                }
            }

            return mdls;
        }

        /// <summary>
        /// 获取相关权限
        /// </summary>
        /// <returns></returns>
        public SysAuth[] GetRelatedAuth()
        {
            // Type为1 代表应用权限
            SysAuth[] auths = SysAuth.FindAllByProperties("Type", 1, "Data", this.ApplicationID);
            return auths;
        }

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("Code"))
            {
                throw new RepeatedKeyException("存在重复的 模块键 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 创建应用(将同时生成一条Type为1的权限信息)
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    // 事务开始
                    this.CreateAndFlush();

                    SysAuth auth = new SysAuth();
                    auth.CreateByApplication(this);

                    trans.VoteCommit();
                }
                catch (Exception ex)
                {
                    trans.VoteRollBack();

                    throw ex;
                }
            }
        }

        /// <summary>
        /// 修改应用(同时修改类型为1，对应权限的名称)
        /// </summary>
        public void DoUpdate()
        {
            this.DoValidate();

            SysAuth[] auths = this.GetRelatedAuth();

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    this.UpdateAndFlush();

                    if (auths.Length > 0)
                    {
                        foreach (SysAuth auth in auths)
                        {
                            auth.UpdateByApplication(this);
                        }
                    }
                    else
                    {
                        SysAuth auth = new SysAuth();
                        auth.CreateByApplication(this);
                    }

                    trans.VoteCommit();
                }
                catch (Exception ex)
                {
                    trans.VoteRollBack();

                    throw ex;
                }
            }
        }

        /// <summary>
        /// 删除应用（同时删除类型为1, 对应的权限）
        /// </summary>
        public void DoDelete()
        {
            SysAuth[] auths = this.GetRelatedAuth();

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    foreach (SysAuth auth in auths)
                    {
                        auth.DoDelete();
                    }

                    this.Delete();

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

    }
}

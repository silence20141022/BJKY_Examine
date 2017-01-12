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
    public partial class SysModule
    {
        #region 私有变量

        [NonSerialized]
        private SysModule _Parent;

        [NonSerialized]
        private IList<SysModule> _Children;

        private bool? _IsLeaf = null;

        #endregion

        #region 成员属性
        
        /// <summary>
        /// 此组的所在组（父组）
        /// </summary>
        [JsonIgnore]
        public SysModule Parent
        {
            get
            {
                if (!IsTop)
                {
                    _Parent = SysModule.Find(this.ParentID);
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
                    ICriterion cirt = Expression.Eq("ParentID", this.ModuleID);
                    bool isLeaf = !SysModule.Exists(cirt);
                    _IsLeaf = isLeaf;
                }

                return _IsLeaf.GetValueOrDefault();
            }
        }

        /// <summary>
        /// 此组的所拥有的子模块
        /// </summary>
        [JsonIgnore]
        public IList<SysModule> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = SysModule.FindAllByProperties("ParentID", this.ModuleID);
                }

                return _Children;
            }
        }

        #endregion

        #region 重载

        /// <summary>
        /// 创建模块
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
            if (this.ModuleID != null)
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
            if (this.ModuleID != null)
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

        #region 公共方法

        /// <summary>
        /// 获取模块所属的应用
        /// </summary>
        /// <returns></returns>
        public SysApplication OwnerApplication()
        {
            if (!String.IsNullOrEmpty(ApplicationID))
            {
                return SysApplication.Find(this.ApplicationID);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取相关权限
        /// </summary>
        /// <returns></returns>
        public SysAuth[] GetRelatedAuth()
        {
            // Type为1 代表应用权限
            SysAuth[] auths = SysAuth.FindAllByProperties("Type", 1, "ModuleID", this.ModuleID);
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
                throw new RepeatedKeyException("存在重复的 模块编号 “" + this.Code + "”");
            }
        }

        /// <summary>
        /// 创建模块
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
                    auth.CreateByModule(this);

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
        /// 修改模块(同时修改类型为1，对应权限的名称)
        /// </summary>
        /// <returns></returns>
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
                            auth.UpdateByModule(this);
                        }
                    }
                    else
                    {
                        SysAuth auth = new SysAuth();
                        auth.CreateByModule(this);
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
        /// 删除模块
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

        /// <summary>
        /// 获取相应层级子模块
        /// </summary>
        /// <returns></returns>
        public SysModule[] GetSubs()
        {
            int currLevel = this.PathLevel.GetValueOrDefault();
            ICriterion crit = Expression.Gt("PathLevel", currLevel);

            return SysModule.FindAll(crit);
        }

        /// <summary>
        /// 获取相应层级子模块
        /// </summary>
        /// <param name="level">当前层级向下层级</param>
        /// <returns></returns>
        public SysModule[] GetSubs(int level)
        {
            int currLevel = this.PathLevel.GetValueOrDefault();
            int maxLevel = this.PathLevel.GetValueOrDefault() + level;

            DetachedCriteria crits = DetachedCriteria.For<SysModule>();

            crits.Add(Expression.Gt("PathLevel", currLevel));
            crits.Add(Expression.Le("PathLevel", maxLevel));

            return SysModule.FindAll(crits);
        }

        /// <summary>
        /// 添加顶层模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsTop()
        {
            this.Path = null;
            this.PathLevel = 0;

            this.DoCreate();
        }

        /// <summary>
        /// 添加兄弟模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(string sibID)
        {
            SysModule sib = SysModule.Find(sibID);

            CreateAsSib(sib);
        }

        /// <summary>
        /// 添加兄弟模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(SysModule sib)
        {
            this.ParentID = sib.ParentID;
            this.ApplicationID = sib.ApplicationID;
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
            SysModule parent = SysModule.Find(parentID);

            CreateAsSub(parent);
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(SysModule parent)
        {
            this.ParentID = parent.ModuleID;
            this.ApplicationID = parent.ApplicationID;
            this.Path = String.Format("{0}.{1}", (parent.Path == null ? String.Empty : parent.Path), parent.ModuleID);
            this.PathLevel = parent.PathLevel + 1;

            this.DoCreate();
        }

        #endregion

    } // SysUser
}


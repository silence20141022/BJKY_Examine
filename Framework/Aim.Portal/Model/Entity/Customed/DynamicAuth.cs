
using System;
using System.Linq;
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
	public partial class DynamicAuth
    {
        #region 私有成员

        public const string GranteStatusString = "G";

        [NonSerialized]
        private DynamicAuth _Parent;

        [NonSerialized]
        private IList<DynamicAuth> _Children;

        [NonSerialized]
        private DynamicAuthCatalog _Catalog;

        private bool? _IsLeaf = null;

        #endregion

        #region 成员属性

        /// <summary>
        /// 此组的所在组（父组）
        /// </summary>
        [JsonIgnore]
        public DynamicAuth Parent
        {
            get
            {
                if (!IsTop)
                {
                    _Parent = DynamicAuth.Find(this.ParentID);
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
                    DetachedCriteria crits = DetachedCriteria.For<DynamicAuth>();
                    crits.Add(Expression.Eq("ParentID", this.DynamicAuthID));
                    crits.Add(Expression.IsNotNull("EditStatus"));
                    crits.Add(Expression.Not(Expression.Eq("EditStatus", String.Empty)));
                    bool isLeaf = !DynamicAuth.Exists(crits);

                    _IsLeaf = isLeaf;
                }

                return _IsLeaf.GetValueOrDefault();
            }
        }

        /// <summary>
        /// 是否允许授权(不允许手工创建，一般为子节点)
        /// </summary>
        public bool Grantable
        {
            get { return this.CheckEditStatus(GranteStatusString); }
            set
            {
                if (value)
                {
                    this.SetEditStatus(GranteStatusString);
                }
                else
                {
                    this.RemoveEditStatus(GranteStatusString);
                }
            }
        }

        /// <summary>
        /// 此组的所拥有的子模块
        /// </summary>
        [JsonIgnore]
        public IList<DynamicAuth> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = DynamicAuth.FindAllByProperties("ParentID", this.DynamicAuthID);
                }

                return _Children;
            }
        }

        /// <summary>
        /// 此权限分类
        /// </summary>
        [JsonIgnore]
        public DynamicAuthCatalog Catalog
        {
            get
            {
                if (_Catalog == null)
                {
                    _Catalog = DynamicAuthCatalog.FindAllByProperties("Code", this.CatalogCode).First();
                }

                return _Catalog;
            }
        }
        
        #endregion

        #region 重载

        public override void Create()
        {
            this.CreatedDate = DateTime.Now;

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                this.SetFullEditStatus();
                this.SetEditStatus(GranteStatusString);
            }

            base.Create();
        }

        public override void CreateAndFlush()
        {
            this.CreatedDate = DateTime.Now;

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                this.SetFullEditStatus();
                this.SetEditStatus(GranteStatusString);
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
            if (this.DynamicAuthID != null)
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
			if (this.DynamicAuthID != null)
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
            /*if (!this.IsPropertyUnique("UniqueKey"))
            {
                throw new RepeatedKeyException("存在重复的 UniqueKey “" + this.UniqueKey + "”");
            }*/
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        [ActiveRecordTransaction]
        public void DoCreate()
        {
            this.DoValidate();

            if (String.IsNullOrEmpty(this.EditStatus))
            {
                bool isEditable = this.Editable;
                bool isGrantable = this.Grantable;
                this.SetFullEditStatus();
                this.Editable = isEditable;
                this.Grantable = isGrantable;
            }

            this.CreateAndFlush();

            IList<string> ops = this.Catalog.GetAllowOperations().Select(tent => tent.Code).ToList();
            DynamicPermission.GrantDAuthToUsers(this, new string[] { this.CreaterID }, ops, null, SysSystem.SYS_USERID, SysSystem.SYS_USERNAME);
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
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    // 删除所有授权(这里应当使用存储过程--暂时使用 表关联功能)
                    DynamicPermission[] dps = DynamicPermission.FindAllByProperty("AuthID", this.DynamicAuthID);
                    foreach (DynamicPermission tdp in dps)
                    {
                        tdp.DoDelete();
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

        /// <summary>
        /// 获取相应层级子节点
        /// </summary>
        /// <returns></returns>
        public DynamicAuth[] GetSubs()
        {
            int currLevel = this.PathLevel.GetValueOrDefault();
            ICriterion crit = Expression.Gt("PathLevel", currLevel);

            return DynamicAuth.FindAll(crit);
        }

        /// <summary>
        /// 获取相应层级子节点
        /// </summary>
        /// <param name="level">当前层级向下层级</param>
        /// <returns></returns>
        public DynamicAuth[] GetSubs(int level)
        {
            int currLevel = this.PathLevel.GetValueOrDefault();
            int maxLevel = this.PathLevel.GetValueOrDefault() + level;

            DetachedCriteria crits = DetachedCriteria.For<DynamicAuth>();

            crits.Add(Expression.Gt("PathLevel", currLevel));
            crits.Add(Expression.Le("PathLevel", maxLevel));

            return DynamicAuth.FindAll(crits);
        }

        /// <summary>
        /// 添加顶层节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsTop(string cid)
        {
            DynamicAuthCatalog dac = DynamicAuthCatalog.Find(cid);
            this.CatalogCode = dac.Code;

            this.Path = null;
            this.PathLevel = 0;

            this.DoCreate();
        }

        /// <summary>
        /// 添加兄弟节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(string sibID)
        {
            DynamicAuth sib = DynamicAuth.Find(sibID);

            CreateAsSib(sib);
        }

        /// <summary>
        /// 添加兄弟节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(DynamicAuth sib)
        {
            this.ParentID = sib.ParentID;
            this.Path = sib.Path;
            this.PathLevel = sib.PathLevel;
            this.CatalogCode = sib.CatalogCode;

            this.DoCreate();
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(string parentID)
        {
            DynamicAuth parent = DynamicAuth.Find(parentID);

            CreateAsSub(parent);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(DynamicAuth parent)
        {
            this.ParentID = parent.DynamicAuthID;
            this.CatalogCode = parent.CatalogCode;
            this.Path = String.Format("{0}.{1}", (parent.Path == null ? String.Empty : parent.Path), parent.DynamicAuthID);
            this.PathLevel = parent.PathLevel + 1;

            this.DoCreate();
        }

        #endregion

    } // DynamicAuth
}



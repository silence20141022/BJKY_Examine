
using System;
using System.Linq;
using System.IO;
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
	public partial class FileFolder
    {
        #region 私有成员

        [NonSerialized]
        private FileModule _FileModule;

        #endregion

        #region 成员属性

        /// <summary>
        /// 此组的所在组（父组）
        /// </summary>
        [JsonIgnore]
        public FileModule FileModule
        {
            get
            {
                if (_FileModule == null && !String.IsNullOrEmpty(this.ModuleId))
                {
                    _FileModule = FileModule.Find(this.ModuleId);
                }

                return _FileModule;
            }
        }

        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        [JsonIgnore]
        public string FolderPath
        {
            get
            {
                string fpath = System.IO.Path.Combine(this.FileModule.RootPath, this.Path == null ? "" : this.Path);

                return fpath;
            }
        }

        /// <summary>
        /// 路径深度
        /// </summary>
        public int PathLevel
        {
            get
            {
                if (String.IsNullOrEmpty(this.Path))
                {
                    return 0;
                }
                else
                {
                    return this.Path.Split('.').Count();
                }
            }
        }
        
        #endregion

        #region 重载

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("FolderKey"))
            {
                throw new RepeatedKeyException("存在重复的 键 “" + this.FolderKey + "”");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            this.CreateTime = DateTime.Now;

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    using (new SessionScope())
                    {

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

        /*

        /// <summary>
        /// 获取相应层级子节点
        /// </summary>
        /// <returns></returns>
        public FileFolder[] GetSubs()
        {
            int currLevel = this.PathLevel;
            ICriterion crit = Expression.Gt("PathLevel", currLevel);

            return FileFolder.FindAll(crit);
        }

        /// <summary>
        /// 获取相应层级子节点
        /// </summary>
        /// <param name="level">当前层级向下层级</param>
        /// <returns></returns>
        public FileFolder[] GetSubs(int level)
        {
            int currLevel = this.PathLevel;
            int maxLevel = this.PathLevel + level;

            DetachedCriteria crits = DetachedCriteria.For<FileFolder>();

            crits.Add(Expression.Gt("PathLevel", currLevel));
            crits.Add(Expression.Le("PathLevel", maxLevel));

            return FileFolder.FindAll(crits);
        }
         * 
         * */

        /// <summary>
        /// 添加顶层节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsTop(string cid)
        {
            this.Path = null;

            this.DoCreate();
        }

        /// <summary>
        /// 添加兄弟节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(string sibID)
        {
            FileFolder sib = FileFolder.Find(sibID);

            CreateAsSib(sib);
        }

        /// <summary>
        /// 添加兄弟节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(FileFolder sib)
        {
            this.ParentId = sib.ParentId;
            this.Path = sib.Path;

            this.DoCreate();
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(string parentID)
        {
            FileFolder parent = FileFolder.Find(parentID);

            CreateAsSub(parent);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(FileFolder parent)
        {
            this.ParentId = parent.Id;
            this.Path = String.Format("{0}{1}", (parent.Path == null ? String.Empty : parent.Path + "."), parent.Id);

            this.DoCreate();
        }

        #endregion

    } // FileFolder
}



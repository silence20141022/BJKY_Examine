// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using NHibernate.Criterion;
using NHibernate.Transform;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [Serializable]
    public partial class SysGroup : TreeNodeEntityBase<SysGroup>
    {
        #region 私有变量

        [NonSerialized]
        private SysGroup _ParentGroup;

        [NonSerialized]
        private IList<SysGroup> _AllGroups = new List<SysGroup>();

        [NonSerialized]
        private IList<SysGroup> _ChildGroups = new List<SysGroup>();

        private bool _isLeaf = false;
        private string _aim_Filter = "";


        [NonSerialized]
        private IList<SysAuth> _Auth = new List<SysAuth>();

        [NonSerialized]
        private IList<SysRole> _Role = new List<SysRole>();

        [NonSerialized]
        private IList<SysUser> _User = new List<SysUser>();

        #endregion

        #region 成员属性

        //树型所需参数扩展
        public string Aim_Filter
        {
            get { return _aim_Filter; }
            set { _aim_Filter = value; }
        }

        //是否叶子节点判断
        public override bool? IsLeaf
        {
            get { return !SysGroup.Exists("ParentID = ?", this.GroupID); }
            set { _isLeaf = value.GetValueOrDefault(); }
        }

        /// <summary>
        /// 此组的所在组（父组）
        /// </summary>
        public SysGroup ParentGroup
        {
            get 
            {
                if (!IsTopGroup)
                {
                    _ParentGroup = SysGroup.Find(this.ParentID);
                }

                return _ParentGroup;
            }
        }

        /// <summary>
        /// 自己及所有父辈组
        /// </summary>
        [JsonIgnore]
        public IList<SysGroup> AllGroup
        {
            get
            {
                _AllGroups = this.RetrieveAllGroup();
                return _AllGroups;
            }
        }

        /// <summary>
        /// 是否顶层组（没有父节点）
        /// </summary>
        public bool IsTopGroup
        {
            get
            {
                return String.IsNullOrEmpty(this.ParentID);
            }
        }

        /// <summary>
        /// 此组的所拥有的子组(组中组)
        /// </summary>
        [JsonIgnore]
        [HasMany(typeof(SysGroup), Table = "SysGroup", ColumnKey = "ParentID", Lazy = true)]
        public IList<SysGroup> ChildGroups
        {
            get { return _ChildGroups; }
            set { _ChildGroups = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysAuth), Table = "SysGroupPermission", ColumnRef = "AuthID", ColumnKey = "GroupID", Lazy = true)]
        public IList<SysAuth> Auth
        {
            get { return _Auth; }
            set { _Auth = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysGroupRole", ColumnRef = "RoleID", ColumnKey = "GroupID", Lazy = true)]
        public IList<SysRole> Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        [JsonIgnore]
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUserGroup", ColumnRef = "UserID", ColumnKey = "GroupID", Lazy = true)]
        public IList<SysUser> User
        {
            get { return _User; }
            set { _User = value; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            //if (!this.IsPropertyUnique("Code"))
            //{
            //    throw new RepeatedKeyException("存在重复的 编号 “" + this.Code + "”");
            //}
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

        #region 重载

        [ActiveRecordTransaction]
        public override void Delete()
        {
            // 删除本节点及子节点以及相关子节点类型
            string sql = String.Format("DELETE FROM SysUserGroup WHERE GroupID IN (SELECT GroupID FROM SysGroup WHERE Path LIKE '%{0}%' OR GroupID = '{0}')"
                + "DELETE FROM SysGroup WHERE Path LIKE '%{0}%' OR GroupID = '{0}'; ", this.GroupID);

            if (!String.IsNullOrEmpty(this.ParentID))
            {
                //sql += String.Format("UPDATE SysParameterCatalog SET IsLeaf = 1 WHERE ParameterCatalogID = '{0}' AND NOT EXISTS (SELECT ParameterCatalogID FROM SysParameterCatalog WHERE ParentID = '{0}')", this.ParentID);
            }

            DataHelper.QueryValue(sql);
        }

        #endregion

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            this.Delete();
        }

        /// <summary>
        /// 批量删除操作
        /// </summary>
        public static void DoBatchDelete(params object[] args)
        {
            SysGroup[] tents = SysGroup.FindAllByPrimaryKeys(args);

            foreach (SysGroup tent in tents)
            {
                tent.DoDelete();
            }
        }

        /// <summary>
        /// 是否拥有某权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool HasPermission(SysAuth auth)
        {
            return AllAuth.Contains(auth);
        }

        /// <summary>
        /// 是否是某角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsRole(SysRole role)
        {
            return AllRole.Contains(role);
        }

        private ReadOnlyCollection<SysAuth> m_allAuth = null;

        /// <summary>
        /// 组所有权限(自带权限+所有父组权限+所有角色权限)
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<SysAuth> AllAuth
        {
            get
            {
                if (m_allAuth == null)
                {
                    m_allAuth = new ReadOnlyCollection<SysAuth>(RetrieveAllAuth());
                }

                return m_allAuth;
            }
        }

        private ReadOnlyCollection<SysRole> m_allRole = null;

        /// <summary>
        /// 组所有角色(自有角色+所有父组角色)
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<SysRole> AllRole
        {
            get
            {
                if (m_allRole == null)
                {
                    m_allRole = new ReadOnlyCollection<SysRole>(RetrieveAllRole());
                }

                return m_allRole;
            }
        }

        /// <summary>
        /// 获取所有相关组（自己和所有祖父辈组)
        /// </summary>
        /// <returns></returns>
        private IList<SysGroup> RetrieveAllGroup()
        {
            IList<SysGroup> groups = new List<SysGroup>();

            if (!this.IsTopGroup)
            {
                string[] groupIDs = this.Path.Split('.');
                ICriterion hqlCriterion = Expression.In("GroupID", groupIDs);
                groups = SysGroup.FindAll(hqlCriterion);
            }

            return groups;
        }

        /// <summary>
        /// 获取当前组所有角色
        /// </summary>
        /// <returns></returns>
        private IList<SysRole> RetrieveAllRole()
        {
            IList<SysRole> roles = new List<SysRole>();

            foreach (SysRole tRole in this.Role)
            {
                roles.Add(tRole);
            }

            foreach (SysGroup tGroup in this.AllGroup)
            {
                foreach (SysRole tRole in tGroup.Role)
                {
                    if (!roles.Contains(tRole))
                    {
                        roles.Add(tRole);
                    }
                }
            }

            return roles;
        }

        /// <summary>
        /// 获取用户所拥有所有权限(自带角色+组角色)
        /// </summary>
        /// <returns></returns>
        private IList<SysAuth> RetrieveAllAuth()
        {
            IList<SysAuth> auths = new List<SysAuth>();

            foreach (SysAuth tAuth in this.Auth)
            {
                auths.Add(tAuth);
            }

            foreach (SysRole tRole in this.AllRole)
            {
                foreach (SysAuth tAuth in tRole.Auth)
                {
                    if (!auths.Contains(tAuth))
                    {
                        auths.Add(tAuth);
                    }
                }
            }

            foreach (SysGroup tGroup in this.AllGroup)
            {
                foreach (SysAuth tAuth in tGroup.Auth)
                {
                    if (!auths.Contains(tAuth))
                    {
                        auths.Add(tAuth);
                    }
                }
            }

            return auths;
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
            SysGroup sib = SysGroup.Find(sibID);

            CreateAsSib(sib);
        }

        /// <summary>
        /// 添加兄弟模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSib(SysGroup sib)
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
            SysGroup parent = SysGroup.Find(parentID);

            CreateAsSub(parent);
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="module"></param>
        public void CreateAsSub(SysGroup parent)
        {
            this.ParentID = parent.GroupID;
            this.Path = String.Format("{0}.{1}", (parent.Path == null ? String.Empty : parent.Path), parent.GroupID);
            this.PathLevel = parent.PathLevel + 1;

            this.DoCreate();
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="usrs"></param>
        public void RemoveUsers(IEnumerable<SysUser> usrs)
        {
            IEnumerable<string> idsToRemove = usrs.Select(ent => ent.UserID);

            this.RemoveUsers(idsToRemove);
        }

        /// <summary>
        /// 根据用户ID移除组人员
        /// </summary>
        /// <param name="usrIDs"></param>
        public void RemoveUsers(IEnumerable<string> usrIDs)
        {
            IEnumerable<SysUser> usrsToRemove = this.User.Where(ent => usrIDs.Contains(ent.UserID));

            while (usrsToRemove.Count() > 0)
            {
                this.User.Remove(usrsToRemove.First());
            }
        }

        /// <summary>
        /// 根据用户Id添加组成员
        /// </summary>
        /// <param name="usrIDs"></param>
        public void AddUsers(IEnumerable<string> usrIDs)
        {
            IEnumerable<string> currIDs = this.User.Select(ent => ent.UserID);
            IEnumerable<string> idsToAdd = usrIDs.Where(ent => !currIDs.Contains(ent));

            if (idsToAdd != null && idsToAdd.Count() > 0)
            {
                string[] idListToAdd = idsToAdd.ToArray<string>();

                SysUser[] usrsToAdd = SysUser.FindAll(Expression.In("UserID", idListToAdd));

                foreach (SysUser usr in usrsToAdd)
                {
                    foreach (SysGroup group in usr.Group)
                    {
                        group.User.Remove(usr);
                    }
                    this.User.Add(usr);
                }
            }
        }

        /// <summary>
        /// 添加新组员
        /// </summary>
        /// <param name="usrs"></param>
        public void AddUsers(IEnumerable<SysUser> usrs)
        {
            IEnumerable<string> currIDs = this.User.Select(ent => ent.UserID);

            IEnumerable<SysUser> usrsToAdd = usrs.Where(ent => !currIDs.Contains(ent.UserID));

            foreach (SysUser usr in usrsToAdd)
            {
                foreach (SysGroup group in usr.Group)
                {
                    group.User.Remove(usr);
                }
                this.User.Add(usr);
            }
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

        #region 静态成员

        /// <summary>
        /// 由编码获取Enumeration
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SysGroup Get(string code)
        {
            SysGroup[] tents = SysGroup.FindAllByProperty(SysGroup.Prop_Code, code);
            if (tents != null && tents.Length > 0)
            {
                return tents[0];
            }

            return null;
        }



        #endregion

    } // SysUser
}


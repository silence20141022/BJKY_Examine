using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;
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
	public partial class DynamicPermission
    {
        #region 成员

        [NonSerialized]
        private DynamicPermissionCatalog _Catalog;

        #endregion

        #region 成员属性

        /// <summary>
        /// 此授权分类
        /// </summary>
        [JsonIgnore]
        public DynamicPermissionCatalog Catalog
        {
            get
            {
                if (_Catalog == null)
                {
                    _Catalog = DynamicPermissionCatalog.FindAllByProperties("Code", this.CatalogCode).First();
                }

                return _Catalog;
            }
        }
        
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
        /// 是否允许指定操作
        /// </summary>
        public bool IsAllowOperation(string op)
        {
            if (String.IsNullOrEmpty(this.Operation))
            {
                return false;
            }

            DynamicOperations dop = new DynamicOperations(this.Operation);

            return dop.Exists(op);
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="op"></param>
        public void AddOperation(string op)
        {
            DynamicOperations dop = new DynamicOperations(this.Operation);

            dop.Add(op);
            this.Operation = dop.ToString();
        }

        public void AddOperation(IList<string> ops)
        {
            DynamicOperations dop = new DynamicOperations(this.Operation);

            dop.Add(ops);
            this.Operation = dop.ToString();
        }

        /// <summary>
        /// 移除操作
        /// </summary>
        /// <param name="op"></param>
        public void RemoveOperation(string op)
        {
            if (!String.IsNullOrEmpty(op))
            {
                DynamicOperations dop = new DynamicOperations(this.Operation);
                dop.Remove(op);

                this.Operation = dop.ToString();
            }
        }

        public void RemoveOperation(IList<string> ops)
        {
            DynamicOperations dop = new DynamicOperations(this.Operation);

            dop.Remove(ops);
            this.Operation = dop.ToString();
        }

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

        #region 静态方法

        #region 获取权限

        /// <summary>
        /// 获取组织权限
        /// </summary>
        /// <param name="pdata"></param>
        /// <param name="syspCatalog"></param>
        /// <returns></returns>
        public static DynamicPermission[] GetOrgPermissions(string pdata, DynamicPermissionCatalog.SysCatalogEnum sysPCatalog)
        {
            return GetPermissionsByCatalogCode(pdata, sysPCatalog.ToString());
        }

        /// <summary>
        /// 由权限分类获取权限
        /// </summary>
        /// <param name="pdata"></param>
        /// <param name="syspCatalog"></param>
        /// <returns></returns>
        public static DynamicPermission[] GetPermissionsByCatalogCode(string pdata, string pCatalogString)
        {
            DynamicPermission[] dps = DynamicPermission.FindAll(Expression.Eq("Data", pdata), Expression.Eq("CatalogCode", pCatalogString));

            return dps;
        }

        #endregion

        #region 组授权

        public static void GrantDAuthToGroup(DynamicAuth auth, string groupID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToGroups(new DynamicAuth[] { auth }, new string[] { groupID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthsToGroup(IList<DynamicAuth> auths, string groupID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToGroups(auths, new string[] { groupID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthToGroups(DynamicAuth auth, IList<string> groupIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToGroups(new DynamicAuth[] { auth }, groupIDs, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthsToGroups(IList<DynamicAuth> auths, IList<string> groupIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    IList<DynamicPermission> dps = GrantDAuths(auths, groupIDs, operations, DynamicPermissionCatalog.SysCatalogEnum.SYS_GROUP.ToString(), tag, granterID, granterName);
                    SysGroup[] grps = SysGroup.FindAll(Expression.In("GroupID", groupIDs.ToList()));

                    foreach (DynamicPermission tdp in dps)
                    {
                        IEnumerable<SysGroup> tgrps = grps.Where(tent => tent.GroupID == tdp.Data);

                        if (tgrps.Count() > 0)
                        {
                            SysGroup tgrp = tgrps.First();

                            tdp.Name = tgrp.Name;
                            tdp.Tag = tgrp.Type.ToString();
                        }

                        tdp.Update();
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

        public static void RevokeDAuthFromGroup(string authID, string groupID)
        {
            RevokeDAuthsFromGroups(new string[] { authID }, new string[] { groupID });
        }

        public static void RevokeDAuthsFromGroup(IList<string> authIDs, string groupID)
        {
            RevokeDAuthsFromGroups(authIDs, new string[] { groupID });
        }

        public static void RevokeDAuthFromGroups(string authID, IList<string> groupIDs)
        {
            RevokeDAuthsFromGroups(new string[] { authID }, groupIDs);
        }

        public static void RevokeDAuthsFromGroups(IList<string> authIDs, IList<string> groupIDs)
        {
            RevokeDAuths(authIDs, groupIDs, DynamicPermissionCatalog.SysCatalogEnum.SYS_GROUP.ToString());
        }

        #endregion

        #region 角色授权

        public static void GrantDAuthToRole(DynamicAuth auth, string roleID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToRoles(new DynamicAuth[] { auth }, new string[] { roleID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthsToRole(IList<DynamicAuth> auths, string roleID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToRoles(auths, new string[] { roleID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthToRoles(DynamicAuth auth, IList<string> roleIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToRoles(new DynamicAuth[] { auth }, roleIDs, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthsToRoles(IList<DynamicAuth> auths, IList<string> roleIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    IList<DynamicPermission> dps = GrantDAuths(auths, roleIDs, operations, DynamicPermissionCatalog.SysCatalogEnum.SYS_ROLE.ToString(), tag, granterID, granterName);
                    SysRole[] rols = SysRole.FindAll(Expression.In("RoleID", roleIDs.ToList()));

                    foreach (DynamicPermission tdp in dps)
                    {
                        IEnumerable<SysRole> roles = rols.Where(tent => tent.RoleID == tdp.Data);

                        if (roles.Count() > 0)
                        {
                            SysRole trole = roles.First();

                            tdp.Name = trole.Name;
                            tdp.Tag = trole.Type.ToString();
                        }

                        tdp.Update();
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

        public static void RevokeDAuthFromRole(string authID, string roleID)
        {
            RevokeDAuthsFromRoles(new string[] { authID }, new string[] { roleID });
        }

        public static void RevokeDAuthsFromRole(IList<string> authIDs, string roleID)
        {
            RevokeDAuthsFromRoles(authIDs, new string[] { roleID });
        }

        public static void RevokeDAuthFromRoles(string authID, IList<string> roleIDs)
        {
            RevokeDAuthsFromRoles(new string[] { authID }, roleIDs);
        }

        public static void RevokeDAuthsFromRoles(IList<string> authIDs, IList<string> roleIDs)
        {
            RevokeDAuths(authIDs, roleIDs, DynamicPermissionCatalog.SysCatalogEnum.SYS_ROLE.ToString());
        }

        #endregion

        #region 用户授权

        public static void GrantDAuthToUser(DynamicAuth auth, string userID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToUsers(new DynamicAuth[] { auth }, new string[] { userID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthsToUser(IList<DynamicAuth> auths, string userID, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToUsers(auths, new string[] { userID }, operations, tag, granterID, granterName);
        }

        public static void GrantDAuthToUsers(DynamicAuth auth, IList<string> userIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            GrantDAuthsToUsers(new DynamicAuth[] { auth }, userIDs, operations, tag, granterID, granterName);
        }

        [ActiveRecordTransaction]
        public static void GrantDAuthsToUsers(IList<DynamicAuth> auths, IList<string> userIDs, IList<string> operations, string tag, string granterID, string granterName)
        {
            IList<DynamicPermission> dps = GrantDAuths(auths, userIDs, operations, DynamicPermissionCatalog.SysCatalogEnum.SYS_USER.ToString(), tag, granterID, granterName);
            SysUser[] usrs = SysUser.FindAll(Expression.In("UserID", userIDs.ToList()));

            foreach (DynamicPermission tdp in dps)
            {
                IEnumerable<string> usrname = usrs.Where(tent => tent.UserID == tdp.Data).Select(tent => tent.Name);

                if (usrname.Count() > 0)
                {
                    tdp.Name = usrname.First();
                }

                tdp.Update();
            }
        }

        public static void RevokeDAuthFromUser(string authID, string userID)
        {
            RevokeDAuthsFromUsers(new string[] { authID }, new string[] { userID });
        }

        public static void RevokeDAuthsFromUser(IList<string> authIDs, string userID)
        {
            RevokeDAuthsFromUsers(authIDs, new string[] { userID });
        }

        public static void RevokeDAuthFromUsers(string authID, IList<string> userIDs)
        {
            RevokeDAuthsFromUsers(new string[] { authID }, userIDs);
        }

        public static void RevokeDAuthsFromUsers(IList<string> authIDs, IList<string> userIDs)
        {
            RevokeDAuths(authIDs, userIDs, DynamicPermissionCatalog.SysCatalogEnum.SYS_USER.ToString());
        }

        #endregion

        #region 整体授权

        /// <summary>
        /// 授权
        /// </summary>
        [ActiveRecordTransaction]
        public static IList<DynamicPermission> GrantDAuths(IList<DynamicAuth> dauths, IList<string> datas, IList<string> operations, string pcatalog, string tag, string granterID, string granterName)
        {
            IList<DynamicPermission> rtnps = new List<DynamicPermission>();

            IList authIDs = dauths.Select(tent => tent.DynamicAuthID).ToList();

            DynamicPermission[] dps = DynamicPermission.FindAll(Expression.Eq("CatalogCode", pcatalog), Expression.In("AuthID", authIDs), Expression.In("Data", datas.ToList()));

            // 获取权限分类
            IList dacatalogcode = dauths.Select(tent => tent.CatalogCode).Distinct().ToList();
            DynamicAuthCatalog[] dac = DynamicAuthCatalog.FindAll(Expression.In("Code", dacatalogcode));

            foreach (DynamicAuth dauth in dauths)
            {
                DynamicAuthCatalog tdac = dac.First(tent => tent.Code == dauth.CatalogCode);
                IList<string> defaultOperations = null;

                if (tdac != null)
                {
                    defaultOperations = tdac.GetDefauleOperations().Select(tent => tent.Code).ToList();
                }

                foreach (string data in datas)
                {
                    IEnumerable<DynamicPermission> tdps = dps.Where<DynamicPermission>(tent => tent.AuthID == dauth.DynamicAuthID && tent.Data == data && tent.CatalogCode == pcatalog);

                    if (tdps.Count() > 0)
                    {
                        if (operations != null && operations.Count > 0)
                        {
                            foreach (DynamicPermission tdp in tdps)
                            {
                                if (operations != null && operations.Count() > 0)
                                {
                                    tdp.AddOperation(operations);
                                }
                                else if (defaultOperations != null)
                                {
                                    // 添加默认操作
                                    tdp.AddOperation(defaultOperations);
                                }

                                if (tag != null)
                                {
                                    tdp.Tag = tag;
                                }

                                tdp.Update();

                                rtnps.Add(tdp);
                            }
                        }
                    }
                    else
                    {
                        DynamicPermission dp = new DynamicPermission();

                        dp.AuthID = dauth.DynamicAuthID;
                        dp.AuthCatalogCode = dauth.CatalogCode;
                        dp.Data = data;
                        dp.CatalogCode = pcatalog;
                        dp.CreaterID = granterID;
                        dp.CreaterName = granterName;
                        dp.Name = string.Format("{0} to {1}", dauth.Name, data);

                        if (tag != null)
                        {
                            // 设置tag
                            dp.Tag = tag;
                        }

                        if (operations != null && operations.Count() > 0)
                        {
                            dp.AddOperation(operations);
                        }
                        else if (defaultOperations != null)
                        {
                            // 添加默认操作
                            dp.AddOperation(defaultOperations);
                        }

                        dp.Create();

                        rtnps.Add(dp);
                    }
                }
            }

            return rtnps;
        }

        /// <summary>
        /// 回收权限
        /// </summary>
        public static void RevokeDAuths(IList<string> authIDs, IList<string> datas, string pcatalog)
        {
            DynamicPermission[] dps = DynamicPermission.FindAll(Expression.In("AuthID", authIDs.ToList()), Expression.In("Data", datas.ToList()), Expression.Eq("CatalogCode", pcatalog));

            foreach (DynamicPermission tdp in dps)
            {
                tdp.DoDelete();
            }
        }

        #endregion

        #region 工具方法

        /// <summary>
        /// 合并权限组织限制到List
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="roleIDs"></param>
        /// <param name="groupIDs"></param>
        public static IList<ICriterion> CombinOrgCriterionsToList(IList<string> userIDs, IList<string> roleIDs, IList<string> groupIDs)
        {
            IList<ICriterion> orgcrits = new List<ICriterion>();

            if (userIDs != null && userIDs.Count > 0)
            {
                orgcrits.Add(Expression.And(Expression.In("Data", userIDs.ToList()),
                    Expression.Eq("CatalogCode", DynamicPermissionCatalog.SysCatalogEnum.SYS_USER.ToString())));
            }

            if (roleIDs != null && roleIDs.Count > 0)
            {
                orgcrits.Add(Expression.And(Expression.In("Data", roleIDs.ToList()),
                Expression.Eq("CatalogCode", DynamicPermissionCatalog.SysCatalogEnum.SYS_ROLE.ToString())));
            }

            if (groupIDs != null && groupIDs.Count > 0)
            {
                orgcrits.Add(Expression.And(Expression.In("Data", groupIDs.ToList()),
                Expression.Eq("CatalogCode", DynamicPermissionCatalog.SysCatalogEnum.SYS_GROUP.ToString())));
            }

            return orgcrits;
        }

        /// <summary>
        /// 并集权限组织限制到一个ICriterion
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="roleIDs"></param>
        /// <param name="groupIDs"></param>
        public static ICriterion UnionOrgCriterions(IList<string> userIDs, IList<string> roleIDs, IList<string> groupIDs)
        {
            IList<ICriterion> orgcrits = CombinOrgCriterionsToList(userIDs, roleIDs, groupIDs);
            ICriterion ucrit = SearchHelper.UnionCriterions(orgcrits.ToArray());

            return ucrit;
        }

        /// <summary>
        /// 交集权限组织限制到一个ICriterion
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="roleIDs"></param>
        /// <param name="groupIDs"></param>
        public static ICriterion IntersectOrgCriterions(IList<string> userIDs, IList<string> roleIDs, IList<string> groupIDs)
        {
            IList<ICriterion> orgcrits = CombinOrgCriterionsToList(userIDs, roleIDs, groupIDs);
            ICriterion ucrit = SearchHelper.IntersectCriterions(orgcrits.ToArray());

            return ucrit;
        }

        #endregion

        #endregion

    } // DynamicPermission

    public class DynamicOperations
    {
        #region 成员变量

        public const char DivChar = ',';

        private IList<string> operations = new List<string>();

        #endregion

        #region 属性

        /// <summary>
        /// 属性字符串
        /// </summary>
        private string OperationString
        {
            get
            {
                return StringHelper.Join(operations, DivChar);
            }
        }

        #endregion

        #region 构造函数

        public DynamicOperations()
        {
        }

        public DynamicOperations(string opstr)
        {
            if (opstr != null)
            {
                operations = opstr.Split(DivChar);
            }
        }

        #endregion

        #region 操作

        public bool Exists(string opstr)
        {
            if (IsValidOpStr(opstr))
            {
                return operations.Contains(opstr);
            }

            return false;
        }

        public DynamicOperations Add(string opstr)
        {
            return this.Add(new string[] { opstr });
        }

        public DynamicOperations Add(IEnumerable<string> opstrs)
        {
            if (opstrs == null)
            {
                return this;
            }

            foreach (string opstr in opstrs)
            {
                if (IsValidOpStr(opstr))
                {
                    if (!operations.Contains(opstr))
                    {
                        operations.Add(opstr);
                    }
                }
            }

            return this;
        }

        public DynamicOperations Remove(string opstr)
        {
            return this.Remove(new string[] { opstr });
        }

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="opstrs"></param>
        /// <returns></returns>
        public DynamicOperations Remove(IList<string> opstrs)
        {
            if (opstrs == null)
            {
                return this;
            }

            foreach (string opstr in opstrs)
            {
                if (IsValidOpStr(opstr))
                {
                    operations.Remove(opstr);
                }
            }

            return this;
        }

        #endregion

        #region 重载

        public override string ToString()
        {
            return StringHelper.Join(operations, DivChar);
        }

        #endregion

        #region 静态函数

        /// <summary>
        /// 检查是否合法操作字符（不包含分割字符串）
        /// </summary>
        /// <param name="opstr"></param>
        /// <returns></returns>
        private bool IsValidOpStr(string opstr)
        {
            return !String.IsNullOrEmpty(opstr) && !opstr.Contains(DivChar);
        }

        #endregion
    }
}



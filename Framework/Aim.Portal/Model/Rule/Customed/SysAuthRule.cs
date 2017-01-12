// Business class SysAuthRule generated from SysAuth
// Creator: Ray
// Created Date: [2010-04-10]

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Newtonsoft.Json.Linq;
using Aim.Data;
using Aim.Portal.Model;
	
namespace Aim.Portal.Model
{
	public partial class SysAuthRule
	{
        #region 静态成员

        /// <summary>
        /// 由权限列表获取所有用户应用
        /// </summary>
        /// <param name="authList"></param>
        /// <returns></returns>
        public static IList<SysApplication> RetrieveAllApplicationFromAuthList(IList<SysAuth> authList)
        {
            IList<SysApplication> apps = new List<SysApplication>();

            List<string> appIDs = new List<string>();

            foreach (SysAuth auth in authList)
            {
                // 类型为1 为入口权限(Application或Module)
                if (auth.Type == 1 && !String.IsNullOrEmpty(auth.Data))
                {
                    appIDs.Add(auth.Data);
                }
            }

            ICriterion hqlCriterion = Expression.In("ApplicationID", appIDs);
            apps = SysApplication.FindAll(hqlCriterion);

            return apps;
        }

        /// <summary>
        /// 由权限列表获取所有用户模块
        /// </summary>
        /// <param name="authList"></param>
        /// <returns></returns>
        public static IList<SysModule> RetrieveAllModulesFromAuthList(IList<SysAuth> authList)
		{
            IList<SysModule> ents = new List<SysModule>();

            List<string> ids = new List<string>();

            foreach (SysAuth auth in authList)
            {
                // 类型为1 为入口权限(Application或Module)
                if (auth.Type == 1 && !String.IsNullOrEmpty(auth.ModuleID))
                {
                    ids.Add(auth.ModuleID);
                }
            }

            ICriterion hqlCriterion = Expression.In("ModuleID", ids);
            ents = SysModule.FindAll(hqlCriterion);

            return ents;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        new public static SysAuth[] FindAll()
        {
            return SysAuth.FindAll("FROM SysAuth auth WHERE auth.AuthID <> ?", '0');
        }

        /// <summary>
        /// 授权给用户
        /// </summary>
        public static void GrantAuthToUser(ICollection authIDs, string  userID)
        {
            ICollection<SysAuth> tAuths = GetAuthByIDs(authIDs);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysAuth auth in tAuths)
                        {
                            user.Auth.Add(auth);
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
        }

        /// <summary>
        /// 从用户回收权限
        /// </summary>
        public static void RevokeAuthFromUser(ICollection authIDs, string userID)
        {
            IList authList = GetAuthList(authIDs);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysAuth> tAuths = user.Auth.Where(ent => authList.Contains(ent.AuthID));
                        while (tAuths.Count() > 0)
                        {
                            user.Auth.Remove(tAuths.First());
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
        }

        /// <summary>
        /// 否定授权给用户
        /// </summary>
        public static void GrantAuthNoToUser(ICollection authIDs, string userID)
        {
            ICollection<SysAuth> tAuths = GetAuthByIDs(authIDs);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysAuth auth in tAuths)
                        {
                            user.AuthNo.Add(auth);
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
        }

        /// <summary>
        /// 从用户回收否定权限
        /// </summary>
        public static void RevokeAuthNoFromUser(ICollection authIDs, string userID)
        {
            IList authList = GetAuthList(authIDs);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysAuth> tAuths = user.AuthNo.Where(ent => authList.Contains(ent.AuthID));
                        while (tAuths.Count() > 0)
                        {
                            user.AuthNo.Remove(tAuths.First());
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
        }

        /// <summary>
        /// 授权给组
        /// </summary>
        public static void GrantAuthToGroup(ICollection authIDs, string groupID)
        {
            ICollection<SysAuth> tAuths = GetAuthByIDs(authIDs);

            using (new SessionScope())
            {
                SysGroup group = SysGroup.Find(groupID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysAuth auth in tAuths)
                        {
                            group.Auth.Add(auth);
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
        }

        /// <summary>
        /// 从组回收权限
        /// </summary>
        public static void RevokeAuthFromGroup(ICollection authIDs, string groupID)
        {
            IList authList = GetAuthList(authIDs);

            using (new SessionScope())
            {
                SysGroup group = SysGroup.Find(groupID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysAuth> tAuths = group.Auth.Where(ent => authList.Contains(ent.AuthID));
                        while (tAuths.Count() > 0)
                        {
                            group.Auth.Remove(tAuths.First());
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
        }

        /// <summary>
        /// 授权给角色
        /// </summary>
        public static void GrantAuthToRole(ICollection authIDs, string roleID)
        {
            ICollection<SysAuth> tAuths = GetAuthByIDs(authIDs);

            using (new SessionScope())
            {
                SysRole role = SysRole.Find(roleID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysAuth auth in tAuths)
                        {
                            role.Auth.Add(auth);
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
        }

        /// <summary>
        /// 从角色回收权限
        /// </summary>
        public static void RevokeAuthFromRole(ICollection authIDs, string roleID)
        {
            IList authList = GetAuthList(authIDs);

            using (new SessionScope())
            {
                SysRole role = SysRole.Find(roleID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysAuth> tAuths = role.Auth.Where(ent => authList.Contains(ent.AuthID));
                        while (tAuths.Count() > 0)
                        {
                            role.Auth.Remove(tAuths.First());
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
        }

		#endregion

        #region 私有函数

        /// <summary>
        /// 由ID集合获取权限集合
        /// </summary>
        /// <param name="authIDs"></param>
        /// <returns></returns>
        public static IList GetAuthList(ICollection authIDs)
        {
            IList myAuthIDs = null;

            if (authIDs is JArray)
            {
                JArray arrAuths = authIDs as JArray;
                myAuthIDs = new List<string>(arrAuths.Values<string>());
            }
            else
            {
                myAuthIDs = authIDs as IList;
            }

            return myAuthIDs;
        }

        /// <summary>
        /// 由ID集合获取权限集合
        /// </summary>
        /// <param name="authIDs"></param>
        /// <returns></returns>
        public static ICollection<SysAuth> GetAuthByIDs(ICollection authIDs)
        {
            ICollection myAuthIDs = null;

            if (authIDs is JArray)
            {
                JArray arrAuths = authIDs as JArray;
                myAuthIDs = new List<string>(arrAuths.Values<string>());
            }
            else
            {
                myAuthIDs = authIDs;
            }

            SysAuth[] tAuths = SysAuth.FindAll(Expression.In("AuthID", myAuthIDs));

            return tAuths;
        }

        #endregion

    } // SysAuth
}


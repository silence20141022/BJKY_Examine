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
    public partial class SysRoleRule
	{
        #region 静态成员

        /// <summary>
        /// 分配角色给用户
        /// </summary>
        public static void GrantRoleToUser(ICollection ids, string  userID)
        {
            ICollection<SysRole> tRols = GetRoleByIDs(ids);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysRole role in tRols)
                        {
                            user.Role.Add(role);
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
        /// 从用户回收角色
        /// </summary>
        public static void RevokeRoleFromUser(ICollection ids, string userID)
        {
            IList list = GetRolList(ids);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysRole> trole = user.Role.Where(ent => list.Contains(ent.RoleID));
                        while (trole.Count() > 0)
                        {
                            user.Role.Remove(trole.First());
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
        /// 分配角色给组
        /// </summary>
        public static void GrantRoleToGroup(ICollection ids, string groupID)
        {
            ICollection<SysRole> tents = GetRoleByIDs(ids);

            using (new SessionScope())
            {
                SysGroup group = SysGroup.Find(groupID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysRole ent in tents)
                        {
                            group.Role.Add(ent);
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
        /// 从组回收角色
        /// </summary>
        public static void RevokeRoleFromGroup(ICollection ids, string groupID)
        {
            IList list = GetRolList(ids);

            using (new SessionScope())
            {
                SysGroup group = SysGroup.Find(groupID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysRole> tents = group.Role.Where(ent => list.Contains(ent.RoleID));
                        while (tents.Count() > 0)
                        {
                            group.Role.Remove(tents.First());
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
        /// 由ID集合获取角色集合
        /// </summary>
        /// <param name="authIDs"></param>
        /// <returns></returns>
        public static IList GetRolList(ICollection ids)
        {
            IList myIDs = null;

            if (ids is JArray)
            {
                JArray arr = ids as JArray;
                myIDs = new List<string>(arr.Values<string>());
            }
            else
            {
                myIDs = ids as IList;
            }

            return myIDs;
        }

        /// <summary>
        /// 由ID集合获取权限集合
        /// </summary>
        /// <param name="authIDs"></param>
        /// <returns></returns>
        public static ICollection<SysRole> GetRoleByIDs(ICollection ids)
        {
            ICollection myIDs = null;

            if (ids is JArray)
            {
                JArray arr = ids as JArray;
                myIDs = new List<string>(arr.Values<string>());
            }
            else
            {
                myIDs = ids;
            }

            SysRole[] tEnts = SysRole.FindAll(Expression.In("RoleID", myIDs));

            return tEnts;
        }

        #endregion

    } // SysAuth
}


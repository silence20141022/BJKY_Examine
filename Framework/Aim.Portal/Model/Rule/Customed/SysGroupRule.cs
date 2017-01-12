using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
    public partial class SysGroupRule
    {
        #region 静态成员

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public static SysGroup[] GetRootGroupList()
        {
            SimpleQuery query = new SimpleQuery(typeof(SysGroup)
                , @"FROM SysGroup WHERE ParentID IS NULL ");

            SysGroup[] users = (SysGroup[])SysGroup.ExecuteQuery(query);
            return users;
        }

        /// <summary>
        /// 分配组给用户
        /// </summary>
        public static void GrantGroupToUser(ICollection ids, string userID)
        {
            ICollection<SysGroup> tGrps = GetGroupByIDs(ids);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        foreach (SysGroup grp in tGrps)
                        {
                            user.Group.Add(grp);
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
        /// 从用户回收组
        /// </summary>
        public static void RevokeGroupFromUser(ICollection ids, string userID)
        {
            IList list = GetGrpList(ids);

            using (new SessionScope())
            {
                SysUser user = SysUser.Find(userID);

                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        IEnumerable<SysGroup> tgrp = user.Group.Where(ent => list.Contains(ent.GroupID));
                        while (tgrp.Count() > 0)
                        {
                            user.Group.Remove(tgrp.First());
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
        public static IList GetGrpList(ICollection ids)
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
        public static ICollection<SysGroup> GetGroupByIDs(ICollection ids)
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

            SysGroup[] tEnts = SysGroup.FindAll(Expression.In("GroupID", myIDs));

            return tEnts;
        }

        #endregion

    } // SysGroupRule
}


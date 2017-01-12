using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
using Aim.Common.Authentication;
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
    public partial class SysUserRule
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns>成功则返回用户信息，错误则返回null</returns>
        public static SysUser Authenticate(string loginName, string password)
        {
            SysUser[] users = (SysUser[])SysUser.FindAll(
                         Expression.Eq(SysUser.Prop_LoginName, loginName),
                         Expression.Eq(SysUser.Prop_Status, 1),
                         SearchHelper.UnionCriterions(
                            Expression.Eq(SysUser.Prop_Password, password),
                            Expression.Eq(SysUser.Prop_Password, String.Empty),
                            Expression.IsNull(SysUser.Prop_Password)));

            if (users.Length == 1)
            {
                return users.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取系统用户数
        /// </summary>
        /// <returns></returns>
        public static int RetrieveUserCount()
        {
            return ActiveRecordMediator.Count(typeof(SysUser), " 1 = 1");
        }

        /// <summary>
        /// 获取指定状态系统用户数
        /// </summary>
        /// <returns></returns>
        public static int RetrieveUserCount(byte? status)
        {
            string filter = String.Empty;

            if (status == null)
            {
                filter += " Status IS NULL ";
            }
            else
            {
                filter += String.Format(" Status = {0} ", status);
            }

            return ActiveRecordMediator.Count(typeof(SysUser), filter);
        }
    }
}

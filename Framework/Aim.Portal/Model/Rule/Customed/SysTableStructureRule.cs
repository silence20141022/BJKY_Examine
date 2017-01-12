// Business class SysUserRule generated from SysUser
// Creator: Ray
// Created Date: [2000-04-27]

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using NHibernate;
using Aim.Data;
using Aim.Portal.Model;
	
namespace Aim.Portal.Model
{
    public partial class SysTableStructureRule
	{
		#region 静态成员

        /// <summary>
        /// 获取表主键
        /// </summary>
        /// <returns></returns>
        public static string[] GetTablePrimaryKey(string tableName)
        {
            SysTableStructure[] sts = SysTableStructure.FindAllByProperties("TableName", tableName, "IsPrimary", 1);

            return sts.Select(tent => tent.FieldName).ToArray();
        }
		
		#endregion
		
	} // SysUserRule
}


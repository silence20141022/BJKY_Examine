// Business class SysRoleTypeRule generated from SysRoleType
// Creator: Ray
// Created Date: [2000-04-23]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using NHibernate;
using Aim.Data;
using Aim.Portal.Model;
	
namespace Aim.Portal.Model
{
	public partial class SysGroupTypeRule
    {
        /// <summary>
        /// 获取枚举
        /// </summary>
        public static DataEnum GetGroupTypeEnum()
        {
            DataEnum de = new DataEnum();

            SysGroupType[] ents = SysGroupType.FindAll();

            foreach (SysGroupType ent in ents)
            {
                de.Add(ent.GroupTypeID.ToString(), ent.Name);
            }

            return de;
        }
		
	} // SysRoleType
}


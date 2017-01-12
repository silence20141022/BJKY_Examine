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
	public partial class SysRoleTypeRule
    {
        /// <summary>
        /// 获取枚举
        /// </summary>
        public static DataEnum GetRoleTypeEnum()
        {
            DataEnum de = new DataEnum();

            SysRoleType[] ents = SysRoleType.FindAll();

            foreach (SysRoleType ent in ents)
            {
                de.Add(ent.RoleTypeID.ToString(), ent.Name);
            }

            return de;
        }
		
	} // SysRoleType
}


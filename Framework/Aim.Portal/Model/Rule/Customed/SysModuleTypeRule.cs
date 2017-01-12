// Business class SysModule generated from SysModule
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using Aim.Data;
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
	public partial class SysModuleTypeRule	
	{
        /// <summary>
        /// 获取模块类型枚举
        /// </summary>
        public static DataEnum GetModuleTypeEnum()
        {
            DataEnum de = new DataEnum();

            SysModuleType[] sml = SysModuleType.FindAll();

            foreach (SysModuleType sm in sml)
            {
                de.Add(sm.ModuleTypeID.ToString(), sm.Name);
            }

            return de;
        }

	} // SysModule
}


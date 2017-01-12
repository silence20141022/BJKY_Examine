// Business class SysModule generated from SysModule
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Portal.Model;
	
namespace Aim.Portal.Model
{
	public partial class SysModuleRule	
	{
        /// <summary>
        /// 有模块键获取模块
        /// </summary>
        /// <param name="code"></param>
        public static SysModule FindByCode(string code)
        {
            SysModule mdl = SysModule.FindFirstByProperties("Code", code);
            return mdl;
        }

	} // SysModule
}


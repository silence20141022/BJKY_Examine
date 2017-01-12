// Business class SysUser generated from SysUser
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [Serializable]
    public partial class SysAuthType
    {
        #region 私有变量

        private bool? _HasAuth = null;

        #endregion

        #region 成员属性

        /// <summary>
        /// 此类型是否拥有权限
        /// </summary>
        public bool HasAuth
        {
            get
            {
                if (_HasAuth == null)
                {
                    return SysAuth.Exists("Type = ?", this.AuthTypeID);
                }

                return _HasAuth.GetValueOrDefault();
            }
        }

        #endregion

    } // SysAuthType
}


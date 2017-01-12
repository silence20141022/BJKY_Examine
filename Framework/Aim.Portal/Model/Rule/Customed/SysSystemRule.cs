using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
    public partial class SysSystemRule
    {
        /// <summary>
        /// 获取当前
        /// </summary>
        /// <returns></returns>
        public static SysSystem GetCurrentSystemInfo()
        {
            return SysSystem.FindAllByProperty("IsCurrent", true).First();
        }
    }
}

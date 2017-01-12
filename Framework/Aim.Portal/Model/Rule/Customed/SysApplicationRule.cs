using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Portal.Model;

namespace Aim.Portal.Model
{
    public partial class SysApplicationRule
    {
        /// <summary>
        /// 有模块键获取模块
        /// </summary>
        /// <param name="code"></param>
        public static SysApplication FindByCode(string code)
        {
            SysApplication app = SysApplication.FindFirstByProperties("Code", code);
            return app;
        }

    }
}

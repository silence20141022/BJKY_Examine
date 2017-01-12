using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Aim.Examining.Web
{
    public class Utility
    {
        public static string[] GetDeptInfo(string userid)
        {
            string[] strarray = new string[2];
            string sql = "select top 1 g.GroupID,g.Name as GroupName from SysUserGroup u left join SysGroup g where u.Userid='{0}' and g.Type=2";
            sql = string.Format(sql, userid);
            DataTable dt = DbMgr.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                strarray[0] = dt.Rows[0]["GroupID"] + "";
                strarray[1] = dt.Rows[0]["GroupName"] + "";
            }
            return strarray;
        }
    }
}
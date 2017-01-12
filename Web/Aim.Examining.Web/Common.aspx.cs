using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aim.Examining.Web
{
    public partial class Common : System.Web.UI.Page
    {
        //int totalProperty = 0;
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            switch (action)
            {
                case "loaddept":
                    //4b54389a-6537-4748-823c-fb55223afbad 职能后勤部门 
                    //bde12833-038a-4ec6-bfc9-41f630c70380 研究设计部门
                    //3273c49a-1f9b-4328-b54c-d01e39c39edc 产业部门
                    //037f85a8-3777-4015-9bc2-dc5aba4ccb28 两厂
                    string sql = @"select * from sysgroup where parentid='4b54389a-6537-4748-823c-fb55223afbad' or parentid='bde12833-038a-4ec6-bfc9-41f630c70380'
                    or parentid='3273c49a-1f9b-4328-b54c-d01e39c39edc' or parentid='037f85a8-3777-4015-9bc2-dc5aba4ccb28'";
                    dt = DbMgr.GetDataTable(sql);
                    //IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式 
                    //iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                    var json = JsonConvert.SerializeObject(dt);
                    Response.Write("{rows:" + json + ",total:" + dt.Rows.Count + "}");
                    Response.End();
                    break;
                case "loaduser":
                    if (!string.IsNullOrEmpty(Request["deptid"] + ""))
                    { 
                        sql = @"select t.*,g.Name as GroupName from sysuser t  left join SysUserGroup s  on t.UserId=s.UserId
                        left join SysGroup g on g.GroupId=s.GroupId 
                        where PATINDEX('%{0}%', Path) > 0 and (parentid='4b54389a-6537-4748-823c-fb55223afbad' or parentid='bde12833-038a-4ec6-bfc9-41f630c70380'
                        or parentid='3273c49a-1f9b-4328-b54c-d01e39c39edc' or parentid='037f85a8-3777-4015-9bc2-dc5aba4ccb28')";
                        sql = string.Format(sql, Request["deptid"]);
                        dt = DbMgr.GetDataTable(sql);
                    }
                    if (!string.IsNullOrEmpty(Request["username"] + ""))
                    {
                        sql = @"select t.*,g.Name as GroupName from sysuser t 
                        left join SysUserGroup s on t.UserId=s.UserId
                        left join SysGroup g on g.GroupId=s.GroupId 
                        where t.Name like '%{0}%' and (parentid='4b54389a-6537-4748-823c-fb55223afbad' or parentid='bde12833-038a-4ec6-bfc9-41f630c70380'
                        or parentid='3273c49a-1f9b-4328-b54c-d01e39c39edc' or parentid='037f85a8-3777-4015-9bc2-dc5aba4ccb28')";
                        sql = string.Format(sql, Request["username"]); 
                        dt = DbMgr.GetDataTable(sql);
                    }
                    var json_user = JsonConvert.SerializeObject(dt);
                    Response.Write(json_user);
                    Response.End();
                    break;
            }
        }
    }
}
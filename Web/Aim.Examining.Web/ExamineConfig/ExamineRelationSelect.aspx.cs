using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Examining.Model;
using Aim.Data;
using Aim.Portal.Web.UI;
using Aim.Utilities;
using System.Data.SqlClient;
using System.Configuration;
using Aim.Portal.Model;
namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineRelationSelect : ExamBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //            string ExamineType = Server.HtmlDecode(RequestData.Get<string>("ExamineType"));
            //            string LaunchDeptId = RequestData.Get<string>("LaunchDeptId");
            //            string temp = "";
            //            if (ExamineType == "院级考核")
            //            {
            //                temp = ",(select top 1 Id  from BJKY_Examine..ExamineIndicator where BeRoleCode=A.Value) as ExamineIndicatorId ";
            //            }
            //            else
            //            {
            //                temp = ",(select top 1 Id  from BJKY_Examine..ExamineIndicator where BeRoleCode=A.Value and BelongDeptId='" + LaunchDeptId + "') as ExamineIndicatorId ";
            //            }

            //            string sql = @"select A.EnumerationID,A.Code,A.Name,A.Value,
            //            (select top 1 Id from BJKY_Examine..ExamineRelation where BeRoleCode=A.Value) as ExamineRelationId {0} from SysEnumeration as A where           
            //            A.ParentId=(select top 1 EnumerationID from SysEnumeration where Code='BeExamineObject') and A.Tag='{1}'";
            //            sql = string.Format(sql, temp, ExamineType);
            string sql = @"select * from BJKY_Examine..ExamineRelation  ";
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
        }
    }
}



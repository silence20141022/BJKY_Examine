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
    public partial class IndicatorSelect : ExamBasePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            string ExamineType = Server.HtmlDecode(RequestData.Get<string>("ExamineType"));
            string LaunchDeptId = RequestData.Get<string>("LaunchDeptId");
            string BeRoleCode = RequestData.Get<string>("BeRoleCode");
            string sql = "";
            if (ExamineType == "院级考核")
            {
                sql = @"select * from BJKY_Examine..ExamineIndicator as A where   BelongDeptId='" + LaunchDeptId + "'";
            }
            else
            {
                sql = @"select * from BJKY_Examine..ExamineIndicator  where  BelongDeptId='" + LaunchDeptId + "'";
            }
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
        }
    }
}



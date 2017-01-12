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
namespace Aim.Examining.Web.DeptConfig
{
    public partial class DeptIndicatorSelect : ExamBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //找到部门允许自定义的部门指标的二级指标
            string sql = @"select A.Id,A.MaxScore,A.IndicatorSecondName,C.BelongDeptId,C.BelongDeptName,C.BeRoleName,C.IndicatorName
            from BJKY_Examine..IndicatorSecond as A left join BJKY_Examine..IndicatorFirst as B  on A.IndicatorFirstId=B.Id
            left join BJKY_Examine..ExamineIndicator as C on B.ExamineIndicatorId=C.Id
            where B.CustomColumn='T' and C.BelongDeptId in (select Id from BJKY_Examine..PersonConfig 
            where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位') )";
            sql = string.Format(sql, UserInfo.UserID);
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
        }
    }
}



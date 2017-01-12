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
    public partial class DeptExamineRelationSelect : ExamBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GroupID = RequestData.Get<string>("GroupID");
            string sql = @"select * from BJKY_Examine..DeptExamineRelation  where GroupID='" + GroupID + "' order by RelationName asc";
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
        }
    }
}



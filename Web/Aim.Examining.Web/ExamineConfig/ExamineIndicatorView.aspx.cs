using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Model;
using System.Data;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineIndicatorView : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id            
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                sql = @"select A.IndicatorSecondName,A.MaxScore,A.SortIndex,A.IndicatorFirstId,B.IndicatorFirstName,
                        B.MaxScore as BMaxScore,B.SortIndex as BSortIndex from BJKY_Examine..IndicatorSecond as A 
                    left join BJKY_Examine..IndicatorFirst as B on A.IndicatorFirstId=B.Id where B.ExamineIndicatorId='{0}' order by BSortIndex,A.SortIndex asc";
                sql = string.Format(sql, id);
                PageState.Add("DataList", DataHelper.QueryDictList(sql));
                ExamineIndicator eiEnt = ExamineIndicator.Find(id);
                PageState.Add("BaseInfo", eiEnt.IndicatorName + "-->" + eiEnt.BeRoleName);
            }
        }
    }
}


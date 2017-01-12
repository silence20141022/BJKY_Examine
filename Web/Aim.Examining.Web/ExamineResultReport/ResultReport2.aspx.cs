using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Aim.Data;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Examining.Model;

namespace Aim.Examining.Web
{
    public partial class ResultReport2 : ExamListPage
    {
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (RequestActionString)
            {
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            IList<EasyDictionary> dics = new List<EasyDictionary>();
            sql = "select distinct Year from BJKY_Examine..ExamYearResult order by Year asc";
            IList<EasyDictionary> yearDics = DataHelper.QueryDictList(sql);
            foreach (EasyDictionary yearDic in yearDics)
            {
                EasyDictionary dic = new EasyDictionary();
                dic.Add("Year", yearDic.Get<string>("Year"));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where ApproveLevel is not null and Year='" + yearDic.Get<string>("Year") + "'";
                decimal t = DataHelper.QueryValue<int>(sql);
                dic.Add("Total", t);
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and (ApproveLevel='优秀' or AppealLevel='优秀')";
                decimal q1 = DataHelper.QueryValue<int>(sql);
                dic.Add("优秀", q1);
                dic.Add("优秀占比", Math.Round(q1 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and (ApproveLevel='良好' or AppealLevel='良好')";
                decimal q2 = DataHelper.QueryValue<int>(sql);
                dic.Add("良好", q2);
                dic.Add("良好占比", Math.Round(q2 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and (ApproveLevel='称职' or AppealLevel='称职')";
                decimal q3 = DataHelper.QueryValue<int>(sql);
                dic.Add("称职", q3);
                dic.Add("称职占比", Math.Round(q3 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and (ApproveLevel='不称职' or AppealLevel='不称职')";
                decimal q4 = DataHelper.QueryValue<int>(sql);
                dic.Add("不称职", q4);
                dic.Add("不称职占比", Math.Round(q4 * 100 / t, 2));
                dics.Add(dic);
            }
            PageState.Add("DataList", dics);
        }
    }
}


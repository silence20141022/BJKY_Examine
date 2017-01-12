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
    public partial class ResultReport1 : ExamListPage
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
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "'";
                decimal t = DataHelper.QueryValue<int>(sql);
                dic.Add("Total", t);
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=95";
                decimal q1 = DataHelper.QueryValue<int>(sql);
                dic.Add("95->100", q1);
                dic.Add("95->100占比", Math.Round(q1 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=90 and IntegrationScore < 95";
                decimal q2 = DataHelper.QueryValue<int>(sql);
                dic.Add("90->94", q2);
                dic.Add("90->94占比", Math.Round(q2 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=85 and IntegrationScore < 90";
                decimal q3 = DataHelper.QueryValue<int>(sql);
                dic.Add("85->89", q3);
                dic.Add("85->89占比", Math.Round(q3 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=80 and IntegrationScore < 85";
                decimal q4 = DataHelper.QueryValue<int>(sql);
                dic.Add("80->84", q4);
                dic.Add("80->84占比", Math.Round(q4 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=75 and IntegrationScore < 80";
                decimal q5 = DataHelper.QueryValue<int>(sql);
                dic.Add("75->79", q5);
                dic.Add("75->79占比", Math.Round(q5 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=70 and IntegrationScore < 75";
                decimal q6 = DataHelper.QueryValue<int>(sql);
                dic.Add("70->74", q6);
                dic.Add("70->74占比", Math.Round(q6 * 100 / t, 2));
                sql = @"select count(Id) from BJKY_Examine..ExamYearResult where Year='" + yearDic.Get<string>("Year") + "' and IntegrationScore>=0 and IntegrationScore < 70";
                decimal q7 = DataHelper.QueryValue<int>(sql);
                dic.Add("0->69", q7);
                dic.Add("0->69占比", Math.Round(q7 * 100 / t, 2));
                dics.Add(dic);
            }
            PageState.Add("DataList", dics);
        }
    }
}


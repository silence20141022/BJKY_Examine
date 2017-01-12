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
using Aim.Examining.Web;
using Aim.Examining.Model;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class QuarterResultWrite : ExamListPage
    {
        private string ExamineStageId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            switch (RequestActionString)
            {
                case "AutoSave":
                    IList<string> entStrList = RequestData.GetList<string>("data");
                    if (entStrList.Count > 0)
                    {
                        JObject json = JsonHelper.GetObject<JObject>(entStrList[0]);
                        IList<ExamineStageResult> esrEnts = null;
                        ExamineStageResult esrEnt = null;
                        if (json.Value<int>("FirstScore") > 0)
                        {
                            esrEnts = ExamineStageResult.FindAllByProperties(ExamineStageResult.Prop_UserId, json.Value<string>("UserId"),
                                ExamineStageResult.Prop_Year, "2013", ExamineStageResult.Prop_StageType, "1");
                            if (esrEnts.Count > 0)
                            {
                                esrEnts[0].Score = json.Value<decimal>("FirstScore");
                                esrEnts[0].DoUpdate();
                            }
                            else
                            {
                                esrEnt = new ExamineStageResult();
                                esrEnt.UserId = json.Value<string>("UserId");
                                esrEnt.UserName = json.Value<string>("UserName");
                                esrEnt.DeptId = json.Value<string>("DeptId");
                                esrEnt.DeptName = json.Value<string>("DeptName");
                                esrEnt.Year = "2013";
                                esrEnt.StageType = "1";
                                esrEnt.Score = json.Value<decimal>("FirstScore");
                                esrEnt.DoCreate();
                            }
                        }
                        if (json.Value<int>("SecondScore") > 0)
                        {
                            esrEnts = ExamineStageResult.FindAllByProperties(ExamineStageResult.Prop_UserId, json.Value<string>("UserId"),
                                ExamineStageResult.Prop_Year, "2013", ExamineStageResult.Prop_StageType, "2");
                            if (esrEnts.Count > 0)
                            {
                                esrEnts[0].Score = json.Value<decimal>("SecondScore");
                                esrEnts[0].DoUpdate();
                            }
                            else
                            {
                                esrEnt = new ExamineStageResult();
                                esrEnt.UserId = json.Value<string>("UserId");
                                esrEnt.UserName = json.Value<string>("UserName");
                                esrEnt.DeptId = json.Value<string>("DeptId");
                                esrEnt.DeptName = json.Value<string>("DeptName");
                                esrEnt.Year = "2013";
                                esrEnt.StageType = "2";
                                esrEnt.Score = json.Value<decimal>("SecondScore");
                                esrEnt.DoCreate();
                            }
                        }
                        if (json.Value<int>("ThirdScore") > 0)
                        {
                            esrEnts = ExamineStageResult.FindAllByProperties(ExamineStageResult.Prop_UserId, json.Value<string>("UserId"),
                                ExamineStageResult.Prop_Year, "2013", ExamineStageResult.Prop_StageType, "3");
                            if (esrEnts.Count > 0)
                            {
                                esrEnts[0].Score = json.Value<decimal>("ThirdScore");
                                esrEnts[0].DoUpdate();
                            }
                            else
                            {
                                esrEnt = new ExamineStageResult();
                                esrEnt.UserId = json.Value<string>("UserId");
                                esrEnt.UserName = json.Value<string>("UserName");
                                esrEnt.DeptId = json.Value<string>("DeptId");
                                esrEnt.DeptName = json.Value<string>("DeptName");
                                esrEnt.Year = "2013";
                                esrEnt.StageType = "3";
                                esrEnt.Score = json.Value<decimal>("ThirdScore");
                                esrEnt.DoCreate();
                            }
                        }
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }

        }
        private void DoSelect()
        {
            string sql = @"select * from BJKY_Examine..PersonConfig ";
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("UserId"); dt.Columns.Add(dc);
            dc = new DataColumn("UserName"); dt.Columns.Add(dc);
            dc = new DataColumn("DeptId"); dt.Columns.Add(dc);
            dc = new DataColumn("DeptName"); dt.Columns.Add(dc);
            dc = new DataColumn("Year"); dt.Columns.Add(dc);
            dc = new DataColumn("FirstScore", typeof(decimal)); dt.Columns.Add(dc);
            dc = new DataColumn("SecondScore", typeof(decimal)); dt.Columns.Add(dc);
            dc = new DataColumn("ThirdScore", typeof(decimal)); dt.Columns.Add(dc);
            sql = @"select  Id,GroupName  from BJKY_Examine..PersonConfig 
                  where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') 
                  and (GroupType='职能服务部门' or GroupType='经营目标单位')";//找到当前登录人的部门
            sql = string.Format(sql, UserInfo.UserID);
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            if (dics.Count > 0)
            {
                PersonConfig pcEnt = PersonConfig.Find(dics[0].Get<string>("Id"));
                SearchCriterion.RecordCount = pcEnt.PeopleQuan.HasValue ? pcEnt.PeopleQuan.Value : 0;
                string userIds = pcEnt.SecondLeaderIds + "," + pcEnt.ClerkIds;
                string userNames = pcEnt.SecondLeaderNames + "," + pcEnt.ClerkNames;
                if (!string.IsNullOrEmpty(userIds))
                {
                    string[] userIdArray = userIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] userNameArray = userNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < userIdArray.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["UserId"] = userIdArray[i];
                        dr["UserName"] = userNameArray[i];
                        dr["DeptId"] = pcEnt.Id;
                        dr["DeptName"] = pcEnt.GroupName;
                        dr["Year"] = "2013";
                        sql = @"select top 1 isnull(Score,0) from BJKY_Examine..ExamineStageResult where UserId='" + userIdArray[i] + "' and Year='2013' and StageType='1' ";
                        dr["FirstScore"] = DataHelper.QueryValue<decimal>(sql);
                        sql = @"select top 1 isnull(Score,0) from BJKY_Examine..ExamineStageResult where UserId='" + userIdArray[i] + "' and Year='2013' and StageType='2' ";
                        dr["SecondScore"] = DataHelper.QueryValue<decimal>(sql);
                        sql = @"select top 1 isnull(Score,0) from BJKY_Examine..ExamineStageResult where UserId='" + userIdArray[i] + "' and Year='2013' and StageType='3' ";
                        dr["ThirdScore"] = DataHelper.QueryValue<decimal>(sql);
                        dt.Rows.Add(dr);
                    }
                }
                PageState.Add("DataList", dt);
                PageState.Add("GroupName", dics[0].Get<string>("GroupName"));
            }
        }
    }
}


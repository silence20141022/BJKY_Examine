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

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class LevelAdvice : ExamListPage
    {
        private IList<ExamYearResult> ents = null;
        ExamineStage esEnt = null;
        string ExamineStageId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            IList<string> YearResultIds = RequestData.GetList<string>("YearResultIds");
            switch (RequestActionString)
            {
                case "Submit":
                    esEnt.State = 4;
                    esEnt.DoUpdate();
                    break;
                case "AutoSave":
                    string id = RequestData.Get<string>("id");
                    string AdviceLevel = RequestData.Get<string>("AdviceLevel");
                    if (!string.IsNullOrEmpty(id))
                    {
                        ExamYearResult eyrEnt = ExamYearResult.Find(id);
                        eyrEnt.AdviceLevel = AdviceLevel;
                        //副院长和部门员工不需要人力资源部评定等级
                        if (eyrEnt.BeRoleCode != "BeDeptClerk" && eyrEnt.BeRoleCode != "BeDeputyDirector")
                        {
                            eyrEnt.ApproveLevel = AdviceLevel;
                        }
                        eyrEnt.DoUpdate();
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string sql = @"select *,(select top 1 Name from SysEnumeration where Code=ExamYearResult.BeRoleCode ) as BeRoleName 
            from BJKY_Examine..ExamYearResult where ExamineStageId='" + ExamineStageId + "' order by BeRoleCode desc";
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            PageState.Add("DataList", dics);
            SysConfig scEnt = SysConfig.FindAll().First<SysConfig>();
            decimal? ExcellentQuan = 0;
            decimal? GoodQuan = 0;
            if (esEnt.ExamineType == "院级考核")
            {
                ExcellentQuan = (int)Math.Floor(((double)dics.Count) * ((double)scEnt.FirstLeaderExcellentPercent) / 100);
                GoodQuan = (int)Math.Floor(((double)dics.Count) * ((double)scEnt.FirstLeaderGoodPercent) / 100);
            }
            else
            {
                PersonConfig pcEnt = PersonConfig.Find(esEnt.LaunchDeptId);
                ExcellentQuan = pcEnt.ExcellentQuan.GetValueOrDefault();
                GoodQuan = pcEnt.GoodQuan.GetValueOrDefault();
            }
            var obj = new
             {
                 ExamineStageName = esEnt.StageName,
                 State = esEnt.State,
                 Year = esEnt.Year,
                 ExcellentQuan = ExcellentQuan.Value,
                 GoodQuan = GoodQuan.Value,
                 ExamineType = esEnt.ExamineType
             };
            PageState.Add("Obj", obj);
        }
    }
}


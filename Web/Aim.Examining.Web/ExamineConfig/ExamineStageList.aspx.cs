using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using Aim.Examining.Model;
using Castle.ActiveRecord;

namespace Aim.Examining.Web.ExamineConfig
{
    public partial class ExamineStageList : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string sql = string.Empty;
        ExamineStage esEnt = null;
        IList<ExamineTask> etEnts = null;
        IList<ExamineStageDetail> esdEnts = null;
        IList<ExamineStageDeptDetail> esddEnts = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                esEnt = ExamineStage.Find(id);
            }
            switch (RequestActionString)
            {
                case "UpdatePathLevel":
                    IList<SysGroup> sgEnts = SysGroup.FindAll();
                    foreach (SysGroup sgEnt in sgEnts)
                    {
                        if (!string.IsNullOrEmpty(sgEnt.Path))
                        {
                            string[] array = sgEnt.Path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            sgEnt.PathLevel = array.Length;
                            sgEnt.DoUpdate();
                        }
                    }
                    break;
                case "CreateTask":
                    esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, id);
                    bool allowStart = true;//确认考核对象  考核关系  考核指标 配置是否齐全
                    if (esdEnts.Count == 0)
                    {
                        allowStart = false;
                    }
                    else
                    {
                        foreach (ExamineStageDetail esdEnt in esdEnts)
                        {
                            if (string.IsNullOrEmpty(esdEnt.ExamineRelationId) || string.IsNullOrEmpty(esdEnt.ExamineIndicatorId))
                            {
                                allowStart = false; break;
                            }
                        }
                    }
                    if (allowStart)
                    {
                        CreateTask();//生成考核任务
                        esEnt.State = 1;//更新考核阶段状态为
                        etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, id);
                        esEnt.TaskQuan = etEnts.Count;
                        esEnt.DoUpdate();
                        PageState.Add("Result", "考核任务生成成功！");
                    }
                    else
                    {
                        PageState.Add("Result", "考核任务生成失败！请检查考核对象、考核关系、考核指标是否配置完整！");
                    }
                    break;
                case "TakeBack":
                    etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, id);
                    foreach (ExamineTask etEnt in etEnts)
                    {
                        etEnt.DoDelete();
                    }
                    //删除所有的自定义指标
                    sql = "delete BJKY_Examine..CustomIndicator where ExamineStageId='" + id + "'";
                    DataHelper.ExecSql(sql);
                    esEnt.State = 0;
                    esEnt.TaskQuan = 0;
                    esEnt.DoUpdate();
                    PageState.Add("Result", "T");
                    break;
                case "JudgeCustomIndicator":
                    if (esEnt.ExamineType == "部门级考核")
                    {
                        sql = @"select A.* from BJKY_Examine..DeptExamineRelation as A left join BJKY_Examine..ExamineStageDetail as B on A.Id=B.ExamineRelationId 
                            where B.ExamineStageId='" + esEnt.Id + "'";//找到部门考核关系里面的所有被考核人  查找他们的自定义指标
                        IList<EasyDictionary> dicsRelation = DataHelper.QueryDictList(sql);
                        string beUserIds = ""; string beUserNames = "";
                        foreach (EasyDictionary dic in dicsRelation)
                        {
                            beUserIds += dic.Get<string>("BeUserIds") + ",";
                            beUserNames += dic.Get<string>("BeUserNames") + ",";
                        }
                        string[] userIdArray = null;
                        if (!string.IsNullOrEmpty(beUserIds))
                        {
                            userIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        bool allowLaunch = true;
                        for (int i = 0; i < userIdArray.Length; i++)
                        {
                            IList<CustomIndicator> ciEnts = CustomIndicator.FindAllByProperties("CreateId", userIdArray[i], "Year", esEnt.Year, "StageType", esEnt.StageType, "Result", "同意");
                            if (ciEnts.Count == 0)
                            {
                                allowLaunch = false;
                                break;
                            }
                        }
                        PageState.Add("Result", allowLaunch == true ? "T" : "F");
                    }
                    else
                    {
                        PageState.Add("Result", "T");
                    }
                    break;
                case "Launch"://生成考核任务
                    LaunchExamine();
                    break;
                case "CancelLaunch":
                    CancelLaunch();
                    break;
                case "EndExamine":
                    EndExamine();
                    break;
                case "delete":
                    IList<ExamineStageDeptDetail> esddEnts = ExamineStageDeptDetail.FindAllByProperty(ExamineStageDeptDetail.Prop_ExamineStageId, id);
                    foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                    {
                        esddEnt.DoDelete();
                    }
                    esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, id);
                    foreach (ExamineStageDetail esdEnt in esdEnts)
                    {
                        esdEnt.DoDelete();
                    }
                    esEnt.DoDelete();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string where = "";
            foreach (CommonSearchCriterionItem item in SearchCriterion.Searches.Searches)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    switch (item.PropertyName)
                    {
                        case "StartTime":
                            where += " and StartTime>='" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            sql = @"select  Id,GroupName  from BJKY_Examine..PersonConfig 
                  where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位')";
            //            sql = @"select * from sysgroup where (parentid='4b54389a-6537-4748-823c-fb55223afbad' or parentid='bde12833-038a-4ec6-bfc9-41f630c70380'
            //            or parentid='3273c49a-1f9b-4328-b54c-d01e39c39edc' or parentid='037f85a8-3777-4015-9bc2-dc5aba4ccb28') and GroupID in
            //            (select GroupID from SysUserGroup where UserID='{0}')";
            sql = string.Format(sql, UserInfo.UserID);
            IList<EasyDictionary> tempDics = DataHelper.QueryDictList(sql);
            if (tempDics.Count > 0)
            {
                if (tempDics[0].Get<string>("GroupName").IndexOf("人力资源") >= 0)
                {
                    PageState.Add("Remove", "F");
                }
                else
                {
                    PageState.Add("Remove", "T");
                }
            }
            sql = @"select * ,(select Count(Id) from BJKY_Examine..ExamineTask  where State!='1' and State!='0' and State!='4'
            and ExamineStageId=ExamineStage.Id) as SubmitQuan from BJKY_Examine..ExamineStage where LaunchDeptId in 
            (select Id from BJKY_Examine..PersonConfig 
             where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位'))" + where;
            sql = string.Format(sql, UserInfo.UserID);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "CreateTime";
            string asc = search.Orders.Count <= 0 || !search.Orders[0].Ascending ? " desc" : " asc";
            string pageSql = @"
		    WITH OrderedOrders AS
		    (SELECT *,
		    ROW_NUMBER() OVER (order by {0} {1})as RowNumber
		    FROM ({2}) temp ) 
		    SELECT * 
		    FROM OrderedOrders 
		    WHERE RowNumber between {3} and {4}";
            pageSql = string.Format(pageSql, order, asc, sql, (search.CurrentPageIndex - 1) * search.PageSize + 1, search.CurrentPageIndex * search.PageSize);
            IList<EasyDictionary> dicts = DataHelper.QueryDictList(pageSql);
            return dicts;
        }
        private void CreateTask()
        {
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, esEnt.Id);
            foreach (ExamineStageDetail esdEnt in esdEnts)
            {
                DeptExamineRelation derEnt = DeptExamineRelation.Find(esdEnt.ExamineRelationId);
                string[] beUserIdArray = new string[] { }; string[] beUserNameArray = new string[] { };
                if (!string.IsNullOrEmpty(derEnt.BeUserIds))
                {
                    beUserIdArray = derEnt.BeUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    beUserNameArray = derEnt.BeUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                string toUserIds = derEnt.UpLevelUserIds + "," + derEnt.SameLevelUserIds + "," + derEnt.DownLevelUserIds;
                string toUserNames = derEnt.UpLevelUserNames + "," + derEnt.SameLevelUserNames + "," + derEnt.DownLevelUserNames;
                string[] toUserIdArray = new string[] { }; string[] toUserNameArray = new string[] { };
                if (!string.IsNullOrEmpty(toUserIds))
                {
                    toUserIdArray = toUserIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    toUserNameArray = toUserNames.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                for (int i = 0; i < beUserIdArray.Length; i++)
                {
                    //2013-12-5追加功能 生成考核任务的时候针对每一个考核对象,自动创建本阶段考核的自定义考核指标
                    CreateCustomIndicator(beUserIdArray[i], beUserNameArray[i], esEnt, esdEnt);
                    for (int j = 0; j < toUserIdArray.Length; j++)
                    {
                        if (beUserIdArray[i] != toUserIdArray[j])//防止一个人身兼两职。自己对自己不能打分
                        {
                            ExamineTask etEnt = new ExamineTask();
                            etEnt.ExamineStageId = esEnt.Id;
                            etEnt.BeUserId = beUserIdArray[i];
                            etEnt.BeUserName = beUserNameArray[i];
                            string[] bedept = Utility.GetDeptInfo(beUserIdArray[i]);
                            etEnt.BeDeptId = bedept[0];
                            etEnt.BeDeptName = bedept[1];
                            etEnt.ToUserId = toUserIdArray[j];
                            etEnt.ToUserName = toUserNameArray[j];
                            string[] todept = Utility.GetDeptInfo(toUserIdArray[j]);
                            etEnt.ToDeptId = todept[0];
                            etEnt.ToDeptName = todept[1];
                            etEnt.ExamineRelationId = esdEnt.ExamineRelationId;
                            etEnt.ExamineIndicatorId = esdEnt.ExamineIndicatorId;
                            etEnt.State = "0";
                            etEnt.DoCreate();
                        }
                    }
                }
            }
        }
        private string[] GetUserIdAndName(IList<EasyDictionary> dics)
        {
            string Ids = ""; string Names = "";
            string DeptIds = ""; string DeptNames = "";
            for (int i = 0; i < dics.Count; i++)
            {
                if (i != dics.Count - 1)
                {
                    Ids += dics[i].Get<string>("UserIds") + ",";
                    Names += dics[i].Get<string>("UserNames") + ",";
                    DeptIds += dics[i].Get<string>("DeptIds") + ",";
                    DeptNames += dics[i].Get<string>("DeptNames") + ",";
                }
                else
                {
                    Ids += dics[i].Get<string>("UserIds");
                    Names += dics[i].Get<string>("UserNames");
                    DeptIds += dics[i].Get<string>("DeptIds");
                    DeptNames += dics[i].Get<string>("DeptNames");
                }
            }
            return new string[] { Ids, Names, DeptIds, DeptNames };
        }
        private void LaunchExamine()
        {
            // 如果是部门级考核 启动时需要把自定义指标状态置为3
            if (esEnt.ExamineType == "部门级考核")
            {
                sql = @"select A.* from BJKY_Examine..DeptExamineRelation as A left join BJKY_Examine..ExamineStageDetail as B on A.Id=B.ExamineRelationId 
                            where B.ExamineStageId='" + esEnt.Id + "'";//找到部门考核关系里面的所有被考核人  查找他们的自定义指标
                IList<EasyDictionary> dicsRelation = DataHelper.QueryDictList(sql);
                string beUserIds = ""; string beUserNames = "";
                foreach (EasyDictionary dic in dicsRelation)
                {
                    beUserIds += dic.Get<string>("BeUserIds") + ",";
                    beUserNames += dic.Get<string>("BeUserNames") + ",";
                }
                string[] userIdArray = null;
                if (!string.IsNullOrEmpty(beUserIds))
                {
                    userIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                for (int j = 0; j < userIdArray.Length; j++)
                {
                    IList<CustomIndicator> ciEnts = CustomIndicator.FindAllByProperties("CreateId", userIdArray[j], "Year", esEnt.Year, "StageType", esEnt.StageType);
                    if (ciEnts.Count > 0)
                    {
                        ciEnts[0].State = "3";//启动考核的时候将自定义指标的状态改为3  已结束
                        ciEnts[0].DoUpdate();
                    }
                }
                IList<ExamineTask> etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, id);
                foreach (ExamineTask etEnt in etEnts)
                {
                    etEnt.State = "1";//任务状态
                    etEnt.DoUpdate();
                }
                esEnt.State = 2;
                esEnt.DoUpdate();
                PageState.Add("Result", "T");
            }
            else//院级考核
            {
                IList<ExamineTask> etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, id);
                foreach (ExamineTask etEnt in etEnts)
                {
                    etEnt.State = "1";//任务状态
                    etEnt.DoUpdate();
                }
                esEnt.State = 2;
                esEnt.DoUpdate();
                PageState.Add("Result", "T");
            }
        }
        private void CancelLaunch()//撤销启动
        {
            if (esEnt.ExamineType == "部门级考核")//撤销启动的时候 如果是部门级考核 需要将考核对象的自定义指标状态设置为2  也就是可以还填写自我评价
            {
                sql = @"select A.* from BJKY_Examine..DeptExamineRelation as A left join BJKY_Examine..ExamineStageDetail as B on A.Id=B.ExamineRelationId 
                            where B.ExamineStageId='" + esEnt.Id + "'";//找到部门考核关系里面的所有被考核人  查找他们的自定义指标
                IList<EasyDictionary> dicsRelation = DataHelper.QueryDictList(sql);
                string beUserIds = ""; string beUserNames = "";
                foreach (EasyDictionary dic in dicsRelation)
                {
                    beUserIds += dic.Get<string>("BeUserIds") + ",";
                    beUserNames += dic.Get<string>("BeUserNames") + ",";
                }
                string[] userIdArray = null;
                if (!string.IsNullOrEmpty(beUserIds))
                {
                    userIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                for (int j = 0; j < userIdArray.Length; j++)
                {
                    IList<CustomIndicator> ciEnts = CustomIndicator.FindAllByProperties("CreateId", userIdArray[j], "Year", esEnt.Year, "StageType", esEnt.StageType);
                    if (ciEnts.Count > 0)
                    {
                        ciEnts[0].State = "2";
                        ciEnts[0].DoUpdate();
                    }
                }
            }
            IList<ExamineTask> etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, esEnt.Id);
            foreach (ExamineTask etEnt in etEnts)
            {
                IList<IndicatorScore> isEnts = IndicatorScore.FindAllByProperty(IndicatorScore.Prop_ExamineTaskId, etEnt.Id);
                foreach (IndicatorScore isEnt in isEnts)
                {
                    isEnt.DoDelete();
                }
                etEnt.DoDelete();
            }
            esEnt.TaskQuan = 0;
            esEnt.State = 0;
            esEnt.DoUpdate();
        }
        private void EndExamine()
        {
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, id);
            foreach (ExamineStageDetail esdEnt in esdEnts)
            {
                DeptExamineRelation derEnt = DeptExamineRelation.Find(esdEnt.ExamineRelationId);
                string[] beUserIdArray = new string[] { }; string[] beUserNameArray = new string[] { };
                if (!string.IsNullOrEmpty(derEnt.BeUserIds))
                {
                    beUserIdArray = derEnt.BeUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    beUserNameArray = derEnt.BeUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                for (int i = 0; i < beUserIdArray.Length; i++)
                {
                    ExamineStageResult esrEnt = new ExamineStageResult();
                    esrEnt.ExamineStageId = esEnt.Id;
                    esrEnt.UserId = beUserIdArray[i];
                    esrEnt.UserName = beUserNameArray[i];
                    string[] bedept = Utility.GetDeptInfo(beUserIdArray[i]);
                    esrEnt.DeptId = bedept[0];
                    esrEnt.DeptName = bedept[1];
                    esrEnt.StageType = esEnt.StageType;
                    esrEnt.Year = esEnt.Year;
                    //分别计算上级  同级  下级评分  
                    if (!string.IsNullOrEmpty(derEnt.UpLevelUserIds))//如果有上级考核人
                    {
                        // IList<UserBalance> ubEnts = UserBalance.FindAllByProperties("ExamineRelationId", derEnt.Id, "ToRoleCode", "UpLevel");//该考核关系下 上级考核的人员权重明细
                        decimal subScore = 0;
                        //                            if (ubEnts.Count > 0)//如果存在人员权重
                        //                            {
                        //                                foreach (UserBalance ubEnt in ubEnts)
                        //                                {
                        //                                    if (ubEnt.Balance > 0)
                        //                                    {
                        //                                        sql = @"select top 1 Score from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}'  
                        //                                            and BeUserId='{1}' and  ToUserId ='{2}'";
                        //                                        sql = string.Format(sql, esEnt.Id, beUserIdArray[i], ubEnt.ToUserId);
                        //                                        subScore += DataHelper.QueryValue<decimal>(sql) * (ubEnt.Balance.Value / 100);
                        //                                    }
                        //                                }
                        //                            }
                        //                            else
                        //                            {
                        sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}'  
                                and BeUserId='{1}' and PatIndex('%'+ToUserId+'%','{2}')>0";
                        sql = string.Format(sql, esEnt.Id, beUserIdArray[i], derEnt.UpLevelUserIds);
                        subScore = DataHelper.QueryValue<decimal>(sql);
                        // }
                        esrEnt.UpAvgScore = subScore;
                    }
                    if (!string.IsNullOrEmpty(derEnt.SameLevelUserIds))//如果有同级考核人
                    {
                        sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}'  
                            and BeUserId='{1}' and PatIndex('%'+ToUserId+'%','{2}')>0";
                        sql = string.Format(sql, esEnt.Id, beUserIdArray[i], derEnt.SameLevelUserIds);
                        esrEnt.SameAvgScore = DataHelper.QueryValue<decimal>(sql);
                    }
                    if (!string.IsNullOrEmpty(derEnt.DownLevelUserIds))//如果有下级考核人
                    {
                        sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}'  
                            and BeUserId='{1}' and PatIndex('%'+ToUserId+'%','{2}')>0";
                        sql = string.Format(sql, esEnt.Id, beUserIdArray[i], derEnt.DownLevelUserIds);
                        esrEnt.DownAvgScore = DataHelper.QueryValue<decimal>(sql);
                    }
                    decimal score = 0;
                    //这里需要考虑一种特例就是  权重配好了 但可能没人给他打分   就是有权重  无分的现象  这个时候该权重不计入总权重
                    int upWeight = 0; int sameWeight = 0; int downWeight = 0;
                    if (derEnt.UpLevelWeight > 0 && esrEnt.UpAvgScore > 0)
                    {
                        upWeight = derEnt.UpLevelWeight.Value;
                    }
                    if (derEnt.SameLevelWeight > 0 && esrEnt.SameAvgScore > 0)
                    {
                        sameWeight = derEnt.SameLevelWeight.Value;
                    }
                    if (derEnt.DownLevelWeight > 0 && esrEnt.DownAvgScore > 0)
                    {
                        downWeight = derEnt.DownLevelWeight.Value;
                    }
                    int totalWeight = upWeight + sameWeight + downWeight;
                    if (upWeight > 0 && esrEnt.UpAvgScore > 0)
                    {
                        score += (((decimal)upWeight / (decimal)totalWeight) * esrEnt.UpAvgScore).Value;
                    }
                    if (sameWeight > 0 && esrEnt.SameAvgScore > 0)
                    {
                        score += (((decimal)sameWeight / (decimal)totalWeight) * esrEnt.SameAvgScore).Value;
                    }
                    if (downWeight > 0 && esrEnt.DownAvgScore > 0)
                    {
                        score += (((decimal)downWeight / (decimal)totalWeight) * esrEnt.DownAvgScore).Value;
                    }
                    esrEnt.Score = score;
                    esrEnt.SortIndex = i + 1;
                    esrEnt.DoCreate();
                }
            }
            IList<ExamineTask> etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, esEnt.Id);//更改任务状态
            foreach (ExamineTask etEnt in etEnts)
            {//0  表示已生成的任务   1  表示已启动的任务 可以打分   
                if (etEnt.State == "2")//提交的任务
                {
                    etEnt.State = "3";//已结束的任务
                }
                else
                {
                    etEnt.State = "4";//已作废的任务
                }
                etEnt.DoUpdate();
            }
            if (esEnt.StageType == "4")
            {
                CaculateYearResult();
            }
            esEnt.State = 3; // 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束'
            esEnt.DoUpdate();
            PageState.Add("Result", "T");
        }
        private void CreateCustomIndicator(string beUserId, string beUserName, ExamineStage esEnt, ExamineStageDetail esdEnt)
        {
            ExamineIndicator eiEnt = ExamineIndicator.Find(esdEnt.ExamineIndicatorId);
            IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperties(IndicatorFirst.Prop_ExamineIndicatorId, eiEnt.Id, IndicatorFirst.Prop_CustomColumn, "T");
            if (ifEnts.Count > 0)
            {
                IList<IndicatorSecond> isEnts = IndicatorSecond.FindAllByProperty(IndicatorSecond.Prop_IndicatorFirstId, ifEnts[0].Id);
                if (isEnts.Count > 0)
                {
                    CustomIndicator ciEnt = new CustomIndicator();
                    ciEnt.ExamineStageId = esEnt.Id;
                    ciEnt.CreateId = beUserId;
                    ciEnt.CreateName = beUserName;
                    ciEnt.CreateTime = DateTime.Now;
                    ciEnt.DeptId = esEnt.LaunchDeptId;
                    ciEnt.DeptName = esEnt.LaunchDeptName;
                    ciEnt.IndicatorNo = DataHelper.QueryValue<string>("select BJKY_Examine.dbo.fun_getIndicatorNo()");
                    ciEnt.DeptIndicatorName = eiEnt.IndicatorName;
                    ciEnt.DeptIndicatorId = eiEnt.Id;
                    ciEnt.IndicatorSecondId = isEnts[0].Id;
                    ciEnt.IndicatorSecondName = ifEnts[0].IndicatorFirstName;
                    ciEnt.Weight = ifEnts[0].MaxScore;
                    ciEnt.Year = esEnt.Year;
                    ciEnt.StageType = esEnt.StageType;
                    ciEnt.State = "0";
                    ciEnt.DoCreate();
                }
            }
        }
        private void CaculateYearResult()
        {
            IList<EasyDictionary> dics = null;
            //每次部门内被考核的人员可能有所变动 以最后一阶段的人为准
            IList<ExamineStageResult> esrEnts = ExamineStageResult.FindAllByProperty("SortIndex", ExamineStageResult.Prop_ExamineStageId, esEnt.Id);
            for (int i = 0; i < esrEnts.Count; i++)
            {
                ExamYearResult eyrEnt = new ExamYearResult();
                eyrEnt.ExamineStageId = esEnt.Id;
                //if (esEnt.ExamineType == "院级考核")
                //{
                //    eyrEnt.BeRoleCode = esrEnts[i].BeRoleCode;
                //    eyrEnt.BeRoleName = esrEnts[i].BeRoleName;
                //}
                eyrEnt.UserId = esrEnts[i].UserId;
                eyrEnt.UserName = esrEnts[i].UserName;
                eyrEnt.DeptId = esrEnts[i].DeptId;
                eyrEnt.DeptName = esrEnts[i].DeptName;
                eyrEnt.Year = esrEnts[i].Year;
                int quarters = 0;//便于后面统计平均分 
                //2012 比较特殊直接取部门填报分 因此用2012SQL 首次考完记得去掉
                //                sql = @"select Score from BJKY_Examine..ExamineStageResult as A left join BJKY_Examine..ExamineStage as B on A.ExamineStageId=B.Id where 
                //                A.Year='" + esEnt.Year + "' and A.StageType='1' and A.UserId='" + esrEnts[i].UserId + "' and A.DeptId='" + esrEnts[i].DeptId + "' and B.ExamineType='" + esEnt.ExamineType + "'";
                //此SQL加类型限制 是担心院级考核取到该人在部门级考核里第一季度的分
                sql = @"select Score from BJKY_Examine..ExamineStageResult  where 
                Year='" + esEnt.Year + "' and  StageType='1' and  UserId='" + esrEnts[i].UserId + "' and DeptId='" + esrEnts[i].DeptId + "'";
                dics = DataHelper.QueryDictList(sql);
                quarters += dics.Count > 0 ? 1 : 0;
                if (dics.Count > 0)
                {
                    eyrEnt.FirstQuarterScore = dics[0].Get<decimal>("Score");
                }
                sql = @"select Score from BJKY_Examine..ExamineStageResult  where 
                Year='" + esEnt.Year + "' and  StageType='2' and  UserId='" + esrEnts[i].UserId + "' and DeptId='" + esrEnts[i].DeptId + "'";
                dics = DataHelper.QueryDictList(sql);
                quarters += dics.Count > 0 ? 1 : 0;
                if (dics.Count > 0)
                {
                    eyrEnt.SecondQuarterScore = dics[0].Get<decimal>("Score");
                }
                sql = @"select Score from BJKY_Examine..ExamineStageResult  where 
                Year='" + esEnt.Year + "' and  StageType='3' and  UserId='" + esrEnts[i].UserId + "' and DeptId='" + esrEnts[i].DeptId + "'";
                dics = DataHelper.QueryDictList(sql);
                quarters += dics.Count > 0 ? 1 : 0;
                if (dics.Count > 0)
                {
                    eyrEnt.ThirdQuarterScore = dics[0].Get<decimal>("Score");
                }
                eyrEnt.FourthQuarterScore = esrEnts[i].Score;
                eyrEnt.UpLevelScore = esrEnts[i].UpAvgScore;
                eyrEnt.SameLevelScore = esrEnts[i].SameAvgScore;
                eyrEnt.DownLevelScore = esrEnts[i].DownAvgScore;
                int? totalWeight = 0;
                IList<SysConfig> scEnts = SysConfig.FindAll();//取各角色各阶段类型权重 + 
                if (esEnt.ExamineType == "院级考核")
                {
                    eyrEnt.IntegrationScore = esrEnts[i].Score;
                }
                else
                {
                    if (quarters > 0) //平均分只有部门级考核有 是针对前三季度算的
                    {
                        eyrEnt.AvgScore = (eyrEnt.FirstQuarterScore.GetValueOrDefault() + eyrEnt.SecondQuarterScore.GetValueOrDefault() + eyrEnt.ThirdQuarterScore.GetValueOrDefault()) / quarters;
                    }
                    totalWeight = (eyrEnt.AvgScore.HasValue ? scEnts[0].ClerkQuarterWeight : 0) + (eyrEnt.FourthQuarterScore.HasValue ? scEnts[0].ClerkYearWeight : 0);
                    eyrEnt.IntegrationScore = (eyrEnt.AvgScore.GetValueOrDefault()) * (scEnts[0].ClerkQuarterWeight) / totalWeight + (eyrEnt.FourthQuarterScore.GetValueOrDefault()) * (scEnts[0].ClerkYearWeight) / totalWeight;
                }
                eyrEnt.DoCreate();
            }
        }
        private string[] GetBeUsersInfo(ExamineStageDetail esdEnt)
        {
            string beUserIds = string.Empty;//存储被考核对象具体的人
            string beUserNames = string.Empty;
            string beDeptIds = string.Empty;
            string beDeptNames = string.Empty;
            if (esdEnt.BeRoleCode == "BeDeputyDirector")//副院级领导
            {
                IList<PersonConfig> pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupCode, "DeputyDirector");
                if (pcEnts.Count > 0)
                {
                    if (!string.IsNullOrEmpty(pcEnts[0].ClerkIds))
                    {
                        beUserIds = pcEnts[0].ClerkIds;
                        beUserNames = pcEnts[0].ClerkNames;
                        beDeptIds = pcEnts[0].ClerkGroupIds;
                        beDeptNames = pcEnts[0].ClerkGroupNames;
                    }
                }
            }
            if (esdEnt.BeRoleCode == "BeExecutiveDeptLeader")//职能服务部门正职   +分管工作的副职也被当做中层干部来考核
            {
                sql = @"select (isnull(FirstLeaderIds,null)+','+isnull(ChargeSecondLeaderIds,'')) as beUserIds,
                    (isnull(FirstLeaderNames,'')+','+isnull(ChargeSecondLeaderNames,'')) as beUserNames,
                    (isnull(FirstLeaderGroupIds,'')+','+isnull(ChargeSecondLeaderGroupIds,'')) as beDeptIds,
                    (isnull(FirstLeaderGroupNames,'')+','+isnull(ChargeSecondLeaderGroupNames,'')) as beDeptNames
                     from BJKY_Examine..PersonConfig where GroupType='职能服务部门' and Id in 
                     (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
                foreach (EasyDictionary dic in dics)
                {
                    beUserIds += dic.Get<string>("beUserIds") + ",";
                    beUserNames += dic.Get<string>("beUserNames") + ",";
                    beDeptIds += dic.Get<string>("beDeptIds") + ",";
                    beDeptNames += dic.Get<string>("beDeptNames") + ",";
                }
            }
            if (esdEnt.BeRoleCode == "BeBusinessDeptLeader")//经营目标单位正职
            {
                sql = @"select (isnull(FirstLeaderIds,'')+','+isnull(ChargeSecondLeaderIds,'')) as beUserIds,
                    (isnull(FirstLeaderNames,'')+','+isnull(ChargeSecondLeaderNames,'')) as beUserNames,
                    (isnull(FirstLeaderGroupIds,'')+','+isnull(ChargeSecondLeaderGroupIds,'')) as beDeptIds,
                    (isnull(FirstLeaderGroupNames,'')+','+isnull(ChargeSecondLeaderGroupNames,'')) as beDeptNames
                    from BJKY_Examine..PersonConfig where GroupType='经营目标单位' and Id in 
                    (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
                foreach (EasyDictionary dic in dics)
                {
                    beUserIds += dic.Get<string>("beUserIds") + ",";
                    beUserNames += dic.Get<string>("beUserNames") + ",";
                    beDeptIds += dic.Get<string>("beDeptIds") + ",";
                    beDeptNames += dic.Get<string>("beDeptNames") + ",";
                }
            }
            if (esdEnt.BeRoleCode == "FunctionDept")//如果考核职能部门  直接把需要考核的部门当成  UserId .UserName  
            {
                esddEnts = ExamineStageDeptDetail.FindAllByProperties(ExamineStageDeptDetail.Prop_ExamineStageId, id, ExamineStageDeptDetail.Prop_GroupType, "职能服务部门");
                foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                {
                    beUserIds += esddEnt.GroupID + ",";
                    beUserNames += esddEnt.GroupName + ",";
                    beDeptIds += esddEnt.GroupID + ",";
                    beDeptNames += esddEnt.GroupName + ",";
                }
            }
            if (esdEnt.BeRoleCode == "BusinessObjectiveDept")//如果考核职能部门  直接把需要考核的部门当成  UserId .UserName  
            {
                esddEnts = ExamineStageDeptDetail.FindAllByProperties(ExamineStageDeptDetail.Prop_ExamineStageId, id, ExamineStageDeptDetail.Prop_GroupType, "经营目标单位");
                foreach (ExamineStageDeptDetail esddEnt in esddEnts)
                {
                    beUserIds += esddEnt.GroupID;
                    beUserNames += esddEnt.GroupName;
                    beDeptIds += esddEnt.GroupID;
                    beDeptNames += esddEnt.GroupName;
                }
            }
            return new string[] { beUserIds, beUserNames, beDeptIds, beDeptNames };
        }
    }
}


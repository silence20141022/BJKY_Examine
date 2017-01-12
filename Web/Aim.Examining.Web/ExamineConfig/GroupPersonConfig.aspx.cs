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
    public partial class GroupPersonConfig : ExamListPage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string sql = string.Empty;
        string ExamineStageId = "";
        ExamineStage esEnt = null;
        PersonConfig pcEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                pcEnt = PersonConfig.Find(id);
            }
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            switch (RequestActionString)
            {
                case "AutoSave":
                    int ExcellentQuan = RequestData.Get<int>("ExcellentQuan");
                    int GoodQuan = RequestData.Get<int>("GoodQuan");
                    if (ExcellentQuan > 0 && pcEnt.PeopleQuan > 0)
                    {
                        pcEnt.ExcellentRate = Math.Round(((decimal)ExcellentQuan / (decimal)pcEnt.PeopleQuan) * 100, 2);
                        pcEnt.ExcellentQuan = ExcellentQuan;
                        PageState.Add("ExcellentRate", pcEnt.ExcellentRate);
                    }
                    if (GoodQuan > 0 && pcEnt.PeopleQuan > 0)
                    {
                        pcEnt.GoodRate = Math.Round(((decimal)GoodQuan / (decimal)pcEnt.PeopleQuan) * 100, 2);
                        pcEnt.GoodQuan = GoodQuan;
                        PageState.Add("GoodRate", pcEnt.GoodRate);
                    }
                    pcEnt.DoUpdate();
                    break;
                case "delete":
                    IList<string> strList = RequestData.GetList<string>("ids");
                    foreach (string str in strList)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            pcEnt = PersonConfig.Find(str);
                            pcEnt.DoDelete();
                        }
                    }
                    break;
                case "CreateTaskAgain":
                    if (!string.IsNullOrEmpty(ExamineStageId))
                    {
                        esEnt = ExamineStage.Find(ExamineStageId);
                        IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, ExamineStageId);
                        //从这个入口进来生成任务 说明考核阶段的各条件都是满足的 
                        StartExamine();
                        //临时任务创建完毕以后 把该考核阶段对应的两个任务集合进行对比
                        AmendTask();
                    }
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
                            where += " and StartTime>'" + item.Value + "' ";
                            break;
                        case "EndTime":
                            where += " and EndTime<='" + (item.Value.ToString()).Replace(" 0:00:00", " 23:59:59") + "' ";
                            break;
                        case "ClerkNames":
                            where += " and (PatIndex('%" + item.Value + "%' ,FirstLeaderNames)>0 or PatIndex('%" + item.Value + "%',SecondLeaderNames)>0 or PatIndex('%" + item.Value + "%',ChargeSecondLeaderNames)>0  or PatIndex('%" + item.Value + "%',InstituteClerkDelegateNames)>0 or PatIndex('%" + item.Value + "%',DeptClerkDelegateNames)>0  or PatIndex('%" + item.Value + "%',ClerkNames)>0) ";
                            break;
                        default:
                            where += " and " + item.PropertyName + " like '%" + item.Value + "%'";
                            break;
                    }
                }
            }
            sql = @"select * from BJKY_Examine..PersonConfig where 1=1 " + where + " order by GroupType asc ,GroupName asc";
            PageState.Add("DataList", DataHelper.QueryDictList(sql));
        }
        private void StartExamine()
        {
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, esEnt.Id);
            if (esEnt.ExamineType == "院级考核")
            {
                foreach (ExamineStageDetail esdEnt in esdEnts)
                {
                    string beUserIds = string.Empty;//存储被考核对象具体的人
                    string beUserNames = string.Empty;
                    string beDeptIds = string.Empty;
                    string beDeptNames = string.Empty;
                    string[] array = GetBeUsersInfo(esdEnt);
                    beUserIds = array[0];
                    beUserNames = array[1];
                    beDeptIds = array[2];
                    beDeptNames = array[3];
                    ExamineRelation erEnt = ExamineRelation.Find(esdEnt.ExamineRelationId);
                    ConfirmPara(beUserIds, beUserNames, beDeptIds, beDeptNames, esdEnt, erEnt);
                    CreateSpecialTask(beUserIds, beUserNames, beDeptIds, beDeptNames, esdEnt);
                }
            }
            else//部门级考核
            {
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
                        for (int j = 0; j < toUserIdArray.Length; j++)
                        {
                            if (beUserIdArray[i] != toUserIdArray[j])//防止一个人身兼两职。自己对自己不能打分
                            {
                                TempTask etEnt = new TempTask();//部门级考核不存在角色的概念。直接对人生成任务
                                etEnt.ExamineStageId = esEnt.Id;
                                etEnt.BeUserId = beUserIdArray[i];
                                etEnt.BeUserName = beUserNameArray[i];
                                etEnt.BeDeptId = esEnt.LaunchDeptId;
                                etEnt.BeDeptName = esEnt.LaunchDeptName;
                                etEnt.ToUserId = toUserIdArray[j];
                                etEnt.ToUserName = toUserNameArray[j];
                                etEnt.ToDeptId = esEnt.LaunchDeptId;
                                etEnt.ToDeptName = esEnt.LaunchDeptName;
                                etEnt.ExamineRelationId = esdEnt.ExamineRelationId;
                                etEnt.ExamineIndicatorId = esdEnt.ExamineIndicatorId;
                                etEnt.State = "0";
                                etEnt.DoCreate();
                            }
                        }
                    }
                }
            }
        }
        private void ConfirmPara(string beUserIds, string beUserNames, string beDeptIds, string beDeptNames, ExamineStageDetail esdEnt, ExamineRelation erEnt)
        {
            string toRoleCodes = erEnt.UpLevelCode + "," + erEnt.SameLevelCode + "," + erEnt.DownLevelCode;  //考核关系知道后  找到所有考核对象
            string toRoleNames = erEnt.UpLevelName + "," + erEnt.SameLevelName + "," + erEnt.DownLevelName;
            if (!string.IsNullOrEmpty(toRoleCodes))
            {
                string[] toRoleCodeArray = toRoleCodes.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] toRoleNameArray = toRoleNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int f = 0; f < toRoleCodeArray.Length; f++)
                {
                    CreateTask(beUserIds, beUserNames, beDeptIds, beDeptNames, esdEnt, toRoleCodeArray[f], toRoleNameArray[f]);
                }
            }
        }
        private void CreateTask(string beUserIds, string beUserNames, string beDeptIds, string beDeptNames, ExamineStageDetail esdEnt, string toRoleCode, string toRoleName)
        {
            string toUserIds = "";
            string toUserNames = "";
            string sql = "";
            string[] array = null;
            DataTable dt = new DataTable();
            IList<PersonConfig> pcEnts = null;
            switch (toRoleCode)//对所有的考核对象进行判断 确定参与考核打分的人员
            {
                case "DirectorSecretary"://院长书记
                case "DeputyDirector"://副院长
                case "EnterpriseDirector"://如果上级里面有控股企业董事长和监事长
                case "EnterpriseDeputyDirector":
                    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupCode, toRoleCode);
                    if (pcEnts.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(pcEnts[0].ClerkIds))
                        {
                            toUserIds = pcEnts[0].ClerkIds;
                            toUserNames = pcEnts[0].ClerkNames;
                        }
                    }
                    break;
                case "ExcutiveDeptLeader"://职能服务部门正职               
                    sql = @"select FirstLeaderIds as UserIds,FirstLeaderNames as UserNames  from BJKY_Examine..PersonConfig where GroupType='职能服务部门' and GroupID in 
                         (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0];
                    toUserNames = array[1];
                    break;
                case "BusinessDeptLeader"://经营目标单位正职
                    sql = @"select FirstLeaderIds as UserIds, FirstLeaderNames as UserNames from BJKY_Examine..PersonConfig where GroupType='经营目标单位' and GroupID in 
                          (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0];
                    toUserNames = array[1];
                    break;
                case "ExcutiveDeptClerkDelegate"://职能部门员工代表
                    sql = @"select InstituteClerkDelegateIds as UserIds,InstituteClerkDelegateNames as UserNames from BJKY_Examine..PersonConfig
                    where GroupType='职能服务部门' and GroupID in  (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0];
                    toUserNames = array[1];
                    break;
                case "BusinessDeptClerkDelegate"://经营单位员工代表
                    sql = @"select  InstituteClerkDelegateIds as UserIds, InstituteClerkDelegateNames as UserNames from BJKY_Examine..PersonConfig
                    where GroupType='经营目标单位' and GroupID in (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0];
                    toUserNames = array[1];
                    break;
                case "DeptFirstLeader"://部门正职    只有部门内考核的时候才会有此考核对象编号  
                    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);//这个时候需要取考核阶段里面的启动部门 
                    if (pcEnts.Count > 0)
                    {
                        toUserIds = pcEnts[0].FirstLeaderIds;
                        toUserNames = pcEnts[0].FirstLeaderNames;
                    }
                    break;
                case "DeptSecondLeader"://部门副职
                    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);//这个时候需要取考核阶段里面的启动部门 
                    if (pcEnts.Count > 0)
                    {
                        toUserIds = pcEnts[0].SecondLeaderIds;
                        toUserNames = pcEnts[0].SecondLeaderNames;
                    }
                    break;
                case "DeptClerkDelegate":
                    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);//这个时候需要取考核阶段里面的启动部门 
                    if (pcEnts.Count > 0)
                    {
                        toUserIds = pcEnts[0].DeptClerkDelegateIds;
                        toUserNames = pcEnts[0].DeptClerkDelegateNames;
                    }
                    break;
                case "DeptClerk":
                    pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);//这个时候需要取考核阶段里面的启动部门 
                    if (pcEnts.Count > 0)
                    {
                        toUserIds = pcEnts[0].ClerkIds;
                        toUserNames = pcEnts[0].ClerkNames;
                    }
                    break;
            }
            string[] beUserIdArray = new string[] { }; string[] beUserNameArray = new string[] { };
            string[] beDeptIdArray = new string[] { }; string[] beDeptNameArray = new string[] { };
            string[] toUserIdArray = new string[] { }; string[] toUserNameArray = new string[] { };
            if (!string.IsNullOrEmpty(beUserIds))
            {
                beUserIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(beUserNames))
            {
                beUserNameArray = beUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(beDeptIds))
            {
                beDeptIdArray = beDeptIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(beDeptNames))
            {
                beDeptNameArray = beDeptNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(toUserIds))
            {
                toUserIdArray = toUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(toUserNames))
            {
                toUserNameArray = toUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            #region
            for (int i = 0; i < beUserIdArray.Length; i++)
            {
                for (int j = 0; j < toUserIdArray.Length; j++)
                {
                    if (beUserIdArray[i] != toUserIdArray[j])//防止一个人身兼两职。自己对自己不能打分
                    {
                        TempTask etEnt = new TempTask();
                        etEnt.ExamineStageId = esEnt.Id;
                        etEnt.BeRoleCode = esdEnt.BeRoleCode;
                        etEnt.BeRoleName = esdEnt.BeRoleName;
                        etEnt.BeUserId = beUserIdArray[i];
                        etEnt.BeUserName = beUserNameArray[i];
                        if (i <= beDeptIdArray.Length - 1)
                        {
                            etEnt.BeDeptId = beDeptIdArray[i];
                        }
                        if (i <= beDeptNameArray.Length - 1)
                        {
                            etEnt.BeDeptName = beDeptNameArray[i];
                        }
                        etEnt.ToRoleCode = toRoleCode;
                        etEnt.ToRoleName = toRoleName;
                        etEnt.ToUserId = toUserIdArray[j];
                        etEnt.ToUserName = toUserNameArray[j];//考核人的部门获取有待更新  to do
                        sql = @"select GroupID,Name  from SysGroup where PathLevel='4' and 
                                      PatIndex('%'+GroupID+'%',(select Path from SysGroup where GroupID=(select top 1 GroupID from SysUserGroup where UserID='" + toUserIdArray[j] + "') ))>0";//通过人直接找4级部门
                        IList<EasyDictionary> dics3 = DataHelper.QueryDictList(sql);
                        if (dics3.Count > 0)
                        {
                            etEnt.ToDeptId = dics3[0].Get<string>("GroupID");
                            etEnt.ToDeptName = dics3[0].Get<string>("Name");
                        }
                        etEnt.ExamineRelationId = esdEnt.ExamineRelationId;
                        etEnt.ExamineIndicatorId = esdEnt.ExamineIndicatorId;
                        etEnt.State = "0";
                        etEnt.DoCreate();
                    }
                }
            }
            #endregion
        }
        private void CreateSpecialTask(string beUserIds, string beUserNames, string beDeptIds, string beDeptNames, ExamineStageDetail esdEnt) //特例  如果被考对象经营目标单位正职  还需要推送一部分任务到 人力资源部 工作业绩打分人
        {
            if (esdEnt.BeRoleCode == "BeBusinessDeptLeader")
            {
                IList<PersonConfig> pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupCode, "HRAchievementWritor");
                IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperty(IndicatorFirst.Prop_InsteadColumn, "T");//配置考核项中。 确实有人力资源打分项
                if (pcEnts.Count > 0 && ifEnts.Count > 0)
                {
                    if (!string.IsNullOrEmpty(pcEnts[0].ClerkIds))
                    {
                        string[] beUserIdArray = new string[] { };
                        if (!string.IsNullOrEmpty(beUserIds))
                        {
                            beUserIdArray = beUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        string[] beUserNameArray = new string[] { };
                        if (!string.IsNullOrEmpty(beUserNames))
                        {
                            beUserNameArray = beUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        string[] beDeptIdArray = new string[] { };
                        if (!string.IsNullOrEmpty(beDeptIds))
                        {
                            beDeptIdArray = beDeptIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        string[] beDeptNameArray = new string[] { };
                        if (!string.IsNullOrEmpty(beDeptNames))
                        {
                            beDeptNameArray = beDeptNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        string[] toUserIdArray = new string[] { };
                        if (!string.IsNullOrEmpty(pcEnts[0].ClerkIds))
                        {
                            toUserIdArray = pcEnts[0].ClerkIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        string[] toUserNameArray = new string[] { };
                        if (!string.IsNullOrEmpty(pcEnts[0].ClerkNames))
                        {
                            toUserNameArray = pcEnts[0].ClerkNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        for (int i = 0; i < beUserIdArray.Length; i++)
                        {
                            for (int j = 0; j < toUserIdArray.Length; j++)
                            {
                                if (beUserIdArray[i] != toUserIdArray[j])//防止一个人身兼两职。自己对自己不能打分
                                {
                                    TempTask etEnt = new TempTask();
                                    etEnt.ExamineStageId = esEnt.Id;
                                    etEnt.BeRoleCode = esdEnt.BeRoleCode;
                                    etEnt.BeRoleName = esdEnt.BeRoleName;
                                    etEnt.BeUserId = beUserIdArray[i];
                                    etEnt.BeUserName = beUserNameArray[i];
                                    if (i <= beDeptIdArray.Length - 1)
                                    {
                                        etEnt.BeDeptId = beDeptIdArray[i];
                                    }
                                    if (i <= beDeptNameArray.Length - 1)
                                    {
                                        etEnt.BeDeptName = beDeptNameArray[i];
                                    }
                                    etEnt.ToRoleCode = "HRAchievementWritor";
                                    etEnt.ToRoleName = ifEnts[0].IndicatorFirstName + "填报人";
                                    etEnt.ToUserId = toUserIdArray[j];
                                    etEnt.ToUserName = toUserNameArray[j];
                                    sql = @"select GroupID,Name  from SysGroup where PathLevel='4' and 
                                      PatIndex('%'+GroupID+'%',(select Path from SysGroup where GroupID=(select top 1 GroupID from SysUserGroup where UserID='" + toUserIdArray[j] + "') ))>0";//通过人直接找4级部门
                                    IList<EasyDictionary> dics3 = DataHelper.QueryDictList(sql);
                                    if (dics3.Count > 0)
                                    {
                                        etEnt.ToDeptId = dics3[0].Get<string>("GroupID");
                                        etEnt.ToDeptName = dics3[0].Get<string>("Name");
                                    }
                                    etEnt.ExamineRelationId = esdEnt.ExamineRelationId;
                                    etEnt.ExamineIndicatorId = esdEnt.ExamineIndicatorId;
                                    etEnt.State = "0";
                                    etEnt.Tag = "1";
                                    etEnt.DoCreate();
                                }
                            }
                        }
                    }
                }
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
                     from BJKY_Examine..PersonConfig where GroupType='职能服务部门' and GroupId in 
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
                    from BJKY_Examine..PersonConfig where GroupType='经营目标单位' and GroupId in 
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
            if (esdEnt.BeRoleCode == "BeDeptSecondLeader")//部门副职
            {
                IList<PersonConfig> pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);
                if (pcEnts.Count > 0)
                {
                    beUserIds = pcEnts[0].SecondLeaderIds;
                    beUserNames = pcEnts[0].SecondLeaderNames;
                    beDeptIds = pcEnts[0].SecondLeaderGroupIds;
                    beDeptNames = pcEnts[0].SecondLeaderGroupNames;
                }
            }
            if (esdEnt.BeRoleCode == "BeDeptClerk")//部门员工
            {
                IList<PersonConfig> pcEnts = PersonConfig.FindAllByProperty(PersonConfig.Prop_GroupID, esEnt.LaunchDeptId);
                if (pcEnts.Count > 0)
                {
                    beUserIds = pcEnts[0].ClerkIds;
                    beUserNames = pcEnts[0].ClerkNames;
                    beDeptIds = pcEnts[0].ClerkGroupIds;
                    beDeptNames = pcEnts[0].ClerkGroupNames;
                }
            }
            return new string[] { beUserIds, beUserNames, beDeptIds, beDeptNames };
        }
        private string[] GetUserIdAndName(IList<EasyDictionary> dics)
        {
            string Ids = "";
            string Names = "";
            for (int i = 0; i < dics.Count; i++)
            {
                if (i != dics.Count - 1)
                {
                    Ids += dics[i].Get<string>("UserIds") + ",";
                    Names += dics[i].Get<string>("UserNames") + ",";
                }
                else
                {
                    Ids += dics[i].Get<string>("UserIds");
                    Names += dics[i].Get<string>("UserNames");
                }
            }
            return new string[] { Ids, Names };
        }
        private void AmendTask()
        {
            IList<TempTask> ttEnts = TempTask.FindAllByProperty(TempTask.Prop_ExamineStageId, ExamineStageId);
            IList<ExamineTask> etEnts = null;
            foreach (TempTask ttEnt in ttEnts)
            {
                if (esEnt.StageType == "院级考核")
                {
                    etEnts = ExamineTask.FindAllByProperties(ExamineTask.Prop_ExamineStageId, ExamineStageId, "BeUserId", ttEnt.BeUserId, "BeRoleCode", ttEnt.BeRoleCode, "BeDeptId", ttEnt.BeDeptId, "ToUserId", ttEnt.ToUserId, "ToRoleCode", ttEnt.ToRoleCode);
                }
                else
                {
                    etEnts = ExamineTask.FindAllByProperties(ExamineTask.Prop_ExamineStageId, ExamineStageId, "BeUserId", ttEnt.BeUserId, "BeDeptId", ttEnt.BeDeptId, "ToUserId", ttEnt.ToUserId);
                }
                if (etEnts.Count > 0)
                {
                    ttEnt.AmendState = "-";
                }
                else
                {
                    ttEnt.AmendState = "+";
                }
                ttEnt.DoUpdate();
            }
            etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, ExamineStageId);
            foreach (ExamineTask etEnt in etEnts)
            {
                if (esEnt.StageType == "院级考核")
                {
                    ttEnts = TempTask.FindAllByProperties("ExamineStageId", ExamineStageId, "BeUserId", etEnt.BeUserId, "BeRoleCode", etEnt.BeRoleCode, "BeDeptId", etEnt.BeDeptId, "ToUserId", etEnt.ToUserId, "ToRoleCode", etEnt.ToRoleCode);
                }
                else
                {
                    ttEnts = TempTask.FindAllByProperties("ExamineStageId", ExamineStageId, "BeUserId", etEnt.BeUserId, "BeDeptId", etEnt.BeDeptId, "ToUserId", etEnt.ToUserId);
                }
                if (ttEnts.Count == 0)
                {
                    etEnt.AmendState = "-";
                }
                etEnt.DoUpdate();
            }
            sql = "delete BJKY_Examine..TempTask where ExamineStageId='" + ExamineStageId + "' and AmendState='-'";
            DataHelper.ExecSql(sql);
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
    }
}


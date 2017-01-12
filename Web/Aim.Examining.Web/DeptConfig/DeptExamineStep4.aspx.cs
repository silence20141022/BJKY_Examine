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
using Aim.Examining.Web;
using Aim.Examining.Model;
using System.Data;

namespace Aim.Examining.Web
{
    public partial class DeptExamineStep4 : ExamListPage
    {
        string id = string.Empty;
        string sql = "";
        ExamineStage esEnt = null;
        IList<ExamineTask> etEnts = null;
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
                case "delete":
                    string ids = RequestData.Get<string>("ids");
                    sql = "delete BJKY_Examine..ExamineTask where PatIndex('%'+id+'%','" + ids + "')>0";
                    DataHelper.ExecSql(sql);
                    break;
                case "canceltask":
                    if (esEnt.State == 1)
                    {
                        sql = "delete BJKY_Examine..ExamineTask where ExamineStageId='" + id + "'";
                        DataHelper.ExecSql(sql);
                        sql = "delete BJKY_Examine..CustomIndicator where ExamineStageId='" + id + "'";
                        DataHelper.ExecSql(sql);
                        esEnt.TaskQuan = 0;
                        esEnt.State = 0;
                        esEnt.DoUpdate();
                    }
                    break;
                case "createtask":
                    if (esEnt.State == 0)
                    {
                        CreateExamineTask();//同时创建自定义的考核指标
                        esEnt.State = 1;//更新考核阶段状态为1 已生成
                        etEnts = ExamineTask.FindAllByProperty(ExamineTask.Prop_ExamineStageId, id);
                        esEnt.TaskQuan = etEnts.Count;
                        esEnt.DoUpdate();
                    }
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void CreateExamineTask() //
        {
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, esEnt.Id);
            //if (esEnt.ExamineType == "院级考核")
            //{
            //    foreach (ExamineStageDetail esdEnt in esdEnts)
            //    {
            //        string beUserIds = string.Empty;//存储被考核对象具体的人
            //        string beUserNames = string.Empty;
            //        string beDeptIds = string.Empty;
            //        string beDeptNames = string.Empty;
            //        string[] array = GetBeUsersInfo(esdEnt);
            //        beUserIds = array[0];
            //        beUserNames = array[1];
            //        beDeptIds = array[2];
            //        beDeptNames = array[3];
            //        ExamineRelation erEnt = ExamineRelation.Find(esdEnt.ExamineRelationId);
            //        ConfirmPara(beUserIds, beUserNames, beDeptIds, beDeptNames, esdEnt, erEnt);
            //        CreateSpecialTask(beUserIds, beUserNames, beDeptIds, beDeptNames, esdEnt);
            //    }
            //}
            //else//部门级考核
            //{
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
                            ExamineTask etEnt = new ExamineTask();//部门级考核不存在角色的概念。直接对人生成任务
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
                // }
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
            string toDeptIds = "";
            string toDeptNames = "";
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
                            toDeptIds = pcEnts[0].ClerkGroupIds;
                            toDeptNames = pcEnts[0].ClerkGroupNames;
                        }
                    }
                    break;
                case "ExcutiveDeptLeader"://职能服务部门正职               
                    sql = @"select FirstLeaderIds as UserIds,FirstLeaderNames as UserNames,FirstLeaderGroupIds as DeptIds,FirstLeaderGroupNames as DeptNames
                    from BJKY_Examine..PersonConfig where GroupType='职能服务部门' and Id in 
                    (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0]; toUserNames = array[1];
                    toDeptIds = array[2]; toDeptNames = array[3];
                    break;
                case "BusinessDeptLeader"://经营目标单位正职
                    sql = @"select FirstLeaderIds as UserIds, FirstLeaderNames as UserNames,FirstLeaderGroupIds as DeptIds,FirstLeaderGroupNames as DeptNames
                    from BJKY_Examine..PersonConfig where GroupType='经营目标单位' and Id in 
                    (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0]; toUserNames = array[1];
                    toDeptIds = array[2]; toDeptNames = array[3];
                    break;
                case "ExcutiveDeptClerkDelegate"://职能部门员工代表
                    sql = @"select InstituteClerkDelegateIds as UserIds,InstituteClerkDelegateNames as UserNames,InstituteClerkDelegateGroupIds as DeptIds,InstituteClerkDelegateGroupNames as DeptNames
                    from BJKY_Examine..PersonConfig
                    where GroupType='职能服务部门' and Id in  (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0]; toUserNames = array[1];
                    toDeptIds = array[2]; toDeptNames = array[3];
                    break;
                case "BusinessDeptClerkDelegate"://经营单位员工代表
                    sql = @"select  InstituteClerkDelegateIds as UserIds, InstituteClerkDelegateNames as UserNames,InstituteClerkDelegateGroupIds as DeptIds,InstituteClerkDelegateGroupNames as DeptNames
                    from BJKY_Examine..PersonConfig
                    where GroupType='经营目标单位' and Id in (select GroupID from BJKY_Examine..ExamineStageDeptDetail where ExamineStageId='" + esEnt.Id + "')";
                    array = GetUserIdAndName(DataHelper.QueryDictList(sql));
                    toUserIds = array[0]; toUserNames = array[1];
                    toDeptIds = array[2]; toDeptNames = array[3];
                    break;

            }
            string[] beUserIdArray = new string[] { }; string[] beUserNameArray = new string[] { };
            string[] beDeptIdArray = new string[] { }; string[] beDeptNameArray = new string[] { };
            string[] toUserIdArray = new string[] { }; string[] toUserNameArray = new string[] { };
            string[] toDeptIdArray = new string[] { }; string[] toDeptNameArray = new string[] { };
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
            if (!string.IsNullOrEmpty(toDeptIds))
            {
                toDeptIdArray = toDeptIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(toDeptNames))
            {
                toDeptNameArray = toDeptNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            #region
            for (int i = 0; i < beUserIdArray.Length; i++)
            {
                for (int j = 0; j < toUserIdArray.Length; j++)
                {
                    if (beUserIdArray[i] != toUserIdArray[j])//防止一个人身兼两职。自己对自己不能打分
                    {
                        ExamineTask etEnt = new ExamineTask();
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
                        etEnt.ToUserName = toUserNameArray[j];
                        etEnt.ToDeptId = toDeptIdArray[j];
                        etEnt.ToDeptName = toDeptNameArray[j];
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
                                    ExamineTask etEnt = new ExamineTask();
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
                                    sql = @" select  Id,GroupName from BJKY_Examine..PersonConfig
                                    where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and 
                                    (GroupType='职能服务部门' or GroupType='经营目标单位')";
                                    sql = string.Format(sql, toUserIdArray[j]);
                                    IList<EasyDictionary> dics3 = DataHelper.QueryDictList(sql);
                                    if (dics3.Count > 0)
                                    {
                                        etEnt.ToDeptId = dics3[0].Get<string>("Id");
                                        etEnt.ToDeptName = dics3[0].Get<string>("GroupName");
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
                esddEnts = ExamineStageDeptDetail.FindAllByProperties("ExamineStageId", id, "GroupType", "职能服务部门");
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
                esddEnts = ExamineStageDeptDetail.FindAllByProperties("ExamineStageId", id, "GroupType", "经营目标单位");
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
        private void CreateCustomIndicator(string beUserId, string beUserName, ExamineStage esEnt, ExamineStageDetail esdEnt)
        {
            ExamineIndicator eiEnt = ExamineIndicator.Find(esdEnt.ExamineIndicatorId);
            IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperties(IndicatorFirst.Prop_ExamineIndicatorId, eiEnt.Id, IndicatorFirst.Prop_CustomColumn, "T");
            if (ifEnts.Count > 0)//只有当考核指标下指定了需要自定义的指标后，才有必要创建自定义指标 如果没有直接生成考核任务即可
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
            sql = "select * from BJKY_Examine..ExamineTask where ExamineStageId='" + id + "'" + where;
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("State", esEnt.State);
        }
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(1) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "ToUserName";
            string asc = search.Orders.Count <= 0 || !search.Orders[0].Ascending ? " asc" : " desc";
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


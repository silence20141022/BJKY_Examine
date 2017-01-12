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
    public partial class DeptExamineStep7 : ExamBasePage
    {
        string id = string.Empty;
        ExamineStage esEnt = null;
        string ToUserId = "";
        string sql = "";
        IList<ExamineStageDeptDetail> esddEnts = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestData.Get<string>("id");
            ToUserId = RequestData.Get<string>("ToUserId");
            if (!string.IsNullOrEmpty(id))
            {
                esEnt = ExamineStage.Find(id);
            }
            switch (RequestActionString)
            {
                case "endexamine":
                    EndExamine();
                    PageState.Add("Id", esEnt.Id);
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
            string sql = @"select  A.ToUserId,A.ToUserName,(select Phone from SysUser where UserID=A.ToUserId) as Phone,
           (select count(Id) from  BJKY_Examine..ExamineTask where ExamineStageId='{0}'
            and State='1' and ToUserId=A.ToUserId) as UnSubmitQuan,count(*) as TaskQuan from BJKY_Examine..ExamineTask as A
            where A.ExamineStageId='{1}'" + where + "group by A.ToUserId ,A.ToUserName";
            sql = string.Format(sql, id, id);
            PageState.Add("DataList", GetPageData(sql, SearchCriterion));
            PageState.Add("State", esEnt.State);
        }
        private void EndExamine()
        {
            IList<ExamineStageDetail> esdEnts = ExamineStageDetail.FindAllByProperty(ExamineStageDetail.Prop_ExamineStageId, id);
            #region 院级考核算分
            if (esEnt.ExamineType == "院级考核")
            {
                string beUserIds = string.Empty;//存储被考核对象具体的人
                string beUserNames = string.Empty;
                string beDeptIds = string.Empty;
                string beDeptNames = string.Empty;
                foreach (ExamineStageDetail esdEnt in esdEnts)//循环考核对象
                {
                    string[] array = GetBeUsersInfo(esdEnt);
                    beUserIds = array[0];
                    beUserNames = array[1];
                    beDeptIds = array[2];
                    beDeptNames = array[3];
                    //人+部门+被考核对象CODE就可以找到这个其下所有的任务 不能单纯按人查。因为有特例。一个人在在多个部门任职
                    string[] beUserIdArray = new string[] { }; string[] beUserNameArray = new string[] { };
                    string[] beDeptIdArray = new string[] { }; string[] beDeptNameArray = new string[] { };
                    if (!string.IsNullOrEmpty(beUserIds))//防止部分角色下无人
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
                    ExamineRelation erEnt = ExamineRelation.Find(esdEnt.ExamineRelationId);
                    string UpLevelCode = ""; string SameLevelCode = ""; string DownLevelCode = "";
                    if (!string.IsNullOrEmpty(erEnt.UpLevelCode))
                    {
                        string[] upArray = erEnt.UpLevelCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        for (int n = 0; n < upArray.Length; n++)
                        {
                            if (n != upArray.Length - 1)
                            {
                                UpLevelCode += "'" + upArray[n] + "',";
                            }
                            else
                            {
                                UpLevelCode += "'" + upArray[n] + "'";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(erEnt.SameLevelCode))
                    {
                        string[] sameArray = erEnt.SameLevelCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        for (int n = 0; n < sameArray.Length; n++)
                        {
                            if (n != sameArray.Length - 1)
                            {
                                SameLevelCode += "'" + sameArray[n] + "',";
                            }
                            else
                            {
                                SameLevelCode += "'" + sameArray[n] + "'";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(erEnt.DownLevelCode))
                    {
                        string[] downArray = erEnt.DownLevelCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        for (int n = 0; n < downArray.Length; n++)
                        {
                            if (n != downArray.Length - 1)
                            {
                                DownLevelCode += "'" + downArray[n] + "',";
                            }
                            else
                            {
                                DownLevelCode += "'" + downArray[n] + "'";
                            }
                        }
                    }
                    #region 开始计算个人成绩
                    for (int i = 0; i < beUserIdArray.Length; i++)
                    {
                        ExamineStageResult esrEnt = new ExamineStageResult();
                        esrEnt.ExamineStageId = esEnt.Id;
                        esrEnt.BeRoleCode = esdEnt.BeRoleCode;
                        esrEnt.BeRoleName = esdEnt.BeRoleName;
                        esrEnt.UserId = beUserIdArray[i];
                        esrEnt.UserName = beUserNameArray[i];
                        esrEnt.DeptId = beDeptIdArray[i];
                        esrEnt.DeptName = beDeptNameArray[i];
                        esrEnt.StageType = esEnt.StageType;
                        esrEnt.Year = esEnt.Year;
                        //分别计算上级  同级  下级评分   
                        decimal tempScore = 0; string subBalanceUserIds = ""; int subBalanceWeightTotal = 0;
                        if (esdEnt.BeRoleCode == "BeBusinessDeptLeader")//经营目标单位正职 （有一部分特殊任务）   需要计算特殊分   Tag='1'
                        {
                            sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                            and BeDeptId='{2}' and BeUserId='{3}' and Tag='1'";
                            sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i]);
                            tempScore = DataHelper.QueryValue<decimal>(sql);
                        }
                        if (!string.IsNullOrEmpty(UpLevelCode))//如果有上级考核对象
                        {
                            //因为考核关系有时候需要把级别权重拆分到人分权重   不能单独取所有上级的平均分 //20130430  已更新到如下代码                           
                            IList<UserBalance> ubEnts = UserBalance.FindAllByProperties("ExamineRelationId", erEnt.Id, "ToRoleCode", "UpLevel");//该考核关系下 上级考核的人员权重明细
                            foreach (UserBalance ubEnt in ubEnts)
                            {
                                if (ubEnt.Balance > 0)
                                {
                                    subBalanceUserIds += ubEnt.ToUserId + ",";
                                    subBalanceWeightTotal += ubEnt.Balance.Value;
                                }
                            }
                            decimal subScore = 0;
                            if (subBalanceWeightTotal < 100)//如果人员权重没有将100的总权重分配完
                            {
                                sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                                and BeDeptId='{2}' and BeUserId='{3}' and ToRoleCode in (" + UpLevelCode + ") and PatIndex('%ToUserId%','{4}')= 0";//去掉已分配人员权重的人
                                sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i], subBalanceUserIds);
                                if (DataHelper.QueryValue<decimal>(sql) > 0)
                                {
                                    foreach (UserBalance ubEnt in ubEnts)
                                    {
                                        if (ubEnt.Balance > 0)
                                        {
                                            sql = @"select top 1 Score from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                                            and BeDeptId='{2}' and BeUserId='{3}' and  ToUserId='{4}'";
                                            sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i], ubEnt.ToUserId);
                                            subScore += DataHelper.QueryValue<decimal>(sql) * (ubEnt.Balance.Value / 100);
                                        }
                                    }
                                    esrEnt.UpAvgScore = subScore + (DataHelper.QueryValue<decimal>(sql) * (100 - subBalanceWeightTotal) / 100) + tempScore;
                                }
                                else //如果没有其他的分数，但权重又没有分配完
                                {
                                    foreach (UserBalance ubEnt in ubEnts)
                                    {
                                        if (ubEnt.Balance > 0)
                                        {
                                            sql = @"select top 1 Score from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                                            and BeDeptId='{2}' and BeUserId='{3}' and  ToUserId='{4}'";
                                            sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i], ubEnt.ToUserId);
                                            subScore += DataHelper.QueryValue<decimal>(sql) * (ubEnt.Balance.Value / subBalanceWeightTotal);
                                        }
                                    }
                                    esrEnt.UpAvgScore = subScore + tempScore;
                                }
                            }
                            else//如果人员权重已经满100，这个时候不计算已打分但未分配子权重的人员提交的考核任务
                            {
                                esrEnt.UpAvgScore = subScore + tempScore;
                            }
                        }
                        if (!string.IsNullOrEmpty(SameLevelCode))//如果有同级考核对象
                        {
                            sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                            and BeDeptId='{2}' and BeUserId='{3}' and ToRoleCode in (" + SameLevelCode + ")";
                            sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i]);
                            esrEnt.SameAvgScore = DataHelper.QueryValue<decimal>(sql) + tempScore;
                        }
                        if (!string.IsNullOrEmpty(DownLevelCode))//如果有下级考核对象
                        {
                            sql = @"select avg(isnull(Score,0)) from BJKY_Examine..ExamineTask where State='2' and ExamineStageId='{0}' and BeRoleCode='{1}'
                            and BeDeptId='{2}' and BeUserId='{3}' and ToRoleCode in (" + DownLevelCode + ")";
                            sql = string.Format(sql, esEnt.Id, esdEnt.BeRoleCode, beDeptIdArray[i], beUserIdArray[i]);
                            esrEnt.DownAvgScore = DataHelper.QueryValue<decimal>(sql) + tempScore;
                        }
                        decimal score = 0;
                        //这里需要考虑一种特例就是  权重配好了 但可能没人给他打分   就是有权重  无分的现象  这个时候该权重不计入总权重
                        int upWeight = 0; int sameWeight = 0; int downWeight = 0;
                        if (erEnt.UpLevelWeight > 0 && esrEnt.UpAvgScore > 0)
                        {
                            upWeight = erEnt.UpLevelWeight.Value;
                        }
                        if (erEnt.SameLevelWeight > 0 && esrEnt.SameAvgScore > 0)
                        {
                            sameWeight = erEnt.SameLevelWeight.Value;
                        }
                        if (erEnt.DownLevelWeight > 0 && esrEnt.DownAvgScore > 0)
                        {
                            downWeight = erEnt.DownLevelWeight.Value;
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
                    #endregion
                }
            }
            #endregion
            #region 部门级考核
            else  //部门级考核关联的是部门级考核关系
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
                    for (int i = 0; i < beUserIdArray.Length; i++)
                    {
                        ExamineStageResult esrEnt = new ExamineStageResult();
                        esrEnt.ExamineStageId = esEnt.Id;
                        esrEnt.UserId = beUserIdArray[i];
                        esrEnt.UserName = beUserNameArray[i];
                        esrEnt.DeptId = esEnt.LaunchDeptId;
                        esrEnt.DeptName = esEnt.LaunchDeptName;
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
                            //                                                                        and BeUserId='{1}' and  ToUserId ='{2}'";
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
            }
            #endregion
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
        private void CaculateYearResult()
        {
            IList<EasyDictionary> dics = null;
            //每次部门内被考核的人员可能有所变动 以最后一阶段的人为准
            IList<ExamineStageResult> esrEnts = ExamineStageResult.FindAllByProperty("SortIndex", ExamineStageResult.Prop_ExamineStageId, esEnt.Id);
            for (int i = 0; i < esrEnts.Count; i++)
            {
                ExamYearResult eyrEnt = new ExamYearResult();
                eyrEnt.ExamineStageId = esEnt.Id;
                if (esEnt.ExamineType == "院级考核")
                {
                    eyrEnt.BeRoleCode = esrEnts[i].BeRoleCode;
                    eyrEnt.BeRoleName = esrEnts[i].BeRoleName;
                }
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
        private IList<EasyDictionary> GetPageData(String sql, SearchCriterion search)
        {
            SearchCriterion.RecordCount = DataHelper.QueryValue<int>("select count(*) from (" + sql + ") t");
            string order = search.Orders.Count > 0 ? search.Orders[0].PropertyName : "ToUserName";
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
    }
}


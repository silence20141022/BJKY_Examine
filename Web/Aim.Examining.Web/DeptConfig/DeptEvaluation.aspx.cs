using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Aim.Examining.Model;
using Aim.Data;
using System.Collections.Generic;

namespace Aim.Examining.Web.DetpConfig
{
    public partial class DeptEvaluation : ExamBasePage
    {
        string Index = "";
        string ExamineStageId = "";
        string sql = "";
        string id = "";
        string ExamineRelationId = "";
        ExamineStage esEnt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Index = RequestData.Get<string>("Index");
            ExamineStageId = RequestData.Get<string>("ExamineStageId");
            if (!string.IsNullOrEmpty(ExamineStageId))
            {
                esEnt = ExamineStage.Find(ExamineStageId);
            }
            id = RequestData.Get<string>("id");
            ExamineRelationId = RequestData.Get<string>("ExamineRelationId");
            switch (RequestActionString)
            {
                case "SaveSubScore":
                    SaveSubScore();
                    break;
                case "Submit":
                    SubmitTask();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            string sequence = "";
            DeptExamineRelation derEnt = DeptExamineRelation.TryFind(ExamineRelationId);//根据阶段取得部门
            if (derEnt != null)
            {
                sequence = derEnt.BeUserNames;
            }
            if (Index == "0")
            {
                sql = @"select *,CharIndex(BeUserName,'{0}') as SortIndex from BJKY_Examine..ExamineTask where State='1' and
                ExamineStageId='{1}' and  ToUserId='{2}' and  ExamineRelationId='{3}' order by SortIndex ";
                sql = string.Format(sql, sequence, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (Index == "1")
            {
                sql = @"select *,CharIndex(BeUserName,'{0}') as SortIndex  from BJKY_Examine..ExamineTask where State='2' and
                ExamineStageId='{1}' and  ToUserId='{2}' and  ExamineRelationId='{3}'";
                sql = string.Format(sql, sequence, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (Index == "2")
            {
                sql = @"select *,CharIndex(BeUserName,'{0}') as SortIndex  from BJKY_Examine..ExamineTask  where State='3' and
                ExamineStageId='{1}' and  ToUserId='{2}' and  ExamineRelationId='{3}' order by SortIndex ";
                sql = string.Format(sql, sequence, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (!string.IsNullOrEmpty(id))//单任务查看明细
            {
                sql = @"select * from BJKY_Examine..ExamineTask where Id='" + id + "'";
            }

            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);//找到所有符合条件的任务
            if (dics.Count > 0)
            {
                if (Index != "2")
                {
                    DataTable dt = new DataTable();
                    DataColumn dc = new DataColumn("Id"); dt.Columns.Add(dc);
                    dc = new DataColumn("BeUserName"); dt.Columns.Add(dc);
                    dc = new DataColumn("BeDeptName"); dt.Columns.Add(dc);
                    dc = new DataColumn("Score"); dt.Columns.Add(dc);
                    dc = new DataColumn("Tag"); dt.Columns.Add(dc);

                    DataTable dt1 = new DataTable();//这个表专门用来存储一级指标名称和各2级指标数量
                    DataColumn dc1 = new DataColumn("IndicatorFirstName"); dt1.Columns.Add(dc1);
                    dc1 = new DataColumn("SecondCount"); dt1.Columns.Add(dc1);
                    ExamineIndicator eiEnt = ExamineIndicator.Find(dics[0].Get<string>("ExamineIndicatorId"));
                    IList<IndicatorFirst> ifEnts = IndicatorFirst.FindAllByProperty("SortIndex", IndicatorFirst.Prop_ExamineIndicatorId, eiEnt.Id);
                    foreach (IndicatorFirst ifEnt in ifEnts)
                    {
                        IList<IndicatorSecond> isEnts = IndicatorSecond.FindAllByProperty("SortIndex", IndicatorSecond.Prop_IndicatorFirstId, ifEnt.Id);
                        DataRow dr1 = dt1.NewRow();
                        dr1["IndicatorFirstName"] = ifEnt.IndicatorFirstName + "(" + ifEnt.MaxScore + ")";
                        dr1["SecondCount"] = isEnts.Count;
                        if (isEnts.Count > 0)
                        {
                            dt1.Rows.Add(dr1);//防止只有一级指标无2级指标。
                        }
                        foreach (IndicatorSecond isEnt in isEnts)
                        {
                            string standard = "!" + ifEnt.CustomColumn + "#" + isEnt.ToolTip;//加入#号前面的字符是为了验证自定义指标列
                            dc = new DataColumn(isEnt.Id + isEnt.IndicatorSecondName + "(" + isEnt.MaxScore + ")" + standard);//列名有多种信息组合而成。便于前台取对应的值
                            dt.Columns.Add(dc);//循环2级指标构建剩余列
                        }
                    }
                    IList<EasyDictionary> secDics = null;
                    if (dics.Count > 0)//通过任务的指标Id找到其下所有 的二级指标
                    {
                        sql = @"select A.Id,  a.IndicatorSecondName,B.CustomColumn,A.ToolTip,A.MaxScore from  BJKY_Examine..IndicatorSecond A	                           
	                          left join BJKY_Examine..IndicatorFirst B  on  A.IndicatorFirstId=B.Id 
	                          where B.ExamineIndicatorid='{0}' ";
                        sql = string.Format(sql, dics[0].Get<string>("ExamineIndicatorId"));
                        secDics = DataHelper.QueryDictList(sql);
                    }
                    foreach (EasyDictionary dic in dics)//通过任务构建行记录
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = dic.Get<string>("Id");
                        dr["BeUserName"] = dic.Get<string>("BeUserName");
                        dr["BeDeptName"] = dic.Get<string>("BeDeptName");
                        dr["Score"] = dic.Get<string>("Score");
                        dr["Tag"] = dic.Get<string>("Tag");
                        foreach (EasyDictionary secDic in secDics)
                        {
                            string standard = "!" + secDic.Get<string>("CustomColumn") + "#" + secDic.Get<string>("ToolTip");
                            IList<IndicatorScore> insEnts = IndicatorScore.FindAllByProperties(IndicatorScore.Prop_ExamineTaskId, dic.Get<string>("Id"), IndicatorScore.Prop_IndicatorSecondId, secDic.Get<string>("Id"));
                            if (insEnts.Count > 0)
                            {
                                dr[secDic.Get<string>("Id") + secDic.Get<string>("IndicatorSecondName") + "(" + secDic.Get<string>("MaxScore") + ")" + standard] = insEnts[0].SubScore;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    PageState.Add("DataList", dt);
                    PageState.Add("ColumnData", dt.Columns);
                    PageState.Add("DataList1", dt1);
                }
                else
                {
                    InitialHistoryScore(dics);
                }
                esEnt = ExamineStage.Find(ExamineStageId);
                string stageType = "";
                if (esEnt.StageType != "4")
                {
                    stageType = "第" + esEnt.StageType + "季度";
                }
                else
                {
                    stageType = "年度";
                }
                PageState.Add("Title", esEnt.Year + stageType + "考核评分表");
            }
        }
        private void SaveSubScore()
        {
            string ExamineTaskId = RequestData.Get<string>("ExamineTaskId");
            ExamineTask etEnt = ExamineTask.Find(ExamineTaskId);
            string IndicatorSecondId = RequestData.Get<string>("IndicatorSecondId");
            decimal SubScore = RequestData.Get<decimal>("SubScore");
            //decimal Score = RequestData.Get<decimal>("Score");
            IList<IndicatorScore> insEnts = IndicatorScore.FindAllByProperties(IndicatorScore.Prop_ExamineTaskId, ExamineTaskId, IndicatorScore.Prop_IndicatorSecondId, IndicatorSecondId);
            if (insEnts.Count > 0)//更新
            {
                insEnts[0].SubScore = SubScore;
                insEnts[0].DoUpdate();
            }
            else//创建
            {
                IndicatorScore insEnt = new IndicatorScore();
                IndicatorSecond isEnt = IndicatorSecond.Find(IndicatorSecondId);
                IndicatorFirst ifEnt = IndicatorFirst.Find(isEnt.IndicatorFirstId);
                string tooltip = "!" + ifEnt.CustomColumn + "#" + isEnt.ToolTip;//加入#号前面的字符是为了标识该指标下有自定义指标，有明细分                
                insEnt.ExamineTaskId = ExamineTaskId;
                insEnt.IndicatorFirstId = isEnt.IndicatorFirstId;
                insEnt.IndicatorFirstName = isEnt.IndicatorFirstName;
                insEnt.FirstMaxScore = ifEnt.MaxScore;
                insEnt.FirstSortIndex = ifEnt.SortIndex;
                insEnt.IndicatorSecondId = isEnt.Id;
                insEnt.IndicatorSecondName = isEnt.IndicatorSecondName;
                insEnt.SecondMaxScore = isEnt.MaxScore;
                insEnt.SecondSortIndex = isEnt.SortIndex;
                insEnt.SubScore = SubScore;
                insEnt.ToolTip = tooltip;
                insEnt.DoCreate();
            }
            sql = "select isnull(SUM(SubScore),0) FROM BJKY_Examine..IndicatorScore WHERE ExamineTaskId = '" + ExamineTaskId + "'";
            etEnt.Score = DataHelper.QueryValue<decimal>(sql);
            etEnt.DoUpdate();
            PageState.Add("Score", etEnt.Score);
        }
        private void InitialHistoryScore(IList<EasyDictionary> dics)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Id"); dt.Columns.Add(dc);
            dc = new DataColumn("BeUserName"); dt.Columns.Add(dc);
            dc = new DataColumn("BeDeptName"); dt.Columns.Add(dc);
            dc = new DataColumn("Score"); dt.Columns.Add(dc);
            dc = new DataColumn("Tag"); dt.Columns.Add(dc);

            DataTable dt1 = new DataTable();//这个表专门用来存储一级指标名称和各2级指标数量
            DataColumn dc1 = new DataColumn("IndicatorFirstName"); dt1.Columns.Add(dc1);
            dc1 = new DataColumn("SecondCount"); dt1.Columns.Add(dc1);
            sql = @"select distinct IndicatorFirstId, IndicatorFirstName,FirstMaxScore,IndicatorSecondId,IndicatorSecondName,SecondMaxScore,ToolTip 
                from BJKY_Examine..IndicatorScore where ExamineTaskId in 
                (select Id from BJKY_Examine..ExamineTask  where State='3' and
                ExamineStageId='" + ExamineStageId + "' and  ToUserId='" + UserInfo.UserID + "' and  ExamineRelationId='" + ExamineRelationId + "')   ";
            IList<EasyDictionary> inDics = DataHelper.QueryDictList(sql);
            string indicatorFirstId = "";
            int count = 0;
            for (int i = 0; i < inDics.Count; i++)
            {
                if (inDics[i].Get<string>("IndicatorFirstId") != indicatorFirstId)
                {
                    if (!string.IsNullOrEmpty(indicatorFirstId))
                    {
                        DataRow dr1 = dt1.NewRow();
                        dr1["IndicatorFirstName"] = inDics[i - 1].Get<string>("IndicatorFirstName") + "(" + inDics[i - 1].Get<string>("FirstMaxScore") + ")";
                        dr1["SecondCount"] = count;
                        dt1.Rows.Add(dr1);
                    }
                    count = 0;
                    indicatorFirstId = inDics[i].Get<string>("IndicatorFirstId");
                }
                count += 1;
                if (i == inDics.Count - 1)//最后一个
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["IndicatorFirstName"] = inDics[i].Get<string>("IndicatorFirstName") + "(" + inDics[i].Get<string>("FirstMaxScore") + ")";
                    dr1["SecondCount"] = count;
                    dt1.Rows.Add(dr1);
                }
                dc = new DataColumn(inDics[i].Get<string>("IndicatorSecondId") + inDics[i].Get<string>("IndicatorSecondName") + "(" + inDics[i].Get<string>("SecondMaxScore") + ")" + inDics[i].Get<string>("ToolTip"));//列名有多种信息组合而成。便于前台取对应的值
                dt.Columns.Add(dc);
            }
            foreach (EasyDictionary dic0 in dics)//插入数据
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = dic0.Get<string>("Id");
                dr["BeUserName"] = dic0.Get<string>("BeUserName");
                dr["BeDeptName"] = dic0.Get<string>("BeDeptName");
                dr["Score"] = dic0.Get<string>("Score");
                dr["Tag"] = dic0.Get<string>("Tag");
                sql = @"select * from  BJKY_Examine..IndicatorScore where ExamineTaskId='" + dic0.Get<string>("Id") + "'";
                IList<EasyDictionary> scoreDics = DataHelper.QueryDictList(sql);
                foreach (EasyDictionary dic4 in scoreDics)
                {
                    dr[dic4.Get<string>("IndicatorSecondId") + dic4.Get<string>("IndicatorSecondName") + "(" + dic4.Get<string>("SecondMaxScore") + ")" + dic4.Get<string>("ToolTip")] = dic4.Get<string>("SubScore");
                }
                dt.Rows.Add(dr);
            }
            PageState.Add("DataList", dt);
            PageState.Add("ColumnData", dt.Columns);
            PageState.Add("DataList1", dt1);
        }
        private void SubmitTask()
        {
            IList<string> taskIds = RequestData.GetList<string>("taskIds");
            if (taskIds.Count > 0)
            {
                foreach (string str in taskIds)
                {
                    ExamineTask etEnt = ExamineTask.Find(str);
                    etEnt.State = "2";
                    etEnt.DoUpdate();
                }
            }
        }
    }
}

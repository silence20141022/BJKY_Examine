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

namespace Aim.Examining.Web.ExamineTaskManage
{
    public partial class ExamineEvaluation : ExamBasePage
    {
        string Index = "";
        string ExamineStageId = "";
        string sql = "";
        string id = "";
        string ExamineRelationId = "";
        string BeRoleCode = "";
        //string BeRoleName = "";
        //string ToRoleCode = "";
        //string ToRoleName = "";
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
            BeRoleCode = RequestData.Get<string>("BeRoleCode");
            //ToRoleCode = RequestData.Get<string>("ToRoleCode");
            //BeRoleName = Server.HtmlDecode(RequestData.Get<string>("BeRoleName"));
            //ToRoleName = Server.HtmlDecode(RequestData.Get<string>("ToRoleName"));
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
            if (Index == "0")
            {
                sql = @"select * from BJKY_Examine..ExamineTask where State='1' and
                ExamineStageId='{0}' and  ToUserId='{1}' and  ExamineRelationId='{2}' order by BeDeptName,BeUserName ";
                sql = string.Format(sql, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (Index == "1")
            {
                sql = @"select * from BJKY_Examine..ExamineTask where State='2' and
                ExamineStageId='{0}' and  ToUserId='{1}' and  ExamineRelationId='{2}' order by BeDeptName,BeUserName";
                sql = string.Format(sql, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (Index == "2")
            {
                sql = @"select *  from BJKY_Examine..ExamineTask where State='3' and
                ExamineStageId='{0}' and  ToUserId='{1}' and  ExamineRelationId='{2}' order by BeDeptName,BeUserName";
                sql = string.Format(sql, ExamineStageId, UserInfo.UserID, ExamineRelationId);
            }
            if (!string.IsNullOrEmpty(id))//单任务查看明细
            {
                ExamineTask etEnt = ExamineTask.Find(id);
                ExamineStageId = etEnt.ExamineStageId;
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
                            string standard = "!" + ifEnt.InsteadColumn + "#" + isEnt.ToolTip;//加入#号前面的字符是为了验证经营开发正职 部分要素分是由人力资源部打的
                            dc = new DataColumn(isEnt.Id + isEnt.IndicatorSecondName + "(" + isEnt.MaxScore + ")" + standard);//列名有多种信息组合而成。便于前台取对应的值
                            dt.Columns.Add(dc);//循环2级指标构建剩余列
                        }
                    }
                    IList<EasyDictionary> secDics = null;
                    if (dics.Count > 0)//通过任务的指标Id找到其下所有 的二级指标
                    {
                        sql = @"select A.Id,  a.IndicatorSecondName,B.InsteadColumn,A.ToolTip,A.MaxScore from  BJKY_Examine..IndicatorSecond A	                           
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
                            string standard = "!" + secDic.Get<string>("InsteadColumn") + "#" + secDic.Get<string>("ToolTip");
                            IList<IndicatorScore> insEnts = IndicatorScore.FindAllByProperties(IndicatorScore.Prop_ExamineTaskId, dic.Get<string>("Id"), IndicatorScore.Prop_IndicatorSecondId, secDic.Get<string>("Id"));
                            if (insEnts.Count > 0)
                            {
                                dr[secDic.Get<string>("Id") + secDic.Get<string>("IndicatorSecondName") + "(" + secDic.Get<string>("MaxScore") + ")" + standard] = insEnts[0].SubScore;
                            }
                            //if (standard.IndexOf("T") > 0)
                            //{
                            //    sql = @"select Top 1 Score  from BJKY_Examine..ExamineTask where BeUserId='{0}' and ToUserId='D2564369-7FFE-45A5-8830-14EE3A8833F7'";
                            //    sql = string.Format(sql, dic.Get<string>("BeUserId"));
                            //    dr[secDic.Get<string>("Id") + secDic.Get<string>("IndicatorSecondName") + "(" + secDic.Get<string>("MaxScore") + ")" + standard] = DataHelper.QueryValue<decimal>(sql);
                            //}
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
                ExamineStage esEnt = ExamineStage.Find(ExamineStageId);
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
            decimal Score = RequestData.Get<decimal>("Score");
            IList<IndicatorScore> insEnts = IndicatorScore.FindAllByProperties(IndicatorScore.Prop_ExamineTaskId, ExamineTaskId, IndicatorScore.Prop_IndicatorSecondId, IndicatorSecondId);
            if (insEnts.Count > 0)//更新
            {
                insEnts[0].SubScore = SubScore; insEnts[0].DoUpdate();
            }
            else//创建
            {
                IndicatorScore insEnt = new IndicatorScore();
                IndicatorSecond isEnt = IndicatorSecond.Find(IndicatorSecondId);
                IndicatorFirst ifEnt = IndicatorFirst.Find(isEnt.IndicatorFirstId);
                string tooltip = "!" + ifEnt.InsteadColumn + "#" + isEnt.ToolTip;//加入#号前面的字符是为了验证经营开发正职 部分要素分是由人力资源部打的                
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
                etEnt.Score = Score;
                etEnt.DoUpdate();
            }
            etEnt.Score = Score;
            etEnt.DoUpdate();
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
            sql = @"select *,(select count(Id) from BJKY_Examine..IndicatorScore where IndicatorFirstId=t.IndicatorFirstId and ExamineTaskId='{0}') as SecQuan           
             from (select distinct IndicatorFirstId,IndicatorFirstName,FirstMaxScore,FirstSortIndex from BJKY_Examine..IndicatorScore 
             where ExamineTaskId='{1}' ) as t order by FirstSortIndex asc";
            sql = string.Format(sql, dics[0].Get<string>("Id"), dics[0].Get<string>("Id"));//找到某一个任务下所有的一级指标
            IList<EasyDictionary> inDics = DataHelper.QueryDictList(sql);
            foreach (EasyDictionary dic1 in inDics)
            {
                DataRow dr1 = dt1.NewRow();
                dr1["IndicatorFirstName"] = dic1.Get<string>("IndicatorFirstName") + "(" + dic1.Get<string>("FirstMaxScore") + ")";
                dr1["SecondCount"] = dic1.Get<string>("SecQuan");
                dt1.Rows.Add(dr1);
                //通过一级指标下的二级指标构建主表的列
                sql = @"select IndicatorSecondId,IndicatorSecondName,SecondMaxScore,ToolTip from BJKY_Examine..IndicatorScore where 
                IndicatorFirstId='" + dic1.Get<string>("IndicatorFirstId") + "' and ExamineTaskId='" + dics[0].Get<string>("Id") + "' order by SecondSortIndex asc";
                IList<EasyDictionary> isDics = DataHelper.QueryDictList(sql);
                foreach (EasyDictionary dic2 in isDics)
                {
                    dc = new DataColumn(dic2.Get<string>("IndicatorSecondId") + dic2.Get<string>("IndicatorSecondName") + "(" + dic2.Get<string>("SecondMaxScore") + ")" + dic2.Get<string>("ToolTip"));//列名有多种信息组合而成。便于前台取对应的值
                    dt.Columns.Add(dc);//循环2级指标构建剩余列
                }
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

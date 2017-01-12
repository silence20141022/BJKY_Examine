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
using Aim.Examining.Model;
using System.Text;
using Newtonsoft.Json.Linq;
namespace Aim.Examining.Web.DeptConfig
{
    public partial class DeptExamineRelationEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id  
        DeptExamineRelation ent = null;
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            if (!string.IsNullOrEmpty(id))
            {
                ent = DeptExamineRelation.Find(id);
            }
            IList<string> strList1 = RequestData.GetList<string>("data1");
            IList<string> strList2 = RequestData.GetList<string>("data2");
            IList<string> strList3 = RequestData.GetList<string>("data3");
            IList<string> strList4 = RequestData.GetList<string>("data4");
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<DeptExamineRelation>();
                    UpdateToField(strList1, "data1");
                    UpdateToField(strList2, "data2");
                    UpdateToField(strList3, "data3");
                    UpdateToField(strList4, "data4");
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<DeptExamineRelation>();
                    ent.DoCreate();
                    UpdateToField(strList1, "data1");
                    UpdateToField(strList2, "data2");
                    UpdateToField(strList3, "data3");
                    UpdateToField(strList4, "data4");
                    ent.DoUpdate();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        public void UpdateToField(IList<string> strList, string sign)
        {
            string ids = ""; string names = "";
            for (int i = 0; i < strList.Count; i++)
            {
                if (!string.IsNullOrEmpty(strList[i]))
                {
                    JObject json = JsonHelper.GetObject<JObject>(strList[i]);
                    if (i != strList.Count - 1)
                    {
                        ids += json.Value<string>("Id") + ",";
                        names += json.Value<string>("Name") + ",";
                    }
                    else
                    {
                        ids += json.Value<string>("Id");
                        names += json.Value<string>("Name");
                    }
                    //保存人员权重明细
                    if (json.Value<int>("Weight") > 0)
                    {
                        string ToRoleCode = sign == "data2" ? "UpLevel" : sign == "data3" ? "SameLevel" : "DownLevel";
                        IList<UserBalance> ubEnts = UserBalance.FindAllByProperties("ExamineRelationId", ent.Id, "ToUserId", json.Value<string>("Id"), "ToRoleCode", ToRoleCode);
                        if (ubEnts.Count > 0)
                        {
                            ubEnts[0].Balance = json.Value<int>("Weight");
                            ubEnts[0].DoUpdate();
                        }
                        else
                        {
                            UserBalance ubEnt = new UserBalance();
                            ubEnt.ExamineRelationId = ent.Id;
                            ubEnt.RelationName = ent.RelationName;
                            ubEnt.ToUserId = json.Value<string>("Id");
                            ubEnt.ToUserName = json.Value<string>("Name");
                            ubEnt.ToRoleCode = ToRoleCode;
                            ubEnt.ToRoleName = ToRoleCode == "UpLevel" ? "上级评分人" : ToRoleCode == "SameLevel" ? "同级评分人" : "下级评分人";
                            ubEnt.Balance = json.Value<int>("Weight");
                            ubEnt.DoCreate();
                        }
                    }
                }
            }
            switch (sign)
            {
                case "data1":
                    ent.BeUserIds = ids;
                    ent.BeUserNames = names;
                    break;
                case "data2":
                    ent.UpLevelUserIds = ids;
                    ent.UpLevelUserNames = names;
                    break;
                case "data3":
                    ent.SameLevelUserIds = ids;
                    ent.SameLevelUserNames = names;
                    break;
                case "data4":
                    ent.DownLevelUserIds = ids;
                    ent.DownLevelUserNames = names;
                    break;
            }
        }
        private void DoSelect()
        {
            sql = @"select * from BJKY_Examine..PersonConfig where (PatIndex('%{0}%',FirstLeaderIds)>0 or PatIndex('%{0}%',SecondLeaderIds)>0 
            or PatIndex('%{0}%',ClerkIds)>0) and (GroupType='职能服务部门' or GroupType='经营目标单位')";
            sql = string.Format(sql, UserInfo.UserID);
            EasyDictionary dic = DataHelper.QueryDict(sql, "Id", "GroupName");
            IList<EasyDictionary> dics = DataHelper.QueryDictList(sql);
            if (dics.Count > 0)
            {
                PageState.Add("GroupID", dics[0].Get<string>("GroupID"));//把这个送到前台方便选人
            }
            PageState.Add("GroupEnum", dic);
            if (op == "c")
            {
                var Obj = new
                {
                    GroupID = dic.Keys.First(),
                    GroupName = dic.Values.First()
                };
                SetFormData(Obj);
            }
            else
            {
                string[] ids = new string[] { };
                string[] names = new string[] { };
                IList<EasyDictionary> beDics = new List<EasyDictionary>();
                if (!string.IsNullOrEmpty(ent.BeUserIds))
                {
                    ids = ent.BeUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    names = ent.BeUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ids.Length; i++)
                    {
                        EasyDictionary beDic = new EasyDictionary();
                        beDic.Add("Id", ids[i]);
                        beDic.Add("Name", names[i]);
                        beDics.Add(beDic);
                    }
                    PageState.Add("DataList1", beDics);
                }
                IList<EasyDictionary> upDics = new List<EasyDictionary>();
                if (!string.IsNullOrEmpty(ent.UpLevelUserIds))
                {
                    ids = ent.UpLevelUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    names = ent.UpLevelUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ids.Length; i++)
                    {
                        EasyDictionary upDic = new EasyDictionary();
                        upDic.Add("Id", ids[i]);
                        upDic.Add("Name", names[i]);
                        sql = "select top 1 Balance from BJKY_Examine..UserBalance where ExamineRelationId='" + ent.Id + "' and ToUserId='" + ids[i] + "' and ToRoleCode='UpLevel'";
                        upDic.Add("Weight", DataHelper.QueryValue<int>(sql) > 0 ? DataHelper.QueryValue<int>(sql) + "" : null);
                        upDics.Add(upDic);
                    }
                    PageState.Add("DataList2", upDics);
                }
                IList<EasyDictionary> sameDics = new List<EasyDictionary>();
                if (!string.IsNullOrEmpty(ent.SameLevelUserIds))
                {
                    ids = ent.SameLevelUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    names = ent.SameLevelUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ids.Length; i++)
                    {
                        EasyDictionary sameDic = new EasyDictionary();
                        sameDic.Add("Id", ids[i]);
                        sameDic.Add("Name", names[i]);
                        sql = "select top 1 Balance from BJKY_Examine..UserBalance where ExamineRelationId='" + ent.Id + "' and ToUserId='" + ids[i] + "' and ToRoleCode='SameLevel'";
                        sameDic.Add("Weight", DataHelper.QueryValue<int>(sql) > 0 ? DataHelper.QueryValue<int>(sql) + "" : null);
                        sameDics.Add(sameDic);
                    }
                    PageState.Add("DataList3", sameDics);
                }
                IList<EasyDictionary> downDics = new List<EasyDictionary>();
                if (!string.IsNullOrEmpty(ent.DownLevelUserIds))
                {
                    ids = ent.DownLevelUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    names = ent.DownLevelUserNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ids.Length; i++)
                    {
                        EasyDictionary downDic = new EasyDictionary();
                        downDic.Add("Id", ids[i]);
                        downDic.Add("Name", names[i]);
                        sql = "select top 1 Balance from BJKY_Examine..UserBalance where ExamineRelationId='" + ent.Id + "' and ToUserId='" + ids[i] + "' and ToRoleCode='DownLevel'";
                        downDic.Add("Weight", DataHelper.QueryValue<int>(sql) > 0 ? DataHelper.QueryValue<int>(sql) + "" : null);
                        downDics.Add(downDic);
                    }
                    PageState.Add("DataList4", downDics);
                }
                SetFormData(ent);
            }
            //找到登录人所在的部门 有可能一个人多部门的情形          
        }
    }
}


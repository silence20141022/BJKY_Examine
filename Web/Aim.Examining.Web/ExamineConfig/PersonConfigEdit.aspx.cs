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
namespace Aim.Examining.Web.ExamineConfig
{
    public partial class PersonConfigEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id  
        PersonConfig ent = null;
        int peopleQuan = 0;
        string[] array1 = null;
        string[] array2 = null;
        string[] array3 = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            IList<string> strList1 = null;
            IList<string> strList2 = null;
            IList<string> strList3 = null;
            IList<string> strList4 = null;
            IList<string> strList5 = null;
            IList<string> strList6 = null;
            if (!string.IsNullOrEmpty(id))
            {
                ent = PersonConfig.Find(id);
            }
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<PersonConfig>();
                    strList1 = RequestData.GetList<string>("dataLeader");
                    SetPersonConfigEnts(strList1, "dataLeader"); //firstLeader
                    strList2 = RequestData.GetList<string>("data1");
                    SetPersonConfigEnts(strList2, "data1");      //SecondLeaderIds
                    strList3 = RequestData.GetList<string>("data2");
                    SetPersonConfigEnts(strList3, "data2");      //ChargeSecondLeader
                    strList4 = RequestData.GetList<string>("data3");
                    SetPersonConfigEnts(strList4, "data3");      //InstituteClerkDelegate
                    strList5 = RequestData.GetList<string>("data4");
                    SetPersonConfigEnts(strList5, "data4");      //DeptClerkDelegate
                    strList6 = RequestData.GetList<string>("data5");
                    SetPersonConfigEnts(strList6, "data5");      //Clerk                    
                    if (!string.IsNullOrEmpty(ent.FirstLeaderIds))
                    {
                        array1 = ent.FirstLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (!string.IsNullOrEmpty(ent.SecondLeaderIds))
                    {
                        array2 = ent.SecondLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (!string.IsNullOrEmpty(ent.ClerkIds))
                    {
                        array3 = ent.ClerkIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    peopleQuan = (array1 != null ? array1.Length : 0) + (array2 != null ? array2.Length : 0) + (array3 != null ? array3.Length : 0);
                    ent.PeopleQuan = peopleQuan;
                    if (peopleQuan > 0)
                    {
                        ent.ExcellentRate = Math.Round(((decimal)(ent.ExcellentQuan.HasValue ? ent.ExcellentQuan : 0) / (decimal)peopleQuan) * 100, 2);
                        ent.GoodRate = Math.Round(((decimal)(ent.GoodQuan.HasValue ? ent.GoodQuan : 0) / (decimal)peopleQuan) * 100, 2);
                    }
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<PersonConfig>();
                    ent.DoCreate();
                    strList1 = RequestData.GetList<string>("dataLeader");
                    SetPersonConfigEnts(strList1, "dataLeader"); //firstLeader
                    strList2 = RequestData.GetList<string>("data1");
                    SetPersonConfigEnts(strList2, "data1");      //SecondLeaderIds
                    strList3 = RequestData.GetList<string>("data2");
                    SetPersonConfigEnts(strList3, "data2");      //ChargeSecondLeader
                    strList4 = RequestData.GetList<string>("data3");
                    SetPersonConfigEnts(strList4, "data3");      //InstituteClerkDelegate
                    strList5 = RequestData.GetList<string>("data4");
                    SetPersonConfigEnts(strList5, "data4");      //DeptClerkDelegate
                    strList6 = RequestData.GetList<string>("data5");
                    SetPersonConfigEnts(strList6, "data5");      //Clerk                   
                    //计算部门人数 
                    if (!string.IsNullOrEmpty(ent.FirstLeaderIds))
                    {
                        array1 = ent.FirstLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (!string.IsNullOrEmpty(ent.SecondLeaderIds))
                    {
                        array2 = ent.SecondLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (!string.IsNullOrEmpty(ent.ClerkIds))
                    {
                        array3 = ent.ClerkIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    peopleQuan = (array1 != null ? array1.Length : 0) + (array2 != null ? array2.Length : 0) + (array3 != null ? array3.Length : 0);
                    ent.PeopleQuan = peopleQuan;//创建的时候直接把系统配置部门优秀比率 和良好比率带过来 算出良好和优秀的人数
                    SysConfig scEnt = SysConfig.FindAll().First<SysConfig>();
                    ent.ExcellentRate = scEnt.DeptExcellentPercent;
                    ent.GoodRate = scEnt.DeptGoodPercent;
                    ent.ExcellentQuan = (int)Math.Floor((double)((ent.ExcellentRate / 100) * peopleQuan));
                    ent.GoodQuan = (int)Math.Floor((double)((ent.GoodRate / 100) * peopleQuan));
                    ent.DoUpdate();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        public void SetPersonConfigEnts(IList<string> strList, string sign)
        {
            string ids = ""; string names = ""; string groupIds = ""; string groupNames = "";
            for (int i = 0; i < strList.Count; i++)
            {
                JObject json = JsonHelper.GetObject<JObject>(strList[i]);
                if (!string.IsNullOrEmpty(json.Value<string>("Id")))
                {
                    if (i != strList.Count - 1)
                    {
                        ids += json.Value<string>("Id") + ",";
                        names += json.Value<string>("Name") + ",";
                        groupIds += ent.Id + ",";
                        groupNames += ent.GroupName + ",";
                    }
                    else
                    {
                        ids += json.Value<string>("Id");
                        names += json.Value<string>("Name");
                        groupIds += ent.Id;
                        groupNames += ent.GroupName;
                    }
                }
            }
            switch (sign)
            {
                case "dataLeader":
                    ent.FirstLeaderIds = ids;
                    ent.FirstLeaderNames = names;
                    ent.FirstLeaderGroupIds = groupIds;
                    ent.FirstLeaderGroupNames = groupNames;
                    break;
                case "data1":
                    ent.SecondLeaderIds = ids;
                    ent.SecondLeaderNames = names;
                    ent.SecondLeaderGroupIds = groupIds;
                    ent.SecondLeaderGroupNames = groupNames;
                    break;
                case "data2":
                    ent.ChargeSecondLeaderIds = ids;
                    ent.ChargeSecondLeaderNames = names;
                    ent.ChargeSecondLeaderGroupIds = groupIds;
                    ent.ChargeSecondLeaderGroupNames = groupNames;
                    break;
                case "data3":
                    ent.InstituteClerkDelegateIds = ids;
                    ent.InstituteClerkDelegateNames = names;
                    ent.InstituteClerkDelegateGroupIds = groupIds;
                    ent.InstituteClerkDelegateGroupNames = groupNames;
                    break;
                case "data4":
                    ent.DeptClerkDelegateIds = ids;
                    ent.DeptClerkDelegateNames = names;
                    ent.DeptClerkDelegateGroupIds = groupIds;
                    ent.DeptClerkDelegateGroupNames = groupNames;
                    break;
                case "data5":
                    ent.ClerkIds = ids;
                    ent.ClerkNames = names;
                    ent.ClerkGroupIds = groupIds;
                    ent.ClerkGroupNames = groupNames;
                    break;
            }
        }
        private void DoSelect()
        {
            if (op != "c")
            {
                SetFormData(ent);
                IList<EasyDictionary> leaderList = null;
                if (!string.IsNullOrEmpty(ent.FirstLeaderIds))
                {
                    leaderList = new List<EasyDictionary>();
                    string[] ids = ent.FirstLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] names = ent.FirstLeaderNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ids.Length; i++)
                    {
                        EasyDictionary lDic = new EasyDictionary();
                        lDic.Add("Id", ids[i]);
                        lDic.Add("Name", names[i]);
                        leaderList.Add(lDic);
                    }
                    PageState.Add("DataList", leaderList);
                }
                IList<EasyDictionary> secondList = null;
                if (!string.IsNullOrEmpty(ent.SecondLeaderIds))
                {
                    secondList = new List<EasyDictionary>();
                    string[] secondId = ent.SecondLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] secondName = ent.SecondLeaderNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < secondId.Length; i++)
                    {
                        EasyDictionary fDic = new EasyDictionary();
                        fDic.Add("Id", secondId[i]);
                        fDic.Add("Name", secondName[i]);
                        secondList.Add(fDic);
                    }
                    PageState.Add("DataList1", secondList);
                }
                IList<EasyDictionary> secondList_two = null;
                if (!string.IsNullOrEmpty(ent.ChargeSecondLeaderIds))
                {
                    secondList_two = new List<EasyDictionary>();
                    string[] secondId_two = ent.ChargeSecondLeaderIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] secondName_two = ent.ChargeSecondLeaderNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < secondId_two.Length; i++)
                    {
                        EasyDictionary fDic = new EasyDictionary();
                        fDic.Add("Id", secondId_two[i]);
                        fDic.Add("Name", secondName_two[i]);
                        secondList_two.Add(fDic);
                    }
                    PageState.Add("DataList2", secondList_two);
                }
                IList<EasyDictionary> third1 = null;
                if (!string.IsNullOrEmpty(ent.InstituteClerkDelegateIds))
                {
                    third1 = new List<EasyDictionary>();
                    string[] third1_Id = ent.InstituteClerkDelegateIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] third1_Name = ent.InstituteClerkDelegateNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < third1_Id.Length; i++)
                    {
                        EasyDictionary fDic = new EasyDictionary();
                        fDic.Add("Id", third1_Id[i]);
                        fDic.Add("Name", third1_Name[i]);
                        third1.Add(fDic);
                    }
                    PageState.Add("DataList3", third1);
                }
                IList<EasyDictionary> third2 = null;
                if (!string.IsNullOrEmpty(ent.DeptClerkDelegateIds))
                {
                    third2 = new List<EasyDictionary>();
                    string[] third2_Id = ent.DeptClerkDelegateIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] third2_Name = ent.DeptClerkDelegateNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < third2_Id.Length; i++)
                    {
                        EasyDictionary fDic = new EasyDictionary();
                        fDic.Add("Id", third2_Id[i]);
                        fDic.Add("Name", third2_Name[i]);
                        third2.Add(fDic);
                    }
                    PageState.Add("DataList4", third2);
                }
                IList<EasyDictionary> fourth = null;
                if (!string.IsNullOrEmpty(ent.ClerkIds))
                {
                    fourth = new List<EasyDictionary>();
                    string[] fourth_Id = ent.ClerkIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] fourth_Name = ent.ClerkNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < fourth_Id.Length; i++)
                    {
                        EasyDictionary fDic = new EasyDictionary();
                        fDic.Add("Id", fourth_Id[i]);
                        fDic.Add("Name", fourth_Name[i]);
                        fourth.Add(fDic);
                    }
                    PageState.Add("DataList5", fourth);
                }
            }
        }
    }

}


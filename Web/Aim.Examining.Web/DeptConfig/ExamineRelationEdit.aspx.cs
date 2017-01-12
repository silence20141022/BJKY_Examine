using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Aim.Portal.Web;
using System.Data;

namespace Aim.Examining.Web.DeptConfig
{
    public partial class ExamineRelationEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            string sql = string.Empty;
            DataTable dt = null;
            string result = string.Empty;
            switch (action)
            {
                case "save":
                    JObject json = (JObject)JsonConvert.DeserializeObject(Request["formdata"]);
                    JArray jarry1 = JsonConvert.DeserializeObject<JArray>(Request["data1"]);
                    JArray jarry2 = JsonConvert.DeserializeObject<JArray>(Request["data2"]);
                    JArray jarry3 = JsonConvert.DeserializeObject<JArray>(Request["data3"]);
                    JArray jarry4 = JsonConvert.DeserializeObject<JArray>(Request["data4"]);
                    string[] array1 = GetKeysAndValues(jarry1);
                    string[] array2 = GetKeysAndValues(jarry2);
                    string[] array3 = GetKeysAndValues(jarry3);
                    string[] array4 = GetKeysAndValues(jarry4);
                    if (string.IsNullOrEmpty(json.Value<string>("Id")))
                    {
                        sql = @"insert into BJKY_Examine..DeptExamineRelation (Id,RelationName,BeUserIds,BeUserNames,UpLevelUserIds,UpLevelUserNames,UpLevelWeight,
                        SameLevelUserIds,SameLevelUserNames,SameLevelWeight,DownLevelUserIds,DownLevelUserNames,DownLevelWeight,GroupID,GroupName,CreateId,CreateName,CreateTime) 
                        values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',GETDATE())";
                        sql = string.Format(sql, Guid.NewGuid(), json.Value<string>("RelationName"), array1[0], array1[1], array2[0], array2[1],
                        json.Value<string>("UpLevelWeight"), array3[0], array3[1], json.Value<string>("SameLevelWeight"), array4[0], array4[1],
                        json.Value<string>("DownLevelWeight"), json.Value<string>("GroupID"), json.Value<string>("GroupName"), json.Value<string>("CreateId"), json.Value<string>("CreateName"));
                        result = DbMgr.ExecuteNonQuery(sql) + "";
                    }
                    else
                    {
                        sql = @"update BJKY_Examine..DeptExamineRelation set RelationName='{1}',BeUserIds='{2}',BeUserNames='{3}',UpLevelUserIds='{4}',
                        UpLevelUserNames='{5}',UpLevelWeight='{6}',SameLevelUserIds='{7}',SameLevelUserNames='{8}',SameLevelWeight='{9}',
                        DownLevelUserIds='{10}',DownLevelUserNames='{11}',DownLevelWeight='{12}' where Id='{0}'";
                        sql = string.Format(sql, json.Value<string>("Id"), json.Value<string>("RelationName"), array1[0], array1[1], array2[0], array2[1], json.Value<string>("UpLevelWeight"),
                        array3[0], array3[1], json.Value<string>("SameLevelWeight"), array4[0], array4[1], json.Value<string>("DownLevelWeight"));
                        result = DbMgr.ExecuteNonQuery(sql) + "";
                    }
                    result = result == "1" ? "true" : "false";
                    Response.Write(result);
                    Response.End();
                    break;
                case "loadform":
                    if (string.IsNullOrEmpty(Request["id"] + ""))
                    {
                        //取当前用户的姓名和所在部门信息
                        string createname = WebPortalService.CurrentUserInfo.Name;
                        string createid = WebPortalService.CurrentUserInfo.UserID;
//                        sql = @"select * from sysgroup where (parentid='4b54389a-6537-4748-823c-fb55223afbad' or parentid='bde12833-038a-4ec6-bfc9-41f630c70380'
//                                                or parentid='3273c49a-1f9b-4328-b54c-d01e39c39edc' or parentid='037f85a8-3777-4015-9bc2-dc5aba4ccb28') and GroupID in
//                                                (select GroupID from SysUserGroup where UserID='" + createid + "')";
                        sql = @"select  Id,GroupName from BJKY_Examine..PersonConfig 
                        where (ClerkIds like '%{0}%' or SecondLeaderIds like '%{0}%' or FirstLeaderIds like '%{0}%') and (GroupType='职能服务部门' or GroupType='经营目标单位')";
                        sql = string.Format(sql, createid);
                        string groupid = string.Empty;
                        string groupname = string.Empty;
                        dt = DbMgr.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            groupid = dt.Rows[0]["Id"] + "";
                            groupname = dt.Rows[0]["GroupName"] + "";
                        }
                        result = "{formdata:{CreateId:'" + createid + "',CreateName:'" + createname + "',GroupID:'" + groupid + "',GroupName:'" + groupname + "'},data1:[],data2:[],data3:[],data4:[]}";
                    }
                    else
                    {
                        sql = "select * from BJKY_Examine..DeptExamineRelation where Id='" + Request["id"] + "'";
                        dt = DbMgr.GetDataTable(sql);
                        string formdata = JsonConvert.SerializeObject(dt).TrimStart('[').TrimEnd(']');
                        string data1 = SerilizeJsonString(dt.Rows[0]["BeUserIds"] + "", dt.Rows[0]["BeUserNames"] + "");
                        string data2 = SerilizeJsonString(dt.Rows[0]["UpLevelUserIds"] + "", dt.Rows[0]["UpLevelUserNames"] + "");
                        string data3 = SerilizeJsonString(dt.Rows[0]["SameLevelUserIds"] + "", dt.Rows[0]["SameLevelUserNames"] + "");
                        string data4 = SerilizeJsonString(dt.Rows[0]["DownLevelUserIds"] + "", dt.Rows[0]["DownLevelUserNames"] + "");
                        result = "{formdata:" + formdata + ",data1:" + data1 + ",data2:" + data2 + ",data3:" + data3 + ",data4:" + data4 + "}";
                    }
                    Response.Write(result);
                    Response.End();
                    break;
            }
        }
        public string[] GetKeysAndValues(JArray jarray)
        {
            string beuserids = string.Empty;
            string beusernames = string.Empty;
            for (int i = 0; i < jarray.Count; i++)
            {
                if (i != jarray.Count - 1)
                {
                    beuserids += jarray[i].Value<string>("UserID") + ",";
                    beusernames += jarray[i].Value<string>("Name") + ",";
                }
                else
                {
                    beuserids += jarray[i].Value<string>("UserID");
                    beusernames += jarray[i].Value<string>("Name");
                }
            }
            return new string[] { beuserids, beusernames };
        }
        public string SerilizeJsonString(string keys, string values)
        {
            string[] array1 = keys.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] array2 = values.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string json = "[";
            for (int i = 0; i < array1.Length; i++)
            {
                if (i != array1.Length - 1)
                {
                    json += "{UserID:\"" + array1[i] + "\",Name:\"" + array2[i] + "\"},";
                }
                else
                {
                    json += "{UserID:\"" + array1[i] + "\",Name:\"" + array2[i] + "\"}";
                }
            }
            json += "]";
            return json;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Aim.Data;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Component;
using Aim.Component.ThirdpartySupport.MsOffice;
using Aim.Utilities;
using Aim.Security;


namespace Aim.Portal.Web.Modules.SysApp.OrgMag
{
    public partial class UsrList : BaseListPage
    {
        #region 属性

        #endregion

        #region 变量

        string op = String.Empty;
        string id = String.Empty;   // 对象id
        string type = String.Empty; // 查询类型

        private SysUser[] users = null;

        #endregion

        #region 构造函数

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.Files.Count > 0)
            {
                string guid = Guid.NewGuid().ToString();
                string filePath = "//WorkTime//InputExcelFiles//" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Request.Files[0].FileName);
                this.Request.Files[0].SaveAs(Server.MapPath(filePath));
                ExcelProcessor ep = ExcelService.GetProcessor(Server.MapPath(filePath));
                DataSet ds = ep.GetDataSet();
                InputDatas(ds.Tables[0]);
                Response.Write("{success:true}");
                Response.End();
            }

            id = RequestData.Get<string>("id", String.Empty);
            type = RequestData.Get<string>("type", String.Empty);
            SearchCriterion.AutoOrder = false;
            SearchCriterion.SetOrder(SysUser.Prop_WorkNo);
            string dName = SearchCriterion.GetSearchValue<string>("Name");
            string workNo = SearchCriterion.GetSearchValue<string>("WorkNo");
            SearchCriterion.SetOrder(SysUser.Prop_WorkNo);
            if (dName != null && dName.Trim() != "")
            {
                string where = "select * from SysUser where " + GetPinyinWhereString("Name", dName);
                where += " and WorkNo like '%" + workNo + "%'";
                this.PageState.Add("UsrList", DataHelper.QueryDictList(where));
            }
            else
            {
                users = SysUserRule.FindAll(SearchCriterion);
                this.PageState.Add("UsrList", users);
            }

            SysUser usr = null;
            switch (this.RequestAction)
            {
                case RequestActionEnum.Create:
                    usr = this.GetPostedData<SysUser>();
                    usr.DoCreate();
                    this.SetMessage("新建成功！");
                    break;
                case RequestActionEnum.Update:
                    usr = this.GetMergedData<SysUser>();
                    usr.DoUpdate();
                    this.SetMessage("保存成功！");
                    break;
                case RequestActionEnum.Delete:
                    usr = this.GetTargetData<SysUser>();
                    usr.DoDelete();
                    this.SetMessage("删除成功！");
                    break;
                default:
                    if (RequestActionString == "clearpass")
                    {
                        usr = SysUser.Find(this.RequestData.Get<string>("UserId"));
                        usr.Password = "";
                        usr.Remark = "";
                        usr.Save();
                    }
                    else if (RequestActionString == "setpass")
                    {
                        MD5Encrypt encrypt = new MD5Encrypt();
                        SysUser[] users = SysUser.FindAll();
                        foreach (SysUser user in users)
                        {

                            Random rnd = new Random();
                            int rndNum = rnd.Next(10000000, 99999999);
                            string encryPassword = encrypt.GetMD5FromString(rndNum.ToString());
                            user.Password = encryPassword;
                            user.Remark = rndNum.ToString();
                            user.Save();
                        }
                    }
                    else if (RequestActionString == "checkkey")
                    {
                        if (SysUser.FindAllByProperties("Server_IAGUID", this.RequestData.Get<string>("usbguid")).Length > 0)
                        {
                            this.PageState.Add("UserName", SysUser.FindAllByProperties("Server_IAGUID", this.RequestData.Get<string>("usbguid"))[0].Name);
                        }
                        else
                        {
                            this.PageState.Add("UserName", "");
                        }
                    }
                    else if (RequestActionString == "setkey")
                    {
                        SysUser user = SysUser.Find(this.RequestData.Get<string>("userid"));
                        user.Server_IAGUID = this.RequestData.Get<string>("usbguid");
                        user.Server_Seed = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                        user.ThreeDESKEY = "ABCDEFGhijklmn0123456789";
                        user.Save();
                    }
                    else if (RequestActionString == "sendmail")
                    {
                        IList<object> idList = RequestData.GetList<object>("IdList");
                        SysUser[] tents = SysUser.FindAll(Expression.In("UserID", idList.ToArray()));

                        foreach (SysUser user in tents)
                        {
                            string body = SysParameter.FindAllByProperties("Code", "MailText")[0].Description + "<br>";
                            body += "您的登录用户名:" + user.LoginName + ";密码:" + user.Remark;
                            string mailAccount = SysParameter.FindAllByProperties("Code", "MailAccount")[0].Value;
                            string mailPass = SysParameter.FindAllByProperties("Code", "MailPassword")[0].Value;
                            string mailServer = SysParameter.FindAllByProperties("Code", "MailSmtpServer")[0].Value;
                            string mailSenderAddress = SysParameter.FindAllByProperties("Code", "MailSenderAddress")[0].Value;
                            MailHelper.SendMail(mailSenderAddress, user.Email, "工时分配系统邮件", body, mailAccount, mailPass, mailServer);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region 私有方法

        private void InputDatas(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row[2] != null && row[2].ToString().Trim() != "")
                {
                    string workNo = "";
                    try
                    {
                        if (SysUser.FindAllByProperties("WorkNo", row[1].ToString().Trim()).Length == 0)
                        {
                            SysUser sysUser = new SysUser();
                            sysUser.WorkNo = row[1].ToString().Trim();
                            sysUser.Name = row[2].ToString().Trim();
                            if (row[3].ToString().Trim() == "")
                            {
                                sysUser.LoginName = GetPingyin(row[2].ToString().Trim());
                            }
                            else
                            {
                                sysUser.LoginName = row[3].ToString();
                            }
                            sysUser.Email = row[5].ToString();
                            sysUser.Remark = row[6].ToString();
                            sysUser.Status = 1;
                            sysUser.CreateDate = DateTime.Now;
                            sysUser.Save();
                            if (SysGroup.FindAllByProperties("Name", row[4].ToString().Trim()).Length > 0)
                            {
                                using (new SessionScope())
                                {
                                    SysGroup grp = SysGroup.FindAllByProperties("Name", row[4].ToString().Trim())[0];

                                    IList<string> userIDs = new List<string>();
                                    userIDs.Add(sysUser.UserID);
                                    grp.AddUsers(userIDs);
                                }
                            }
                        }
                        else
                        {
                            if (SysGroup.FindAllByProperties("Name", row[4].ToString().Trim()).Length > 0)
                            {
                                SysUser sysUser = SysUser.FindAllByProperties("WorkNo", row[1].ToString().Trim())[0];
                                using (new SessionScope())
                                {
                                    if (sysUser.RetrieveAllGroup().Where(en => en.Type == 2 && en.Name != row[4].ToString()).Count() == 0)
                                    {
                                        if (sysUser.RetrieveAllGroup().Where(en => en.Type == 2).Count() > 0)
                                        {
                                            foreach (SysGroup gp in sysUser.RetrieveAllGroup().Where(en => en.Type == 2))
                                                gp.User.Remove(sysUser);
                                        }
                                        SysGroup grp = SysGroup.FindAllByProperties("Name", row[4].ToString().Trim())[0];
                                        IList<string> userIDs = new List<string>();
                                        userIDs.Add(sysUser.UserID);
                                        grp.AddUsers(userIDs);
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
        }
        public string GetPinyinWhereString(string fieldName, string pinyinIndex)
        {
            string[,] hz = Tool.GetHanziScope(pinyinIndex);
            string whereString = "(";
            for (int i = 0; i < hz.GetLength(0); i++)
            {
                whereString += "(SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) >= '" + hz[i, 0] + "' AND SUBSTRING(" + fieldName + ", " + (i + 1) + ", 1) <= '" + hz[i, 1] + "') AND ";
            }
            if (whereString.Substring(whereString.Length - 4, 4) == "AND ")
                return whereString.Substring(0, whereString.Length - 4) + ")";
            else
                return "(1=1)";
        }

        public string GetPingyin(string name)
        {
            return Tool.ConvertToFullHanzi(name);
        }
        #endregion
    }
}

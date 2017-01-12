using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Data;
using Aim.Web;
using Aim.Web.UI;
using Aim.Portal.Entity;
using Aim.Portal.Rule;

namespace Aim.Portal.Web.CommonPages
{
    public partial class DataImport : BasePage
    {
        #region 变量

        string op = String.Empty; // 用户编辑操作
        string code = String.Empty; // 对象编码

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            code = RequestData.Get<string>("code");

            SysDataImportTemplate ent = null;
            
            switch (this.RequestAction)
            {
                case RequestActionEnum.Custom:
                    if (RequestActionString == "import")
                    {
                        code = FormData.Get<string>("Code");

                        if (!String.IsNullOrEmpty(code))
                        {
                            ent = SysDataImportTemplate.FindFirstByProperties("Code", code);

                            string fileFullName = FormData.Get<string>("DataFileID");
                            if (!String.IsNullOrEmpty(fileFullName))
                            {
                                DataImportService.ImportData(ent.Config, fileFullName);

                                // 导入完成后删除原文件
                                FileService.DeleteFileByFullID(fileFullName);
                            }
                        }
                    }
                    else if (RequestActionString == "gettmplid")
                    {
                        // 下载文件模板时用于获取模板文件id(异步)
                        if (IsAsyncRequest && !String.IsNullOrEmpty(code))
                        {
                            ent = SysDataImportTemplate.FindFirstByProperties("Code", code);

                            // 返回模板文件ID
                            PageState.Add("id", FileService.GetFileIDByFullID(ent.TemplateFileID));
                        }
                    }
                    break;
                case RequestActionEnum.Default:
                    if (!String.IsNullOrEmpty(code))
                    {
                        ent = SysDataImportTemplate.FindFirstByProperties("Code", code);
                        this.SetFormData(ent);
                    }
                    break;
            }
        }

        #endregion
    }
}


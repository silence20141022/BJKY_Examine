using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
//using System.Web.Script.Serialization;

using Aim.Data;
using Aim.Common;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;


namespace Aim.Portal.Web.Modules.SysApp.MdlMag
{
    public partial class AuthGroup : BaseListPage
    {
        #region 属性

        private SysGroup[] ents = null;

        #endregion

        #region 变量

        #endregion

        #region 构造函数

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsAsyncRequest)
            {
                switch (this.RequestAction)
                {
                    case RequestActionEnum.Custom:
                        if (RequestActionString == "querychildren")
                        {
                            string id = (RequestData.ContainsKey("ID") ? RequestData["ID"].ToString() : String.Empty);
                            string type = String.Empty;

                            if (RequestData.ContainsKey("Type"))
                            {
                                type = RequestData["Type"].ToString().ToLower();

                                if (type == "gtype")
                                {
                                    ents = SysGroup.FindAll("FROM SysGroup as ent WHERE ent.Type = ?  order by sortindex", id);

                                    this.PageState.Add("DtList", ents);
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                SysGroupType[] typeList = SysGroupType.FindAllByProperties("GroupTypeID",2);
                this.PageState.Add("DtList", typeList);
            }
        }

        #endregion
    }
}

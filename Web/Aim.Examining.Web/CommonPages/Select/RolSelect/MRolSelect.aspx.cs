using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Web.UI;

namespace Aim.Portal.Web.CommonPages
{
    public partial class MRolSelect : BasePage
    {
        #region 构造函数

        public MRolSelect()
        {
            IsCheckLogon = false;
        }

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}

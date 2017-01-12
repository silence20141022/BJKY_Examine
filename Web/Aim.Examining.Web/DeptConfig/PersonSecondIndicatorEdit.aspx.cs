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
using System.Data;
using System.Text;
namespace Aim.Examining.Web.DeptConfig
{
    public partial class PersonSecondIndicatorEdit : ExamBasePage
    {
        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id 
        string Tooltip = string.Empty;
        PersonSecondIndicator ent = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            SetTolTip();
            switch (RequestActionString)
            {
                case "update":
                    ent = GetMergedData<PersonSecondIndicator>();
                    ent.ToolTip = Tooltip;
                    ent.DoUpdate();
                    break;
                case "create":
                    ent = GetPostedData<PersonSecondIndicator>();
                    ent.ToolTip = Tooltip;
                    ent.DoCreate();
                    break;
                case "delete":
                    ent = GetTargetData<PersonSecondIndicator>();
                    ent.DoDelete();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoSelect()
        {
            if (op != "c" && op != "cs")
            {
                if (!String.IsNullOrEmpty(id))
                {
                    ent = PersonSecondIndicator.Find(id);
                    this.PageState.Add("DataList", ent.ToolTip);
                }
                this.SetFormData(ent);
            }
        }
        private void SetTolTip()
        /*转换成字典数组字符串*/
        {
            IList<string> tip = this.RequestData.GetList<string>("data");
            if (tip != null && tip.Count > 0)
            {
                Tooltip = "[" + string.Join(",", tip.ToArray().Select(ten => { return ten.Replace("\r", ""); }).ToArray()) + "]";
            }
        }
    }
}


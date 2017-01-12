using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Aim.Portal.Model;
using NHibernate;
using Castle.ActiveRecord;
using System.Text.RegularExpressions;
using System.Reflection;
using Aim.Common;
using Aim.Common.Service;
using Aim.Data;

namespace Aim.Portal.Model
{
    public partial class WebPartExt
    {
        public WebPartExt(WebPart webpart)
        {
            WebPart = webpart;
        }
        public WebPartExt(WebPart webpart,string userid)
        {
            WebPart = webpart;
            UserId = userid;
        }
        private string userId = "";

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private WebPart webpart = null;

        public WebPart WebPart
        {
            get { return webpart; }
            set { webpart = value; }
        }

        public delegate void GetSourceListEventHandler(ref DataCollection dl, string blockKey);
        public event GetSourceListEventHandler GetSourceEvent;

        public DataCollection GetDataSource()
        {
            DataCollection dt = new DataCollection();
            if (GetSourceEvent != null)
                GetSourceEvent(ref dt, WebPart.BlockKey);
            else
            {
                string sql = WebPart.RepeatDataDataSql;
                sql = sql.Replace("[UserId]", UserId);
                /*ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(ActiveRecordBase));

                // Now you can use sess.DbConnection 
                //sess.Connection.CreateCommand();
                SqlDataAdapter sda = new SqlDataAdapter(sql, sess.Connection.ConnectionString);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                ActiveRecordMediator.GetSessionFactoryHolder().ReleaseSession(sess); 
                dt = CommonDataHelper.DataTableToDataCollection(ds.Tables[0]);
                sda.Dispose();
                ds.Dispose();*/
                dt = CommonDataHelper.DataTableToDataCollection(DataHelper.QueryDataTable(sql));
            }
            return dt;
        }
        //获取标题头
        public string GetHeadHtml()
        {
            string template = WebPart.HeadHtml;
            Regex rg = new Regex("\\[[^][]*\\]", RegexOptions.Multiline);
            MatchCollection mtc = rg.Matches(template);
            string tcs = "";
            foreach (Match mt in mtc)
            {
                if (tcs.IndexOf(mt.Value) >= 0)
                    continue;
                string val = mt.Value;
                tcs += val + ",";
                string col = val.Substring(1, mt.Value.Length - 2);
                Type t =WebPart.GetType();
                string content = t.GetProperty(col).GetValue(WebPart, null) == null ? "" : t.GetProperty(col).GetValue(WebPart, null).ToString();
                template = template.Replace(val, content);
            }
            return template;
        }
        //获取内容
        public string GetContentHtml()
        {
            string template = WebPart.RepeatItemTemplate;
            string html = "";

            DataCollection source = this.GetDataSource();
            if (source.GetElementCount() > 0)
            {
                for (int i = 0; i < source.GetElementCount(); i++)
                {
                    if (i == WebPart.RepeatItemCount)
                        break;
                    string item = template;
                    Regex rg = new Regex("\\[[^][]*\\]", RegexOptions.Multiline);
                    MatchCollection mtc = rg.Matches(template);
                    foreach (Match mt in mtc)
                    {
                        item = item.Replace(mt.Value, source.GetElement(i).GetAttr(mt.Value.Substring(1, mt.Value.Length - 2)).ToString());
                    }
                    html += item;
                }
                return html;
            }
            else
            {
                return "暂无记录";
            }
        }
        //获取页脚
        public string GetFootHtml()
        {
            string template = WebPart.FootHtml;
            Regex rg = new Regex("\\[[^][]*\\]", RegexOptions.Multiline);
            MatchCollection mtc = rg.Matches(template);
            foreach (Match mt in mtc)
            {
                string val = mt.Value;
                string col = val.Substring(1, mt.Value.Length - 2);
                Type t = WebPart.GetType(); 
                template = template.Replace(val, t.GetProperty(col).GetValue(WebPart, null).ToString());
            }
            return template;
        }

    }
}

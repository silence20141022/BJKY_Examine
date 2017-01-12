using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aim.Common;

namespace Aim.Portal.Web
{
    /// <summary>
    /// Web消息，用于程序与客户端交互
    /// </summary>
    public class WebMessage : Message
    {
        #region 属性
        
        /// <summary>
        /// 默认消息标签
        /// </summary>
        public const string DefaultMessageLabel = "__WEB";

        #endregion

        #region 构造函数

        public WebMessage()
        {
            this.lable = WebMessage.DefaultMessageLabel;
        }

        public WebMessage(string msg)
            : this()
        {
            this.Content = msg;
        }

        #endregion

        #region Message 成员

        /// <summary>
        /// 消息标签
        /// </summary>
        public override string Lable
        {
            get
            {
                return base.Lable;
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public override string Content
        {
            get
            {
                return base.Content;
            }
            set
            {
                base.Content = value;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 转换为Json String
        /// </summary>
        /// <returns></returns>
        public virtual string ToJsonString()
        {
            return JsonHelper.GetJsonString(this);
        }

        #endregion
    }

    /// <summary>
    /// Web异常信息
    /// </summary>
    public class WebExceptionMessage : WebMessage
    {
        #region 静态成员

        /// <summary>
        /// 默认消息标签
        /// </summary>
        new public const string DefaultMessageLabel = "__EXCEPTION";

        /// <summary>
        /// 安全异常标签
        /// </summary>
        public const string SecurityMessageLabel = "__SEXCEPTION";

        #endregion


        #region 构造函数

        public WebExceptionMessage()
        {
            this.lable = WebExceptionMessage.DefaultMessageLabel;
        }

        public WebExceptionMessage(string exstr)
            : this()
        {
            this.Content = exstr;
        }

        public WebExceptionMessage(Exception ex)
            : this()
        {
            this.Content = ex.Message;
        }

        #endregion
    }
}

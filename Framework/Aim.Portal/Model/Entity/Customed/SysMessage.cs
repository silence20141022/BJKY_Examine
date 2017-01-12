
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
	public partial class SysMessage
    {
        #region 私有成员

        #endregion

        #region 成员属性
        
        #endregion

        #region 重载

        public override void Create()
        {
			
            base.Create();
        }

        public override void CreateAndFlush()
        {
			
            base.CreateAndFlush();
        }

        public override void Update()
        {
			
            base.Update();
        }

        public override void UpdateAndFlush()
        {
			
            base.UpdateAndFlush();
        }

        public override void Save()
        {

            base.Save();
        }

        public override void SaveAndFlush()
        {

            base.SaveAndFlush();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            /*if (!this.IsPropertyUnique("UniqueKey"))
            {
                throw new RepeatedKeyException("存在重复的 UniqueKey “" + this.UniqueKey + "”");
            }*/

            if (String.IsNullOrEmpty(this.Title))
            {
                throw new MessageException("标题不能为空。");
            }

            if (String.IsNullOrEmpty(this.ReceiverId))
            {
                throw new MessageException("接收人不能为空。");
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            // 事务开始
            this.CreateAndFlush();
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            if (IsSenderDelete != null && IsReceiverDelete != null && IsSenderDelete.GetValueOrDefault() && IsReceiverDelete.GetValueOrDefault())
            {
                base.Delete();
            }
            else
            {
                base.Update();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void DoSend(params string[] receiverIds)
        {
            if (receiverIds == null || receiverIds.Length <= 0)
            {
                if (!String.IsNullOrEmpty(this.ReceiverId))
                {
                    receiverIds = this.ReceiverId.Split(',', ';');
                }
            }

            if (receiverIds != null && receiverIds.Length > 0)
            {
                SysUser[] users = SysUser.FindAllByPrimaryKeys(receiverIds);

                foreach (SysUser tuser in users)
                {
                    SysMessage msg = new SysMessage();
                    msg.SenderId = this.SenderId;
                    msg.SenderName = this.SenderName;
                    msg.Title = this.Title;
                    msg.MessageContent = this.MessageContent;
                    msg.Attachment = this.Attachment;
                    msg.SendTime = DateTime.Now;
                    msg.ReceiverId = tuser.UserID;
                    msg.ReceiverName = tuser.Name;

                    msg.DoCreate();
                }
            }
        }

        #region 静态方法

        /// <summary>
        /// 消息发送操作
        /// </summary>
        public static void Send(string senderId, string title, string content, string attachment, params string[] receiverIds)
        {
            if (receiverIds != null && receiverIds.Length > 0)
            {
                SysUser sender = SysUser.Find(senderId);

                SysMessage msg = new SysMessage();
                msg.SenderId = sender.UserID;
                msg.SenderName = sender.Name;
                msg.Title = title;
                msg.MessageContent = content;
                msg.Attachment = attachment;

                msg.DoSend(receiverIds);

            }
            else
            {
                throw new MessageException("接收人不能为空。");
            }
        }

        #endregion


        #endregion

    } // SysMessage
}



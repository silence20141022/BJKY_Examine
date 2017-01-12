// Business class SysMessage generated from SysMessage
// Creator: Ray
// Created Date: [2010-05-04]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SysMessage")]
	public partial class SysMessage : EntityBase<SysMessage> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_SenderId = "SenderId";
		public static string Prop_SenderName = "SenderName";
		public static string Prop_ReceiverId = "ReceiverId";
		public static string Prop_ReceiverName = "ReceiverName";
		public static string Prop_FullId = "FullId";
		public static string Prop_Title = "Title";
		public static string Prop_MessageContent = "MessageContent";
		public static string Prop_ReplayMessageId = "ReplayMessageId";
		public static string Prop_SendTime = "SendTime";
		public static string Prop_ReadTime = "ReadTime";
		public static string Prop_Attachment = "Attachment";
		public static string Prop_IsRead = "IsRead";
		public static string Prop_IsSenderDelete = "IsSenderDelete";
		public static string Prop_IsReceiverDelete = "IsReceiverDelete";

		#endregion

		#region Private_Variables

		private string _id;
		private string _senderId;
		private string _senderName;
		private string _receiverId;
		private string _receiverName;
		private string _fullId;
		private string _title;
		private string _messageContent;
		private string _replayMessageId;
		private DateTime? _sendTime;
		private DateTime? _readTime;
		private string _attachment;
		private bool? _isRead;
		private bool? _isSenderDelete;
		private bool? _isReceiverDelete;


		#endregion

		#region Constructors

		public SysMessage()
		{
		}

		public SysMessage(
			string p_id,
			string p_senderId,
			string p_senderName,
			string p_receiverId,
			string p_receiverName,
			string p_fullId,
			string p_title,
			string p_messageContent,
			string p_replayMessageId,
			DateTime? p_sendTime,
			DateTime? p_readTime,
			string p_attachment,
			bool? p_isRead,
			bool? p_isSenderDelete,
			bool p_isReceiverDelete)
		{
			_id = p_id;
			_senderId = p_senderId;
			_senderName = p_senderName;
			_receiverId = p_receiverId;
			_receiverName = p_receiverName;
			_fullId = p_fullId;
			_title = p_title;
			_messageContent = p_messageContent;
			_replayMessageId = p_replayMessageId;
			_sendTime = p_sendTime;
			_readTime = p_readTime;
			_attachment = p_attachment;
			_isRead = p_isRead;
			_isSenderDelete = p_isSenderDelete;
			_isReceiverDelete = p_isReceiverDelete;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("SenderId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string SenderId
		{
			get { return _senderId; }
			set
			{
				if ((_senderId == null) || (value == null) || (!value.Equals(_senderId)))
				{
					_senderId = value;
					NotifyPropertyChanged(SysMessage.Prop_SenderId);
				}
			}
		}

		[Property("SenderName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string SenderName
		{
			get { return _senderName; }
			set
			{
				if ((_senderName == null) || (value == null) || (!value.Equals(_senderName)))
				{
					_senderName = value;
					NotifyPropertyChanged(SysMessage.Prop_SenderName);
				}
			}
		}

		[Property("ReceiverId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ReceiverId
		{
			get { return _receiverId; }
			set
			{
				if ((_receiverId == null) || (value == null) || (!value.Equals(_receiverId)))
				{
					_receiverId = value;
					NotifyPropertyChanged(SysMessage.Prop_ReceiverId);
				}
			}
		}

		[Property("ReceiverName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ReceiverName
		{
			get { return _receiverName; }
			set
			{
				if ((_receiverName == null) || (value == null) || (!value.Equals(_receiverName)))
				{
					_receiverName = value;
					NotifyPropertyChanged(SysMessage.Prop_ReceiverName);
				}
			}
		}

		[Property("FullId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1850)]
		public string FullId
		{
			get { return _fullId; }
			set
			{
				if ((_fullId == null) || (value == null) || (!value.Equals(_fullId)))
				{
					_fullId = value;
					NotifyPropertyChanged(SysMessage.Prop_FullId);
				}
			}
		}

		[Property("Title", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 80)]
		public string Title
		{
			get { return _title; }
			set
			{
				if ((_title == null) || (value == null) || (!value.Equals(_title)))
				{
					_title = value;
					NotifyPropertyChanged(SysMessage.Prop_Title);
				}
			}
		}

		[Property("MessageContent", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string MessageContent
		{
			get { return _messageContent; }
			set
			{
				if ((_messageContent == null) || (value == null) || (!value.Equals(_messageContent)))
				{
					_messageContent = value;
					NotifyPropertyChanged(SysMessage.Prop_MessageContent);
				}
			}
		}

		[Property("ReplayMessageId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ReplayMessageId
		{
			get { return _replayMessageId; }
			set
			{
				if ((_replayMessageId == null) || (value == null) || (!value.Equals(_replayMessageId)))
				{
					_replayMessageId = value;
					NotifyPropertyChanged(SysMessage.Prop_ReplayMessageId);
				}
			}
		}

		[Property("SendTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? SendTime
		{
			get { return _sendTime; }
			set
			{
				if (value != _sendTime)
				{
					_sendTime = value;
					NotifyPropertyChanged(SysMessage.Prop_SendTime);
				}
			}
		}

		[Property("ReadTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ReadTime
		{
			get { return _readTime; }
			set
			{
				if (value != _readTime)
				{
					_readTime = value;
					NotifyPropertyChanged(SysMessage.Prop_ReadTime);
				}
			}
		}

		[Property("Attachment", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string Attachment
		{
			get { return _attachment; }
			set
			{
				if ((_attachment == null) || (value == null) || (!value.Equals(_attachment)))
				{
					_attachment = value;
					NotifyPropertyChanged(SysMessage.Prop_Attachment);
				}
			}
		}

		[Property("IsRead", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public bool? IsRead
		{
			get { return _isRead; }
			set
			{
				if (value != _isRead)
				{
					_isRead = value;
					NotifyPropertyChanged(SysMessage.Prop_IsRead);
				}
			}
		}

		[Property("IsSenderDelete", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public bool? IsSenderDelete
		{
			get { return _isSenderDelete; }
			set
			{
				if (value != _isSenderDelete)
				{
					_isSenderDelete = value;
					NotifyPropertyChanged(SysMessage.Prop_IsSenderDelete);
				}
			}
		}

		[Property("IsReceiverDelete", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public bool? IsReceiverDelete
		{
			get { return _isReceiverDelete; }
			set
			{
				if (value != _isReceiverDelete)
				{
					_isReceiverDelete = value;
					NotifyPropertyChanged(SysMessage.Prop_IsReceiverDelete);
				}
			}
		}

		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			PropertyChangedEventHandler localPropertyChanged = PropertyChanged;
			if (localPropertyChanged != null)
			{
				localPropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		#endregion

	} // SysMessage
}


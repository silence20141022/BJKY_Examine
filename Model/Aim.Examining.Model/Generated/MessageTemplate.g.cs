// Business class MessageTemplate generated from MessageTemplate
// Creator: Ray
// Created Date: [2012-12-25]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("MessageTemplate")]
	public partial class MessageTemplate : ExamModelBase<MessageTemplate>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_TemplateType = "TemplateType";
		public static string Prop_TemplateName = "TemplateName";
		public static string Prop_TemplateContent = "TemplateContent";
		public static string Prop_State = "State";
		public static string Prop_Remark = "Remark";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _templateType;
		private string _templateName;
		private string _templateContent;
		private string _state;
		private string _remark;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public MessageTemplate()
		{
		}

		public MessageTemplate(
			string p_id,
			string p_templateType,
			string p_templateName,
			string p_templateContent,
			string p_state,
			string p_remark,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_templateType = p_templateType;
			_templateName = p_templateName;
			_templateContent = p_templateContent;
			_state = p_state;
			_remark = p_remark;
			_createId = p_createId;
			_createName = p_createName;
			_createTime = p_createTime;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			  set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TemplateType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string TemplateType
		{
			get { return _templateType; }
			set
			{
				if ((_templateType == null) || (value == null) || (!value.Equals(_templateType)))
				{
                    object oldValue = _templateType;
					_templateType = value;
					RaisePropertyChanged(MessageTemplate.Prop_TemplateType, oldValue, value);
				}
			}

		}

		[Property("TemplateName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string TemplateName
		{
			get { return _templateName; }
			set
			{
				if ((_templateName == null) || (value == null) || (!value.Equals(_templateName)))
				{
                    object oldValue = _templateName;
					_templateName = value;
					RaisePropertyChanged(MessageTemplate.Prop_TemplateName, oldValue, value);
				}
			}

		}

		[Property("TemplateContent", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string TemplateContent
		{
			get { return _templateContent; }
			set
			{
				if ((_templateContent == null) || (value == null) || (!value.Equals(_templateContent)))
				{
                    object oldValue = _templateContent;
					_templateContent = value;
					RaisePropertyChanged(MessageTemplate.Prop_TemplateContent, oldValue, value);
				}
			}

		}

		[Property("State", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string State
		{
			get { return _state; }
			set
			{
				if ((_state == null) || (value == null) || (!value.Equals(_state)))
				{
                    object oldValue = _state;
					_state = value;
					RaisePropertyChanged(MessageTemplate.Prop_State, oldValue, value);
				}
			}

		}

		[Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Remark
		{
			get { return _remark; }
			set
			{
				if ((_remark == null) || (value == null) || (!value.Equals(_remark)))
				{
                    object oldValue = _remark;
					_remark = value;
					RaisePropertyChanged(MessageTemplate.Prop_Remark, oldValue, value);
				}
			}

		}

		[Property("CreateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CreateId
		{
			get { return _createId; }
			set
			{
				if ((_createId == null) || (value == null) || (!value.Equals(_createId)))
				{
                    object oldValue = _createId;
					_createId = value;
					RaisePropertyChanged(MessageTemplate.Prop_CreateId, oldValue, value);
				}
			}

		}

		[Property("CreateName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CreateName
		{
			get { return _createName; }
			set
			{
				if ((_createName == null) || (value == null) || (!value.Equals(_createName)))
				{
                    object oldValue = _createName;
					_createName = value;
					RaisePropertyChanged(MessageTemplate.Prop_CreateName, oldValue, value);
				}
			}

		}

		[Property("CreateTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreateTime
		{
			get { return _createTime; }
			set
			{
				if (value != _createTime)
				{
                    object oldValue = _createTime;
					_createTime = value;
					RaisePropertyChanged(MessageTemplate.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // MessageTemplate
}


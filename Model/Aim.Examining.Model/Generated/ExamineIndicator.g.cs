// Business class ExamineIndicator generated from ExamineIndicator
// Creator: Ray
// Created Date: [2012-12-03]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineIndicator")]
	public partial class ExamineIndicator : ExamModelBase<ExamineIndicator>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_IndicatorName = "IndicatorName";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_BelongDeptId = "BelongDeptId";
		public static string Prop_BelongDeptName = "BelongDeptName";
		public static string Prop_Remark = "Remark";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";

		#endregion

		#region Private_Variables

		private string _id;
		private string _indicatorName;
		private string _beRoleCode;
		private string _beRoleName;
		private string _belongDeptId;
		private string _belongDeptName;
		private string _remark;
		private DateTime? _createTime;
		private string _createId;
		private string _createName;


		#endregion

		#region Constructors

		public ExamineIndicator()
		{
		}

		public ExamineIndicator(
			string p_id,
			string p_indicatorName,
			string p_beRoleCode,
			string p_beRoleName,
			string p_belongDeptId,
			string p_belongDeptName,
			string p_remark,
			DateTime? p_createTime,
			string p_createId,
			string p_createName)
		{
			_id = p_id;
			_indicatorName = p_indicatorName;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_belongDeptId = p_belongDeptId;
			_belongDeptName = p_belongDeptName;
			_remark = p_remark;
			_createTime = p_createTime;
			_createId = p_createId;
			_createName = p_createName;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			// set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("IndicatorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string IndicatorName
		{
			get { return _indicatorName; }
			set
			{
				if ((_indicatorName == null) || (value == null) || (!value.Equals(_indicatorName)))
				{
                    object oldValue = _indicatorName;
					_indicatorName = value;
					RaisePropertyChanged(ExamineIndicator.Prop_IndicatorName, oldValue, value);
				}
			}

		}

		[Property("BeRoleCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BeRoleCode
		{
			get { return _beRoleCode; }
			set
			{
				if ((_beRoleCode == null) || (value == null) || (!value.Equals(_beRoleCode)))
				{
                    object oldValue = _beRoleCode;
					_beRoleCode = value;
					RaisePropertyChanged(ExamineIndicator.Prop_BeRoleCode, oldValue, value);
				}
			}

		}

		[Property("BeRoleName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BeRoleName
		{
			get { return _beRoleName; }
			set
			{
				if ((_beRoleName == null) || (value == null) || (!value.Equals(_beRoleName)))
				{
                    object oldValue = _beRoleName;
					_beRoleName = value;
					RaisePropertyChanged(ExamineIndicator.Prop_BeRoleName, oldValue, value);
				}
			}

		}

		[Property("BelongDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string BelongDeptId
		{
			get { return _belongDeptId; }
			set
			{
				if ((_belongDeptId == null) || (value == null) || (!value.Equals(_belongDeptId)))
				{
                    object oldValue = _belongDeptId;
					_belongDeptId = value;
					RaisePropertyChanged(ExamineIndicator.Prop_BelongDeptId, oldValue, value);
				}
			}

		}

		[Property("BelongDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BelongDeptName
		{
			get { return _belongDeptName; }
			set
			{
				if ((_belongDeptName == null) || (value == null) || (!value.Equals(_belongDeptName)))
				{
                    object oldValue = _belongDeptName;
					_belongDeptName = value;
					RaisePropertyChanged(ExamineIndicator.Prop_BelongDeptName, oldValue, value);
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
					RaisePropertyChanged(ExamineIndicator.Prop_Remark, oldValue, value);
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
					RaisePropertyChanged(ExamineIndicator.Prop_CreateTime, oldValue, value);
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
					RaisePropertyChanged(ExamineIndicator.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamineIndicator.Prop_CreateName, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineIndicator
}


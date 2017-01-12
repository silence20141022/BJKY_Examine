// Business class CustomIndicator generated from CustomIndicator
// Creator: Ray
// Created Date: [2013-12-09]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("CustomIndicator")]
	public partial class CustomIndicator : ExamModelBase<CustomIndicator>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_IndicatorNo = "IndicatorNo";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_IndicatorSecondId = "IndicatorSecondId";
		public static string Prop_IndicatorSecondName = "IndicatorSecondName";
		public static string Prop_DeptIndicatorName = "DeptIndicatorName";
		public static string Prop_DeptIndicatorId = "DeptIndicatorId";
		public static string Prop_Weight = "Weight";
		public static string Prop_Year = "Year";
		public static string Prop_StageType = "StageType";
		public static string Prop_State = "State";
		public static string Prop_Result = "Result";
		public static string Prop_DeptId = "DeptId";
		public static string Prop_DeptName = "DeptName";
		public static string Prop_Remark = "Remark";
		public static string Prop_Summary = "Summary";
		public static string Prop_Opinion = "Opinion";
		public static string Prop_ApproveUserId = "ApproveUserId";
		public static string Prop_ApproveUserName = "ApproveUserName";
		public static string Prop_ApproveTime = "ApproveTime";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";

		#endregion

		#region Private_Variables

		private string _id;
		private string _indicatorNo;
		private string _examineStageId;
		private string _indicatorSecondId;
		private string _indicatorSecondName;
		private string _deptIndicatorName;
		private string _deptIndicatorId;
		private int? _weight;
		private string _year;
		private string _stageType;
		private string _state;
		private string _result;
		private string _deptId;
		private string _deptName;
		private string _remark;
		private string _summary;
		private string _opinion;
		private string _approveUserId;
		private string _approveUserName;
		private DateTime? _approveTime;
		private DateTime? _createTime;
		private string _createId;
		private string _createName;


		#endregion

		#region Constructors

		public CustomIndicator()
		{
		}

		public CustomIndicator(
			string p_id,
			string p_indicatorNo,
			string p_examineStageId,
			string p_indicatorSecondId,
			string p_indicatorSecondName,
			string p_deptIndicatorName,
			string p_deptIndicatorId,
			int? p_weight,
			string p_year,
			string p_stageType,
			string p_state,
			string p_result,
			string p_deptId,
			string p_deptName,
			string p_remark,
			string p_summary,
			string p_opinion,
			string p_approveUserId,
			string p_approveUserName,
			DateTime? p_approveTime,
			DateTime? p_createTime,
			string p_createId,
			string p_createName)
		{
			_id = p_id;
			_indicatorNo = p_indicatorNo;
			_examineStageId = p_examineStageId;
			_indicatorSecondId = p_indicatorSecondId;
			_indicatorSecondName = p_indicatorSecondName;
			_deptIndicatorName = p_deptIndicatorName;
			_deptIndicatorId = p_deptIndicatorId;
			_weight = p_weight;
			_year = p_year;
			_stageType = p_stageType;
			_state = p_state;
			_result = p_result;
			_deptId = p_deptId;
			_deptName = p_deptName;
			_remark = p_remark;
			_summary = p_summary;
			_opinion = p_opinion;
			_approveUserId = p_approveUserId;
			_approveUserName = p_approveUserName;
			_approveTime = p_approveTime;
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

		[Property("IndicatorNo", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string IndicatorNo
		{
			get { return _indicatorNo; }
			set
			{
				if ((_indicatorNo == null) || (value == null) || (!value.Equals(_indicatorNo)))
				{
                    object oldValue = _indicatorNo;
					_indicatorNo = value;
					RaisePropertyChanged(CustomIndicator.Prop_IndicatorNo, oldValue, value);
				}
			}

		}

		[Property("ExamineStageId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ExamineStageId
		{
			get { return _examineStageId; }
			set
			{
				if ((_examineStageId == null) || (value == null) || (!value.Equals(_examineStageId)))
				{
                    object oldValue = _examineStageId;
					_examineStageId = value;
					RaisePropertyChanged(CustomIndicator.Prop_ExamineStageId, oldValue, value);
				}
			}

		}

		[Property("IndicatorSecondId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string IndicatorSecondId
		{
			get { return _indicatorSecondId; }
			set
			{
				if ((_indicatorSecondId == null) || (value == null) || (!value.Equals(_indicatorSecondId)))
				{
                    object oldValue = _indicatorSecondId;
					_indicatorSecondId = value;
					RaisePropertyChanged(CustomIndicator.Prop_IndicatorSecondId, oldValue, value);
				}
			}

		}

		[Property("IndicatorSecondName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string IndicatorSecondName
		{
			get { return _indicatorSecondName; }
			set
			{
				if ((_indicatorSecondName == null) || (value == null) || (!value.Equals(_indicatorSecondName)))
				{
                    object oldValue = _indicatorSecondName;
					_indicatorSecondName = value;
					RaisePropertyChanged(CustomIndicator.Prop_IndicatorSecondName, oldValue, value);
				}
			}

		}

		[Property("DeptIndicatorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string DeptIndicatorName
		{
			get { return _deptIndicatorName; }
			set
			{
				if ((_deptIndicatorName == null) || (value == null) || (!value.Equals(_deptIndicatorName)))
				{
                    object oldValue = _deptIndicatorName;
					_deptIndicatorName = value;
					RaisePropertyChanged(CustomIndicator.Prop_DeptIndicatorName, oldValue, value);
				}
			}

		}

		[Property("DeptIndicatorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string DeptIndicatorId
		{
			get { return _deptIndicatorId; }
			set
			{
				if ((_deptIndicatorId == null) || (value == null) || (!value.Equals(_deptIndicatorId)))
				{
                    object oldValue = _deptIndicatorId;
					_deptIndicatorId = value;
					RaisePropertyChanged(CustomIndicator.Prop_DeptIndicatorId, oldValue, value);
				}
			}

		}

		[Property("Weight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? Weight
		{
			get { return _weight; }
			set
			{
				if (value != _weight)
				{
                    object oldValue = _weight;
					_weight = value;
					RaisePropertyChanged(CustomIndicator.Prop_Weight, oldValue, value);
				}
			}

		}

		[Property("Year", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Year
		{
			get { return _year; }
			set
			{
				if ((_year == null) || (value == null) || (!value.Equals(_year)))
				{
                    object oldValue = _year;
					_year = value;
					RaisePropertyChanged(CustomIndicator.Prop_Year, oldValue, value);
				}
			}

		}

		[Property("StageType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string StageType
		{
			get { return _stageType; }
			set
			{
				if ((_stageType == null) || (value == null) || (!value.Equals(_stageType)))
				{
                    object oldValue = _stageType;
					_stageType = value;
					RaisePropertyChanged(CustomIndicator.Prop_StageType, oldValue, value);
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
					RaisePropertyChanged(CustomIndicator.Prop_State, oldValue, value);
				}
			}

		}

		[Property("Result", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Result
		{
			get { return _result; }
			set
			{
				if ((_result == null) || (value == null) || (!value.Equals(_result)))
				{
                    object oldValue = _result;
					_result = value;
					RaisePropertyChanged(CustomIndicator.Prop_Result, oldValue, value);
				}
			}

		}

		[Property("DeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string DeptId
		{
			get { return _deptId; }
			set
			{
				if ((_deptId == null) || (value == null) || (!value.Equals(_deptId)))
				{
                    object oldValue = _deptId;
					_deptId = value;
					RaisePropertyChanged(CustomIndicator.Prop_DeptId, oldValue, value);
				}
			}

		}

		[Property("DeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string DeptName
		{
			get { return _deptName; }
			set
			{
				if ((_deptName == null) || (value == null) || (!value.Equals(_deptName)))
				{
                    object oldValue = _deptName;
					_deptName = value;
					RaisePropertyChanged(CustomIndicator.Prop_DeptName, oldValue, value);
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
					RaisePropertyChanged(CustomIndicator.Prop_Remark, oldValue, value);
				}
			}

		}

		[Property("Summary", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Summary
		{
			get { return _summary; }
			set
			{
				if ((_summary == null) || (value == null) || (!value.Equals(_summary)))
				{
                    object oldValue = _summary;
					_summary = value;
					RaisePropertyChanged(CustomIndicator.Prop_Summary, oldValue, value);
				}
			}

		}

		[Property("Opinion", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Opinion
		{
			get { return _opinion; }
			set
			{
				if ((_opinion == null) || (value == null) || (!value.Equals(_opinion)))
				{
                    object oldValue = _opinion;
					_opinion = value;
					RaisePropertyChanged(CustomIndicator.Prop_Opinion, oldValue, value);
				}
			}

		}

		[Property("ApproveUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ApproveUserId
		{
			get { return _approveUserId; }
			set
			{
				if ((_approveUserId == null) || (value == null) || (!value.Equals(_approveUserId)))
				{
                    object oldValue = _approveUserId;
					_approveUserId = value;
					RaisePropertyChanged(CustomIndicator.Prop_ApproveUserId, oldValue, value);
				}
			}

		}

		[Property("ApproveUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ApproveUserName
		{
			get { return _approveUserName; }
			set
			{
				if ((_approveUserName == null) || (value == null) || (!value.Equals(_approveUserName)))
				{
                    object oldValue = _approveUserName;
					_approveUserName = value;
					RaisePropertyChanged(CustomIndicator.Prop_ApproveUserName, oldValue, value);
				}
			}

		}

		[Property("ApproveTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ApproveTime
		{
			get { return _approveTime; }
			set
			{
				if (value != _approveTime)
				{
                    object oldValue = _approveTime;
					_approveTime = value;
					RaisePropertyChanged(CustomIndicator.Prop_ApproveTime, oldValue, value);
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
					RaisePropertyChanged(CustomIndicator.Prop_CreateTime, oldValue, value);
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
					RaisePropertyChanged(CustomIndicator.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(CustomIndicator.Prop_CreateName, oldValue, value);
				}
			}

		}

		#endregion
	} // CustomIndicator
}


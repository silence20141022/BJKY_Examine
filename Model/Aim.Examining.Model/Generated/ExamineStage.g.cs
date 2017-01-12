// Business class ExamineStage generated from ExamineStage
// Creator: Ray
// Created Date: [2012-12-09]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineStage")]
	public partial class ExamineStage : ExamModelBase<ExamineStage>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_StageName = "StageName";
		public static string Prop_ExamineType = "ExamineType";
		public static string Prop_StageType = "StageType";
		public static string Prop_Year = "Year";
		public static string Prop_StartTime = "StartTime";
		public static string Prop_EndTime = "EndTime";
		public static string Prop_LaunchUserId = "LaunchUserId";
		public static string Prop_LaunchUserName = "LaunchUserName";
		public static string Prop_LaunchDeptId = "LaunchDeptId";
		public static string Prop_LaunchDeptName = "LaunchDeptName";
		public static string Prop_State = "State";
		public static string Prop_TaskQuan = "TaskQuan";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_Remark = "Remark";

		#endregion

		#region Private_Variables

		private string _id;
		private string _stageName;
		private string _examineType;
		private string _stageType;
		private string _year;
		private DateTime? _startTime;
		private DateTime? _endTime;
		private string _launchUserId;
		private string _launchUserName;
		private string _launchDeptId;
		private string _launchDeptName;
		private int? _state;
		private int? _taskQuan;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;
		private string _remark;


		#endregion

		#region Constructors

		public ExamineStage()
		{
		}

		public ExamineStage(
			string p_id,
			string p_stageName,
			string p_examineType,
			string p_stageType,
			string p_year,
			DateTime? p_startTime,
			DateTime? p_endTime,
			string p_launchUserId,
			string p_launchUserName,
			string p_launchDeptId,
			string p_launchDeptName,
			int? p_state,
			int? p_taskQuan,
			string p_createId,
			string p_createName,
			DateTime? p_createTime,
			string p_remark)
		{
			_id = p_id;
			_stageName = p_stageName;
			_examineType = p_examineType;
			_stageType = p_stageType;
			_year = p_year;
			_startTime = p_startTime;
			_endTime = p_endTime;
			_launchUserId = p_launchUserId;
			_launchUserName = p_launchUserName;
			_launchDeptId = p_launchDeptId;
			_launchDeptName = p_launchDeptName;
			_state = p_state;
			_taskQuan = p_taskQuan;
			_createId = p_createId;
			_createName = p_createName;
			_createTime = p_createTime;
			_remark = p_remark;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			 set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("StageName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string StageName
		{
			get { return _stageName; }
			set
			{
				if ((_stageName == null) || (value == null) || (!value.Equals(_stageName)))
				{
                    object oldValue = _stageName;
					_stageName = value;
					RaisePropertyChanged(ExamineStage.Prop_StageName, oldValue, value);
				}
			}

		}

		[Property("ExamineType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ExamineType
		{
			get { return _examineType; }
			set
			{
				if ((_examineType == null) || (value == null) || (!value.Equals(_examineType)))
				{
                    object oldValue = _examineType;
					_examineType = value;
					RaisePropertyChanged(ExamineStage.Prop_ExamineType, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_StageType, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_Year, oldValue, value);
				}
			}

		}

		[Property("StartTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? StartTime
		{
			get { return _startTime; }
			set
			{
				if (value != _startTime)
				{
                    object oldValue = _startTime;
					_startTime = value;
					RaisePropertyChanged(ExamineStage.Prop_StartTime, oldValue, value);
				}
			}

		}

		[Property("EndTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? EndTime
		{
			get { return _endTime; }
			set
			{
				if (value != _endTime)
				{
                    object oldValue = _endTime;
					_endTime = value;
					RaisePropertyChanged(ExamineStage.Prop_EndTime, oldValue, value);
				}
			}

		}

		[Property("LaunchUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string LaunchUserId
		{
			get { return _launchUserId; }
			set
			{
				if ((_launchUserId == null) || (value == null) || (!value.Equals(_launchUserId)))
				{
                    object oldValue = _launchUserId;
					_launchUserId = value;
					RaisePropertyChanged(ExamineStage.Prop_LaunchUserId, oldValue, value);
				}
			}

		}

		[Property("LaunchUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string LaunchUserName
		{
			get { return _launchUserName; }
			set
			{
				if ((_launchUserName == null) || (value == null) || (!value.Equals(_launchUserName)))
				{
                    object oldValue = _launchUserName;
					_launchUserName = value;
					RaisePropertyChanged(ExamineStage.Prop_LaunchUserName, oldValue, value);
				}
			}

		}

		[Property("LaunchDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string LaunchDeptId
		{
			get { return _launchDeptId; }
			set
			{
				if ((_launchDeptId == null) || (value == null) || (!value.Equals(_launchDeptId)))
				{
                    object oldValue = _launchDeptId;
					_launchDeptId = value;
					RaisePropertyChanged(ExamineStage.Prop_LaunchDeptId, oldValue, value);
				}
			}

		}

		[Property("LaunchDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string LaunchDeptName
		{
			get { return _launchDeptName; }
			set
			{
				if ((_launchDeptName == null) || (value == null) || (!value.Equals(_launchDeptName)))
				{
                    object oldValue = _launchDeptName;
					_launchDeptName = value;
					RaisePropertyChanged(ExamineStage.Prop_LaunchDeptName, oldValue, value);
				}
			}

		}

		[Property("State", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? State
		{
			get { return _state; }
			set
			{
				if (value != _state)
				{
                    object oldValue = _state;
					_state = value;
					RaisePropertyChanged(ExamineStage.Prop_State, oldValue, value);
				}
			}

		}

		[Property("TaskQuan", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? TaskQuan
		{
			get { return _taskQuan; }
			set
			{
				if (value != _taskQuan)
				{
                    object oldValue = _taskQuan;
					_taskQuan = value;
					RaisePropertyChanged(ExamineStage.Prop_TaskQuan, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_CreateTime, oldValue, value);
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
					RaisePropertyChanged(ExamineStage.Prop_Remark, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineStage
}


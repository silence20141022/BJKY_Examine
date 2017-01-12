// Business class Feedback generated from Feedback
// Creator: Ray
// Created Date: [2012-12-13]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("Feedback")]
	public partial class Feedback : ExamModelBase<Feedback>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamYearResultId = "ExamYearResultId";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_Year = "Year";
		public static string Prop_UserId = "UserId";
		public static string Prop_UserName = "UserName";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_DeptId = "DeptId";
		public static string Prop_DeptName = "DeptName";
		public static string Prop_DirectLeaderIds = "DirectLeaderIds";
		public static string Prop_DirectLeaderNames = "DirectLeaderNames";
		public static string Prop_ExamineGrade = "ExamineGrade";
		public static string Prop_IntegrationScore = "IntegrationScore";
		public static string Prop_FirstQuarterScore = "FirstQuarterScore";
		public static string Prop_SecondQuarterScore = "SecondQuarterScore";
		public static string Prop_ThirdQuarterScore = "ThirdQuarterScore";
		public static string Prop_YearScore = "YearScore";
		public static string Prop_Advantage = "Advantage";
		public static string Prop_Shortcoming = "Shortcoming";
		public static string Prop_PlanAndMethod = "PlanAndMethod";
		public static string Prop_State = "State";
		public static string Prop_Result = "Result";
		public static string Prop_FeedbackTime = "FeedbackTime";
		public static string Prop_DirectLeaderSignDate = "DirectLeaderSignDate";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examYearResultId;
		private string _examineStageId;
		private string _year;
		private string _userId;
		private string _userName;
		private string _beRoleCode;
		private string _beRoleName;
		private string _deptId;
		private string _deptName;
		private string _directLeaderIds;
		private string _directLeaderNames;
		private string _examineGrade;
		private System.Decimal? _integrationScore;
		private System.Decimal? _firstQuarterScore;
		private System.Decimal? _secondQuarterScore;
		private System.Decimal? _thirdQuarterScore;
		private System.Decimal? _yearScore;
		private string _advantage;
		private string _shortcoming;
		private string _planAndMethod;
		private int? _state;
		private string _result;
		private DateTime? _feedbackTime;
		private DateTime? _directLeaderSignDate;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public Feedback()
		{
		}

		public Feedback(
			string p_id,
			string p_examYearResultId,
			string p_examineStageId,
			string p_year,
			string p_userId,
			string p_userName,
			string p_beRoleCode,
			string p_beRoleName,
			string p_deptId,
			string p_deptName,
			string p_directLeaderIds,
			string p_directLeaderNames,
			string p_examineGrade,
			System.Decimal? p_integrationScore,
			System.Decimal? p_firstQuarterScore,
			System.Decimal? p_secondQuarterScore,
			System.Decimal? p_thirdQuarterScore,
			System.Decimal? p_yearScore,
			string p_advantage,
			string p_shortcoming,
			string p_planAndMethod,
			int? p_state,
			string p_result,
			DateTime? p_feedbackTime,
			DateTime? p_directLeaderSignDate,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_examYearResultId = p_examYearResultId;
			_examineStageId = p_examineStageId;
			_year = p_year;
			_userId = p_userId;
			_userName = p_userName;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_deptId = p_deptId;
			_deptName = p_deptName;
			_directLeaderIds = p_directLeaderIds;
			_directLeaderNames = p_directLeaderNames;
			_examineGrade = p_examineGrade;
			_integrationScore = p_integrationScore;
			_firstQuarterScore = p_firstQuarterScore;
			_secondQuarterScore = p_secondQuarterScore;
			_thirdQuarterScore = p_thirdQuarterScore;
			_yearScore = p_yearScore;
			_advantage = p_advantage;
			_shortcoming = p_shortcoming;
			_planAndMethod = p_planAndMethod;
			_state = p_state;
			_result = p_result;
			_feedbackTime = p_feedbackTime;
			_directLeaderSignDate = p_directLeaderSignDate;
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

		[Property("ExamYearResultId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ExamYearResultId
		{
			get { return _examYearResultId; }
			set
			{
				if ((_examYearResultId == null) || (value == null) || (!value.Equals(_examYearResultId)))
				{
                    object oldValue = _examYearResultId;
					_examYearResultId = value;
					RaisePropertyChanged(Feedback.Prop_ExamYearResultId, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_ExamineStageId, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_Year, oldValue, value);
				}
			}

		}

		[Property("UserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string UserId
		{
			get { return _userId; }
			set
			{
				if ((_userId == null) || (value == null) || (!value.Equals(_userId)))
				{
                    object oldValue = _userId;
					_userId = value;
					RaisePropertyChanged(Feedback.Prop_UserId, oldValue, value);
				}
			}

		}

		[Property("UserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string UserName
		{
			get { return _userName; }
			set
			{
				if ((_userName == null) || (value == null) || (!value.Equals(_userName)))
				{
                    object oldValue = _userName;
					_userName = value;
					RaisePropertyChanged(Feedback.Prop_UserName, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_BeRoleCode, oldValue, value);
				}
			}

		}

		[Property("BeRoleName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BeRoleName
		{
			get { return _beRoleName; }
			set
			{
				if ((_beRoleName == null) || (value == null) || (!value.Equals(_beRoleName)))
				{
                    object oldValue = _beRoleName;
					_beRoleName = value;
					RaisePropertyChanged(Feedback.Prop_BeRoleName, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_DeptId, oldValue, value);
				}
			}

		}

		[Property("DeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string DeptName
		{
			get { return _deptName; }
			set
			{
				if ((_deptName == null) || (value == null) || (!value.Equals(_deptName)))
				{
                    object oldValue = _deptName;
					_deptName = value;
					RaisePropertyChanged(Feedback.Prop_DeptName, oldValue, value);
				}
			}

		}

		[Property("DirectLeaderIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string DirectLeaderIds
		{
			get { return _directLeaderIds; }
			set
			{
				if ((_directLeaderIds == null) || (value == null) || (!value.Equals(_directLeaderIds)))
				{
                    object oldValue = _directLeaderIds;
					_directLeaderIds = value;
					RaisePropertyChanged(Feedback.Prop_DirectLeaderIds, oldValue, value);
				}
			}

		}

		[Property("DirectLeaderNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string DirectLeaderNames
		{
			get { return _directLeaderNames; }
			set
			{
				if ((_directLeaderNames == null) || (value == null) || (!value.Equals(_directLeaderNames)))
				{
                    object oldValue = _directLeaderNames;
					_directLeaderNames = value;
					RaisePropertyChanged(Feedback.Prop_DirectLeaderNames, oldValue, value);
				}
			}

		}

		[Property("ExamineGrade", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ExamineGrade
		{
			get { return _examineGrade; }
			set
			{
				if ((_examineGrade == null) || (value == null) || (!value.Equals(_examineGrade)))
				{
                    object oldValue = _examineGrade;
					_examineGrade = value;
					RaisePropertyChanged(Feedback.Prop_ExamineGrade, oldValue, value);
				}
			}

		}

		[Property("IntegrationScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? IntegrationScore
		{
			get { return _integrationScore; }
			set
			{
				if (value != _integrationScore)
				{
                    object oldValue = _integrationScore;
					_integrationScore = value;
					RaisePropertyChanged(Feedback.Prop_IntegrationScore, oldValue, value);
				}
			}

		}

		[Property("FirstQuarterScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? FirstQuarterScore
		{
			get { return _firstQuarterScore; }
			set
			{
				if (value != _firstQuarterScore)
				{
                    object oldValue = _firstQuarterScore;
					_firstQuarterScore = value;
					RaisePropertyChanged(Feedback.Prop_FirstQuarterScore, oldValue, value);
				}
			}

		}

		[Property("SecondQuarterScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SecondQuarterScore
		{
			get { return _secondQuarterScore; }
			set
			{
				if (value != _secondQuarterScore)
				{
                    object oldValue = _secondQuarterScore;
					_secondQuarterScore = value;
					RaisePropertyChanged(Feedback.Prop_SecondQuarterScore, oldValue, value);
				}
			}

		}

		[Property("ThirdQuarterScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? ThirdQuarterScore
		{
			get { return _thirdQuarterScore; }
			set
			{
				if (value != _thirdQuarterScore)
				{
                    object oldValue = _thirdQuarterScore;
					_thirdQuarterScore = value;
					RaisePropertyChanged(Feedback.Prop_ThirdQuarterScore, oldValue, value);
				}
			}

		}

		[Property("YearScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? YearScore
		{
			get { return _yearScore; }
			set
			{
				if (value != _yearScore)
				{
                    object oldValue = _yearScore;
					_yearScore = value;
					RaisePropertyChanged(Feedback.Prop_YearScore, oldValue, value);
				}
			}

		}

		[Property("Advantage", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Advantage
		{
			get { return _advantage; }
			set
			{
				if ((_advantage == null) || (value == null) || (!value.Equals(_advantage)))
				{
                    object oldValue = _advantage;
					_advantage = value;
					RaisePropertyChanged(Feedback.Prop_Advantage, oldValue, value);
				}
			}

		}

		[Property("Shortcoming", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Shortcoming
		{
			get { return _shortcoming; }
			set
			{
				if ((_shortcoming == null) || (value == null) || (!value.Equals(_shortcoming)))
				{
                    object oldValue = _shortcoming;
					_shortcoming = value;
					RaisePropertyChanged(Feedback.Prop_Shortcoming, oldValue, value);
				}
			}

		}

		[Property("PlanAndMethod", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string PlanAndMethod
		{
			get { return _planAndMethod; }
			set
			{
				if ((_planAndMethod == null) || (value == null) || (!value.Equals(_planAndMethod)))
				{
                    object oldValue = _planAndMethod;
					_planAndMethod = value;
					RaisePropertyChanged(Feedback.Prop_PlanAndMethod, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_State, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_Result, oldValue, value);
				}
			}

		}

		[Property("FeedbackTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? FeedbackTime
		{
			get { return _feedbackTime; }
			set
			{
				if (value != _feedbackTime)
				{
                    object oldValue = _feedbackTime;
					_feedbackTime = value;
					RaisePropertyChanged(Feedback.Prop_FeedbackTime, oldValue, value);
				}
			}

		}

		[Property("DirectLeaderSignDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DirectLeaderSignDate
		{
			get { return _directLeaderSignDate; }
			set
			{
				if (value != _directLeaderSignDate)
				{
                    object oldValue = _directLeaderSignDate;
					_directLeaderSignDate = value;
					RaisePropertyChanged(Feedback.Prop_DirectLeaderSignDate, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(Feedback.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // Feedback
}


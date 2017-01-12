// Business class ExamYearResult generated from ExamYearResult
// Creator: Ray
// Created Date: [2012-12-18]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamYearResult")]
	public partial class ExamYearResult : ExamModelBase<ExamYearResult>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_UserId = "UserId";
		public static string Prop_UserName = "UserName";
		public static string Prop_DeptId = "DeptId";
		public static string Prop_DeptName = "DeptName";
		public static string Prop_IntegrationScore = "IntegrationScore";
		public static string Prop_AvgScore = "AvgScore";
		public static string Prop_FirstQuarterScore = "FirstQuarterScore";
		public static string Prop_SecondQuarterScore = "SecondQuarterScore";
		public static string Prop_ThirdQuarterScore = "ThirdQuarterScore";
		public static string Prop_FourthQuarterScore = "FourthQuarterScore";
		public static string Prop_UpLevelScore = "UpLevelScore";
		public static string Prop_SameLevelScore = "SameLevelScore";
		public static string Prop_DownLevelScore = "DownLevelScore";
		public static string Prop_AdviceLevel = "AdviceLevel";
		public static string Prop_ApproveLevel = "ApproveLevel";
		public static string Prop_ApproveScore = "ApproveScore";
		public static string Prop_ApproveUserId = "ApproveUserId";
		public static string Prop_ApproveUserName = "ApproveUserName";
		public static string Prop_ApproveTime = "ApproveTime";
		public static string Prop_AppealScore = "AppealScore";
		public static string Prop_AppealLevel = "AppealLevel";
		public static string Prop_AppealEndTime = "AppealEndTime";
		public static string Prop_Year = "Year";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_State = "State";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineStageId;
		private string _beRoleCode;
		private string _beRoleName;
		private string _userId;
		private string _userName;
		private string _deptId;
		private string _deptName;
		private System.Decimal? _integrationScore;
		private System.Decimal? _avgScore;
		private System.Decimal? _firstQuarterScore;
		private System.Decimal? _secondQuarterScore;
		private System.Decimal? _thirdQuarterScore;
		private System.Decimal? _fourthQuarterScore;
		private System.Decimal? _upLevelScore;
		private System.Decimal? _sameLevelScore;
		private System.Decimal? _downLevelScore;
		private string _adviceLevel;
		private string _approveLevel;
		private System.Decimal? _approveScore;
		private string _approveUserId;
		private string _approveUserName;
		private DateTime? _approveTime;
		private System.Decimal? _appealScore;
		private string _appealLevel;
		private DateTime? _appealEndTime;
		private string _year;
		private int? _sortIndex;
		private string _state;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public ExamYearResult()
		{
		}

		public ExamYearResult(
			string p_id,
			string p_examineStageId,
			string p_beRoleCode,
			string p_beRoleName,
			string p_userId,
			string p_userName,
			string p_deptId,
			string p_deptName,
			System.Decimal? p_integrationScore,
			System.Decimal? p_avgScore,
			System.Decimal? p_firstQuarterScore,
			System.Decimal? p_secondQuarterScore,
			System.Decimal? p_thirdQuarterScore,
			System.Decimal? p_fourthQuarterScore,
			System.Decimal? p_upLevelScore,
			System.Decimal? p_sameLevelScore,
			System.Decimal? p_downLevelScore,
			string p_adviceLevel,
			string p_approveLevel,
			System.Decimal? p_approveScore,
			string p_approveUserId,
			string p_approveUserName,
			DateTime? p_approveTime,
			System.Decimal? p_appealScore,
			string p_appealLevel,
			DateTime? p_appealEndTime,
			string p_year,
			int? p_sortIndex,
			string p_state,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_examineStageId = p_examineStageId;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_userId = p_userId;
			_userName = p_userName;
			_deptId = p_deptId;
			_deptName = p_deptName;
			_integrationScore = p_integrationScore;
			_avgScore = p_avgScore;
			_firstQuarterScore = p_firstQuarterScore;
			_secondQuarterScore = p_secondQuarterScore;
			_thirdQuarterScore = p_thirdQuarterScore;
			_fourthQuarterScore = p_fourthQuarterScore;
			_upLevelScore = p_upLevelScore;
			_sameLevelScore = p_sameLevelScore;
			_downLevelScore = p_downLevelScore;
			_adviceLevel = p_adviceLevel;
			_approveLevel = p_approveLevel;
			_approveScore = p_approveScore;
			_approveUserId = p_approveUserId;
			_approveUserName = p_approveUserName;
			_approveTime = p_approveTime;
			_appealScore = p_appealScore;
			_appealLevel = p_appealLevel;
			_appealEndTime = p_appealEndTime;
			_year = p_year;
			_sortIndex = p_sortIndex;
			_state = p_state;
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
					RaisePropertyChanged(ExamYearResult.Prop_ExamineStageId, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_BeRoleCode, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_BeRoleName, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_UserId, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_UserName, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_DeptId, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_DeptName, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_IntegrationScore, oldValue, value);
				}
			}

		}

		[Property("AvgScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? AvgScore
		{
			get { return _avgScore; }
			set
			{
				if (value != _avgScore)
				{
                    object oldValue = _avgScore;
					_avgScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_AvgScore, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_FirstQuarterScore, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_SecondQuarterScore, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_ThirdQuarterScore, oldValue, value);
				}
			}

		}

		[Property("FourthQuarterScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? FourthQuarterScore
		{
			get { return _fourthQuarterScore; }
			set
			{
				if (value != _fourthQuarterScore)
				{
                    object oldValue = _fourthQuarterScore;
					_fourthQuarterScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_FourthQuarterScore, oldValue, value);
				}
			}

		}

		[Property("UpLevelScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? UpLevelScore
		{
			get { return _upLevelScore; }
			set
			{
				if (value != _upLevelScore)
				{
                    object oldValue = _upLevelScore;
					_upLevelScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_UpLevelScore, oldValue, value);
				}
			}

		}

		[Property("SameLevelScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SameLevelScore
		{
			get { return _sameLevelScore; }
			set
			{
				if (value != _sameLevelScore)
				{
                    object oldValue = _sameLevelScore;
					_sameLevelScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_SameLevelScore, oldValue, value);
				}
			}

		}

		[Property("DownLevelScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DownLevelScore
		{
			get { return _downLevelScore; }
			set
			{
				if (value != _downLevelScore)
				{
                    object oldValue = _downLevelScore;
					_downLevelScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_DownLevelScore, oldValue, value);
				}
			}

		}

		[Property("AdviceLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AdviceLevel
		{
			get { return _adviceLevel; }
			set
			{
				if ((_adviceLevel == null) || (value == null) || (!value.Equals(_adviceLevel)))
				{
                    object oldValue = _adviceLevel;
					_adviceLevel = value;
					RaisePropertyChanged(ExamYearResult.Prop_AdviceLevel, oldValue, value);
				}
			}

		}

		[Property("ApproveLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ApproveLevel
		{
			get { return _approveLevel; }
			set
			{
				if ((_approveLevel == null) || (value == null) || (!value.Equals(_approveLevel)))
				{
                    object oldValue = _approveLevel;
					_approveLevel = value;
					RaisePropertyChanged(ExamYearResult.Prop_ApproveLevel, oldValue, value);
				}
			}

		}

		[Property("ApproveScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? ApproveScore
		{
			get { return _approveScore; }
			set
			{
				if (value != _approveScore)
				{
                    object oldValue = _approveScore;
					_approveScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_ApproveScore, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_ApproveUserId, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_ApproveUserName, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_ApproveTime, oldValue, value);
				}
			}

		}

		[Property("AppealScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? AppealScore
		{
			get { return _appealScore; }
			set
			{
				if (value != _appealScore)
				{
                    object oldValue = _appealScore;
					_appealScore = value;
					RaisePropertyChanged(ExamYearResult.Prop_AppealScore, oldValue, value);
				}
			}

		}

		[Property("AppealLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AppealLevel
		{
			get { return _appealLevel; }
			set
			{
				if ((_appealLevel == null) || (value == null) || (!value.Equals(_appealLevel)))
				{
                    object oldValue = _appealLevel;
					_appealLevel = value;
					RaisePropertyChanged(ExamYearResult.Prop_AppealLevel, oldValue, value);
				}
			}

		}

		[Property("AppealEndTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? AppealEndTime
		{
			get { return _appealEndTime; }
			set
			{
				if (value != _appealEndTime)
				{
                    object oldValue = _appealEndTime;
					_appealEndTime = value;
					RaisePropertyChanged(ExamYearResult.Prop_AppealEndTime, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_Year, oldValue, value);
				}
			}

		}

		[Property("SortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? SortIndex
		{
			get { return _sortIndex; }
			set
			{
				if (value != _sortIndex)
				{
                    object oldValue = _sortIndex;
					_sortIndex = value;
					RaisePropertyChanged(ExamYearResult.Prop_SortIndex, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_State, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(ExamYearResult.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamYearResult
}


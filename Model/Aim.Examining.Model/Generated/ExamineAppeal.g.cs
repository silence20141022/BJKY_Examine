// Business class ExamineAppeal generated from ExamineAppeal
// Creator: Ray
// Created Date: [2013-01-05]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineAppeal")]
	public partial class ExamineAppeal : ExamModelBase<ExamineAppeal>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_AppealUserId = "AppealUserId";
		public static string Prop_AppealUserName = "AppealUserName";
		public static string Prop_OriginalScore = "OriginalScore";
		public static string Prop_OriginalLevel = "OriginalLevel";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_ExamineType = "ExamineType";
		public static string Prop_ExamYearResultId = "ExamYearResultId";
		public static string Prop_DeptId = "DeptId";
		public static string Prop_DeptName = "DeptName";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_AppealTime = "AppealTime";
		public static string Prop_AppealEvent = "AppealEvent";
		public static string Prop_AppealReason = "AppealReason";
		public static string Prop_DealAdvices = "DealAdvices";
		public static string Prop_AcceptUserId = "AcceptUserId";
		public static string Prop_AcceptUserName = "AcceptUserName";
		public static string Prop_AcceptSubmitTime = "AcceptSubmitTime";
		public static string Prop_DeptLeaderId = "DeptLeaderId";
		public static string Prop_DeptLeaderName = "DeptLeaderName";
		public static string Prop_DeptLeaderOpinion = "DeptLeaderOpinion";
		public static string Prop_DeptLeaderSubmitTime = "DeptLeaderSubmitTime";
		public static string Prop_HrUserId = "HrUserId";
		public static string Prop_HrUserName = "HrUserName";
		public static string Prop_HrSubmitTime = "HrSubmitTime";
		public static string Prop_HrOpinion = "HrOpinion";
		public static string Prop_ModifiedScore = "ModifiedScore";
		public static string Prop_ModifiedLevel = "ModifiedLevel";
		public static string Prop_State = "State";
		public static string Prop_Result = "Result";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _appealUserId;
		private string _appealUserName;
		private System.Decimal? _originalScore;
		private string _originalLevel;
		private string _examineStageId;
		private string _examineType;
		private string _examYearResultId;
		private string _deptId;
		private string _deptName;
		private string _beRoleCode;
		private string _beRoleName;
		private DateTime? _appealTime;
		private string _appealEvent;
		private string _appealReason;
		private string _dealAdvices;
		private string _acceptUserId;
		private string _acceptUserName;
		private DateTime? _acceptSubmitTime;
		private string _deptLeaderId;
		private string _deptLeaderName;
		private string _deptLeaderOpinion;
		private DateTime? _deptLeaderSubmitTime;
		private string _hrUserId;
		private string _hrUserName;
		private DateTime? _hrSubmitTime;
		private string _hrOpinion;
		private System.Decimal? _modifiedScore;
		private string _modifiedLevel;
		private int? _state;
		private string _result;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public ExamineAppeal()
		{
		}

		public ExamineAppeal(
			string p_id,
			string p_appealUserId,
			string p_appealUserName,
			System.Decimal? p_originalScore,
			string p_originalLevel,
			string p_examineStageId,
			string p_examineType,
			string p_examYearResultId,
			string p_deptId,
			string p_deptName,
			string p_beRoleCode,
			string p_beRoleName,
			DateTime? p_appealTime,
			string p_appealEvent,
			string p_appealReason,
			string p_dealAdvices,
			string p_acceptUserId,
			string p_acceptUserName,
			DateTime? p_acceptSubmitTime,
			string p_deptLeaderId,
			string p_deptLeaderName,
			string p_deptLeaderOpinion,
			DateTime? p_deptLeaderSubmitTime,
			string p_hrUserId,
			string p_hrUserName,
			DateTime? p_hrSubmitTime,
			string p_hrOpinion,
			System.Decimal? p_modifiedScore,
			string p_modifiedLevel,
			int? p_state,
			string p_result,
			DateTime? p_createTime)
		{
			_id = p_id;
			_appealUserId = p_appealUserId;
			_appealUserName = p_appealUserName;
			_originalScore = p_originalScore;
			_originalLevel = p_originalLevel;
			_examineStageId = p_examineStageId;
			_examineType = p_examineType;
			_examYearResultId = p_examYearResultId;
			_deptId = p_deptId;
			_deptName = p_deptName;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_appealTime = p_appealTime;
			_appealEvent = p_appealEvent;
			_appealReason = p_appealReason;
			_dealAdvices = p_dealAdvices;
			_acceptUserId = p_acceptUserId;
			_acceptUserName = p_acceptUserName;
			_acceptSubmitTime = p_acceptSubmitTime;
			_deptLeaderId = p_deptLeaderId;
			_deptLeaderName = p_deptLeaderName;
			_deptLeaderOpinion = p_deptLeaderOpinion;
			_deptLeaderSubmitTime = p_deptLeaderSubmitTime;
			_hrUserId = p_hrUserId;
			_hrUserName = p_hrUserName;
			_hrSubmitTime = p_hrSubmitTime;
			_hrOpinion = p_hrOpinion;
			_modifiedScore = p_modifiedScore;
			_modifiedLevel = p_modifiedLevel;
			_state = p_state;
			_result = p_result;
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

		[Property("AppealUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string AppealUserId
		{
			get { return _appealUserId; }
			set
			{
				if ((_appealUserId == null) || (value == null) || (!value.Equals(_appealUserId)))
				{
                    object oldValue = _appealUserId;
					_appealUserId = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AppealUserId, oldValue, value);
				}
			}

		}

		[Property("AppealUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AppealUserName
		{
			get { return _appealUserName; }
			set
			{
				if ((_appealUserName == null) || (value == null) || (!value.Equals(_appealUserName)))
				{
                    object oldValue = _appealUserName;
					_appealUserName = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AppealUserName, oldValue, value);
				}
			}

		}

		[Property("OriginalScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? OriginalScore
		{
			get { return _originalScore; }
			set
			{
				if (value != _originalScore)
				{
                    object oldValue = _originalScore;
					_originalScore = value;
					RaisePropertyChanged(ExamineAppeal.Prop_OriginalScore, oldValue, value);
				}
			}

		}

		[Property("OriginalLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string OriginalLevel
		{
			get { return _originalLevel; }
			set
			{
				if ((_originalLevel == null) || (value == null) || (!value.Equals(_originalLevel)))
				{
                    object oldValue = _originalLevel;
					_originalLevel = value;
					RaisePropertyChanged(ExamineAppeal.Prop_OriginalLevel, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_ExamineStageId, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_ExamineType, oldValue, value);
				}
			}

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
					RaisePropertyChanged(ExamineAppeal.Prop_ExamYearResultId, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_DeptId, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_DeptName, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_BeRoleCode, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_BeRoleName, oldValue, value);
				}
			}

		}

		[Property("AppealTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? AppealTime
		{
			get { return _appealTime; }
			set
			{
				if (value != _appealTime)
				{
                    object oldValue = _appealTime;
					_appealTime = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AppealTime, oldValue, value);
				}
			}

		}

		[Property("AppealEvent", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string AppealEvent
		{
			get { return _appealEvent; }
			set
			{
				if ((_appealEvent == null) || (value == null) || (!value.Equals(_appealEvent)))
				{
                    object oldValue = _appealEvent;
					_appealEvent = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AppealEvent, oldValue, value);
				}
			}

		}

		[Property("AppealReason", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string AppealReason
		{
			get { return _appealReason; }
			set
			{
				if ((_appealReason == null) || (value == null) || (!value.Equals(_appealReason)))
				{
                    object oldValue = _appealReason;
					_appealReason = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AppealReason, oldValue, value);
				}
			}

		}

		[Property("DealAdvices", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string DealAdvices
		{
			get { return _dealAdvices; }
			set
			{
				if ((_dealAdvices == null) || (value == null) || (!value.Equals(_dealAdvices)))
				{
                    object oldValue = _dealAdvices;
					_dealAdvices = value;
					RaisePropertyChanged(ExamineAppeal.Prop_DealAdvices, oldValue, value);
				}
			}

		}

		[Property("AcceptUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string AcceptUserId
		{
			get { return _acceptUserId; }
			set
			{
				if ((_acceptUserId == null) || (value == null) || (!value.Equals(_acceptUserId)))
				{
                    object oldValue = _acceptUserId;
					_acceptUserId = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AcceptUserId, oldValue, value);
				}
			}

		}

		[Property("AcceptUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AcceptUserName
		{
			get { return _acceptUserName; }
			set
			{
				if ((_acceptUserName == null) || (value == null) || (!value.Equals(_acceptUserName)))
				{
                    object oldValue = _acceptUserName;
					_acceptUserName = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AcceptUserName, oldValue, value);
				}
			}

		}

		[Property("AcceptSubmitTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? AcceptSubmitTime
		{
			get { return _acceptSubmitTime; }
			set
			{
				if (value != _acceptSubmitTime)
				{
                    object oldValue = _acceptSubmitTime;
					_acceptSubmitTime = value;
					RaisePropertyChanged(ExamineAppeal.Prop_AcceptSubmitTime, oldValue, value);
				}
			}

		}

		[Property("DeptLeaderId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string DeptLeaderId
		{
			get { return _deptLeaderId; }
			set
			{
				if ((_deptLeaderId == null) || (value == null) || (!value.Equals(_deptLeaderId)))
				{
                    object oldValue = _deptLeaderId;
					_deptLeaderId = value;
					RaisePropertyChanged(ExamineAppeal.Prop_DeptLeaderId, oldValue, value);
				}
			}

		}

		[Property("DeptLeaderName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string DeptLeaderName
		{
			get { return _deptLeaderName; }
			set
			{
				if ((_deptLeaderName == null) || (value == null) || (!value.Equals(_deptLeaderName)))
				{
                    object oldValue = _deptLeaderName;
					_deptLeaderName = value;
					RaisePropertyChanged(ExamineAppeal.Prop_DeptLeaderName, oldValue, value);
				}
			}

		}

		[Property("DeptLeaderOpinion", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string DeptLeaderOpinion
		{
			get { return _deptLeaderOpinion; }
			set
			{
				if ((_deptLeaderOpinion == null) || (value == null) || (!value.Equals(_deptLeaderOpinion)))
				{
                    object oldValue = _deptLeaderOpinion;
					_deptLeaderOpinion = value;
					RaisePropertyChanged(ExamineAppeal.Prop_DeptLeaderOpinion, oldValue, value);
				}
			}

		}

		[Property("DeptLeaderSubmitTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DeptLeaderSubmitTime
		{
			get { return _deptLeaderSubmitTime; }
			set
			{
				if (value != _deptLeaderSubmitTime)
				{
                    object oldValue = _deptLeaderSubmitTime;
					_deptLeaderSubmitTime = value;
					RaisePropertyChanged(ExamineAppeal.Prop_DeptLeaderSubmitTime, oldValue, value);
				}
			}

		}

		[Property("HrUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string HrUserId
		{
			get { return _hrUserId; }
			set
			{
				if ((_hrUserId == null) || (value == null) || (!value.Equals(_hrUserId)))
				{
                    object oldValue = _hrUserId;
					_hrUserId = value;
					RaisePropertyChanged(ExamineAppeal.Prop_HrUserId, oldValue, value);
				}
			}

		}

		[Property("HrUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string HrUserName
		{
			get { return _hrUserName; }
			set
			{
				if ((_hrUserName == null) || (value == null) || (!value.Equals(_hrUserName)))
				{
                    object oldValue = _hrUserName;
					_hrUserName = value;
					RaisePropertyChanged(ExamineAppeal.Prop_HrUserName, oldValue, value);
				}
			}

		}

		[Property("HrSubmitTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? HrSubmitTime
		{
			get { return _hrSubmitTime; }
			set
			{
				if (value != _hrSubmitTime)
				{
                    object oldValue = _hrSubmitTime;
					_hrSubmitTime = value;
					RaisePropertyChanged(ExamineAppeal.Prop_HrSubmitTime, oldValue, value);
				}
			}

		}

		[Property("HrOpinion", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string HrOpinion
		{
			get { return _hrOpinion; }
			set
			{
				if ((_hrOpinion == null) || (value == null) || (!value.Equals(_hrOpinion)))
				{
                    object oldValue = _hrOpinion;
					_hrOpinion = value;
					RaisePropertyChanged(ExamineAppeal.Prop_HrOpinion, oldValue, value);
				}
			}

		}

		[Property("ModifiedScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? ModifiedScore
		{
			get { return _modifiedScore; }
			set
			{
				if (value != _modifiedScore)
				{
                    object oldValue = _modifiedScore;
					_modifiedScore = value;
					RaisePropertyChanged(ExamineAppeal.Prop_ModifiedScore, oldValue, value);
				}
			}

		}

		[Property("ModifiedLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ModifiedLevel
		{
			get { return _modifiedLevel; }
			set
			{
				if ((_modifiedLevel == null) || (value == null) || (!value.Equals(_modifiedLevel)))
				{
                    object oldValue = _modifiedLevel;
					_modifiedLevel = value;
					RaisePropertyChanged(ExamineAppeal.Prop_ModifiedLevel, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_State, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_Result, oldValue, value);
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
					RaisePropertyChanged(ExamineAppeal.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineAppeal
}


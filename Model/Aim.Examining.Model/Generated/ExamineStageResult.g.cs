// Business class ExamineStageResult generated from ExamineStageResult
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
	[ActiveRecord("ExamineStageResult")]
	public partial class ExamineStageResult : ExamModelBase<ExamineStageResult>
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
		public static string Prop_Year = "Year";
		public static string Prop_StageType = "StageType";
		public static string Prop_UpAvgScore = "UpAvgScore";
		public static string Prop_SameAvgScore = "SameAvgScore";
		public static string Prop_DownAvgScore = "DownAvgScore";
		public static string Prop_Score = "Score";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";

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
		private string _year;
		private string _stageType;
		private System.Decimal? _upAvgScore;
		private System.Decimal? _sameAvgScore;
		private System.Decimal? _downAvgScore;
		private System.Decimal? _score;
		private int? _sortIndex;
		private DateTime? _createTime;
		private string _createId;
		private string _createName;


		#endregion

		#region Constructors

		public ExamineStageResult()
		{
		}

		public ExamineStageResult(
			string p_id,
			string p_examineStageId,
			string p_beRoleCode,
			string p_beRoleName,
			string p_userId,
			string p_userName,
			string p_deptId,
			string p_deptName,
			string p_year,
			string p_stageType,
			System.Decimal? p_upAvgScore,
			System.Decimal? p_sameAvgScore,
			System.Decimal? p_downAvgScore,
			System.Decimal? p_score,
			int? p_sortIndex,
			DateTime? p_createTime,
			string p_createId,
			string p_createName)
		{
			_id = p_id;
			_examineStageId = p_examineStageId;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_userId = p_userId;
			_userName = p_userName;
			_deptId = p_deptId;
			_deptName = p_deptName;
			_year = p_year;
			_stageType = p_stageType;
			_upAvgScore = p_upAvgScore;
			_sameAvgScore = p_sameAvgScore;
			_downAvgScore = p_downAvgScore;
			_score = p_score;
			_sortIndex = p_sortIndex;
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
					RaisePropertyChanged(ExamineStageResult.Prop_ExamineStageId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_BeRoleCode, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_BeRoleName, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_UserId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_UserName, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_DeptId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_DeptName, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_Year, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_StageType, oldValue, value);
				}
			}

		}

		[Property("UpAvgScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? UpAvgScore
		{
			get { return _upAvgScore; }
			set
			{
				if (value != _upAvgScore)
				{
                    object oldValue = _upAvgScore;
					_upAvgScore = value;
					RaisePropertyChanged(ExamineStageResult.Prop_UpAvgScore, oldValue, value);
				}
			}

		}

		[Property("SameAvgScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SameAvgScore
		{
			get { return _sameAvgScore; }
			set
			{
				if (value != _sameAvgScore)
				{
                    object oldValue = _sameAvgScore;
					_sameAvgScore = value;
					RaisePropertyChanged(ExamineStageResult.Prop_SameAvgScore, oldValue, value);
				}
			}

		}

		[Property("DownAvgScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DownAvgScore
		{
			get { return _downAvgScore; }
			set
			{
				if (value != _downAvgScore)
				{
                    object oldValue = _downAvgScore;
					_downAvgScore = value;
					RaisePropertyChanged(ExamineStageResult.Prop_DownAvgScore, oldValue, value);
				}
			}

		}

		[Property("Score", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? Score
		{
			get { return _score; }
			set
			{
				if (value != _score)
				{
                    object oldValue = _score;
					_score = value;
					RaisePropertyChanged(ExamineStageResult.Prop_Score, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_SortIndex, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_CreateTime, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageResult.Prop_CreateName, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineStageResult
}


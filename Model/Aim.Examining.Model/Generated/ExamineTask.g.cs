// Business class ExamineTask generated from ExamineTask
// Creator: Ray
// Created Date: [2012-12-14]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineTask")]
	public partial class ExamineTask : ExamModelBase<ExamineTask>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_ToUserId = "ToUserId";
		public static string Prop_ToUserName = "ToUserName";
		public static string Prop_ToDeptId = "ToDeptId";
		public static string Prop_ToDeptName = "ToDeptName";
		public static string Prop_ToRoleCode = "ToRoleCode";
		public static string Prop_ToRoleName = "ToRoleName";
		public static string Prop_BeUserId = "BeUserId";
		public static string Prop_BeUserName = "BeUserName";
		public static string Prop_BeDeptId = "BeDeptId";
		public static string Prop_BeDeptName = "BeDeptName";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_Score = "Score";
		public static string Prop_State = "State";
		public static string Prop_Tag = "Tag";
		public static string Prop_AmendState = "AmendState";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_ExamineIndicatorId = "ExamineIndicatorId";
		public static string Prop_ExamineRelationId = "ExamineRelationId";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineStageId;
		private string _toUserId;
		private string _toUserName;
		private string _toDeptId;
		private string _toDeptName;
		private string _toRoleCode;
		private string _toRoleName;
		private string _beUserId;
		private string _beUserName;
		private string _beDeptId;
		private string _beDeptName;
		private string _beRoleCode;
		private string _beRoleName;
		private System.Decimal? _score;
		private string _state;
		private string _tag;
		private string _amendState;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;
		private string _examineIndicatorId;
		private string _examineRelationId;


		#endregion

		#region Constructors

		public ExamineTask()
		{
		}

		public ExamineTask(
			string p_id,
			string p_examineStageId,
			string p_toUserId,
			string p_toUserName,
			string p_toDeptId,
			string p_toDeptName,
			string p_toRoleCode,
			string p_toRoleName,
			string p_beUserId,
			string p_beUserName,
			string p_beDeptId,
			string p_beDeptName,
			string p_beRoleCode,
			string p_beRoleName,
			System.Decimal? p_score,
			string p_state,
			string p_tag,
			string p_amendState,
			string p_createId,
			string p_createName,
			DateTime? p_createTime,
			string p_examineIndicatorId,
			string p_examineRelationId)
		{
			_id = p_id;
			_examineStageId = p_examineStageId;
			_toUserId = p_toUserId;
			_toUserName = p_toUserName;
			_toDeptId = p_toDeptId;
			_toDeptName = p_toDeptName;
			_toRoleCode = p_toRoleCode;
			_toRoleName = p_toRoleName;
			_beUserId = p_beUserId;
			_beUserName = p_beUserName;
			_beDeptId = p_beDeptId;
			_beDeptName = p_beDeptName;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_score = p_score;
			_state = p_state;
			_tag = p_tag;
			_amendState = p_amendState;
			_createId = p_createId;
			_createName = p_createName;
			_createTime = p_createTime;
			_examineIndicatorId = p_examineIndicatorId;
			_examineRelationId = p_examineRelationId;
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
					RaisePropertyChanged(ExamineTask.Prop_ExamineStageId, oldValue, value);
				}
			}

		}

		[Property("ToUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ToUserId
		{
			get { return _toUserId; }
			set
			{
				if ((_toUserId == null) || (value == null) || (!value.Equals(_toUserId)))
				{
                    object oldValue = _toUserId;
					_toUserId = value;
					RaisePropertyChanged(ExamineTask.Prop_ToUserId, oldValue, value);
				}
			}

		}

		[Property("ToUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ToUserName
		{
			get { return _toUserName; }
			set
			{
				if ((_toUserName == null) || (value == null) || (!value.Equals(_toUserName)))
				{
                    object oldValue = _toUserName;
					_toUserName = value;
					RaisePropertyChanged(ExamineTask.Prop_ToUserName, oldValue, value);
				}
			}

		}

		[Property("ToDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ToDeptId
		{
			get { return _toDeptId; }
			set
			{
				if ((_toDeptId == null) || (value == null) || (!value.Equals(_toDeptId)))
				{
                    object oldValue = _toDeptId;
					_toDeptId = value;
					RaisePropertyChanged(ExamineTask.Prop_ToDeptId, oldValue, value);
				}
			}

		}

		[Property("ToDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ToDeptName
		{
			get { return _toDeptName; }
			set
			{
				if ((_toDeptName == null) || (value == null) || (!value.Equals(_toDeptName)))
				{
                    object oldValue = _toDeptName;
					_toDeptName = value;
					RaisePropertyChanged(ExamineTask.Prop_ToDeptName, oldValue, value);
				}
			}

		}

		[Property("ToRoleCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ToRoleCode
		{
			get { return _toRoleCode; }
			set
			{
				if ((_toRoleCode == null) || (value == null) || (!value.Equals(_toRoleCode)))
				{
                    object oldValue = _toRoleCode;
					_toRoleCode = value;
					RaisePropertyChanged(ExamineTask.Prop_ToRoleCode, oldValue, value);
				}
			}

		}

		[Property("ToRoleName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ToRoleName
		{
			get { return _toRoleName; }
			set
			{
				if ((_toRoleName == null) || (value == null) || (!value.Equals(_toRoleName)))
				{
                    object oldValue = _toRoleName;
					_toRoleName = value;
					RaisePropertyChanged(ExamineTask.Prop_ToRoleName, oldValue, value);
				}
			}

		}

		[Property("BeUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string BeUserId
		{
			get { return _beUserId; }
			set
			{
				if ((_beUserId == null) || (value == null) || (!value.Equals(_beUserId)))
				{
                    object oldValue = _beUserId;
					_beUserId = value;
					RaisePropertyChanged(ExamineTask.Prop_BeUserId, oldValue, value);
				}
			}

		}

		[Property("BeUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BeUserName
		{
			get { return _beUserName; }
			set
			{
				if ((_beUserName == null) || (value == null) || (!value.Equals(_beUserName)))
				{
                    object oldValue = _beUserName;
					_beUserName = value;
					RaisePropertyChanged(ExamineTask.Prop_BeUserName, oldValue, value);
				}
			}

		}

		[Property("BeDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string BeDeptId
		{
			get { return _beDeptId; }
			set
			{
				if ((_beDeptId == null) || (value == null) || (!value.Equals(_beDeptId)))
				{
                    object oldValue = _beDeptId;
					_beDeptId = value;
					RaisePropertyChanged(ExamineTask.Prop_BeDeptId, oldValue, value);
				}
			}

		}

		[Property("BeDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BeDeptName
		{
			get { return _beDeptName; }
			set
			{
				if ((_beDeptName == null) || (value == null) || (!value.Equals(_beDeptName)))
				{
                    object oldValue = _beDeptName;
					_beDeptName = value;
					RaisePropertyChanged(ExamineTask.Prop_BeDeptName, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_BeRoleCode, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_BeRoleName, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_Score, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_State, oldValue, value);
				}
			}

		}

		[Property("Tag", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Tag
		{
			get { return _tag; }
			set
			{
				if ((_tag == null) || (value == null) || (!value.Equals(_tag)))
				{
                    object oldValue = _tag;
					_tag = value;
					RaisePropertyChanged(ExamineTask.Prop_Tag, oldValue, value);
				}
			}

		}

		[Property("AmendState", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AmendState
		{
			get { return _amendState; }
			set
			{
				if ((_amendState == null) || (value == null) || (!value.Equals(_amendState)))
				{
                    object oldValue = _amendState;
					_amendState = value;
					RaisePropertyChanged(ExamineTask.Prop_AmendState, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(ExamineTask.Prop_CreateTime, oldValue, value);
				}
			}

		}

		[Property("ExamineIndicatorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ExamineIndicatorId
		{
			get { return _examineIndicatorId; }
			set
			{
				if ((_examineIndicatorId == null) || (value == null) || (!value.Equals(_examineIndicatorId)))
				{
                    object oldValue = _examineIndicatorId;
					_examineIndicatorId = value;
					RaisePropertyChanged(ExamineTask.Prop_ExamineIndicatorId, oldValue, value);
				}
			}

		}

		[Property("ExamineRelationId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ExamineRelationId
		{
			get { return _examineRelationId; }
			set
			{
				if ((_examineRelationId == null) || (value == null) || (!value.Equals(_examineRelationId)))
				{
                    object oldValue = _examineRelationId;
					_examineRelationId = value;
					RaisePropertyChanged(ExamineTask.Prop_ExamineRelationId, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineTask
}


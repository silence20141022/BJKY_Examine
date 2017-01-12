// Business class DeptExamineRelation generated from DeptExamineRelation
// Creator: Ray
// Created Date: [2013-01-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("DeptExamineRelation")]
	public partial class DeptExamineRelation : ExamModelBase<DeptExamineRelation>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_RelationName = "RelationName";
		public static string Prop_BeUserIds = "BeUserIds";
		public static string Prop_BeUserNames = "BeUserNames";
		public static string Prop_UpLevelUserIds = "UpLevelUserIds";
		public static string Prop_UpLevelUserNames = "UpLevelUserNames";
		public static string Prop_UpLevelWeight = "UpLevelWeight";
		public static string Prop_SameLevelUserIds = "SameLevelUserIds";
		public static string Prop_SameLevelUserNames = "SameLevelUserNames";
		public static string Prop_SameLevelWeight = "SameLevelWeight";
		public static string Prop_DownLevelUserIds = "DownLevelUserIds";
		public static string Prop_DownLevelUserNames = "DownLevelUserNames";
		public static string Prop_DownLevelWeight = "DownLevelWeight";
		public static string Prop_GroupID = "GroupID";
		public static string Prop_GroupName = "GroupName";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _relationName;
		private string _beUserIds;
		private string _beUserNames;
		private string _upLevelUserIds;
		private string _upLevelUserNames;
		private int? _upLevelWeight;
		private string _sameLevelUserIds;
		private string _sameLevelUserNames;
		private int? _sameLevelWeight;
		private string _downLevelUserIds;
		private string _downLevelUserNames;
		private int? _downLevelWeight;
		private string _groupID;
		private string _groupName;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public DeptExamineRelation()
		{
		}

		public DeptExamineRelation(
			string p_id,
			string p_relationName,
			string p_beUserIds,
			string p_beUserNames,
			string p_upLevelUserIds,
			string p_upLevelUserNames,
			int? p_upLevelWeight,
			string p_sameLevelUserIds,
			string p_sameLevelUserNames,
			int? p_sameLevelWeight,
			string p_downLevelUserIds,
			string p_downLevelUserNames,
			int? p_downLevelWeight,
			string p_groupID,
			string p_groupName,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_relationName = p_relationName;
			_beUserIds = p_beUserIds;
			_beUserNames = p_beUserNames;
			_upLevelUserIds = p_upLevelUserIds;
			_upLevelUserNames = p_upLevelUserNames;
			_upLevelWeight = p_upLevelWeight;
			_sameLevelUserIds = p_sameLevelUserIds;
			_sameLevelUserNames = p_sameLevelUserNames;
			_sameLevelWeight = p_sameLevelWeight;
			_downLevelUserIds = p_downLevelUserIds;
			_downLevelUserNames = p_downLevelUserNames;
			_downLevelWeight = p_downLevelWeight;
			_groupID = p_groupID;
			_groupName = p_groupName;
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

		[Property("RelationName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string RelationName
		{
			get { return _relationName; }
			set
			{
				if ((_relationName == null) || (value == null) || (!value.Equals(_relationName)))
				{
                    object oldValue = _relationName;
					_relationName = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_RelationName, oldValue, value);
				}
			}

		}

		[Property("BeUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 5000)]
		public string BeUserIds
		{
			get { return _beUserIds; }
			set
			{
				if ((_beUserIds == null) || (value == null) || (!value.Equals(_beUserIds)))
				{
                    object oldValue = _beUserIds;
					_beUserIds = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_BeUserIds, oldValue, value);
				}
			}

		}

		[Property("BeUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 5000)]
		public string BeUserNames
		{
			get { return _beUserNames; }
			set
			{
				if ((_beUserNames == null) || (value == null) || (!value.Equals(_beUserNames)))
				{
                    object oldValue = _beUserNames;
					_beUserNames = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_BeUserNames, oldValue, value);
				}
			}

		}

		[Property("UpLevelUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string UpLevelUserIds
		{
			get { return _upLevelUserIds; }
			set
			{
				if ((_upLevelUserIds == null) || (value == null) || (!value.Equals(_upLevelUserIds)))
				{
                    object oldValue = _upLevelUserIds;
					_upLevelUserIds = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_UpLevelUserIds, oldValue, value);
				}
			}

		}

		[Property("UpLevelUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string UpLevelUserNames
		{
			get { return _upLevelUserNames; }
			set
			{
				if ((_upLevelUserNames == null) || (value == null) || (!value.Equals(_upLevelUserNames)))
				{
                    object oldValue = _upLevelUserNames;
					_upLevelUserNames = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_UpLevelUserNames, oldValue, value);
				}
			}

		}

		[Property("UpLevelWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? UpLevelWeight
		{
			get { return _upLevelWeight; }
			set
			{
				if (value != _upLevelWeight)
				{
                    object oldValue = _upLevelWeight;
					_upLevelWeight = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_UpLevelWeight, oldValue, value);
				}
			}

		}

		[Property("SameLevelUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string SameLevelUserIds
		{
			get { return _sameLevelUserIds; }
			set
			{
				if ((_sameLevelUserIds == null) || (value == null) || (!value.Equals(_sameLevelUserIds)))
				{
                    object oldValue = _sameLevelUserIds;
					_sameLevelUserIds = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_SameLevelUserIds, oldValue, value);
				}
			}

		}

		[Property("SameLevelUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string SameLevelUserNames
		{
			get { return _sameLevelUserNames; }
			set
			{
				if ((_sameLevelUserNames == null) || (value == null) || (!value.Equals(_sameLevelUserNames)))
				{
                    object oldValue = _sameLevelUserNames;
					_sameLevelUserNames = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_SameLevelUserNames, oldValue, value);
				}
			}

		}

		[Property("SameLevelWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? SameLevelWeight
		{
			get { return _sameLevelWeight; }
			set
			{
				if (value != _sameLevelWeight)
				{
                    object oldValue = _sameLevelWeight;
					_sameLevelWeight = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_SameLevelWeight, oldValue, value);
				}
			}

		}

		[Property("DownLevelUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string DownLevelUserIds
		{
			get { return _downLevelUserIds; }
			set
			{
				if ((_downLevelUserIds == null) || (value == null) || (!value.Equals(_downLevelUserIds)))
				{
                    object oldValue = _downLevelUserIds;
					_downLevelUserIds = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_DownLevelUserIds, oldValue, value);
				}
			}

		}

		[Property("DownLevelUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string DownLevelUserNames
		{
			get { return _downLevelUserNames; }
			set
			{
				if ((_downLevelUserNames == null) || (value == null) || (!value.Equals(_downLevelUserNames)))
				{
                    object oldValue = _downLevelUserNames;
					_downLevelUserNames = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_DownLevelUserNames, oldValue, value);
				}
			}

		}

		[Property("DownLevelWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? DownLevelWeight
		{
			get { return _downLevelWeight; }
			set
			{
				if (value != _downLevelWeight)
				{
                    object oldValue = _downLevelWeight;
					_downLevelWeight = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_DownLevelWeight, oldValue, value);
				}
			}

		}

		[Property("GroupID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string GroupID
		{
			get { return _groupID; }
			set
			{
				if ((_groupID == null) || (value == null) || (!value.Equals(_groupID)))
				{
                    object oldValue = _groupID;
					_groupID = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_GroupID, oldValue, value);
				}
			}

		}

		[Property("GroupName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				if ((_groupName == null) || (value == null) || (!value.Equals(_groupName)))
				{
                    object oldValue = _groupName;
					_groupName = value;
					RaisePropertyChanged(DeptExamineRelation.Prop_GroupName, oldValue, value);
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
					RaisePropertyChanged(DeptExamineRelation.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(DeptExamineRelation.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(DeptExamineRelation.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // DeptExamineRelation
}


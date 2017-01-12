// Business class ExamineStageDeptDetail generated from ExamineStageDeptDetail
// Creator: Ray
// Created Date: [2014-05-04]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineStageDeptDetail")]
	public partial class ExamineStageDeptDetail : ExamModelBase<ExamineStageDeptDetail>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_GroupID = "GroupID";
		public static string Prop_GroupName = "GroupName";
		public static string Prop_GroupType = "GroupType";
		public static string Prop_FirstLeaderNames = "FirstLeaderNames";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineStageId;
		private string _groupID;
		private string _groupName;
		private string _groupType;
		private string _firstLeaderNames;


		#endregion

		#region Constructors

		public ExamineStageDeptDetail()
		{
		}

		public ExamineStageDeptDetail(
			string p_id,
			string p_examineStageId,
			string p_groupID,
			string p_groupName,
			string p_groupType,
			string p_firstLeaderNames)
		{
			_id = p_id;
			_examineStageId = p_examineStageId;
			_groupID = p_groupID;
			_groupName = p_groupName;
			_groupType = p_groupType;
			_firstLeaderNames = p_firstLeaderNames;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			// set { _id = value; } // 处理列表编辑时去掉注释

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
					RaisePropertyChanged(ExamineStageDeptDetail.Prop_ExamineStageId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageDeptDetail.Prop_GroupID, oldValue, value);
				}
			}

		}

		[Property("GroupName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				if ((_groupName == null) || (value == null) || (!value.Equals(_groupName)))
				{
                    object oldValue = _groupName;
					_groupName = value;
					RaisePropertyChanged(ExamineStageDeptDetail.Prop_GroupName, oldValue, value);
				}
			}

		}

		[Property("GroupType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string GroupType
		{
			get { return _groupType; }
			set
			{
				if ((_groupType == null) || (value == null) || (!value.Equals(_groupType)))
				{
                    object oldValue = _groupType;
					_groupType = value;
					RaisePropertyChanged(ExamineStageDeptDetail.Prop_GroupType, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string FirstLeaderNames
		{
			get { return _firstLeaderNames; }
			set
			{
				if ((_firstLeaderNames == null) || (value == null) || (!value.Equals(_firstLeaderNames)))
				{
                    object oldValue = _firstLeaderNames;
					_firstLeaderNames = value;
					RaisePropertyChanged(ExamineStageDeptDetail.Prop_FirstLeaderNames, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineStageDeptDetail
}


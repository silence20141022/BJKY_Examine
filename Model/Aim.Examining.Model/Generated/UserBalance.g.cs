// Business class UserBalance generated from UserBalance
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
	[ActiveRecord("UserBalance")]
	public partial class UserBalance : ExamModelBase<UserBalance>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineRelationId = "ExamineRelationId";
		public static string Prop_RelationName = "RelationName";
		public static string Prop_ToUserId = "ToUserId";
		public static string Prop_ToUserName = "ToUserName";
		public static string Prop_ToRoleCode = "ToRoleCode";
		public static string Prop_ToRoleName = "ToRoleName";
		public static string Prop_Balance = "Balance";
		public static string Prop_SortIndex = "SortIndex";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineRelationId;
		private string _relationName;
		private string _toUserId;
		private string _toUserName;
		private string _toRoleCode;
		private string _toRoleName;
		private int? _balance;
		private int? _sortIndex;


		#endregion

		#region Constructors

		public UserBalance()
		{
		}

		public UserBalance(
			string p_id,
			string p_examineRelationId,
			string p_relationName,
			string p_toUserId,
			string p_toUserName,
			string p_toRoleCode,
			string p_toRoleName,
			int? p_balance,
			int? p_sortIndex)
		{
			_id = p_id;
			_examineRelationId = p_examineRelationId;
			_relationName = p_relationName;
			_toUserId = p_toUserId;
			_toUserName = p_toUserName;
			_toRoleCode = p_toRoleCode;
			_toRoleName = p_toRoleName;
			_balance = p_balance;
			_sortIndex = p_sortIndex;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			// set { _id = value; } // 处理列表编辑时去掉注释

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
					RaisePropertyChanged(UserBalance.Prop_ExamineRelationId, oldValue, value);
				}
			}

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
					RaisePropertyChanged(UserBalance.Prop_RelationName, oldValue, value);
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
					RaisePropertyChanged(UserBalance.Prop_ToUserId, oldValue, value);
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
					RaisePropertyChanged(UserBalance.Prop_ToUserName, oldValue, value);
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
					RaisePropertyChanged(UserBalance.Prop_ToRoleCode, oldValue, value);
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
					RaisePropertyChanged(UserBalance.Prop_ToRoleName, oldValue, value);
				}
			}

		}

		[Property("Balance", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? Balance
		{
			get { return _balance; }
			set
			{
				if (value != _balance)
				{
                    object oldValue = _balance;
					_balance = value;
					RaisePropertyChanged(UserBalance.Prop_Balance, oldValue, value);
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
					RaisePropertyChanged(UserBalance.Prop_SortIndex, oldValue, value);
				}
			}

		}

		#endregion
	} // UserBalance
}


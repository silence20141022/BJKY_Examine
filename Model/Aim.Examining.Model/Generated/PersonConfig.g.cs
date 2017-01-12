// Business class PersonConfig generated from PersonConfig
// Creator: Ray
// Created Date: [2012-12-24]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("PersonConfig")]
	public partial class PersonConfig : ExamModelBase<PersonConfig>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_GroupID = "GroupID";
		public static string Prop_GroupCode = "GroupCode";
		public static string Prop_GroupName = "GroupName";
		public static string Prop_GroupType = "GroupType";
		public static string Prop_FirstLeaderIds = "FirstLeaderIds";
		public static string Prop_FirstLeaderNames = "FirstLeaderNames";
		public static string Prop_FirstLeaderGroupIds = "FirstLeaderGroupIds";
		public static string Prop_FirstLeaderGroupNames = "FirstLeaderGroupNames";
		public static string Prop_SecondLeaderIds = "SecondLeaderIds";
		public static string Prop_SecondLeaderNames = "SecondLeaderNames";
		public static string Prop_SecondLeaderGroupIds = "SecondLeaderGroupIds";
		public static string Prop_SecondLeaderGroupNames = "SecondLeaderGroupNames";
		public static string Prop_ChargeSecondLeaderIds = "ChargeSecondLeaderIds";
		public static string Prop_ChargeSecondLeaderNames = "ChargeSecondLeaderNames";
		public static string Prop_ChargeSecondLeaderGroupIds = "ChargeSecondLeaderGroupIds";
		public static string Prop_ChargeSecondLeaderGroupNames = "ChargeSecondLeaderGroupNames";
		public static string Prop_InstituteClerkDelegateIds = "InstituteClerkDelegateIds";
		public static string Prop_InstituteClerkDelegateNames = "InstituteClerkDelegateNames";
		public static string Prop_InstituteClerkDelegateGroupIds = "InstituteClerkDelegateGroupIds";
		public static string Prop_InstituteClerkDelegateGroupNames = "InstituteClerkDelegateGroupNames";
		public static string Prop_DeptClerkDelegateIds = "DeptClerkDelegateIds";
		public static string Prop_DeptClerkDelegateNames = "DeptClerkDelegateNames";
		public static string Prop_DeptClerkDelegateGroupIds = "DeptClerkDelegateGroupIds";
		public static string Prop_DeptClerkDelegateGroupNames = "DeptClerkDelegateGroupNames";
		public static string Prop_ClerkIds = "ClerkIds";
		public static string Prop_ClerkNames = "ClerkNames";
		public static string Prop_ClerkGroupIds = "ClerkGroupIds";
		public static string Prop_ClerkGroupNames = "ClerkGroupNames";
		public static string Prop_PeopleQuan = "PeopleQuan";
		public static string Prop_ExcellentRate = "ExcellentRate";
		public static string Prop_GoodRate = "GoodRate";
		public static string Prop_ExcellentQuan = "ExcellentQuan";
		public static string Prop_GoodQuan = "GoodQuan";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _groupID;
		private string _groupCode;
		private string _groupName;
		private string _groupType;
		private string _firstLeaderIds;
		private string _firstLeaderNames;
		private string _firstLeaderGroupIds;
		private string _firstLeaderGroupNames;
		private string _secondLeaderIds;
		private string _secondLeaderNames;
		private string _secondLeaderGroupIds;
		private string _secondLeaderGroupNames;
		private string _chargeSecondLeaderIds;
		private string _chargeSecondLeaderNames;
		private string _chargeSecondLeaderGroupIds;
		private string _chargeSecondLeaderGroupNames;
		private string _instituteClerkDelegateIds;
		private string _instituteClerkDelegateNames;
		private string _instituteClerkDelegateGroupIds;
		private string _instituteClerkDelegateGroupNames;
		private string _deptClerkDelegateIds;
		private string _deptClerkDelegateNames;
		private string _deptClerkDelegateGroupIds;
		private string _deptClerkDelegateGroupNames;
		private string _clerkIds;
		private string _clerkNames;
		private string _clerkGroupIds;
		private string _clerkGroupNames;
		private int? _peopleQuan;
		private System.Decimal? _excellentRate;
		private System.Decimal? _goodRate;
		private int? _excellentQuan;
		private int? _goodQuan;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public PersonConfig()
		{
		}

		public PersonConfig(
			string p_id,
			string p_groupID,
			string p_groupCode,
			string p_groupName,
			string p_groupType,
			string p_firstLeaderIds,
			string p_firstLeaderNames,
			string p_firstLeaderGroupIds,
			string p_firstLeaderGroupNames,
			string p_secondLeaderIds,
			string p_secondLeaderNames,
			string p_secondLeaderGroupIds,
			string p_secondLeaderGroupNames,
			string p_chargeSecondLeaderIds,
			string p_chargeSecondLeaderNames,
			string p_chargeSecondLeaderGroupIds,
			string p_chargeSecondLeaderGroupNames,
			string p_instituteClerkDelegateIds,
			string p_instituteClerkDelegateNames,
			string p_instituteClerkDelegateGroupIds,
			string p_instituteClerkDelegateGroupNames,
			string p_deptClerkDelegateIds,
			string p_deptClerkDelegateNames,
			string p_deptClerkDelegateGroupIds,
			string p_deptClerkDelegateGroupNames,
			string p_clerkIds,
			string p_clerkNames,
			string p_clerkGroupIds,
			string p_clerkGroupNames,
			int? p_peopleQuan,
			System.Decimal? p_excellentRate,
			System.Decimal? p_goodRate,
			int? p_excellentQuan,
			int? p_goodQuan,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_groupID = p_groupID;
			_groupCode = p_groupCode;
			_groupName = p_groupName;
			_groupType = p_groupType;
			_firstLeaderIds = p_firstLeaderIds;
			_firstLeaderNames = p_firstLeaderNames;
			_firstLeaderGroupIds = p_firstLeaderGroupIds;
			_firstLeaderGroupNames = p_firstLeaderGroupNames;
			_secondLeaderIds = p_secondLeaderIds;
			_secondLeaderNames = p_secondLeaderNames;
			_secondLeaderGroupIds = p_secondLeaderGroupIds;
			_secondLeaderGroupNames = p_secondLeaderGroupNames;
			_chargeSecondLeaderIds = p_chargeSecondLeaderIds;
			_chargeSecondLeaderNames = p_chargeSecondLeaderNames;
			_chargeSecondLeaderGroupIds = p_chargeSecondLeaderGroupIds;
			_chargeSecondLeaderGroupNames = p_chargeSecondLeaderGroupNames;
			_instituteClerkDelegateIds = p_instituteClerkDelegateIds;
			_instituteClerkDelegateNames = p_instituteClerkDelegateNames;
			_instituteClerkDelegateGroupIds = p_instituteClerkDelegateGroupIds;
			_instituteClerkDelegateGroupNames = p_instituteClerkDelegateGroupNames;
			_deptClerkDelegateIds = p_deptClerkDelegateIds;
			_deptClerkDelegateNames = p_deptClerkDelegateNames;
			_deptClerkDelegateGroupIds = p_deptClerkDelegateGroupIds;
			_deptClerkDelegateGroupNames = p_deptClerkDelegateGroupNames;
			_clerkIds = p_clerkIds;
			_clerkNames = p_clerkNames;
			_clerkGroupIds = p_clerkGroupIds;
			_clerkGroupNames = p_clerkGroupNames;
			_peopleQuan = p_peopleQuan;
			_excellentRate = p_excellentRate;
			_goodRate = p_goodRate;
			_excellentQuan = p_excellentQuan;
			_goodQuan = p_goodQuan;
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
					RaisePropertyChanged(PersonConfig.Prop_GroupID, oldValue, value);
				}
			}

		}

		[Property("GroupCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string GroupCode
		{
			get { return _groupCode; }
			set
			{
				if ((_groupCode == null) || (value == null) || (!value.Equals(_groupCode)))
				{
                    object oldValue = _groupCode;
					_groupCode = value;
					RaisePropertyChanged(PersonConfig.Prop_GroupCode, oldValue, value);
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
					RaisePropertyChanged(PersonConfig.Prop_GroupName, oldValue, value);
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
					RaisePropertyChanged(PersonConfig.Prop_GroupType, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string FirstLeaderIds
		{
			get { return _firstLeaderIds; }
			set
			{
				if ((_firstLeaderIds == null) || (value == null) || (!value.Equals(_firstLeaderIds)))
				{
                    object oldValue = _firstLeaderIds;
					_firstLeaderIds = value;
					RaisePropertyChanged(PersonConfig.Prop_FirstLeaderIds, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string FirstLeaderNames
		{
			get { return _firstLeaderNames; }
			set
			{
				if ((_firstLeaderNames == null) || (value == null) || (!value.Equals(_firstLeaderNames)))
				{
                    object oldValue = _firstLeaderNames;
					_firstLeaderNames = value;
					RaisePropertyChanged(PersonConfig.Prop_FirstLeaderNames, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string FirstLeaderGroupIds
		{
			get { return _firstLeaderGroupIds; }
			set
			{
				if ((_firstLeaderGroupIds == null) || (value == null) || (!value.Equals(_firstLeaderGroupIds)))
				{
                    object oldValue = _firstLeaderGroupIds;
					_firstLeaderGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_FirstLeaderGroupIds, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string FirstLeaderGroupNames
		{
			get { return _firstLeaderGroupNames; }
			set
			{
				if ((_firstLeaderGroupNames == null) || (value == null) || (!value.Equals(_firstLeaderGroupNames)))
				{
                    object oldValue = _firstLeaderGroupNames;
					_firstLeaderGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_FirstLeaderGroupNames, oldValue, value);
				}
			}

		}

		[Property("SecondLeaderIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string SecondLeaderIds
		{
			get { return _secondLeaderIds; }
			set
			{
				if ((_secondLeaderIds == null) || (value == null) || (!value.Equals(_secondLeaderIds)))
				{
                    object oldValue = _secondLeaderIds;
					_secondLeaderIds = value;
					RaisePropertyChanged(PersonConfig.Prop_SecondLeaderIds, oldValue, value);
				}
			}

		}

		[Property("SecondLeaderNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string SecondLeaderNames
		{
			get { return _secondLeaderNames; }
			set
			{
				if ((_secondLeaderNames == null) || (value == null) || (!value.Equals(_secondLeaderNames)))
				{
                    object oldValue = _secondLeaderNames;
					_secondLeaderNames = value;
					RaisePropertyChanged(PersonConfig.Prop_SecondLeaderNames, oldValue, value);
				}
			}

		}

		[Property("SecondLeaderGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string SecondLeaderGroupIds
		{
			get { return _secondLeaderGroupIds; }
			set
			{
				if ((_secondLeaderGroupIds == null) || (value == null) || (!value.Equals(_secondLeaderGroupIds)))
				{
                    object oldValue = _secondLeaderGroupIds;
					_secondLeaderGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_SecondLeaderGroupIds, oldValue, value);
				}
			}

		}

		[Property("SecondLeaderGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string SecondLeaderGroupNames
		{
			get { return _secondLeaderGroupNames; }
			set
			{
				if ((_secondLeaderGroupNames == null) || (value == null) || (!value.Equals(_secondLeaderGroupNames)))
				{
                    object oldValue = _secondLeaderGroupNames;
					_secondLeaderGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_SecondLeaderGroupNames, oldValue, value);
				}
			}

		}

		[Property("ChargeSecondLeaderIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string ChargeSecondLeaderIds
		{
			get { return _chargeSecondLeaderIds; }
			set
			{
				if ((_chargeSecondLeaderIds == null) || (value == null) || (!value.Equals(_chargeSecondLeaderIds)))
				{
                    object oldValue = _chargeSecondLeaderIds;
					_chargeSecondLeaderIds = value;
					RaisePropertyChanged(PersonConfig.Prop_ChargeSecondLeaderIds, oldValue, value);
				}
			}

		}

		[Property("ChargeSecondLeaderNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string ChargeSecondLeaderNames
		{
			get { return _chargeSecondLeaderNames; }
			set
			{
				if ((_chargeSecondLeaderNames == null) || (value == null) || (!value.Equals(_chargeSecondLeaderNames)))
				{
                    object oldValue = _chargeSecondLeaderNames;
					_chargeSecondLeaderNames = value;
					RaisePropertyChanged(PersonConfig.Prop_ChargeSecondLeaderNames, oldValue, value);
				}
			}

		}

		[Property("ChargeSecondLeaderGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string ChargeSecondLeaderGroupIds
		{
			get { return _chargeSecondLeaderGroupIds; }
			set
			{
				if ((_chargeSecondLeaderGroupIds == null) || (value == null) || (!value.Equals(_chargeSecondLeaderGroupIds)))
				{
                    object oldValue = _chargeSecondLeaderGroupIds;
					_chargeSecondLeaderGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_ChargeSecondLeaderGroupIds, oldValue, value);
				}
			}

		}

		[Property("ChargeSecondLeaderGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string ChargeSecondLeaderGroupNames
		{
			get { return _chargeSecondLeaderGroupNames; }
			set
			{
				if ((_chargeSecondLeaderGroupNames == null) || (value == null) || (!value.Equals(_chargeSecondLeaderGroupNames)))
				{
                    object oldValue = _chargeSecondLeaderGroupNames;
					_chargeSecondLeaderGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_ChargeSecondLeaderGroupNames, oldValue, value);
				}
			}

		}

		[Property("InstituteClerkDelegateIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string InstituteClerkDelegateIds
		{
			get { return _instituteClerkDelegateIds; }
			set
			{
				if ((_instituteClerkDelegateIds == null) || (value == null) || (!value.Equals(_instituteClerkDelegateIds)))
				{
                    object oldValue = _instituteClerkDelegateIds;
					_instituteClerkDelegateIds = value;
					RaisePropertyChanged(PersonConfig.Prop_InstituteClerkDelegateIds, oldValue, value);
				}
			}

		}

		[Property("InstituteClerkDelegateNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string InstituteClerkDelegateNames
		{
			get { return _instituteClerkDelegateNames; }
			set
			{
				if ((_instituteClerkDelegateNames == null) || (value == null) || (!value.Equals(_instituteClerkDelegateNames)))
				{
                    object oldValue = _instituteClerkDelegateNames;
					_instituteClerkDelegateNames = value;
					RaisePropertyChanged(PersonConfig.Prop_InstituteClerkDelegateNames, oldValue, value);
				}
			}

		}

		[Property("InstituteClerkDelegateGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string InstituteClerkDelegateGroupIds
		{
			get { return _instituteClerkDelegateGroupIds; }
			set
			{
				if ((_instituteClerkDelegateGroupIds == null) || (value == null) || (!value.Equals(_instituteClerkDelegateGroupIds)))
				{
                    object oldValue = _instituteClerkDelegateGroupIds;
					_instituteClerkDelegateGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_InstituteClerkDelegateGroupIds, oldValue, value);
				}
			}

		}

		[Property("InstituteClerkDelegateGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string InstituteClerkDelegateGroupNames
		{
			get { return _instituteClerkDelegateGroupNames; }
			set
			{
				if ((_instituteClerkDelegateGroupNames == null) || (value == null) || (!value.Equals(_instituteClerkDelegateGroupNames)))
				{
                    object oldValue = _instituteClerkDelegateGroupNames;
					_instituteClerkDelegateGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_InstituteClerkDelegateGroupNames, oldValue, value);
				}
			}

		}

		[Property("DeptClerkDelegateIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string DeptClerkDelegateIds
		{
			get { return _deptClerkDelegateIds; }
			set
			{
				if ((_deptClerkDelegateIds == null) || (value == null) || (!value.Equals(_deptClerkDelegateIds)))
				{
                    object oldValue = _deptClerkDelegateIds;
					_deptClerkDelegateIds = value;
					RaisePropertyChanged(PersonConfig.Prop_DeptClerkDelegateIds, oldValue, value);
				}
			}

		}

		[Property("DeptClerkDelegateNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string DeptClerkDelegateNames
		{
			get { return _deptClerkDelegateNames; }
			set
			{
				if ((_deptClerkDelegateNames == null) || (value == null) || (!value.Equals(_deptClerkDelegateNames)))
				{
                    object oldValue = _deptClerkDelegateNames;
					_deptClerkDelegateNames = value;
					RaisePropertyChanged(PersonConfig.Prop_DeptClerkDelegateNames, oldValue, value);
				}
			}

		}

		[Property("DeptClerkDelegateGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string DeptClerkDelegateGroupIds
		{
			get { return _deptClerkDelegateGroupIds; }
			set
			{
				if ((_deptClerkDelegateGroupIds == null) || (value == null) || (!value.Equals(_deptClerkDelegateGroupIds)))
				{
                    object oldValue = _deptClerkDelegateGroupIds;
					_deptClerkDelegateGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_DeptClerkDelegateGroupIds, oldValue, value);
				}
			}

		}

		[Property("DeptClerkDelegateGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string DeptClerkDelegateGroupNames
		{
			get { return _deptClerkDelegateGroupNames; }
			set
			{
				if ((_deptClerkDelegateGroupNames == null) || (value == null) || (!value.Equals(_deptClerkDelegateGroupNames)))
				{
                    object oldValue = _deptClerkDelegateGroupNames;
					_deptClerkDelegateGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_DeptClerkDelegateGroupNames, oldValue, value);
				}
			}

		}

		[Property("ClerkIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ClerkIds
		{
			get { return _clerkIds; }
			set
			{
				if ((_clerkIds == null) || (value == null) || (!value.Equals(_clerkIds)))
				{
                    object oldValue = _clerkIds;
					_clerkIds = value;
					RaisePropertyChanged(PersonConfig.Prop_ClerkIds, oldValue, value);
				}
			}

		}

		[Property("ClerkNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ClerkNames
		{
			get { return _clerkNames; }
			set
			{
				if ((_clerkNames == null) || (value == null) || (!value.Equals(_clerkNames)))
				{
                    object oldValue = _clerkNames;
					_clerkNames = value;
					RaisePropertyChanged(PersonConfig.Prop_ClerkNames, oldValue, value);
				}
			}

		}

		[Property("ClerkGroupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ClerkGroupIds
		{
			get { return _clerkGroupIds; }
			set
			{
				if ((_clerkGroupIds == null) || (value == null) || (!value.Equals(_clerkGroupIds)))
				{
                    object oldValue = _clerkGroupIds;
					_clerkGroupIds = value;
					RaisePropertyChanged(PersonConfig.Prop_ClerkGroupIds, oldValue, value);
				}
			}

		}

		[Property("ClerkGroupNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ClerkGroupNames
		{
			get { return _clerkGroupNames; }
			set
			{
				if ((_clerkGroupNames == null) || (value == null) || (!value.Equals(_clerkGroupNames)))
				{
                    object oldValue = _clerkGroupNames;
					_clerkGroupNames = value;
					RaisePropertyChanged(PersonConfig.Prop_ClerkGroupNames, oldValue, value);
				}
			}

		}

		[Property("PeopleQuan", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? PeopleQuan
		{
			get { return _peopleQuan; }
			set
			{
				if (value != _peopleQuan)
				{
                    object oldValue = _peopleQuan;
					_peopleQuan = value;
					RaisePropertyChanged(PersonConfig.Prop_PeopleQuan, oldValue, value);
				}
			}

		}

		[Property("ExcellentRate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? ExcellentRate
		{
			get { return _excellentRate; }
			set
			{
				if (value != _excellentRate)
				{
                    object oldValue = _excellentRate;
					_excellentRate = value;
					RaisePropertyChanged(PersonConfig.Prop_ExcellentRate, oldValue, value);
				}
			}

		}

		[Property("GoodRate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? GoodRate
		{
			get { return _goodRate; }
			set
			{
				if (value != _goodRate)
				{
                    object oldValue = _goodRate;
					_goodRate = value;
					RaisePropertyChanged(PersonConfig.Prop_GoodRate, oldValue, value);
				}
			}

		}

		[Property("ExcellentQuan", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? ExcellentQuan
		{
			get { return _excellentQuan; }
			set
			{
				if (value != _excellentQuan)
				{
                    object oldValue = _excellentQuan;
					_excellentQuan = value;
					RaisePropertyChanged(PersonConfig.Prop_ExcellentQuan, oldValue, value);
				}
			}

		}

		[Property("GoodQuan", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? GoodQuan
		{
			get { return _goodQuan; }
			set
			{
				if (value != _goodQuan)
				{
                    object oldValue = _goodQuan;
					_goodQuan = value;
					RaisePropertyChanged(PersonConfig.Prop_GoodQuan, oldValue, value);
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
					RaisePropertyChanged(PersonConfig.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(PersonConfig.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(PersonConfig.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // PersonConfig
}


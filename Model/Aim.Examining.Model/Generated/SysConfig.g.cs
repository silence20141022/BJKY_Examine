// Business class SysConfig generated from SysConfig
// Creator: Ray
// Created Date: [2012-12-16]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("SysConfig")]
	public partial class SysConfig : ExamModelBase<SysConfig>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ClerkQuarterWeight = "ClerkQuarterWeight";
		public static string Prop_ClerkYearWeight = "ClerkYearWeight";
		public static string Prop_DeptSecondLeaderQuarterWeight = "DeptSecondLeaderQuarterWeight";
		public static string Prop_DeptSecondLeaderYearWeight = "DeptSecondLeaderYearWeight";
		public static string Prop_FirstLeaderExcellentPercent = "FirstLeaderExcellentPercent";
		public static string Prop_FirstLeaderGoodPercent = "FirstLeaderGoodPercent";
		public static string Prop_DeptExcellentPercent = "DeptExcellentPercent";
		public static string Prop_DeptGoodPercent = "DeptGoodPercent";
		public static string Prop_AppealDays = "AppealDays";
		public static string Prop_UserBalanceValid = "UserBalanceValid";
		public static string Prop_MessageValid = "MessageValid";
		public static string Prop_DeptLevelLimit = "DeptLevelLimit";

		#endregion

		#region Private_Variables

		private string _id;
		private int? _clerkQuarterWeight;
		private int? _clerkYearWeight;
		private int? _deptSecondLeaderQuarterWeight;
		private int? _deptSecondLeaderYearWeight;
		private System.Decimal? _firstLeaderExcellentPercent;
		private System.Decimal? _firstLeaderGoodPercent;
		private System.Decimal? _deptExcellentPercent;
		private System.Decimal? _deptGoodPercent;
		private int? _appealDays;
		private string _userBalanceValid;
		private string _messageValid;
		private string _deptLevelLimit;


		#endregion

		#region Constructors

		public SysConfig()
		{
		}

		public SysConfig(
			string p_id,
			int? p_clerkQuarterWeight,
			int? p_clerkYearWeight,
			int? p_deptSecondLeaderQuarterWeight,
			int? p_deptSecondLeaderYearWeight,
			System.Decimal? p_firstLeaderExcellentPercent,
			System.Decimal? p_firstLeaderGoodPercent,
			System.Decimal? p_deptExcellentPercent,
			System.Decimal? p_deptGoodPercent,
			int? p_appealDays,
			string p_userBalanceValid,
			string p_messageValid,
			string p_deptLevelLimit)
		{
			_id = p_id;
			_clerkQuarterWeight = p_clerkQuarterWeight;
			_clerkYearWeight = p_clerkYearWeight;
			_deptSecondLeaderQuarterWeight = p_deptSecondLeaderQuarterWeight;
			_deptSecondLeaderYearWeight = p_deptSecondLeaderYearWeight;
			_firstLeaderExcellentPercent = p_firstLeaderExcellentPercent;
			_firstLeaderGoodPercent = p_firstLeaderGoodPercent;
			_deptExcellentPercent = p_deptExcellentPercent;
			_deptGoodPercent = p_deptGoodPercent;
			_appealDays = p_appealDays;
			_userBalanceValid = p_userBalanceValid;
			_messageValid = p_messageValid;
			_deptLevelLimit = p_deptLevelLimit;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			// set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("ClerkQuarterWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? ClerkQuarterWeight
		{
			get { return _clerkQuarterWeight; }
			set
			{
				if (value != _clerkQuarterWeight)
				{
                    object oldValue = _clerkQuarterWeight;
					_clerkQuarterWeight = value;
					RaisePropertyChanged(SysConfig.Prop_ClerkQuarterWeight, oldValue, value);
				}
			}

		}

		[Property("ClerkYearWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? ClerkYearWeight
		{
			get { return _clerkYearWeight; }
			set
			{
				if (value != _clerkYearWeight)
				{
                    object oldValue = _clerkYearWeight;
					_clerkYearWeight = value;
					RaisePropertyChanged(SysConfig.Prop_ClerkYearWeight, oldValue, value);
				}
			}

		}

		[Property("DeptSecondLeaderQuarterWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? DeptSecondLeaderQuarterWeight
		{
			get { return _deptSecondLeaderQuarterWeight; }
			set
			{
				if (value != _deptSecondLeaderQuarterWeight)
				{
                    object oldValue = _deptSecondLeaderQuarterWeight;
					_deptSecondLeaderQuarterWeight = value;
					RaisePropertyChanged(SysConfig.Prop_DeptSecondLeaderQuarterWeight, oldValue, value);
				}
			}

		}

		[Property("DeptSecondLeaderYearWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? DeptSecondLeaderYearWeight
		{
			get { return _deptSecondLeaderYearWeight; }
			set
			{
				if (value != _deptSecondLeaderYearWeight)
				{
                    object oldValue = _deptSecondLeaderYearWeight;
					_deptSecondLeaderYearWeight = value;
					RaisePropertyChanged(SysConfig.Prop_DeptSecondLeaderYearWeight, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderExcellentPercent", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? FirstLeaderExcellentPercent
		{
			get { return _firstLeaderExcellentPercent; }
			set
			{
				if (value != _firstLeaderExcellentPercent)
				{
                    object oldValue = _firstLeaderExcellentPercent;
					_firstLeaderExcellentPercent = value;
					RaisePropertyChanged(SysConfig.Prop_FirstLeaderExcellentPercent, oldValue, value);
				}
			}

		}

		[Property("FirstLeaderGoodPercent", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? FirstLeaderGoodPercent
		{
			get { return _firstLeaderGoodPercent; }
			set
			{
				if (value != _firstLeaderGoodPercent)
				{
                    object oldValue = _firstLeaderGoodPercent;
					_firstLeaderGoodPercent = value;
					RaisePropertyChanged(SysConfig.Prop_FirstLeaderGoodPercent, oldValue, value);
				}
			}

		}

		[Property("DeptExcellentPercent", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DeptExcellentPercent
		{
			get { return _deptExcellentPercent; }
			set
			{
				if (value != _deptExcellentPercent)
				{
                    object oldValue = _deptExcellentPercent;
					_deptExcellentPercent = value;
					RaisePropertyChanged(SysConfig.Prop_DeptExcellentPercent, oldValue, value);
				}
			}

		}

		[Property("DeptGoodPercent", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DeptGoodPercent
		{
			get { return _deptGoodPercent; }
			set
			{
				if (value != _deptGoodPercent)
				{
                    object oldValue = _deptGoodPercent;
					_deptGoodPercent = value;
					RaisePropertyChanged(SysConfig.Prop_DeptGoodPercent, oldValue, value);
				}
			}

		}

		[Property("AppealDays", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? AppealDays
		{
			get { return _appealDays; }
			set
			{
				if (value != _appealDays)
				{
                    object oldValue = _appealDays;
					_appealDays = value;
					RaisePropertyChanged(SysConfig.Prop_AppealDays, oldValue, value);
				}
			}

		}

		[Property("UserBalanceValid", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string UserBalanceValid
		{
			get { return _userBalanceValid; }
			set
			{
				if ((_userBalanceValid == null) || (value == null) || (!value.Equals(_userBalanceValid)))
				{
                    object oldValue = _userBalanceValid;
					_userBalanceValid = value;
					RaisePropertyChanged(SysConfig.Prop_UserBalanceValid, oldValue, value);
				}
			}

		}

		[Property("MessageValid", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string MessageValid
		{
			get { return _messageValid; }
			set
			{
				if ((_messageValid == null) || (value == null) || (!value.Equals(_messageValid)))
				{
                    object oldValue = _messageValid;
					_messageValid = value;
					RaisePropertyChanged(SysConfig.Prop_MessageValid, oldValue, value);
				}
			}

		}

		[Property("DeptLevelLimit", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string DeptLevelLimit
		{
			get { return _deptLevelLimit; }
			set
			{
				if ((_deptLevelLimit == null) || (value == null) || (!value.Equals(_deptLevelLimit)))
				{
                    object oldValue = _deptLevelLimit;
					_deptLevelLimit = value;
					RaisePropertyChanged(SysConfig.Prop_DeptLevelLimit, oldValue, value);
				}
			}

		}

		#endregion
	} // SysConfig
}


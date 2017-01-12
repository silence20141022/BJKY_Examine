// Business class ExamineRelation generated from ExamineRelation
// Creator: Ray
// Created Date: [2013-01-08]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ExamineRelation")]
	public partial class ExamineRelation : ExamModelBase<ExamineRelation>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_RelationName = "RelationName";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_UpLevelCode = "UpLevelCode";
		public static string Prop_UpLevelName = "UpLevelName";
		public static string Prop_UpLevelWeight = "UpLevelWeight";
		public static string Prop_SameLevelCode = "SameLevelCode";
		public static string Prop_SameLevelName = "SameLevelName";
		public static string Prop_SameLevelWeight = "SameLevelWeight";
		public static string Prop_DownLevelCode = "DownLevelCode";
		public static string Prop_DownLevelName = "DownLevelName";
		public static string Prop_DownLevelWeight = "DownLevelWeight";
		public static string Prop_Remark = "Remark";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _relationName;
		private string _beRoleCode;
		private string _beRoleName;
		private string _upLevelCode;
		private string _upLevelName;
		private int? _upLevelWeight;
		private string _sameLevelCode;
		private string _sameLevelName;
		private int? _sameLevelWeight;
		private string _downLevelCode;
		private string _downLevelName;
		private int? _downLevelWeight;
		private string _remark;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public ExamineRelation()
		{
		}

		public ExamineRelation(
			string p_id,
			string p_relationName,
			string p_beRoleCode,
			string p_beRoleName,
			string p_upLevelCode,
			string p_upLevelName,
			int? p_upLevelWeight,
			string p_sameLevelCode,
			string p_sameLevelName,
			int? p_sameLevelWeight,
			string p_downLevelCode,
			string p_downLevelName,
			int? p_downLevelWeight,
			string p_remark,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_relationName = p_relationName;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_upLevelCode = p_upLevelCode;
			_upLevelName = p_upLevelName;
			_upLevelWeight = p_upLevelWeight;
			_sameLevelCode = p_sameLevelCode;
			_sameLevelName = p_sameLevelName;
			_sameLevelWeight = p_sameLevelWeight;
			_downLevelCode = p_downLevelCode;
			_downLevelName = p_downLevelName;
			_downLevelWeight = p_downLevelWeight;
			_remark = p_remark;
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
					RaisePropertyChanged(ExamineRelation.Prop_RelationName, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_BeRoleCode, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_BeRoleName, oldValue, value);
				}
			}

		}

		[Property("UpLevelCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string UpLevelCode
		{
			get { return _upLevelCode; }
			set
			{
				if ((_upLevelCode == null) || (value == null) || (!value.Equals(_upLevelCode)))
				{
                    object oldValue = _upLevelCode;
					_upLevelCode = value;
					RaisePropertyChanged(ExamineRelation.Prop_UpLevelCode, oldValue, value);
				}
			}

		}

		[Property("UpLevelName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string UpLevelName
		{
			get { return _upLevelName; }
			set
			{
				if ((_upLevelName == null) || (value == null) || (!value.Equals(_upLevelName)))
				{
                    object oldValue = _upLevelName;
					_upLevelName = value;
					RaisePropertyChanged(ExamineRelation.Prop_UpLevelName, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_UpLevelWeight, oldValue, value);
				}
			}

		}

		[Property("SameLevelCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string SameLevelCode
		{
			get { return _sameLevelCode; }
			set
			{
				if ((_sameLevelCode == null) || (value == null) || (!value.Equals(_sameLevelCode)))
				{
                    object oldValue = _sameLevelCode;
					_sameLevelCode = value;
					RaisePropertyChanged(ExamineRelation.Prop_SameLevelCode, oldValue, value);
				}
			}

		}

		[Property("SameLevelName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string SameLevelName
		{
			get { return _sameLevelName; }
			set
			{
				if ((_sameLevelName == null) || (value == null) || (!value.Equals(_sameLevelName)))
				{
                    object oldValue = _sameLevelName;
					_sameLevelName = value;
					RaisePropertyChanged(ExamineRelation.Prop_SameLevelName, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_SameLevelWeight, oldValue, value);
				}
			}

		}

		[Property("DownLevelCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string DownLevelCode
		{
			get { return _downLevelCode; }
			set
			{
				if ((_downLevelCode == null) || (value == null) || (!value.Equals(_downLevelCode)))
				{
                    object oldValue = _downLevelCode;
					_downLevelCode = value;
					RaisePropertyChanged(ExamineRelation.Prop_DownLevelCode, oldValue, value);
				}
			}

		}

		[Property("DownLevelName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string DownLevelName
		{
			get { return _downLevelName; }
			set
			{
				if ((_downLevelName == null) || (value == null) || (!value.Equals(_downLevelName)))
				{
                    object oldValue = _downLevelName;
					_downLevelName = value;
					RaisePropertyChanged(ExamineRelation.Prop_DownLevelName, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_DownLevelWeight, oldValue, value);
				}
			}

		}

		[Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Remark
		{
			get { return _remark; }
			set
			{
				if ((_remark == null) || (value == null) || (!value.Equals(_remark)))
				{
                    object oldValue = _remark;
					_remark = value;
					RaisePropertyChanged(ExamineRelation.Prop_Remark, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(ExamineRelation.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineRelation
}


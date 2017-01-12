// Business class ExamineStageDetail generated from ExamineStageDetail
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
	[ActiveRecord("ExamineStageDetail")]
	public partial class ExamineStageDetail : ExamModelBase<ExamineStageDetail>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineStageId = "ExamineStageId";
		public static string Prop_BeRoleCode = "BeRoleCode";
		public static string Prop_BeRoleName = "BeRoleName";
		public static string Prop_PersonQuan = "PersonQuan";
		public static string Prop_ExamineRelationId = "ExamineRelationId";
		public static string Prop_ExamineIndicatorId = "ExamineIndicatorId";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineStageId;
		private string _beRoleCode;
		private string _beRoleName;
		private int? _personQuan;
		private string _examineRelationId;
		private string _examineIndicatorId;


		#endregion

		#region Constructors

		public ExamineStageDetail()
		{
		}

		public ExamineStageDetail(
			string p_id,
			string p_examineStageId,
			string p_beRoleCode,
			string p_beRoleName,
			int? p_personQuan,
			string p_examineRelationId,
			string p_examineIndicatorId)
		{
			_id = p_id;
			_examineStageId = p_examineStageId;
			_beRoleCode = p_beRoleCode;
			_beRoleName = p_beRoleName;
			_personQuan = p_personQuan;
			_examineRelationId = p_examineRelationId;
			_examineIndicatorId = p_examineIndicatorId;
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
					RaisePropertyChanged(ExamineStageDetail.Prop_ExamineStageId, oldValue, value);
				}
			}

		}

		[Property("BeRoleCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BeRoleCode
		{
			get { return _beRoleCode; }
			set
			{
				if ((_beRoleCode == null) || (value == null) || (!value.Equals(_beRoleCode)))
				{
                    object oldValue = _beRoleCode;
					_beRoleCode = value;
					RaisePropertyChanged(ExamineStageDetail.Prop_BeRoleCode, oldValue, value);
				}
			}

		}

		[Property("BeRoleName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BeRoleName
		{
			get { return _beRoleName; }
			set
			{
				if ((_beRoleName == null) || (value == null) || (!value.Equals(_beRoleName)))
				{
                    object oldValue = _beRoleName;
					_beRoleName = value;
					RaisePropertyChanged(ExamineStageDetail.Prop_BeRoleName, oldValue, value);
				}
			}

		}

		[Property("PersonQuan", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? PersonQuan
		{
			get { return _personQuan; }
			set
			{
				if (value != _personQuan)
				{
                    object oldValue = _personQuan;
					_personQuan = value;
					RaisePropertyChanged(ExamineStageDetail.Prop_PersonQuan, oldValue, value);
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
					RaisePropertyChanged(ExamineStageDetail.Prop_ExamineRelationId, oldValue, value);
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
					RaisePropertyChanged(ExamineStageDetail.Prop_ExamineIndicatorId, oldValue, value);
				}
			}

		}

		#endregion
	} // ExamineStageDetail
}


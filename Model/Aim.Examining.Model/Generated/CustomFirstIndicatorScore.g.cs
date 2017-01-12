// Business class CustomFirstIndicatorScore generated from CustomFirstIndicatorScore
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
	[ActiveRecord("CustomFirstIndicatorScore")]
	public partial class CustomFirstIndicatorScore : ExamModelBase<CustomFirstIndicatorScore>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineTaskId = "ExamineTaskId";
		public static string Prop_PersonFirstIndicatorId = "PersonFirstIndicatorId";
		public static string Prop_CustomScore = "CustomScore";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineTaskId;
		private string _personFirstIndicatorId;
		private System.Decimal? _customScore;


		#endregion

		#region Constructors

		public CustomFirstIndicatorScore()
		{
		}

		public CustomFirstIndicatorScore(
			string p_id,
			string p_examineTaskId,
			string p_personFirstIndicatorId,
			System.Decimal? p_customScore)
		{
			_id = p_id;
			_examineTaskId = p_examineTaskId;
			_personFirstIndicatorId = p_personFirstIndicatorId;
			_customScore = p_customScore;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			// set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("ExamineTaskId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ExamineTaskId
		{
			get { return _examineTaskId; }
			set
			{
				if ((_examineTaskId == null) || (value == null) || (!value.Equals(_examineTaskId)))
				{
                    object oldValue = _examineTaskId;
					_examineTaskId = value;
					RaisePropertyChanged(CustomFirstIndicatorScore.Prop_ExamineTaskId, oldValue, value);
				}
			}

		}

		[Property("PersonFirstIndicatorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string PersonFirstIndicatorId
		{
			get { return _personFirstIndicatorId; }
			set
			{
				if ((_personFirstIndicatorId == null) || (value == null) || (!value.Equals(_personFirstIndicatorId)))
				{
                    object oldValue = _personFirstIndicatorId;
					_personFirstIndicatorId = value;
					RaisePropertyChanged(CustomFirstIndicatorScore.Prop_PersonFirstIndicatorId, oldValue, value);
				}
			}

		}

		[Property("CustomScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? CustomScore
		{
			get { return _customScore; }
			set
			{
				if (value != _customScore)
				{
                    object oldValue = _customScore;
					_customScore = value;
					RaisePropertyChanged(CustomFirstIndicatorScore.Prop_CustomScore, oldValue, value);
				}
			}

		}

		#endregion
	} // CustomFirstIndicatorScore
}


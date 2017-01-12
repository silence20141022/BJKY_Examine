// Business class ScoreStandard generated from ScoreStandard
// Creator: Ray
// Created Date: [2012-12-04]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("ScoreStandard")]
	public partial class ScoreStandard : ExamModelBase<ScoreStandard>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_IndicatorSecondId = "IndicatorSecondId";
		public static string Prop_IndicatorThirdName = "IndicatorThirdName";
		public static string Prop_MaxScore = "MaxScore";
		public static string Prop_SortIndex = "SortIndex";

		#endregion

		#region Private_Variables

		private string _id;
		private string _indicatorSecondId;
		private string _indicatorThirdName;
		private string _maxScore;
		private int? _sortIndex;


		#endregion

		#region Constructors

		public ScoreStandard()
		{
		}

		public ScoreStandard(
			string p_id,
			string p_indicatorSecondId,
			string p_indicatorThirdName,
			string p_maxScore,
			int? p_sortIndex)
		{
			_id = p_id;
			_indicatorSecondId = p_indicatorSecondId;
			_indicatorThirdName = p_indicatorThirdName;
			_maxScore = p_maxScore;
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

		[Property("IndicatorSecondId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string IndicatorSecondId
		{
			get { return _indicatorSecondId; }
			set
			{
				if ((_indicatorSecondId == null) || (value == null) || (!value.Equals(_indicatorSecondId)))
				{
                    object oldValue = _indicatorSecondId;
					_indicatorSecondId = value;
					RaisePropertyChanged(ScoreStandard.Prop_IndicatorSecondId, oldValue, value);
				}
			}

		}

		[Property("IndicatorThirdName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string IndicatorThirdName
		{
			get { return _indicatorThirdName; }
			set
			{
				if ((_indicatorThirdName == null) || (value == null) || (!value.Equals(_indicatorThirdName)))
				{
                    object oldValue = _indicatorThirdName;
					_indicatorThirdName = value;
					RaisePropertyChanged(ScoreStandard.Prop_IndicatorThirdName, oldValue, value);
				}
			}

		}

		[Property("MaxScore", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string MaxScore
		{
			get { return _maxScore; }
			set
			{
				if ((_maxScore == null) || (value == null) || (!value.Equals(_maxScore)))
				{
                    object oldValue = _maxScore;
					_maxScore = value;
					RaisePropertyChanged(ScoreStandard.Prop_MaxScore, oldValue, value);
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
					RaisePropertyChanged(ScoreStandard.Prop_SortIndex, oldValue, value);
				}
			}

		}

		#endregion
	} // ScoreStandard
}


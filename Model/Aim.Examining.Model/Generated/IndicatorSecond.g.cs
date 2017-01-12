// Business class IndicatorSecond generated from IndicatorSecond
// Creator: Ray
// Created Date: [2012-12-18]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("IndicatorSecond")]
	public partial class IndicatorSecond : ExamModelBase<IndicatorSecond>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_IndicatorFirstId = "IndicatorFirstId";
		public static string Prop_IndicatorFirstName = "IndicatorFirstName";
		public static string Prop_IndicatorSecondName = "IndicatorSecondName";
		public static string Prop_MaxScore = "MaxScore";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_ToolTip = "ToolTip";

		#endregion

		#region Private_Variables

		private string _id;
		private string _indicatorFirstId;
		private string _indicatorFirstName;
		private string _indicatorSecondName;
		private int? _maxScore;
		private int? _sortIndex;
		private string _toolTip;


		#endregion

		#region Constructors

		public IndicatorSecond()
		{
		}

		public IndicatorSecond(
			string p_id,
			string p_indicatorFirstId,
			string p_indicatorFirstName,
			string p_indicatorSecondName,
			int? p_maxScore,
			int? p_sortIndex,
			string p_toolTip)
		{
			_id = p_id;
			_indicatorFirstId = p_indicatorFirstId;
			_indicatorFirstName = p_indicatorFirstName;
			_indicatorSecondName = p_indicatorSecondName;
			_maxScore = p_maxScore;
			_sortIndex = p_sortIndex;
			_toolTip = p_toolTip;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		  set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("IndicatorFirstId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string IndicatorFirstId
		{
			get { return _indicatorFirstId; }
			set
			{
				if ((_indicatorFirstId == null) || (value == null) || (!value.Equals(_indicatorFirstId)))
				{
                    object oldValue = _indicatorFirstId;
					_indicatorFirstId = value;
					RaisePropertyChanged(IndicatorSecond.Prop_IndicatorFirstId, oldValue, value);
				}
			}

		}

		[Property("IndicatorFirstName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string IndicatorFirstName
		{
			get { return _indicatorFirstName; }
			set
			{
				if ((_indicatorFirstName == null) || (value == null) || (!value.Equals(_indicatorFirstName)))
				{
                    object oldValue = _indicatorFirstName;
					_indicatorFirstName = value;
					RaisePropertyChanged(IndicatorSecond.Prop_IndicatorFirstName, oldValue, value);
				}
			}

		}

		[Property("IndicatorSecondName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string IndicatorSecondName
		{
			get { return _indicatorSecondName; }
			set
			{
				if ((_indicatorSecondName == null) || (value == null) || (!value.Equals(_indicatorSecondName)))
				{
                    object oldValue = _indicatorSecondName;
					_indicatorSecondName = value;
					RaisePropertyChanged(IndicatorSecond.Prop_IndicatorSecondName, oldValue, value);
				}
			}

		}

		[Property("MaxScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? MaxScore
		{
			get { return _maxScore; }
			set
			{
				if (value != _maxScore)
				{
                    object oldValue = _maxScore;
					_maxScore = value;
					RaisePropertyChanged(IndicatorSecond.Prop_MaxScore, oldValue, value);
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
					RaisePropertyChanged(IndicatorSecond.Prop_SortIndex, oldValue, value);
				}
			}

		}

		[Property("ToolTip", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ToolTip
		{
			get { return _toolTip; }
			set
			{
				if ((_toolTip == null) || (value == null) || (!value.Equals(_toolTip)))
				{
                    object oldValue = _toolTip;
					_toolTip = value;
					RaisePropertyChanged(IndicatorSecond.Prop_ToolTip, oldValue, value);
				}
			}

		}

		#endregion
	} // IndicatorSecond
}


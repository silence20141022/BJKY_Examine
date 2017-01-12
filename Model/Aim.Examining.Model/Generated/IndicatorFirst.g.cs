// Business class IndicatorFirst generated from IndicatorFirst
// Creator: Ray
// Created Date: [2012-12-19]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Examining.Model
{
	[ActiveRecord("IndicatorFirst")]
	public partial class IndicatorFirst : ExamModelBase<IndicatorFirst>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineIndicatorId = "ExamineIndicatorId";
		public static string Prop_IndicatorFirstName = "IndicatorFirstName";
		public static string Prop_InsteadColumn = "InsteadColumn";
		public static string Prop_CustomColumn = "CustomColumn";
		public static string Prop_MaxScore = "MaxScore";
		public static string Prop_SortIndex = "SortIndex";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineIndicatorId;
		private string _indicatorFirstName;
		private string _insteadColumn;
		private string _customColumn;
		private int? _maxScore;
		private int? _sortIndex;


		#endregion

		#region Constructors

		public IndicatorFirst()
		{
		}

		public IndicatorFirst(
			string p_id,
			string p_examineIndicatorId,
			string p_indicatorFirstName,
			string p_insteadColumn,
			string p_customColumn,
			int? p_maxScore,
			int? p_sortIndex)
		{
			_id = p_id;
			_examineIndicatorId = p_examineIndicatorId;
			_indicatorFirstName = p_indicatorFirstName;
			_insteadColumn = p_insteadColumn;
			_customColumn = p_customColumn;
			_maxScore = p_maxScore;
			_sortIndex = p_sortIndex;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			  set { _id = value; } // 处理列表编辑时去掉注释

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
					RaisePropertyChanged(IndicatorFirst.Prop_ExamineIndicatorId, oldValue, value);
				}
			}

		}

		[Property("IndicatorFirstName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string IndicatorFirstName
		{
			get { return _indicatorFirstName; }
			set
			{
				if ((_indicatorFirstName == null) || (value == null) || (!value.Equals(_indicatorFirstName)))
				{
                    object oldValue = _indicatorFirstName;
					_indicatorFirstName = value;
					RaisePropertyChanged(IndicatorFirst.Prop_IndicatorFirstName, oldValue, value);
				}
			}

		}

		[Property("InsteadColumn", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string InsteadColumn
		{
			get { return _insteadColumn; }
			set
			{
				if ((_insteadColumn == null) || (value == null) || (!value.Equals(_insteadColumn)))
				{
                    object oldValue = _insteadColumn;
					_insteadColumn = value;
					RaisePropertyChanged(IndicatorFirst.Prop_InsteadColumn, oldValue, value);
				}
			}

		}

		[Property("CustomColumn", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CustomColumn
		{
			get { return _customColumn; }
			set
			{
				if ((_customColumn == null) || (value == null) || (!value.Equals(_customColumn)))
				{
                    object oldValue = _customColumn;
					_customColumn = value;
					RaisePropertyChanged(IndicatorFirst.Prop_CustomColumn, oldValue, value);
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
					RaisePropertyChanged(IndicatorFirst.Prop_MaxScore, oldValue, value);
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
					RaisePropertyChanged(IndicatorFirst.Prop_SortIndex, oldValue, value);
				}
			}

		}

		#endregion
	} // IndicatorFirst
}


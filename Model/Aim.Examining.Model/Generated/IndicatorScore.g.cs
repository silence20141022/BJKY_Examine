// Business class IndicatorScore generated from IndicatorScore
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
	[ActiveRecord("IndicatorScore")]
	public partial class IndicatorScore : ExamModelBase<IndicatorScore>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ExamineTaskId = "ExamineTaskId";
		public static string Prop_IndicatorFirstId = "IndicatorFirstId";
		public static string Prop_IndicatorFirstName = "IndicatorFirstName";
		public static string Prop_FirstMaxScore = "FirstMaxScore";
		public static string Prop_FirstSortIndex = "FirstSortIndex";
		public static string Prop_IndicatorSecondId = "IndicatorSecondId";
		public static string Prop_IndicatorSecondName = "IndicatorSecondName";
		public static string Prop_SecondMaxScore = "SecondMaxScore";
		public static string Prop_SecondSortIndex = "SecondSortIndex";
		public static string Prop_ToolTip = "ToolTip";
		public static string Prop_SubScore = "SubScore";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";

		#endregion

		#region Private_Variables

		private string _id;
		private string _examineTaskId;
		private string _indicatorFirstId;
		private string _indicatorFirstName;
		private int? _firstMaxScore;
		private int? _firstSortIndex;
		private string _indicatorSecondId;
		private string _indicatorSecondName;
		private int? _secondMaxScore;
		private int? _secondSortIndex;
		private string _toolTip;
		private System.Decimal? _subScore;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;


		#endregion

		#region Constructors

		public IndicatorScore()
		{
		}

		public IndicatorScore(
			string p_id,
			string p_examineTaskId,
			string p_indicatorFirstId,
			string p_indicatorFirstName,
			int? p_firstMaxScore,
			int? p_firstSortIndex,
			string p_indicatorSecondId,
			string p_indicatorSecondName,
			int? p_secondMaxScore,
			int? p_secondSortIndex,
			string p_toolTip,
			System.Decimal? p_subScore,
			string p_createId,
			string p_createName,
			DateTime? p_createTime)
		{
			_id = p_id;
			_examineTaskId = p_examineTaskId;
			_indicatorFirstId = p_indicatorFirstId;
			_indicatorFirstName = p_indicatorFirstName;
			_firstMaxScore = p_firstMaxScore;
			_firstSortIndex = p_firstSortIndex;
			_indicatorSecondId = p_indicatorSecondId;
			_indicatorSecondName = p_indicatorSecondName;
			_secondMaxScore = p_secondMaxScore;
			_secondSortIndex = p_secondSortIndex;
			_toolTip = p_toolTip;
			_subScore = p_subScore;
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
					RaisePropertyChanged(IndicatorScore.Prop_ExamineTaskId, oldValue, value);
				}
			}

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
					RaisePropertyChanged(IndicatorScore.Prop_IndicatorFirstId, oldValue, value);
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
					RaisePropertyChanged(IndicatorScore.Prop_IndicatorFirstName, oldValue, value);
				}
			}

		}

		[Property("FirstMaxScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? FirstMaxScore
		{
			get { return _firstMaxScore; }
			set
			{
				if (value != _firstMaxScore)
				{
                    object oldValue = _firstMaxScore;
					_firstMaxScore = value;
					RaisePropertyChanged(IndicatorScore.Prop_FirstMaxScore, oldValue, value);
				}
			}

		}

		[Property("FirstSortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? FirstSortIndex
		{
			get { return _firstSortIndex; }
			set
			{
				if (value != _firstSortIndex)
				{
                    object oldValue = _firstSortIndex;
					_firstSortIndex = value;
					RaisePropertyChanged(IndicatorScore.Prop_FirstSortIndex, oldValue, value);
				}
			}

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
					RaisePropertyChanged(IndicatorScore.Prop_IndicatorSecondId, oldValue, value);
				}
			}

		}

		[Property("IndicatorSecondName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string IndicatorSecondName
		{
			get { return _indicatorSecondName; }
			set
			{
				if ((_indicatorSecondName == null) || (value == null) || (!value.Equals(_indicatorSecondName)))
				{
                    object oldValue = _indicatorSecondName;
					_indicatorSecondName = value;
					RaisePropertyChanged(IndicatorScore.Prop_IndicatorSecondName, oldValue, value);
				}
			}

		}

		[Property("SecondMaxScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? SecondMaxScore
		{
			get { return _secondMaxScore; }
			set
			{
				if (value != _secondMaxScore)
				{
                    object oldValue = _secondMaxScore;
					_secondMaxScore = value;
					RaisePropertyChanged(IndicatorScore.Prop_SecondMaxScore, oldValue, value);
				}
			}

		}

		[Property("SecondSortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? SecondSortIndex
		{
			get { return _secondSortIndex; }
			set
			{
				if (value != _secondSortIndex)
				{
                    object oldValue = _secondSortIndex;
					_secondSortIndex = value;
					RaisePropertyChanged(IndicatorScore.Prop_SecondSortIndex, oldValue, value);
				}
			}

		}

		[Property("ToolTip", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string ToolTip
		{
			get { return _toolTip; }
			set
			{
				if ((_toolTip == null) || (value == null) || (!value.Equals(_toolTip)))
				{
                    object oldValue = _toolTip;
					_toolTip = value;
					RaisePropertyChanged(IndicatorScore.Prop_ToolTip, oldValue, value);
				}
			}

		}

		[Property("SubScore", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SubScore
		{
			get { return _subScore; }
			set
			{
				if (value != _subScore)
				{
                    object oldValue = _subScore;
					_subScore = value;
					RaisePropertyChanged(IndicatorScore.Prop_SubScore, oldValue, value);
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
					RaisePropertyChanged(IndicatorScore.Prop_CreateId, oldValue, value);
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
					RaisePropertyChanged(IndicatorScore.Prop_CreateName, oldValue, value);
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
					RaisePropertyChanged(IndicatorScore.Prop_CreateTime, oldValue, value);
				}
			}

		}

		#endregion
	} // IndicatorScore
}


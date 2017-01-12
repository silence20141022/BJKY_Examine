// Business class PersonFirstIndicator generated from PersonFirstIndicator
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
    [ActiveRecord("PersonFirstIndicator")]
    public partial class PersonFirstIndicator : ExamModelBase<PersonFirstIndicator>
    {
        #region Property_Names

        public static string Prop_Id = "Id";
        public static string Prop_CustomIndicatorId = "CustomIndicatorId";
        public static string Prop_PersonFirstIndicatorName = "PersonFirstIndicatorName";
        public static string Prop_TotalWeight = "TotalWeight";
        public static string Prop_Weight = "Weight";
        public static string Prop_SortIndex = "SortIndex";
        public static string Prop_IndicatorType = "IndicatorType";
        public static string Prop_CreateId = "CreateId";
        public static string Prop_CreateName = "CreateName";
        public static string Prop_CreateTime = "CreateTime";

        #endregion

        #region Private_Variables

        private string _id;
        private string _customIndicatorId;
        private string _personFirstIndicatorName;
        private int? _totalWeight;
        private int? _weight;
        private int? _sortIndex;
        private string _indicatorType;
        private string _createId;
        private string _createName;
        private DateTime? _createTime;


        #endregion

        #region Constructors

        public PersonFirstIndicator()
        {
        }

        public PersonFirstIndicator(
            string p_id,
            string p_customIndicatorId,
            string p_personFirstIndicatorName,
            int? p_totalWeight,
            int? p_weight,
            int? p_sortIndex,
            string p_indicatorType,
            string p_createId,
            string p_createName,
            DateTime? p_createTime)
        {
            _id = p_id;
            _customIndicatorId = p_customIndicatorId;
            _personFirstIndicatorName = p_personFirstIndicatorName;
            _totalWeight = p_totalWeight;
            _weight = p_weight;
            _sortIndex = p_sortIndex;
            _indicatorType = p_indicatorType;
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

        [Property("CustomIndicatorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
        public string CustomIndicatorId
        {
            get { return _customIndicatorId; }
            set
            {
                if ((_customIndicatorId == null) || (value == null) || (!value.Equals(_customIndicatorId)))
                {
                    object oldValue = _customIndicatorId;
                    _customIndicatorId = value;
                    RaisePropertyChanged(PersonFirstIndicator.Prop_CustomIndicatorId, oldValue, value);
                }
            }

        }

        [Property("PersonFirstIndicatorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string PersonFirstIndicatorName
        {
            get { return _personFirstIndicatorName; }
            set
            {
                if ((_personFirstIndicatorName == null) || (value == null) || (!value.Equals(_personFirstIndicatorName)))
                {
                    object oldValue = _personFirstIndicatorName;
                    _personFirstIndicatorName = value;
                    RaisePropertyChanged(PersonFirstIndicator.Prop_PersonFirstIndicatorName, oldValue, value);
                }
            }

        }

        [Property("TotalWeight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? TotalWeight
        {
            get { return _totalWeight; }
            set
            {
                if (value != _totalWeight)
                {
                    object oldValue = _totalWeight;
                    _totalWeight = value;
                    RaisePropertyChanged(PersonFirstIndicator.Prop_TotalWeight, oldValue, value);
                }
            }

        }

        [Property("Weight", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? Weight
        {
            get { return _weight; }
            set
            {
                if (value != _weight)
                {
                    object oldValue = _weight;
                    _weight = value;
                    RaisePropertyChanged(PersonFirstIndicator.Prop_Weight, oldValue, value);
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
                    RaisePropertyChanged(PersonFirstIndicator.Prop_SortIndex, oldValue, value);
                }
            }

        }

        [Property("IndicatorType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string IndicatorType
        {
            get { return _indicatorType; }
            set
            {
                if ((_indicatorType == null) || (value == null) || (!value.Equals(_indicatorType)))
                {
                    object oldValue = _indicatorType;
                    _indicatorType = value;
                    RaisePropertyChanged(PersonFirstIndicator.Prop_IndicatorType, oldValue, value);
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
                    RaisePropertyChanged(PersonFirstIndicator.Prop_CreateId, oldValue, value);
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
                    RaisePropertyChanged(PersonFirstIndicator.Prop_CreateName, oldValue, value);
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
                    RaisePropertyChanged(PersonFirstIndicator.Prop_CreateTime, oldValue, value);
                }
            }

        }

        #endregion
    } // PersonFirstIndicator
}


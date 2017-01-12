// Business class PersonSecondIndicator generated from PersonSecondIndicator
// Creator: Ray
// Created Date: [2013-01-02]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Examining.Model
{
    [ActiveRecord("PersonSecondIndicator")]
    public partial class PersonSecondIndicator : ExamModelBase<PersonSecondIndicator>
    {
        #region Property_Names

        public static string Prop_Id = "Id";
        public static string Prop_PersonFirstIndicatorId = "PersonFirstIndicatorId";
        public static string Prop_PersonFirstIndicatorName = "PersonFirstIndicatorName";
        public static string Prop_PersonSecondIndicatorName = "PersonSecondIndicatorName";
        public static string Prop_Weight = "Weight";
        public static string Prop_SortIndex = "SortIndex";
        public static string Prop_ToolTip = "ToolTip";
        public static string Prop_SelfRemark = "SelfRemark";
        public static string Prop_CreateId = "CreateId";
        public static string Prop_CreateName = "CreateName";
        public static string Prop_CreateTime = "CreateTime";

        #endregion

        #region Private_Variables

        private string _id;
        private string _personFirstIndicatorId;
        private string _personFirstIndicatorName;
        private string _personSecondIndicatorName;
        private int? _weight;
        private int? _sortIndex;
        private string _toolTip;
        private string _selfRemark;
        private string _createId;
        private string _createName;
        private DateTime? _createTime;


        #endregion

        #region Constructors

        public PersonSecondIndicator()
        {
        }

        public PersonSecondIndicator(
            string p_id,
            string p_personFirstIndicatorId,
            string p_personFirstIndicatorName,
            string p_personSecondIndicatorName,
            int? p_weight,
            int? p_sortIndex,
            string p_toolTip,
            string p_selfRemark,
            string p_createId,
            string p_createName,
            DateTime? p_createTime)
        {
            _id = p_id;
            _personFirstIndicatorId = p_personFirstIndicatorId;
            _personFirstIndicatorName = p_personFirstIndicatorName;
            _personSecondIndicatorName = p_personSecondIndicatorName;
            _weight = p_weight;
            _sortIndex = p_sortIndex;
            _toolTip = p_toolTip;
            _selfRemark = p_selfRemark;
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_PersonFirstIndicatorId, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_PersonFirstIndicatorName, oldValue, value);
                }
            }

        }

        [Property("PersonSecondIndicatorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string PersonSecondIndicatorName
        {
            get { return _personSecondIndicatorName; }
            set
            {
                if ((_personSecondIndicatorName == null) || (value == null) || (!value.Equals(_personSecondIndicatorName)))
                {
                    object oldValue = _personSecondIndicatorName;
                    _personSecondIndicatorName = value;
                    RaisePropertyChanged(PersonSecondIndicator.Prop_PersonSecondIndicatorName, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_Weight, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_SortIndex, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_ToolTip, oldValue, value);
                }
            }

        }

        [Property("SelfRemark", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
        public string SelfRemark
        {
            get { return _selfRemark; }
            set
            {
                if ((_selfRemark == null) || (value == null) || (!value.Equals(_selfRemark)))
                {
                    object oldValue = _selfRemark;
                    _selfRemark = value;
                    RaisePropertyChanged(PersonSecondIndicator.Prop_SelfRemark, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_CreateId, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_CreateName, oldValue, value);
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
                    RaisePropertyChanged(PersonSecondIndicator.Prop_CreateTime, oldValue, value);
                }
            }

        }

        #endregion
    } // PersonSecondIndicator
}


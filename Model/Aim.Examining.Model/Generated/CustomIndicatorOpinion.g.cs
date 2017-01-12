// Business class CustomIndicatorOpinion generated from CustomIndicatorOpinion
// Creator: Ray
// Created Date: [2012-12-27]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Examining.Model
{
    [ActiveRecord("CustomIndicatorOpinion")]
    public partial class CustomIndicatorOpinion : ExamModelBase<CustomIndicatorOpinion>
    {
        #region Property_Names

        public static string Prop_Id = "Id";
        public static string Prop_CustomIndicatorId = "CustomIndicatorId";
        public static string Prop_Opinion = "Opinion";
        public static string Prop_CreateId = "CreateId";
        public static string Prop_CreateName = "CreateName";
        public static string Prop_CreateTime = "CreateTime";

        #endregion

        #region Private_Variables

        private string _id;
        private string _customIndicatorId;
        private string _opinion;
        private string _createId;
        private string _createName;
        private DateTime? _createTime;


        #endregion

        #region Constructors

        public CustomIndicatorOpinion()
        {
        }

        public CustomIndicatorOpinion(
            string p_id,
            string p_customIndicatorId,
            string p_opinion,
            string p_createId,
            string p_createName,
            DateTime? p_createTime)
        {
            _id = p_id;
            _customIndicatorId = p_customIndicatorId;
            _opinion = p_opinion;
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
                    RaisePropertyChanged(CustomIndicatorOpinion.Prop_CustomIndicatorId, oldValue, value);
                }
            }

        }

        [Property("Opinion", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
        public string Opinion
        {
            get { return _opinion; }
            set
            {
                if ((_opinion == null) || (value == null) || (!value.Equals(_opinion)))
                {
                    object oldValue = _opinion;
                    _opinion = value;
                    RaisePropertyChanged(CustomIndicatorOpinion.Prop_Opinion, oldValue, value);
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
                    RaisePropertyChanged(CustomIndicatorOpinion.Prop_CreateId, oldValue, value);
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
                    RaisePropertyChanged(CustomIndicatorOpinion.Prop_CreateName, oldValue, value);
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
                    RaisePropertyChanged(CustomIndicatorOpinion.Prop_CreateTime, oldValue, value);
                }
            }

        }

        #endregion
    } // CustomIndicatorOpinion
}

